using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;
using Moq;
using FluentAssertions;
using TaekwondoApp.Shared.Models;
using TaekwondoOrchestration.ApiService.RepositorieInterfaces;

namespace TaekwondoOrchestration.Tests
{
    public class OrdbogRepositoryTests
    {
        private readonly Mock<IOrdbogRepository> _mockRepo;

        public OrdbogRepositoryTests()
        {
            _mockRepo = new Mock<IOrdbogRepository>();
        }
        [Fact]
        public async Task GetAllOrdbogAsync_ShouldReturnList()
        {
            var expected = new List<Ordbog> { new() { DanskOrd = "Hej" }, new() { DanskOrd = "Tak" } };

            _mockRepo.Setup(repo => repo.GetAllOrdbogAsync()).ReturnsAsync(expected);

            var result = await _mockRepo.Object.GetAllOrdbogAsync();

            result.Should().BeEquivalentTo(expected);
        }

        [Fact]
        public async Task GetAllOrdbogAsync_ShouldReturnEmptyList_WhenNoneExist()
        {
            _mockRepo.Setup(repo => repo.GetAllOrdbogAsync()).ReturnsAsync(new List<Ordbog>());

            var result = await _mockRepo.Object.GetAllOrdbogAsync();

            result.Should().BeEmpty();
        }

        [Fact]
        public async Task GetOrdbogByIdAsync_ShouldReturnItem_WhenExists()
        {
            var id = Guid.NewGuid();
            var ordbog = new Ordbog { OrdbogId = id, DanskOrd = "Hej" };

            _mockRepo.Setup(repo => repo.GetOrdbogByIdAsync(id)).ReturnsAsync(ordbog);

            var result = await _mockRepo.Object.GetOrdbogByIdAsync(id);

            result.Should().BeEquivalentTo(ordbog);
        }

        [Fact]
        public async Task GetOrdbogByIdAsync_ShouldReturnNull_WhenNotFound()
        {
            _mockRepo.Setup(repo => repo.GetOrdbogByIdAsync(It.IsAny<Guid>())).ReturnsAsync((Ordbog?)null);

            var result = await _mockRepo.Object.GetOrdbogByIdAsync(Guid.NewGuid());

            result.Should().BeNull();
        }

        [Fact]
        public async Task CreateOrdbogAsync_ShouldReturnCreatedOrdbog()
        {
            var ordbog = new Ordbog { DanskOrd = "Hej" };

            _mockRepo.Setup(repo => repo.CreateOrdbogAsync(ordbog)).ReturnsAsync(ordbog);

            var result = await _mockRepo.Object.CreateOrdbogAsync(ordbog);

            result.Should().BeEquivalentTo(ordbog);
        }

        [Fact]
        public async Task CreateOrdbogAsync_ShouldReturnNull_WhenCreationFails()
        {
            var ordbog = new Ordbog { DanskOrd = "Hej" };

            _mockRepo.Setup(repo => repo.CreateOrdbogAsync(ordbog)).ReturnsAsync((Ordbog?)null);

            var result = await _mockRepo.Object.CreateOrdbogAsync(ordbog);

            result.Should().BeNull();
        }

        [Fact]
        public async Task UpdateOrdbogAsync_ShouldReturnTrue_WhenSuccessful()
        {
            var ordbog = new Ordbog { DanskOrd = "Hej" };

            _mockRepo.Setup(repo => repo.UpdateOrdbogAsync(ordbog)).ReturnsAsync(true);

            var result = await _mockRepo.Object.UpdateOrdbogAsync(ordbog);

            result.Should().BeTrue();
        }

        [Fact]
        public async Task UpdateOrdbogAsync_ShouldReturnFalse_WhenUnsuccessful()
        {
            var ordbog = new Ordbog { DanskOrd = "Hej" };

            _mockRepo.Setup(repo => repo.UpdateOrdbogAsync(ordbog)).ReturnsAsync(false);

            var result = await _mockRepo.Object.UpdateOrdbogAsync(ordbog);

            result.Should().BeFalse();
        }

        [Fact]
        public async Task DeleteOrdbogAsync_ShouldReturnTrue_WhenDeleted()
        {
            var id = Guid.NewGuid();

            _mockRepo.Setup(repo => repo.DeleteOrdbogAsync(id)).ReturnsAsync(true);

            var result = await _mockRepo.Object.DeleteOrdbogAsync(id);

            result.Should().BeTrue();
        }

        [Fact]
        public async Task DeleteOrdbogAsync_ShouldReturnFalse_WhenNotDeleted()
        {
            var id = Guid.NewGuid();

            _mockRepo.Setup(repo => repo.DeleteOrdbogAsync(id)).ReturnsAsync(false);

            var result = await _mockRepo.Object.DeleteOrdbogAsync(id);

            result.Should().BeFalse();
        }

        [Fact]
        public async Task GetAllOrdbogIncludingDeletedAsync_ShouldReturnList()
        {
            var ordbogs = new List<Ordbog> { new() { DanskOrd = "Hej" } };

            _mockRepo.Setup(repo => repo.GetAllOrdbogIncludingDeletedAsync()).ReturnsAsync(ordbogs);

            var result = await _mockRepo.Object.GetAllOrdbogIncludingDeletedAsync();

            result.Should().BeEquivalentTo(ordbogs);
        }

        [Fact]
        public async Task GetAllOrdbogIncludingDeletedAsync_ShouldReturnEmpty_WhenNoneExist()
        {
            _mockRepo.Setup(repo => repo.GetAllOrdbogIncludingDeletedAsync()).ReturnsAsync(new List<Ordbog>());

            var result = await _mockRepo.Object.GetAllOrdbogIncludingDeletedAsync();

            result.Should().BeEmpty();
        }

        [Fact]
        public async Task GetOrdbogByDanskOrdAsync_ShouldReturnMatch()
        {
            var danskOrd = "Hej";
            var ordbog = new Ordbog { DanskOrd = danskOrd };

            _mockRepo.Setup(repo => repo.GetOrdbogByDanskOrdAsync(danskOrd)).ReturnsAsync(ordbog);

            var result = await _mockRepo.Object.GetOrdbogByDanskOrdAsync(danskOrd);

            result.Should().BeEquivalentTo(ordbog);
        }

        [Fact]
        public async Task GetOrdbogByDanskOrdAsync_ShouldReturnNull_WhenNotFound()
        {
            _mockRepo.Setup(repo => repo.GetOrdbogByDanskOrdAsync(It.IsAny<string>())).ReturnsAsync((Ordbog?)null);

            var result = await _mockRepo.Object.GetOrdbogByDanskOrdAsync("NonExistent");

            result.Should().BeNull();
        }

        [Fact]
        public async Task GetOrdbogByKoranskOrdAsync_ShouldReturnMatch()
        {
            var KoranskOrd = "안녕";
            var ordbog = new Ordbog { KoranskOrd = KoranskOrd };

            _mockRepo.Setup(repo => repo.GetOrdbogByKoranskOrdAsync(KoranskOrd)).ReturnsAsync(ordbog);

            var result = await _mockRepo.Object.GetOrdbogByKoranskOrdAsync(KoranskOrd);

            result.Should().BeEquivalentTo(ordbog);
        }

        [Fact]
        public async Task GetOrdbogByKoranskOrdAsync_ShouldReturnNull_WhenNotFound()
        {
            _mockRepo.Setup(repo => repo.GetOrdbogByKoranskOrdAsync(It.IsAny<string>())).ReturnsAsync((Ordbog?)null);

            var result = await _mockRepo.Object.GetOrdbogByKoranskOrdAsync("NonExistent");

            result.Should().BeNull();
        }

        [Fact]
        public async Task UpdateOrdbogIncludingDeletedAsync_ShouldReturnUpdatedOrdbog()
        {
            var id = Guid.NewGuid();
            var ordbog = new Ordbog { OrdbogId = id };

            _mockRepo.Setup(repo => repo.UpdateOrdbogIncludingDeletedAsync(id, ordbog)).ReturnsAsync(ordbog);

            var result = await _mockRepo.Object.UpdateOrdbogIncludingDeletedAsync(id, ordbog);

            result.Should().BeEquivalentTo(ordbog);
        }

        [Fact]
        public async Task UpdateOrdbogIncludingDeletedAsync_ShouldReturnNull_WhenNotUpdated()
        {
            var id = Guid.NewGuid();
            var ordbog = new Ordbog { OrdbogId = id };

            _mockRepo.Setup(repo => repo.UpdateOrdbogIncludingDeletedAsync(id, ordbog)).ReturnsAsync((Ordbog?)null);

            var result = await _mockRepo.Object.UpdateOrdbogIncludingDeletedAsync(id, ordbog);

            result.Should().BeNull();
        }

        [Fact]
        public async Task UpdateAsync_ShouldReturnTrue_WhenUpdated()
        {
            var ordbog = new Ordbog { DanskOrd = "Hej" };

            _mockRepo.Setup(repo => repo.UpdateAsync(ordbog)).ReturnsAsync(true);

            var result = await _mockRepo.Object.UpdateAsync(ordbog);

            result.Should().BeTrue();
        }

        [Fact]
        public async Task UpdateAsync_ShouldReturnFalse_WhenNotUpdated()
        {
            var ordbog = new Ordbog { DanskOrd = "Hej" };

            _mockRepo.Setup(repo => repo.UpdateAsync(ordbog)).ReturnsAsync(false);

            var result = await _mockRepo.Object.UpdateAsync(ordbog);

            result.Should().BeFalse();
        }

        [Fact]
        public async Task GetOrdbogByIdIncludingDeletedAsync_ShouldReturnItem()
        {
            var id = Guid.NewGuid();
            var ordbog = new Ordbog { OrdbogId = id };

            _mockRepo.Setup(repo => repo.GetOrdbogByIdIncludingDeletedAsync(id)).ReturnsAsync(ordbog);

            var result = await _mockRepo.Object.GetOrdbogByIdIncludingDeletedAsync(id);

            result.Should().BeEquivalentTo(ordbog);
        }

        [Fact]
        public async Task GetOrdbogByIdIncludingDeletedAsync_ShouldReturnNull_WhenNotFound()
        {
            _mockRepo.Setup(repo => repo.GetOrdbogByIdIncludingDeletedAsync(It.IsAny<Guid>())).ReturnsAsync((Ordbog?)null);

            var result = await _mockRepo.Object.GetOrdbogByIdIncludingDeletedAsync(Guid.NewGuid());

            result.Should().BeNull();
        }
    }

}
