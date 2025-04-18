using Moq;
using TaekwondoApp.Shared.DTO;
using TaekwondoApp.Shared.Models;
using TaekwondoOrchestration.ApiService.Repositories;
using TaekwondoOrchestration.ApiService.ServiceInterfaces;
using TaekwondoOrchestration.ApiService.Services;
using FluentAssertions;
using Xunit;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using TaekwondoOrchestration.ApiService.Helpers;
using TaekwondoOrchestration.ApiService.RepositorieInterfaces;

namespace TaekwondoOrchestration.Tests.BrugerTests
{
    public class BrugerServiceTests
    {
        private readonly Mock<IBrugerRepository> _brugerRepositoryMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly Mock<IJwtHelper> _jwtHelperMock;
        private readonly IBrugerService _brugerService;

        public BrugerServiceTests()
        {
            _brugerRepositoryMock = new Mock<IBrugerRepository>();
            _mapperMock = new Mock<IMapper>();
            _jwtHelperMock = new Mock<IJwtHelper>();
            _brugerService = new BrugerService(_brugerRepositoryMock.Object, _mapperMock.Object, _jwtHelperMock.Object);
        }

        // Test for GetAllBrugereAsync
        [Fact]
        public async Task GetAllBrugereAsync_ShouldReturnSuccess_WhenBrugereFound()
        {
            // Arrange
            var brugere = new List<Bruger> { new Bruger { BrugerID = Guid.NewGuid(), Brugernavn = "TestUser" } };
            _brugerRepositoryMock.Setup(repo => repo.GetAllBrugereAsync()).ReturnsAsync(brugere);
            _mapperMock.Setup(mapper => mapper.Map<IEnumerable<BrugerDTO>>(It.IsAny<IEnumerable<Bruger>>()))
                .Returns(new List<BrugerDTO> { new BrugerDTO { BrugerID = brugere[0].BrugerID } });

            // Act
            var result = await _brugerService.GetAllBrugereAsync();

            // Assert
            result.Success.Should().BeTrue();
            result.Value.Should().NotBeEmpty();
        }

        [Fact]
        public async Task GetAllBrugereAsync_ShouldReturnFail_WhenRepositoryReturnsEmptyList()
        {
            // Arrange
            _brugerRepositoryMock.Setup(repo => repo.GetAllBrugereAsync()).ReturnsAsync(new List<Bruger>()); // Simulate repository returning empty list

            // Act
            var result = await _brugerService.GetAllBrugereAsync();

            // Assert
            result.Success.Should().BeFalse(); // The result should be a failure
            result.Errors.Should().Contain("No users found."); // The error message should indicate no users found
        }
        [Fact]
        public async Task GetAllBrugereAsync_ShouldReturnFail_WhenExceptionOccurs()
        {
            // Arrange
            _brugerRepositoryMock.Setup(repo => repo.GetAllBrugereAsync()).ThrowsAsync(new Exception("Database error")); // Simulate an exception in the repository

            // Act
            var result = await _brugerService.GetAllBrugereAsync();

            // Assert
            result.Success.Should().BeFalse(); // The result should be a failure
            result.Errors.Should().Contain("Error occurred: Database error"); // The error message should indicate the exception message
        }
        // Test for GetBrugerByIdAsync
        [Fact]
        public async Task GetBrugerByIdAsync_ShouldReturnSuccess_WhenBrugerExists()
        {
            // Arrange
            var id = Guid.NewGuid();
            var bruger = new Bruger { BrugerID = id, Brugernavn = "TestUser" };
            _brugerRepositoryMock.Setup(repo => repo.GetBrugerByIdAsync(id)).ReturnsAsync(bruger);
            _mapperMock.Setup(mapper => mapper.Map<BrugerDTO>(It.IsAny<Bruger>())).Returns(new BrugerDTO { BrugerID = id });

            // Act
            var result = await _brugerService.GetBrugerByIdAsync(id);

            // Assert
            result.Success.Should().BeTrue();
            result.Value.Should().NotBeNull();
        }

        [Fact]
        public async Task GetBrugerByIdAsync_ShouldReturnFail_WhenBrugerNotFound()
        {
            // Arrange
            var id = Guid.NewGuid();
            _brugerRepositoryMock.Setup(repo => repo.GetBrugerByIdAsync(id)).ReturnsAsync((Bruger)null);

            // Act
            var result = await _brugerService.GetBrugerByIdAsync(id);

            // Assert
            result.Success.Should().BeFalse();
            result.Errors.Should().Contain("Bruger not found.");
        }

        // Test for CreateBrugerAsync
        [Fact]
        public async Task CreateBrugerAsync_ShouldReturnSuccess_WhenBrugerCreated()
        {
            // Arrange
            var newBrugerDto = new BrugerDTO { Brugernavn = "NewUser", ModifiedBy = "Admin" };
            var newBruger = new Bruger { BrugerID = Guid.NewGuid(), Brugernavn = "NewUser" };
            _mapperMock.Setup(mapper => mapper.Map<Bruger>(It.IsAny<BrugerDTO>())).Returns(newBruger);
            _brugerRepositoryMock.Setup(repo => repo.CreateBrugerAsync(It.IsAny<Bruger>())).ReturnsAsync(newBruger);
            _mapperMock.Setup(mapper => mapper.Map<BrugerDTO>(It.IsAny<Bruger>())).Returns(newBrugerDto);

            // Act
            var result = await _brugerService.CreateBrugerAsync(newBrugerDto);

            // Assert
            result.Success.Should().BeTrue();
            result.Value.Should().NotBeNull();
        }

        [Fact]
        public async Task CreateBrugerAsync_ShouldReturnFail_WhenBrugerCreationFails()
        {
            // Arrange
            var newBrugerDto = new BrugerDTO { Brugernavn = "NewUser", ModifiedBy = "Admin" };
            var newBruger = new Bruger { BrugerID = Guid.NewGuid(), Brugernavn = "NewUser" };

            // Setup the mapper and repository mock behaviors
            _mapperMock.Setup(mapper => mapper.Map<Bruger>(It.IsAny<BrugerDTO>())).Returns(newBruger);
            _brugerRepositoryMock.Setup(repo => repo.CreateBrugerAsync(It.IsAny<Bruger>())).ReturnsAsync((Bruger)null); // Simulate repository failure
            _mapperMock.Setup(mapper => mapper.Map<BrugerDTO>(It.IsAny<Bruger>())).Returns((BrugerDTO)null);

            // Act
            var result = await _brugerService.CreateBrugerAsync(newBrugerDto);

            // Assert
            result.Success.Should().BeFalse(); // The result should be a failure
            result.Errors.Should().Contain("Failed to create Bruger."); // The error message should match the expected one
        }

        // Test for UpdateBrugerAsync
        [Fact]
        public async Task UpdateBrugerAsync_ShouldReturnSuccess_WhenBrugerUpdated()
        {
            // Arrange
            var id = Guid.NewGuid();
            var existingBruger = new Bruger { BrugerID = id, Brugernavn = "OldUser" };
            var updatedBrugerDto = new BrugerDTO { Brugernavn = "UpdatedUser", ModifiedBy = "Admin" };

            _brugerRepositoryMock.Setup(repo => repo.GetBrugerByIdAsync(id)).ReturnsAsync(existingBruger);
            _mapperMock.Setup(mapper => mapper.Map(It.IsAny<BrugerDTO>(), It.IsAny<Bruger>())).Callback<BrugerDTO, Bruger>((src, dest) => dest.Brugernavn = src.Brugernavn);
            _brugerRepositoryMock.Setup(repo => repo.UpdateBrugerAsync(It.IsAny<Bruger>())).ReturnsAsync(true);

            // Act
            var result = await _brugerService.UpdateBrugerAsync(id, updatedBrugerDto);

            // Assert
            result.Success.Should().BeTrue();
        }

        [Fact]
        public async Task UpdateBrugerAsync_ShouldReturnFail_WhenBrugerNotFound()
        {
            // Arrange
            var id = Guid.NewGuid();
            var updatedBrugerDto = new BrugerDTO { Brugernavn = "UpdatedUser", ModifiedBy = "Admin" };
            _brugerRepositoryMock.Setup(repo => repo.GetBrugerByIdAsync(id)).ReturnsAsync((Bruger)null);

            // Act
            var result = await _brugerService.UpdateBrugerAsync(id, updatedBrugerDto);

            // Assert
            result.Success.Should().BeFalse();
            result.Errors.Should().Contain("Bruger not found.");
        }

        // Test for DeleteBrugerAsync
        [Fact]
        public async Task DeleteBrugerAsync_ShouldReturnSuccess_WhenBrugerDeleted()
        {
            // Arrange
            var id = Guid.NewGuid();
            var existingBruger = new Bruger { BrugerID = id, Brugernavn = "TestUser", IsDeleted = false };
            _brugerRepositoryMock.Setup(repo => repo.GetBrugerByIdAsync(id)).ReturnsAsync(existingBruger);
            _brugerRepositoryMock.Setup(repo => repo.UpdateBrugerAsync(It.IsAny<Bruger>())).ReturnsAsync(true);

            // Act
            var result = await _brugerService.DeleteBrugerAsync(id);

            // Assert
            result.Success.Should().BeTrue();
        }

        [Fact]
        public async Task DeleteBrugerAsync_ShouldReturnFail_WhenBrugerAlreadyDeleted()
        {
            // Arrange
            var id = Guid.NewGuid();
            var existingBruger = new Bruger { BrugerID = id, Brugernavn = "TestUser", IsDeleted = true };
            _brugerRepositoryMock.Setup(repo => repo.GetBrugerByIdAsync(id)).ReturnsAsync(existingBruger);

            // Act
            var result = await _brugerService.DeleteBrugerAsync(id);

            // Assert
            result.Success.Should().BeFalse();
            result.Errors.Should().Contain("Bruger not found or already deleted.");
        }

        // Test for AuthenticateBrugerAsync
        [Fact]
        public async Task AuthenticateBrugerAsync_ShouldReturnSuccess_WhenCredentialsAreValid()
        {
            // Arrange
            var loginDto = new LoginDTO { EmailOrBrugernavn = "testuser", Brugerkode = "validpassword" };

            var hashedPassword = BCrypt.Net.BCrypt.HashPassword("validpassword");
            var bruger = new Bruger
            {
                BrugerID = Guid.NewGuid(),
                Brugernavn = "TestUser",
                Brugerkode = hashedPassword // The valid BCrypt hash of the password
            };

            _brugerRepositoryMock.Setup(repo => repo.GetBrugerByEmailOrBrugernavnAsync(loginDto.EmailOrBrugernavn))
                .ReturnsAsync(bruger);

            // Mock JWT generation to return a valid token
            _jwtHelperMock.Setup(helper => helper.GenerateToken(It.IsAny<Bruger>())).Returns("validToken");

            // Mock the mapper to map the "bruger" to a "BrugerDTO"
            _mapperMock.Setup(mapper => mapper.Map<BrugerDTO>(It.IsAny<Bruger>())).Returns(new BrugerDTO());

            // Act
            var result = await _brugerService.AuthenticateBrugerAsync(loginDto);

            // Assert
            result.Success.Should().BeTrue();
            result.Value.Should().NotBeNull();
            result.Value.Token.Should().Be("validToken");
        }

        [Fact]
        public async Task AuthenticateBrugerAsync_ShouldReturnFail_WhenInvalidCredentials()
        {
            // Arrange
            var loginDto = new LoginDTO { EmailOrBrugernavn = "invaliduser", Brugerkode = "wrongpassword" };
            _brugerRepositoryMock.Setup(repo => repo.GetBrugerByEmailOrBrugernavnAsync(loginDto.EmailOrBrugernavn)).ReturnsAsync((Bruger)null);

            // Act
            var result = await _brugerService.AuthenticateBrugerAsync(loginDto);

            // Assert
            result.Success.Should().BeFalse();
            result.Errors.Should().Contain("Invalid credentials.");
        }
    }
}
