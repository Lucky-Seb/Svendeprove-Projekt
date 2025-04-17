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

        // GetOrdboger
        [Fact]
        public async Task GetOrdboger_ShouldReturnOk_WhenSuccess()
        {
            var dtos = new List<OrdbogDTO> { SampleDTO };
            _mockService.Setup(s => s.GetAllOrdbogAsync()).ReturnsAsync(Result<IEnumerable<OrdbogDTO>>.Ok(dtos.Cast<OrdbogDTO>()));

            var result = await _controller.GetOrdboger();

            var okResult = result as OkObjectResult;
            okResult.Should().NotBeNull();
            okResult!.StatusCode.Should().Be(200);
        }

        [Fact]
        public async Task GetOrdboger_ShouldReturnBadRequest_WhenFailure()
        {
            _mockService.Setup(s => s.GetAllOrdbogAsync()).ReturnsAsync(Result<IEnumerable<OrdbogDTO>>.Fail("Database error"));

            var result = await _controller.GetOrdboger();

            var badRequest = result as BadRequestObjectResult;
            badRequest.Should().NotBeNull();
            badRequest!.StatusCode.Should().Be(400);
        }

        [Fact]
        public async Task GetOrdbogerIncludingDeleted_ShouldReturnOk_WhenSuccess()
        {
            var dtos = new List<OrdbogDTO> { SampleDTO };
            _mockService.Setup(s => s.GetAllOrdbogIncludingDeletedAsync()).ReturnsAsync(Result<IEnumerable<OrdbogDTO>>.Ok(dtos.Cast<OrdbogDTO>()));

            var result = await _controller.GetOrdbogerIncludingDeleted();

            var okResult = result as OkObjectResult;
            okResult.Should().NotBeNull();
            okResult!.StatusCode.Should().Be(200);
        }

        [Fact]
        public async Task GetOrdbogerIncludingDeleted_ShouldReturnBadRequest_WhenFailure()
        {
            _mockService.Setup(s => s.GetAllOrdbogIncludingDeletedAsync()).ReturnsAsync(Result<IEnumerable<OrdbogDTO>>.Fail("Database error"));

            var result = await _controller.GetOrdbogerIncludingDeleted();

            var badRequest = result as BadRequestObjectResult;
            badRequest.Should().NotBeNull();
            badRequest!.StatusCode.Should().Be(400);
        }

        // PostOrdbog
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

        // DeleteOrdbog
        [Fact]
        public async Task DeleteOrdbog_ShouldReturnOk_WhenSuccess()
        {
            var id = Guid.NewGuid();
            _mockService.Setup(s => s.DeleteOrdbogAsync(id)).ReturnsAsync(Result<bool>.Ok(true)); // Corrected line

            var result = await _controller.DeleteOrdbog(id);

            var okResult = result as OkObjectResult;
            okResult.Should().NotBeNull();
            okResult!.StatusCode.Should().Be(200);
        }

        [Fact]
        public async Task DeleteOrdbog_ShouldReturnBadRequest_WhenFailure()
        {
            var id = Guid.NewGuid();
            _mockService.Setup(s => s.DeleteOrdbogAsync(id)).ReturnsAsync(Result<bool>.Fail("Error deleting the Ordbog")); // Corrected line

            var result = await _controller.DeleteOrdbog(id);

            var badRequest = result as BadRequestObjectResult;
            badRequest.Should().NotBeNull();
            badRequest!.StatusCode.Should().Be(400);
        }

        // GetOrdbog
        [Fact]
        public async Task GetOrdbog_ShouldReturnOk_WhenFound()
        {
            var dto = SampleDTO;
            _mockService.Setup(s => s.GetOrdbogByIdAsync(dto.OrdbogId)).ReturnsAsync(Result<OrdbogDTO>.Ok(dto));

            var result = await _controller.GetOrdbog(dto.OrdbogId);

            result.Should().BeOfType<OkObjectResult>();
        }

        [Fact]
        public async Task GetOrdbog_ShouldReturnBadRequest_WhenNotFound()
        {
            var id = Guid.NewGuid();
            _mockService.Setup(s => s.GetOrdbogByIdAsync(id)).ReturnsAsync(Result<OrdbogDTO>.Fail("Not found"));

            var result = await _controller.GetOrdbog(id);

            result.Should().BeOfType<BadRequestObjectResult>();
        }

        // Restore
        [Fact]
        public async Task Restore_ShouldReturnOk_WhenSuccess()
        {
            var id = Guid.NewGuid();
            var dto = SampleDTO;

            // Mocking the RestoreOrdbogAsync method to return Result<bool>.Ok(true)
            _mockService.Setup(s => s.RestoreOrdbogAsync(id, dto)).ReturnsAsync(Result<bool>.Ok(true));

            var result = await _controller.Restore(id, dto);

            result.Should().BeOfType<OkObjectResult>();
        }

        [Fact]
        public async Task Restore_ShouldReturnBadRequest_WhenFails()
        {
            var id = Guid.NewGuid();
            var dto = SampleDTO;

            // Mocking the RestoreOrdbogAsync method to return Result<bool>.Fail("Restore failed")
            _mockService.Setup(s => s.RestoreOrdbogAsync(id, dto)).ReturnsAsync(Result<bool>.Fail("Restore failed"));

            var result = await _controller.Restore(id, dto);

            result.Should().BeOfType<BadRequestObjectResult>();
        }

        // UpdateOrdbogIncludingDeleted
        [Fact]
        public async Task UpdateOrdbogIncludingDeleted_ShouldReturnOk_WhenSuccess()
        {
            var id = Guid.NewGuid();
            var dto = SampleDTO;
            _mockService.Setup(s => s.UpdateOrdbogIncludingDeletedByIdAsync(id, dto)).ReturnsAsync(Result<OrdbogDTO>.Ok(dto));

            var result = await _controller.UpdateOrdbogIncludingDeleted(id, dto);

            result.Should().BeOfType<OkObjectResult>();
        }

        [Fact]
        public async Task UpdateOrdbogIncludingDeleted_ShouldReturnBadRequest_WhenFails()
        {
            var id = Guid.NewGuid();
            var dto = SampleDTO;
            _mockService.Setup(s => s.UpdateOrdbogIncludingDeletedByIdAsync(id, dto)).ReturnsAsync(Result<OrdbogDTO>.Fail("Update failed"));

            var result = await _controller.UpdateOrdbogIncludingDeleted(id, dto);

            result.Should().BeOfType<BadRequestObjectResult>();
        }

        // UpdateOrdbog
        [Fact]
        public async Task UpdateOrdbog_ShouldReturnOk_WhenSuccess()
        {
            var id = Guid.NewGuid();
            var dto = SampleDTO;
            _mockService.Setup(s => s.UpdateOrdbogAsync(id, dto)).ReturnsAsync(Result<OrdbogDTO>.Ok(dto));

            var result = await _controller.UpdateOrdbog(id, dto);

            result.Should().BeOfType<OkObjectResult>();
        }

        [Fact]
        public async Task UpdateOrdbog_ShouldReturnBadRequest_WhenFails()
        {
            var id = Guid.NewGuid();
            var dto = SampleDTO;
            _mockService.Setup(s => s.UpdateOrdbogAsync(id, dto)).ReturnsAsync(Result<OrdbogDTO>.Fail("Update failed"));

            var result = await _controller.UpdateOrdbog(id, dto);

            result.Should().BeOfType<BadRequestObjectResult>();
        }

        // GetOrdbogByDanskOrd
        [Fact]
        public async Task GetOrdbogByDanskOrd_ShouldReturnOk_WhenFound()
        {
            var dto = SampleDTO;
            _mockService.Setup(s => s.GetOrdbogByDanskOrdAsync(dto.DanskOrd)).ReturnsAsync(Result<OrdbogDTO>.Ok(dto));

            var result = await _controller.GetOrdbogByDanskOrd(dto.DanskOrd);

            result.Should().BeOfType<OkObjectResult>();
        }

        [Fact]
        public async Task GetOrdbogByDanskOrd_ShouldReturnBadRequest_WhenNotFound()
        {
            var danskOrd = "NonExistingWord";
            _mockService.Setup(s => s.GetOrdbogByDanskOrdAsync(danskOrd)).ReturnsAsync(Result<OrdbogDTO>.Fail("Not found"));

            var result = await _controller.GetOrdbogByDanskOrd(danskOrd);

            result.Should().BeOfType<BadRequestObjectResult>();
        }

        // GetOrdbogByKoranOrd
        [Fact]
        public async Task GetOrdbogByKoranOrd_ShouldReturnOk_WhenFound()
        {
            var dto = SampleDTO;
            _mockService.Setup(s => s.GetOrdbogByKoranOrdAsync(dto.KoranskOrd)).ReturnsAsync(Result<OrdbogDTO>.Ok(dto));

            var result = await _controller.GetOrdbogByKoranOrd(dto.KoranskOrd);

            result.Should().BeOfType<OkObjectResult>();
        }

        [Fact]
        public async Task GetOrdbogByKoranOrd_ShouldReturnBadRequest_WhenNotFound()
        {
            var koranOrd = "NonExistingKoranWord";
            _mockService.Setup(s => s.GetOrdbogByKoranOrdAsync(koranOrd)).ReturnsAsync(Result<OrdbogDTO>.Fail("Not found"));

            var result = await _controller.GetOrdbogByKoranOrd(koranOrd);

            result.Should().BeOfType<BadRequestObjectResult>();
        }
    }
}
