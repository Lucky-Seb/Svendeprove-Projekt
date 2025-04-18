using Moq;
using Xunit;
using FluentAssertions;
using TaekwondoApp.Shared.Models;
using TaekwondoApp.Shared.DTO;
using TaekwondoOrchestration.ApiService.Repositories;
using TaekwondoOrchestration.ApiService.Services;
using TaekwondoOrchestration.ApiService.Helpers;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TaekwondoOrchestration.ApiService.RepositorieInterfaces;

namespace TaekwondoOrchestration.Tests.BrugerTests
{
    public class BrugerServiceTests
    {
        private readonly Mock<IBrugerRepository> _mockRepo;
        private readonly Mock<IMapper> _mockMapper;
        private readonly BrugerService _service;

        public BrugerServiceTests()
        {
            _mockRepo = new Mock<IBrugerRepository>();
            _mockMapper = new Mock<IMapper>();
            _service = new BrugerService(_mockRepo.Object, _mockMapper.Object);
        }

        // GetAllBrugereAsync Tests
        [Fact]
        public async Task GetAllBrugereAsync_ShouldReturnBrugere_WhenFound()
        {
            var brugere = new List<Bruger> { new Bruger { Brugernavn = "john_doe" } };
            _mockRepo.Setup(repo => repo.GetAllBrugereAsync()).ReturnsAsync(brugere);
            _mockMapper.Setup(m => m.Map<IEnumerable<BrugerDTO>>(It.IsAny<IEnumerable<Bruger>>()))
                       .Returns(new List<BrugerDTO> { new BrugerDTO { Brugernavn = "john_doe" } });

            var result = await _service.GetAllBrugereAsync();

            result.Success.Should().BeTrue();
            result.Value.Should().HaveCount(1);
        }

        [Fact]
        public async Task GetAllBrugereAsync_ShouldReturnFail_WhenNoBrugereExist()
        {
            _mockRepo.Setup(repo => repo.GetAllBrugereAsync()).ReturnsAsync(new List<Bruger>());
            var result = await _service.GetAllBrugereAsync();

            result.Success.Should().BeFalse();
            result.Errors.Should().Contain("No users found.");
        }

        // GetBrugerByIdAsync Tests
        [Fact]
        public async Task GetBrugerByIdAsync_ShouldReturnBruger_WhenFound()
        {
            var id = Guid.NewGuid();
            var bruger = new Bruger { BrugerID = id, Brugernavn = "john_doe" };
            _mockRepo.Setup(repo => repo.GetBrugerByIdAsync(id)).ReturnsAsync(bruger);
            _mockMapper.Setup(m => m.Map<BrugerDTO>(It.IsAny<Bruger>())).Returns(new BrugerDTO { Brugernavn = "john_doe" });

            var result = await _service.GetBrugerByIdAsync(id);

            result.Success.Should().BeTrue();
            result.Value.Brugernavn.Should().Be("john_doe");
        }

        [Fact]
        public async Task GetBrugerByIdAsync_ShouldReturnFail_WhenNotFound()
        {
            var id = Guid.NewGuid();
            _mockRepo.Setup(repo => repo.GetBrugerByIdAsync(id)).ReturnsAsync((Bruger?)null);

            var result = await _service.GetBrugerByIdAsync(id);

            result.Success.Should().BeFalse();
            result.Errors.Should().Contain("Bruger not found.");
        }

        // CreateBrugerAsync Tests
        [Fact]
        public async Task CreateBrugerAsync_ShouldReturnCreatedBruger()
        {
            var dto = new BrugerDTO { Brugernavn = "john_doe" };
            var entity = new Bruger { Brugernavn = "john_doe" };
            var createdBruger = new Bruger { Brugernavn = "john_doe" };

            _mockMapper.Setup(m => m.Map<Bruger>(dto)).Returns(entity);
            _mockRepo.Setup(repo => repo.CreateBrugerAsync(entity)).ReturnsAsync(createdBruger);
            _mockMapper.Setup(m => m.Map<BrugerDTO>(createdBruger)).Returns(new BrugerDTO { Brugernavn = "john_doe" });

            var result = await _service.CreateBrugerAsync(dto);

            result.Success.Should().BeTrue();
            result.Value.Brugernavn.Should().Be("john_doe");
        }

        [Fact]
        public async Task CreateBrugerAsync_ShouldReturnFail_WhenCreationFails()
        {
            var dto = new BrugerDTO { Brugernavn = "john_doe" };
            _mockRepo.Setup(repo => repo.CreateBrugerAsync(It.IsAny<Bruger>())).ReturnsAsync((Bruger?)null);

            var result = await _service.CreateBrugerAsync(dto);

            result.Success.Should().BeFalse();
            result.Errors.Should().Contain("Failed to create Bruger.");
        }

        // UpdateBrugerAsync Tests
        [Fact]
        public async Task UpdateBrugerAsync_ShouldReturnSuccess_WhenUpdated()
        {
            var id = Guid.NewGuid();
            var dto = new BrugerDTO { Brugernavn = "updated_doe" };
            var existingBruger = new Bruger { BrugerID = id, Brugernavn = "john_doe" };

            _mockRepo.Setup(repo => repo.GetBrugerByIdAsync(id)).ReturnsAsync(existingBruger);
            _mockMapper.Setup(m => m.Map(dto, existingBruger)).Verifiable();
            _mockRepo.Setup(repo => repo.UpdateBrugerAsync(existingBruger)).ReturnsAsync(true);

            var result = await _service.UpdateBrugerAsync(id, dto);

            result.Success.Should().BeTrue();
        }

        [Fact]
        public async Task UpdateBrugerAsync_ShouldReturnFail_WhenNotFound()
        {
            var id = Guid.NewGuid();
            var dto = new BrugerDTO { Brugernavn = "updated_doe" };
            _mockRepo.Setup(repo => repo.GetBrugerByIdAsync(id)).ReturnsAsync((Bruger?)null);

            var result = await _service.UpdateBrugerAsync(id, dto);

            result.Success.Should().BeFalse();
            result.Errors.Should().Contain("Bruger not found.");
        }

        [Fact]
        public async Task UpdateBrugerAsync_ShouldReturnFail_WhenUpdateFails()
        {
            var id = Guid.NewGuid();
            var dto = new BrugerDTO { Brugernavn = "updated_doe" };
            var existingBruger = new Bruger { BrugerID = id, Brugernavn = "john_doe" };

            _mockRepo.Setup(repo => repo.GetBrugerByIdAsync(id)).ReturnsAsync(existingBruger);
            _mockMapper.Setup(m => m.Map(dto, existingBruger)).Verifiable();
            _mockRepo.Setup(repo => repo.UpdateBrugerAsync(existingBruger)).ReturnsAsync(false);

            var result = await _service.UpdateBrugerAsync(id, dto);

            result.Success.Should().BeFalse();
            result.Errors.Should().Contain("Failed to update Bruger.");
        }

        // DeleteBrugerAsync Tests
        [Fact]
        public async Task DeleteBrugerAsync_ShouldReturnSuccess_WhenDeleted()
        {
            var id = Guid.NewGuid();
            var bruger = new Bruger { BrugerID = id, IsDeleted = false };
            _mockRepo.Setup(repo => repo.GetBrugerByIdAsync(id)).ReturnsAsync(bruger);
            _mockRepo.Setup(repo => repo.UpdateBrugerAsync(bruger)).ReturnsAsync(true);

            var result = await _service.DeleteBrugerAsync(id);

            result.Success.Should().BeTrue();
        }

        [Fact]
        public async Task DeleteBrugerAsync_ShouldReturnFail_WhenBrugerNotFound()
        {
            var id = Guid.NewGuid();
            _mockRepo.Setup(repo => repo.GetBrugerByIdAsync(id)).ReturnsAsync((Bruger?)null);

            var result = await _service.DeleteBrugerAsync(id);

            result.Success.Should().BeFalse();
            result.Errors.Should().Contain("Bruger not found or already deleted.");
        }

        // RestoreBrugerAsync Tests
        [Fact]
        public async Task RestoreBrugerAsync_ShouldReturnSuccess_WhenRestored()
        {
            var id = Guid.NewGuid();
            var dto = new BrugerDTO { ModifiedBy = "admin" };
            var deletedBruger = new Bruger { BrugerID = id, IsDeleted = true };

            _mockRepo.Setup(repo => repo.GetBrugerByIdIncludingDeletedAsync(id)).ReturnsAsync(deletedBruger);
            _mockRepo.Setup(repo => repo.UpdateBrugerAsync(deletedBruger)).ReturnsAsync(true);

            var result = await _service.RestoreBrugerAsync(id, dto);

            result.Success.Should().BeTrue();
        }

        [Fact]
        public async Task RestoreBrugerAsync_ShouldReturnFail_WhenBrugerNotFoundOrNotDeleted()
        {
            var id = Guid.NewGuid();
            var dto = new BrugerDTO { ModifiedBy = "admin" };
            _mockRepo.Setup(repo => repo.GetBrugerByIdIncludingDeletedAsync(id)).ReturnsAsync((Bruger?)null);

            var result = await _service.RestoreBrugerAsync(id, dto);

            result.Success.Should().BeFalse();
            result.Errors.Should().Contain("Bruger not found or not deleted.");
        }

        // GetBrugerByRoleAsync Tests
        [Fact]
        public async Task GetBrugerByRoleAsync_ShouldReturnBrugere_WhenFound()
        {
            var role = "Admin";
            var brugere = new List<Bruger> { new Bruger { Role = role } };
            _mockRepo.Setup(repo => repo.GetBrugerByRoleAsync(role)).ReturnsAsync(brugere);
            _mockMapper.Setup(m => m.Map<IEnumerable<BrugerDTO>>(brugere)).Returns(new List<BrugerDTO> { new BrugerDTO { Role = role } });

            var result = await _service.GetBrugerByRoleAsync(role);

            result.Success.Should().BeTrue();
            result.Value.Should().HaveCount(1);
        }

        [Fact]
        public async Task GetBrugerByRoleAsync_ShouldReturnFail_WhenNoBrugereFound()
        {
            _mockRepo.Setup(repo => repo.GetBrugerByRoleAsync(It.IsAny<string>())).ReturnsAsync(new List<Bruger>());
            var result = await _service.GetBrugerByRoleAsync("NonexistentRole");

            result.Success.Should().BeFalse();
            result.Errors.Should().Contain("No users found.");
        }

        // GetBrugerByBælteAsync Tests
        [Fact]
        public async Task GetBrugerByBælteAsync_ShouldReturnBrugere_WhenFound()
        {
            var bæltegrad = "Black";
            var brugere = new List<Bruger> { new Bruger { Bæltegrad = "Black" } };
            _mockRepo.Setup(repo => repo.GetBrugerByBælteAsync(bæltegrad)).ReturnsAsync(brugere);
            _mockMapper.Setup(m => m.Map<IEnumerable<BrugerDTO>>(brugere)).Returns(new List<BrugerDTO> { new BrugerDTO { Bæltegrad = "Black" } });

            var result = await _service.GetBrugerByBælteAsync(bæltegrad);

            result.Success.Should().BeTrue();
            result.Value.Should().HaveCount(1);
        }

        [Fact]
        public async Task GetBrugerByBælteAsync_ShouldReturnFail_WhenNoBrugereFound()
        {
            _mockRepo.Setup(repo => repo.GetBrugerByBælteAsync(It.IsAny<string>())).ReturnsAsync(new List<Bruger>());
            var result = await _service.GetBrugerByBælteAsync("NonexistentBælte");

            result.Success.Should().BeFalse();
            result.Errors.Should().Contain("No users found.");
        }

        // GetBrugerByBrugernavnAsync Tests
        [Fact]
        public async Task GetBrugerByBrugernavnAsync_ShouldReturnBruger_WhenFound()
        {
            var brugernavn = "john_doe";
            var bruger = new Bruger { Brugernavn = brugernavn };
            _mockRepo.Setup(repo => repo.GetBrugerByBrugernavnAsync(brugernavn)).ReturnsAsync(bruger);
            _mockMapper.Setup(m => m.Map<BrugerDTO>(bruger)).Returns(new BrugerDTO { Brugernavn = brugernavn });

            var result = await _service.GetBrugerByBrugernavnAsync(brugernavn);

            result.Success.Should().BeTrue();
            result.Value.Brugernavn.Should().Be(brugernavn);
        }

        [Fact]
        public async Task GetBrugerByBrugernavnAsync_ShouldReturnFail_WhenNotFound()
        {
            _mockRepo.Setup(repo => repo.GetBrugerByBrugernavnAsync(It.IsAny<string>())).ReturnsAsync((Bruger?)null);

            var result = await _service.GetBrugerByBrugernavnAsync("nonexistent_username");

            result.Success.Should().BeFalse();
            result.Errors.Should().Contain("Bruger not found.");
        }

        // AuthenticateBrugerAsync Tests
        [Fact]
        public async Task AuthenticateBrugerAsync_ShouldReturnBruger_WhenCredentialsAreValid()
        {
            var loginDto = new LoginDTO { EmailOrBrugernavn = "john_doe", Brugerkode = "password123" };
            var bruger = new Bruger { Brugernavn = "john_doe" };
            _mockRepo.Setup(repo => repo.AuthenticateBrugerAsync(loginDto.EmailOrBrugernavn, loginDto.Brugerkode)).ReturnsAsync(bruger);
            _mockMapper.Setup(m => m.Map<BrugerDTO>(bruger)).Returns(new BrugerDTO { Brugernavn = "john_doe" });

            var result = await _service.AuthenticateBrugerAsync(loginDto);

            result.Success.Should().BeTrue();
            result.Value.Brugernavn.Should().Be("john_doe");
        }

        [Fact]
        public async Task AuthenticateBrugerAsync_ShouldReturnFail_WhenInvalidCredentials()
        {
            var loginDto = new LoginDTO { EmailOrBrugernavn = "wrong_username", Brugerkode = "wrong_password" };
            _mockRepo.Setup(repo => repo.AuthenticateBrugerAsync(loginDto.EmailOrBrugernavn, loginDto.Brugerkode)).ReturnsAsync((Bruger?)null);

            var result = await _service.AuthenticateBrugerAsync(loginDto);

            result.Success.Should().BeFalse();
            result.Errors.Should().Contain("Invalid credentials.");
        }
    }
}
