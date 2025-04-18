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
using TaekwondoApp.Shared.Helper;

namespace TaekwondoOrchestration.Tests.BrugerTests
{
    public class BrugerControllerTests
    {
        private readonly Mock<IBrugerService> _mockBrugerService;
        private readonly Mock<IHubContext<OrdbogHub>> _mockHubContext;
        private readonly BrugerController _controller;

        // Fixed GUID for testing purposes
        private readonly Guid FixedGuid = Guid.NewGuid();
        private readonly string TestBæltegrad = "Black";

        public BrugerControllerTests()
        {
            _mockBrugerService = new Mock<IBrugerService>();

            // Mocking the HubContext to ensure Clients.All is not null
            _mockHubContext = new Mock<IHubContext<OrdbogHub>>();

            var mockClients = new Mock<IHubClients>();
            var mockAllClient = new Mock<IClientProxy>();
            mockClients.Setup(clients => clients.All).Returns(mockAllClient.Object);
            _mockHubContext.Setup(h => h.Clients).Returns(mockClients.Object);

            _controller = new BrugerController(_mockBrugerService.Object, _mockHubContext.Object);
        }

        private BrugerDTO SampleDTO => new()
        {
            BrugerID = FixedGuid,
            Brugernavn = "JohnDoe",
            Bæltegrad = TestBæltegrad
        };
        [Fact]
        public async Task GetBrugere_ShouldReturnOk_WhenSuccess()
        {
            var dtos = new List<BrugerDTO> { SampleDTO };
            _mockBrugerService.Setup(s => s.GetAllBrugereAsync()).ReturnsAsync(Result<IEnumerable<BrugerDTO>>.Ok(dtos));

            var result = await _controller.GetBrugere();

            var okResult = result as OkObjectResult;
            okResult.Should().NotBeNull();
            okResult!.StatusCode.Should().Be(200);
        }

        [Fact]
        public async Task GetBrugere_ShouldReturnBadRequest_WhenFailure()
        {
            _mockBrugerService.Setup(s => s.GetAllBrugereAsync()).ReturnsAsync(Result<IEnumerable<BrugerDTO>>.Fail("Database error"));

            var result = await _controller.GetBrugere();

            var badRequest = result as BadRequestObjectResult;
            badRequest.Should().NotBeNull();
            badRequest!.StatusCode.Should().Be(400);
        }
        [Fact]
        public async Task GetBrugerById_ShouldReturnOk_WhenFound()
        {
            _mockBrugerService.Setup(s => s.GetBrugerByIdAsync(FixedGuid)).ReturnsAsync(Result<BrugerDTO>.Ok(SampleDTO));

            var result = await _controller.GetBruger(FixedGuid);

            result.Should().BeOfType<OkObjectResult>();
        }

        [Fact]
        public async Task GetBrugerById_ShouldReturnBadRequest_WhenNotFound()
        {
            _mockBrugerService.Setup(s => s.GetBrugerByIdAsync(FixedGuid)).ReturnsAsync(Result<BrugerDTO>.Fail("Not found"));

            var result = await _controller.GetBruger(FixedGuid);

            result.Should().BeOfType<BadRequestObjectResult>();
        }
        [Fact]
        public async Task GetBrugerByRole_ShouldReturnOk_WhenSuccess()
        {
            var dtos = new List<BrugerDTO> { SampleDTO };
            _mockBrugerService.Setup(s => s.GetBrugerByRoleAsync("Admin")).ReturnsAsync(Result<IEnumerable<BrugerDTO>>.Ok(dtos));

            var result = await _controller.GetBrugerByRole("Admin");

            result.Should().BeOfType<OkObjectResult>();
        }

        [Fact]
        public async Task GetBrugerByRole_ShouldReturnBadRequest_WhenFailure()
        {
            _mockBrugerService.Setup(s => s.GetBrugerByRoleAsync("Admin")).ReturnsAsync(Result<IEnumerable<BrugerDTO>>.Fail("Error"));

            var result = await _controller.GetBrugerByRole("Admin");

            result.Should().BeOfType<BadRequestObjectResult>();
        }
        [Fact]
        public async Task GetBrugerByBæltegrad_ShouldReturnOk_WhenSuccess()
        {
            var dtos = new List<BrugerDTO> { SampleDTO };
            _mockBrugerService.Setup(s => s.GetBrugerByBælteAsync(TestBæltegrad)).ReturnsAsync(Result<IEnumerable<BrugerDTO>>.Ok(dtos));

            var result = await _controller.GetBrugerByBælte(TestBæltegrad);

            result.Should().BeOfType<OkObjectResult>();
        }

        [Fact]
        public async Task GetBrugerByBæltegrad_ShouldReturnBadRequest_WhenFailure()
        {
            _mockBrugerService.Setup(s => s.GetBrugerByBælteAsync(TestBæltegrad)).ReturnsAsync(Result<IEnumerable<BrugerDTO>>.Fail("Error"));

            var result = await _controller.GetBrugerByBælte(TestBæltegrad);

            result.Should().BeOfType<BadRequestObjectResult>();
        }
        //[Fact]
        //public async Task PostBruger_ShouldReturnOk_AndBroadcast_WhenSuccess()
        //{
        //    _mockBrugerService.Setup(s => s.CreateBrugerAsync(SampleDTO)).ReturnsAsync(Result<BrugerDTO>.Ok(SampleDTO));

        //    var result = await _controller.PostBruger(SampleDTO);

        //    var okResult = result as OkObjectResult;
        //    okResult.Should().NotBeNull();
        //    okResult!.StatusCode.Should().Be(200);
        //}

        //[Fact]
        //public async Task PostBruger_ShouldReturnBadRequest_WhenFailure()
        //{
        //    _mockBrugerService.Setup(s => s.CreateBrugerAsync(SampleDTO)).ReturnsAsync(Result<BrugerDTO>.Fail("Creation failed"));

        //    var result = await _controller.PostBruger(SampleDTO);

        //    result.Should().BeOfType<BadRequestObjectResult>();
        //}
        //[Fact]
        //public async Task PutBruger_ShouldReturnOk_WhenSuccess()
        //{
        //    // Arrange: Setup the mock service to return a successful ApiResponse<BrugerDTO>
        //    _mockBrugerService.Setup(s => s.UpdateBrugerAsync(FixedGuid, SampleDTO))
        //        .ReturnsAsync(ApiResponse<BrugerDTO>.Ok(SampleDTO)); // Returning ApiResponse<BrugerDTO>

        //    // Act: Call the controller's PutBruger action
        //    var result = await _controller.PutBruger(FixedGuid, SampleDTO);

        //    // Assert: Check if the result is an OkObjectResult with a 200 status code
        //    var okResult = result as OkObjectResult;
        //    okResult.Should().NotBeNull(); // Assert that it is not null
        //    okResult!.StatusCode.Should().Be(200); // Assert the status code is 200
        //}

        //[Fact]
        //public async Task PutBruger_ShouldReturnBadRequest_WhenFailure()
        //{
        //    _mockBrugerService.Setup(s => s.UpdateBrugerAsync(FixedGuid, SampleDTO)).ReturnsAsync(Result<BrugerDTO>.Fail("Update failed"));

        //    var result = await _controller.PutBruger(FixedGuid, SampleDTO);

        //    result.Should().BeOfType<BadRequestObjectResult>();
        //}
        [Fact]
        public async Task DeleteBruger_ShouldReturnOk_WhenSuccess()
        {
            _mockBrugerService.Setup(s => s.DeleteBrugerAsync(FixedGuid)).ReturnsAsync(Result<bool>.Ok(true));

            var result = await _controller.DeleteBruger(FixedGuid);

            var okResult = result as OkObjectResult;
            okResult.Should().NotBeNull();
            okResult!.StatusCode.Should().Be(200);
        }

        [Fact]
        public async Task DeleteBruger_ShouldReturnBadRequest_WhenFailure()
        {
            _mockBrugerService.Setup(s => s.DeleteBrugerAsync(FixedGuid)).ReturnsAsync(Result<bool>.Fail("Error deleting the Bruger"));

            var result = await _controller.DeleteBruger(FixedGuid);

            var badRequest = result as BadRequestObjectResult;
            badRequest.Should().NotBeNull();
            badRequest!.StatusCode.Should().Be(400);
        }
        //[Fact]
        //public async Task Login_ShouldReturnOk_WhenSuccess()
        //{
        //    var loginDto = new LoginDTO { Brugernavn = "JohnDoe", Password = "Password123" };
        //    _mockBrugerService.Setup(s => s.AuthenticateBrugerAsync(loginDto)).ReturnsAsync(Result<string>.Ok("token"));

        //    var result = await _controller.Login(loginDto);

        //    var okResult = result as OkObjectResult;
        //    okResult.Should().NotBeNull();
        //    okResult!.StatusCode.Should().Be(200);
        //}

        //[Fact]
        //public async Task Login_ShouldReturnBadRequest_WhenFailure()
        //{
        //    var loginDto = new LoginDTO { Brugernavn = "JohnDoe", Password = "WrongPassword" };
        //    _mockBrugerService.Setup(s => s.AuthenticateBrugerAsync(loginDto)).ReturnsAsync(Result<string>.Fail("Invalid credentials"));

        //    var result = await _controller.Login(loginDto);

        //    var badRequest = result as BadRequestObjectResult;
        //    badRequest.Should().NotBeNull();
        //    badRequest!.StatusCode.Should().Be(400);
        //}
    }
}