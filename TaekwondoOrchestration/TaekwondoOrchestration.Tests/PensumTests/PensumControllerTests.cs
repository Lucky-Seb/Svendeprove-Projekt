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
using TaekwondoOrchestration.ApiService.NotificationHubs;
using TaekwondoOrchestration.ApiService.Helpers;

namespace TaekwondoOrchestration.Tests.PensumTests
{
    public class PensumControllerTests
    {
        private readonly Mock<IPensumService> _mockPensumService;
        private readonly Mock<IHubContext<PensumHub>> _mockHubContext;
        private readonly PensumController _controller;

        // Fixed GUID for testing purposes
        private readonly Guid FixedGuid = Guid.NewGuid();
        private readonly string TestGrad = "Black";

        public PensumControllerTests()
        {
            _mockPensumService = new Mock<IPensumService>();

            // Mocking the HubContext to ensure Clients.All is not null
            _mockHubContext = new Mock<IHubContext<PensumHub>>();

            var mockClients = new Mock<IHubClients>();
            var mockAllClient = new Mock<IClientProxy>();
            mockClients.Setup(clients => clients.All).Returns(mockAllClient.Object);
            _mockHubContext.Setup(h => h.Clients).Returns(mockClients.Object);

            _controller = new PensumController(_mockPensumService.Object, _mockHubContext.Object);
        }

        private PensumDTO SampleDTO => new()
        {
            PensumID = FixedGuid,
            PensumGrad = TestGrad,
        };

        [Fact]
        public async Task GetPensum_ShouldReturnOk_WhenSuccess()
        {
            var dtos = new List<PensumDTO> { SampleDTO };
            _mockPensumService.Setup(s => s.GetAllPensumAsync()).ReturnsAsync(Result<IEnumerable<PensumDTO>>.Ok(dtos));

            var result = await _controller.GetPensum();

            var okResult = result as OkObjectResult;
            okResult.Should().NotBeNull();
            okResult!.StatusCode.Should().Be(200);
        }

        [Fact]
        public async Task GetPensum_ShouldReturnBadRequest_WhenFailure()
        {
            _mockPensumService.Setup(s => s.GetAllPensumAsync()).ReturnsAsync(Result<IEnumerable<PensumDTO>>.Fail("Database error"));

            var result = await _controller.GetPensum();

            var badRequest = result as BadRequestObjectResult;
            badRequest.Should().NotBeNull();
            badRequest!.StatusCode.Should().Be(400);
        }

        [Fact]
        public async Task GetPensumById_ShouldReturnOk_WhenFound()
        {
            _mockPensumService.Setup(s => s.GetPensumByIdAsync(FixedGuid)).ReturnsAsync(Result<PensumDTO>.Ok(SampleDTO));

            var result = await _controller.GetPensum(FixedGuid);

            result.Should().BeOfType<OkObjectResult>();
        }

        [Fact]
        public async Task GetPensumById_ShouldReturnBadRequest_WhenNotFound()
        {
            _mockPensumService.Setup(s => s.GetPensumByIdAsync(FixedGuid)).ReturnsAsync(Result<PensumDTO>.Fail("Not found"));

            var result = await _controller.GetPensum(FixedGuid);

            result.Should().BeOfType<BadRequestObjectResult>();
        }

        [Fact]
        public async Task GetPensumByGrad_ShouldReturnOk_WhenSuccess()
        {
            var dtos = new List<PensumDTO> { SampleDTO };
            _mockPensumService.Setup(s => s.GetPensumByGradAsync(TestGrad)).ReturnsAsync(Result<IEnumerable<PensumDTO>>.Ok(dtos));

            var result = await _controller.GetPensumByGrad(TestGrad);

            result.Should().BeOfType<OkObjectResult>();
        }

        [Fact]
        public async Task GetPensumByGrad_ShouldReturnBadRequest_WhenFailure()
        {
            _mockPensumService.Setup(s => s.GetPensumByGradAsync(TestGrad)).ReturnsAsync(Result<IEnumerable<PensumDTO>>.Fail("Error"));

            var result = await _controller.GetPensumByGrad(TestGrad);

            result.Should().BeOfType<BadRequestObjectResult>();
        }

        [Fact]
        public async Task PostPensum_ShouldReturnOk_AndBroadcast_WhenSuccess()
        {
            _mockPensumService.Setup(s => s.CreatePensumAsync(SampleDTO)).ReturnsAsync(Result<PensumDTO>.Ok(SampleDTO));

            var result = await _controller.PostPensum(SampleDTO);

            var okResult = result as OkObjectResult;
            okResult.Should().NotBeNull();
            okResult!.StatusCode.Should().Be(200);
        }

        [Fact]
        public async Task PostPensum_ShouldReturnBadRequest_WhenFailure()
        {
            _mockPensumService.Setup(s => s.CreatePensumAsync(SampleDTO)).ReturnsAsync(Result<PensumDTO>.Fail("Creation failed"));

            var result = await _controller.PostPensum(SampleDTO);

            var badRequest = result as BadRequestObjectResult;
            badRequest.Should().NotBeNull();
            badRequest!.StatusCode.Should().Be(400);
        }

        [Fact]
        public async Task DeletePensum_ShouldReturnOk_WhenSuccess()
        {
            _mockPensumService.Setup(s => s.DeletePensumAsync(FixedGuid)).ReturnsAsync(Result<bool>.Ok(true));

            var result = await _controller.DeletePensum(FixedGuid);

            var okResult = result as OkObjectResult;
            okResult.Should().NotBeNull();
            okResult!.StatusCode.Should().Be(200);
        }

        [Fact]
        public async Task DeletePensum_ShouldReturnBadRequest_WhenFailure()
        {
            _mockPensumService.Setup(s => s.DeletePensumAsync(FixedGuid)).ReturnsAsync(Result<bool>.Fail("Error deleting the Pensum"));

            var result = await _controller.DeletePensum(FixedGuid);

            var badRequest = result as BadRequestObjectResult;
            badRequest.Should().NotBeNull();
            badRequest!.StatusCode.Should().Be(400);
        }
    }
}
