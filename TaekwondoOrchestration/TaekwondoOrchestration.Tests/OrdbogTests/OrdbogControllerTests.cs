using Xunit;
using Moq;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using TaekwondoApp.Shared.DTO;
using TaekwondoOrchestration.ApiService.Controllers;
using TaekwondoOrchestration.ApiService.ServiceInterfaces;
using TaekwondoOrchestration.ApiService.Helpers;
using TaekwondoOrchestration.ApiService.NotificationHubs;
namespace TaekwondoOrchestration.Tests.OrdbogTests
{
    public class OrdbogControllerTests
    {
        private readonly Mock<IOrdbogService> _mockService;
        private readonly Mock<IHubContext<OrdbogHub>> _mockHubContext;
        private readonly OrdbogController _controller;

        public OrdbogControllerTests()
        {
            _mockService = new Mock<IOrdbogService>();
            _mockHubContext = new Mock<IHubContext<OrdbogHub>>();
            _controller = new OrdbogController(_mockService.Object, _mockHubContext.Object);
        }

        private OrdbogDTO SampleDTO => new() { OrdbogId = Guid.NewGuid(), DanskOrd = "Hej", KoranskOrd = "안녕", Beskrivelse = "Hello" };

        [Fact]
        public async Task GetOrdboger_ShouldReturnOk_WhenSuccess()
        {
            var dtos = new List<OrdbogDTO> { SampleDTO };
            _mockService.Setup(s => s.GetAllOrdbogAsync()).ReturnsAsync(Result<List<OrdbogDTO>>.Ok(dtos));

            var result = await _controller.GetOrdboger();

            var okResult = result as OkObjectResult;
            okResult.Should().NotBeNull();
            okResult!.StatusCode.Should().Be(200);
        }

        [Fact]
        public async Task GetOrdboger_ShouldReturnBadRequest_WhenFailure()
        {
            _mockService.Setup(s => s.GetAllOrdbogAsync()).ReturnsAsync(Result<List<OrdbogDTO>>.Fail("Database error"));

            var result = await _controller.GetOrdboger();

            var badRequest = result as BadRequestObjectResult;
            badRequest.Should().NotBeNull();
            badRequest!.StatusCode.Should().Be(400);
        }
        [Fact]
        public async Task PostOrdbog_ShouldReturnOk_AndBroadcast_WhenSuccess()
        {
            var dto = SampleDTO;
            _mockService.Setup(s => s.CreateOrdbogAsync(dto)).ReturnsAsync(Result<OrdbogDTO>.Ok(dto));

            var mockClients = new Mock<IHubClients>();
            var mockAllClient = new Mock<IClientProxy>();
            mockClients.Setup(clients => clients.All).Returns(mockAllClient.Object);
            _mockHubContext.Setup(h => h.Clients).Returns(mockClients.Object);

            var result = await _controller.PostOrdbog(dto);

            result.Should().BeOfType<OkObjectResult>();
            mockAllClient.Verify(c => c.SendAsync("OrdbogUpdated", null, default), Times.Once);
        }

        [Fact]
        public async Task PostOrdbog_ShouldReturnBadRequest_WhenFailure()
        {
            var dto = SampleDTO;
            _mockService.Setup(s => s.CreateOrdbogAsync(dto)).ReturnsAsync(Result<OrdbogDTO>.Fail("Creation failed"));

            var result = await _controller.PostOrdbog(dto);

            result.Should().BeOfType<BadRequestObjectResult>();
        }
        [Fact]
        public async Task DeleteOrdbog_ShouldReturnOk_AndNotify_WhenSuccess()
        {
            var id = Guid.NewGuid();
            _mockService.Setup(s => s.DeleteOrdbogAsync(id)).ReturnsAsync(Result.Ok());

            var mockClients = new Mock<IHubClients>();
            var mockAllClient = new Mock<IClientProxy>();
            mockClients.Setup(clients => clients.All).Returns(mockAllClient.Object);
            _mockHubContext.Setup(h => h.Clients).Returns(mockClients.Object);

            var result = await _controller.DeleteOrdbog(id);

            result.Should().BeOfType<OkObjectResult>();
            mockAllClient.Verify(c => c.SendAsync("OrdbogDeleted", null, default), Times.Once);
        }

        [Fact]
        public async Task DeleteOrdbog_ShouldReturnBadRequest_WhenFailure()
        {
            var id = Guid.NewGuid();
            _mockService.Setup(s => s.DeleteOrdbogAsync(id)).ReturnsAsync(Result.Fail("Delete failed"));

            var result = await _controller.DeleteOrdbog(id);

            result.Should().BeOfType<BadRequestObjectResult>();
        }

    }
}
