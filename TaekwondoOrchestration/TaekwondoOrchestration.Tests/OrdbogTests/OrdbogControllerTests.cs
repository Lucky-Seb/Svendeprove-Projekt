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

        // Fixed GUID for testing purposes
        private readonly Guid FixedGuid = Guid.Parse("b2bb4e9f-93fb-4eaa-bd8a-c93891a07d1f");

        public OrdbogControllerTests()
        {
            _mockService = new Mock<IOrdbogService>();

            // Mocking the HubContext to ensure Clients.All is not null
            _mockHubContext = new Mock<IHubContext<OrdbogHub>>();

            // Mocking IHubClients for Clients.All
            var mockClients = new Mock<IHubClients>();
            var mockAllClient = new Mock<IClientProxy>();
            mockClients.Setup(clients => clients.All).Returns(mockAllClient.Object);
            _mockHubContext.Setup(h => h.Clients).Returns(mockClients.Object);

            _controller = new OrdbogController(_mockService.Object, _mockHubContext.Object);
        }

        private OrdbogDTO SampleDTO => new()
        {
            OrdbogId = FixedGuid,  // Use the predefined GUID
            DanskOrd = "Hej",
            KoranskOrd = "안녕",
            Beskrivelse = "Hello"
        };

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
            _mockService.Setup(s => s.DeleteOrdbogAsync(FixedGuid)).ReturnsAsync(Result<bool>.Ok(true)); // Using Fixed GUID

            var result = await _controller.DeleteOrdbog(FixedGuid);

            var okResult = result as OkObjectResult;
            okResult.Should().NotBeNull();
            okResult!.StatusCode.Should().Be(200);
        }

        [Fact]
        public async Task DeleteOrdbog_ShouldReturnBadRequest_WhenFailure()
        {
            _mockService.Setup(s => s.DeleteOrdbogAsync(FixedGuid)).ReturnsAsync(Result<bool>.Fail("Error deleting the Ordbog")); // Using Fixed GUID

            var result = await _controller.DeleteOrdbog(FixedGuid);

            var badRequest = result as BadRequestObjectResult;
            badRequest.Should().NotBeNull();
            badRequest!.StatusCode.Should().Be(400);
        }

        // GetOrdbog
        [Fact]
        public async Task GetOrdbog_ShouldReturnOk_WhenFound()
        {
            var dto = SampleDTO;
            _mockService.Setup(s => s.GetOrdbogByIdAsync(FixedGuid)).ReturnsAsync(Result<OrdbogDTO>.Ok(dto));

            var result = await _controller.GetOrdbog(FixedGuid);

            result.Should().BeOfType<OkObjectResult>();
        }

        [Fact]
        public async Task GetOrdbog_ShouldReturnBadRequest_WhenNotFound()
        {
            _mockService.Setup(s => s.GetOrdbogByIdAsync(FixedGuid)).ReturnsAsync(Result<OrdbogDTO>.Fail("Not found"));

            var result = await _controller.GetOrdbog(FixedGuid);

            result.Should().BeOfType<BadRequestObjectResult>();
        }

        // Restore
        [Fact]
        public async Task Restore_ShouldReturnOk_WhenSuccess()
        {
            var dto = SampleDTO;

            // Mocking the RestoreOrdbogAsync method to return Result<bool>.Ok(true)
            _mockService.Setup(s => s.RestoreOrdbogAsync(FixedGuid, dto)).ReturnsAsync(Result<bool>.Ok(true));

            var result = await _controller.Restore(FixedGuid, dto);

            result.Should().BeOfType<OkObjectResult>();
        }

        [Fact]
        public async Task Restore_ShouldReturnBadRequest_WhenFails()
        {
            var dto = SampleDTO;

            // Mocking the RestoreOrdbogAsync method to return Result<bool>.Fail("Restore failed")
            _mockService.Setup(s => s.RestoreOrdbogAsync(FixedGuid, dto)).ReturnsAsync(Result<bool>.Fail("Restore failed"));

            var result = await _controller.Restore(FixedGuid, dto);

            result.Should().BeOfType<BadRequestObjectResult>();
        }

        // UpdateOrdbogIncludingDeleted
        [Fact]
        public async Task UpdateOrdbogIncludingDeleted_ShouldReturnOk_WhenSuccess()
        {
            var dto = SampleDTO;
            _mockService.Setup(s => s.UpdateOrdbogIncludingDeletedByIdAsync(FixedGuid, dto)).ReturnsAsync(Result<OrdbogDTO>.Ok(dto));

            var result = await _controller.UpdateOrdbogIncludingDeleted(FixedGuid, dto);

            result.Should().BeOfType<OkObjectResult>();
        }

        [Fact]
        public async Task UpdateOrdbogIncludingDeleted_ShouldReturnBadRequest_WhenFails()
        {
            var dto = SampleDTO;
            _mockService.Setup(s => s.UpdateOrdbogIncludingDeletedByIdAsync(FixedGuid, dto)).ReturnsAsync(Result<OrdbogDTO>.Fail("Update failed"));

            var result = await _controller.UpdateOrdbogIncludingDeleted(FixedGuid, dto);

            result.Should().BeOfType<BadRequestObjectResult>();
        }

        // UpdateOrdbog
        [Fact]
        public async Task UpdateOrdbog_ShouldReturnOk_WhenSuccess()
        {
            var dto = SampleDTO;
            _mockService.Setup(s => s.UpdateOrdbogAsync(FixedGuid, dto)).ReturnsAsync(Result<bool>.Ok(true));

            var result = await _controller.UpdateOrdbog(FixedGuid, dto);

            result.Should().BeOfType<OkObjectResult>();
        }

        [Fact]
        public async Task UpdateOrdbog_ShouldReturnBadRequest_WhenFails()
        {
            var dto = SampleDTO;
            _mockService.Setup(s => s.UpdateOrdbogAsync(FixedGuid, dto)).ReturnsAsync(Result<bool>.Fail("Update failed"));

            var result = await _controller.UpdateOrdbog(FixedGuid, dto);

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
            _mockService.Setup(s => s.GetOrdbogByDanskOrdAsync("NonExistingWord")).ReturnsAsync(Result<OrdbogDTO>.Fail("Not found"));

            var result = await _controller.GetOrdbogByDanskOrd("NonExistingWord");

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
            _mockService.Setup(s => s.GetOrdbogByKoranOrdAsync("NonExistingKoranWord")).ReturnsAsync(Result<OrdbogDTO>.Fail("Not found"));

            var result = await _controller.GetOrdbogByKoranOrd("NonExistingKoranWord");

            result.Should().BeOfType<BadRequestObjectResult>();
        }
    }
}