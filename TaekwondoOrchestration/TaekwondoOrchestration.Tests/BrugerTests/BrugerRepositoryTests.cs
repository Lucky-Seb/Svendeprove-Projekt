using Xunit;
using Moq;
using FluentAssertions;
using AutoMapper;
using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using TaekwondoOrchestration.ApiService.Data;
using TaekwondoOrchestration.ApiService.Repositories;
using TaekwondoApp.Shared.Models;
using TaekwondoApp.Shared.DTO;
using TaekwondoOrchestration.ApiService.RepositorieInterfaces;

namespace TaekwondoOrchestration.Tests.BrugerTests
{
    public class BrugerRepositoryTests
    {
        private readonly Mock<IBrugerRepository> _mockRepo;

        public BrugerRepositoryTests()
        {
            _mockRepo = new Mock<IBrugerRepository>();
        }

        [Fact]
        public async Task GetAllBrugereAsync_ShouldReturnList()
        {
            var expected = new List<Bruger> { new() { Fornavn = "John" }, new() { Fornavn = "Jane" } };
            _mockRepo.Setup(repo => repo.GetAllBrugereAsync()).ReturnsAsync(expected);

            var result = await _mockRepo.Object.GetAllBrugereAsync();

            result.Should().BeEquivalentTo(expected);
        }

        [Fact]
        public async Task GetAllBrugereAsync_ShouldReturnEmptyList_WhenNoneExist()
        {
            _mockRepo.Setup(repo => repo.GetAllBrugereAsync()).ReturnsAsync(new List<Bruger>());

            var result = await _mockRepo.Object.GetAllBrugereAsync();

            result.Should().BeEmpty();
        }

        [Fact]
        public async Task GetBrugerByIdAsync_ShouldReturnBruger_WhenExists()
        {
            var id = Guid.NewGuid();
            var bruger = new Bruger { BrugerID = id, Fornavn = "John" };
            _mockRepo.Setup(repo => repo.GetBrugerByIdAsync(id)).ReturnsAsync(bruger);

            var result = await _mockRepo.Object.GetBrugerByIdAsync(id);

            result.Should().BeEquivalentTo(bruger);
        }

        [Fact]
        public async Task GetBrugerByIdAsync_ShouldReturnNull_WhenNotFound()
        {
            _mockRepo.Setup(repo => repo.GetBrugerByIdAsync(It.IsAny<Guid>())).ReturnsAsync((Bruger?)null);

            var result = await _mockRepo.Object.GetBrugerByIdAsync(Guid.NewGuid());

            result.Should().BeNull();
        }

        [Fact]
        public async Task GetBrugerByRoleAsync_ShouldReturnList_WhenRoleExists()
        {
            var role = "Admin";
            var brugere = new List<Bruger> { new() { Role = role } };
            _mockRepo.Setup(repo => repo.GetBrugerByRoleAsync(role)).ReturnsAsync(brugere);

            var result = await _mockRepo.Object.GetBrugerByRoleAsync(role);

            result.Should().BeEquivalentTo(brugere);
        }

        [Fact]
        public async Task GetBrugerByRoleAsync_ShouldReturnEmptyList_WhenRoleNotFound()
        {
            _mockRepo.Setup(repo => repo.GetBrugerByRoleAsync(It.IsAny<string>())).ReturnsAsync(new List<Bruger>());

            var result = await _mockRepo.Object.GetBrugerByRoleAsync("NonExistentRole");

            result.Should().BeEmpty();
        }

        [Fact]
        public async Task GetBrugerByBælteAsync_ShouldReturnList_WhenBælteExists()
        {
            var bælte = "Sort";
            var brugere = new List<Bruger> { new() { Bæltegrad = bælte } };
            _mockRepo.Setup(repo => repo.GetBrugerByBælteAsync(bælte)).ReturnsAsync(brugere);

            var result = await _mockRepo.Object.GetBrugerByBælteAsync(bælte);

            result.Should().BeEquivalentTo(brugere);
        }

        [Fact]
        public async Task GetBrugerByBælteAsync_ShouldReturnEmptyList_WhenBælteNotFound()
        {
            _mockRepo.Setup(repo => repo.GetBrugerByBælteAsync(It.IsAny<string>())).ReturnsAsync(new List<Bruger>());

            var result = await _mockRepo.Object.GetBrugerByBælteAsync("NonExistentBælte");

            result.Should().BeEmpty();
        }

        [Fact]
        public async Task GetBrugereByKlubAsync_ShouldReturnList_WhenKlubExists()
        {
            var klubId = Guid.NewGuid();
            var brugere = new List<BrugerDTO> { new() { Fornavn = "John" } };
            _mockRepo.Setup(repo => repo.GetBrugereByKlubAsync(klubId)).ReturnsAsync(brugere);

            var result = await _mockRepo.Object.GetBrugereByKlubAsync(klubId);

            result.Should().BeEquivalentTo(brugere);
        }

        [Fact]
        public async Task GetBrugereByKlubAsync_ShouldReturnEmptyList_WhenKlubNotFound()
        {
            _mockRepo.Setup(repo => repo.GetBrugereByKlubAsync(It.IsAny<Guid>())).ReturnsAsync(new List<BrugerDTO>());

            var result = await _mockRepo.Object.GetBrugereByKlubAsync(Guid.NewGuid());

            result.Should().BeEmpty();
        }

        [Fact]
        public async Task GetBrugereByKlubAndBæltegradAsync_ShouldReturnList_WhenExists()
        {
            var klubId = Guid.NewGuid();
            var bælte = "Sort";
            var brugere = new List<BrugerDTO> { new() { Fornavn = "John", Bæltegrad = bælte } };
            _mockRepo.Setup(repo => repo.GetBrugereByKlubAndBæltegradAsync(klubId, bælte)).ReturnsAsync(brugere);

            var result = await _mockRepo.Object.GetBrugereByKlubAndBæltegradAsync(klubId, bælte);

            result.Should().BeEquivalentTo(brugere);
        }

        [Fact]
        public async Task GetBrugereByKlubAndBæltegradAsync_ShouldReturnEmptyList_WhenNotFound()
        {
            _mockRepo.Setup(repo => repo.GetBrugereByKlubAndBæltegradAsync(It.IsAny<Guid>(), It.IsAny<string>())).ReturnsAsync(new List<BrugerDTO>());

            var result = await _mockRepo.Object.GetBrugereByKlubAndBæltegradAsync(Guid.NewGuid(), "NonExistentBælte");

            result.Should().BeEmpty();
        }
        [Fact]
        public async Task GetBrugerByBrugernavnAsync_ShouldReturnBruger_WhenExists()
        {
            var brugernavn = "john_doe";
            var bruger = new Bruger { Brugernavn = brugernavn };
            _mockRepo.Setup(repo => repo.GetBrugerByBrugernavnAsync(brugernavn)).ReturnsAsync(bruger);

            var result = await _mockRepo.Object.GetBrugerByBrugernavnAsync(brugernavn);

            result.Should().BeEquivalentTo(bruger);
        }

        [Fact]
        public async Task GetBrugerByBrugernavnAsync_ShouldReturnNull_WhenNotFound()
        {
            _mockRepo.Setup(repo => repo.GetBrugerByBrugernavnAsync(It.IsAny<string>())).ReturnsAsync((Bruger?)null);

            var result = await _mockRepo.Object.GetBrugerByBrugernavnAsync("nonexistent_username");

            result.Should().BeNull();
        }

        [Fact]
        public async Task GetBrugerByFornavnEfternavnAsync_ShouldReturnList_WhenMatchesFound()
        {
            var fornavn = "John";
            var efternavn = "Doe";
            var brugere = new List<Bruger> { new() { Fornavn = fornavn, Efternavn = efternavn } };
            _mockRepo.Setup(repo => repo.GetBrugerByFornavnEfternavnAsync(fornavn, efternavn)).ReturnsAsync(brugere);

            var result = await _mockRepo.Object.GetBrugerByFornavnEfternavnAsync(fornavn, efternavn);

            result.Should().BeEquivalentTo(brugere);
        }

        [Fact]
        public async Task GetBrugerByFornavnEfternavnAsync_ShouldReturnEmptyList_WhenNoMatch()
        {
            _mockRepo.Setup(repo => repo.GetBrugerByFornavnEfternavnAsync(It.IsAny<string>(), It.IsAny<string>()))
                     .ReturnsAsync(new List<Bruger>());

            var result = await _mockRepo.Object.GetBrugerByFornavnEfternavnAsync("Non", "Existent");

            result.Should().BeEmpty();
        }

        [Fact]
        public async Task CreateBrugerAsync_ShouldReturnCreatedBruger()
        {
            var bruger = new Bruger { Fornavn = "John" };
            _mockRepo.Setup(repo => repo.CreateBrugerAsync(bruger)).ReturnsAsync(bruger);

            var result = await _mockRepo.Object.CreateBrugerAsync(bruger);

            result.Should().BeEquivalentTo(bruger);
        }

        [Fact]
        public async Task CreateBrugerAsync_ShouldReturnNull_WhenCreationFails()
        {
            var bruger = new Bruger { Fornavn = "Fail" };
            _mockRepo.Setup(repo => repo.CreateBrugerAsync(bruger)).ReturnsAsync((Bruger?)null);

            var result = await _mockRepo.Object.CreateBrugerAsync(bruger);

            result.Should().BeNull();
        }

        [Fact]
        public async Task UpdateBrugerAsync_ShouldReturnTrue_WhenSuccessful()
        {
            var bruger = new Bruger { Fornavn = "Updated" };
            _mockRepo.Setup(repo => repo.UpdateBrugerAsync(bruger)).ReturnsAsync(true);

            var result = await _mockRepo.Object.UpdateBrugerAsync(bruger);

            result.Should().BeTrue();
        }

        [Fact]
        public async Task UpdateBrugerAsync_ShouldReturnFalse_WhenFailed()
        {
            var bruger = new Bruger { Fornavn = "NotUpdated" };
            _mockRepo.Setup(repo => repo.UpdateBrugerAsync(bruger)).ReturnsAsync(false);

            var result = await _mockRepo.Object.UpdateBrugerAsync(bruger);

            result.Should().BeFalse();
        }

        [Fact]
        public async Task GetBrugerByIdIncludingDeletedAsync_ShouldReturnBruger_WhenExists()
        {
            var id = Guid.NewGuid();
            var bruger = new Bruger { BrugerID = id };
            _mockRepo.Setup(repo => repo.GetBrugerByIdIncludingDeletedAsync(id)).ReturnsAsync(bruger);

            var result = await _mockRepo.Object.GetBrugerByIdIncludingDeletedAsync(id);

            result.Should().BeEquivalentTo(bruger);
        }

        [Fact]
        public async Task GetBrugerByIdIncludingDeletedAsync_ShouldReturnNull_WhenNotFound()
        {
            _mockRepo.Setup(repo => repo.GetBrugerByIdIncludingDeletedAsync(It.IsAny<Guid>())).ReturnsAsync((Bruger?)null);

            var result = await _mockRepo.Object.GetBrugerByIdIncludingDeletedAsync(Guid.NewGuid());

            result.Should().BeNull();
        }

        [Fact]
        public async Task DeleteBrugerAsync_ShouldReturnTrue_WhenDeleted()
        {
            var id = Guid.NewGuid();
            _mockRepo.Setup(repo => repo.DeleteBrugerAsync(id)).ReturnsAsync(true);

            var result = await _mockRepo.Object.DeleteBrugerAsync(id);

            result.Should().BeTrue();
        }

        [Fact]
        public async Task DeleteBrugerAsync_ShouldReturnFalse_WhenDeleteFails()
        {
            var id = Guid.NewGuid();
            _mockRepo.Setup(repo => repo.DeleteBrugerAsync(id)).ReturnsAsync(false);

            var result = await _mockRepo.Object.DeleteBrugerAsync(id);

            result.Should().BeFalse();
        }

        [Fact]
        public async Task AuthenticateBrugerAsync_ShouldReturnDTO_WhenAuthenticated()
        {
            var dto = new BrugerDTO { Fornavn = "John" };
            _mockRepo.Setup(repo => repo.AuthenticateBrugerAsync("email", "password")).ReturnsAsync(dto);

            var result = await _mockRepo.Object.AuthenticateBrugerAsync("email", "password");

            result.Should().BeEquivalentTo(dto);
        }

        [Fact]
        public async Task AuthenticateBrugerAsync_ShouldReturnNull_WhenAuthenticationFails()
        {
            _mockRepo.Setup(repo => repo.AuthenticateBrugerAsync(It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync((BrugerDTO?)null);

            var result = await _mockRepo.Object.AuthenticateBrugerAsync("wrong", "credentials");

            result.Should().BeNull();
        }
    }
}
