using Xunit;
using Moq;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TaekwondoApp.Shared.Models;
using TaekwondoOrchestration.ApiService.RepositorieInterfaces;

namespace TaekwondoOrchestration.Tests.PensumTests
{
    public class PensumRepositoryTests
    {
        private readonly Mock<IPensumRepository> _mockRepo;

        public PensumRepositoryTests()
        {
            _mockRepo = new Mock<IPensumRepository>();
        }

        [Fact]
        public async Task GetAllPensumAsync_ShouldReturnList()
        {
            var expected = new List<Pensum> { new() { PensumGrad = "Rød" }, new() { PensumGrad = "Sort" } };
            _mockRepo.Setup(r => r.GetAllPensumAsync()).ReturnsAsync(expected);

            var result = await _mockRepo.Object.GetAllPensumAsync();

            result.Should().BeEquivalentTo(expected);
        }

        [Fact]
        public async Task GetPensumByIdAsync_ShouldReturnPensum_WhenExists()
        {
            var id = Guid.NewGuid();
            var pensum = new Pensum { PensumID = id, PensumGrad = "Sort" };
            _mockRepo.Setup(r => r.GetPensumByIdAsync(id)).ReturnsAsync(pensum);

            var result = await _mockRepo.Object.GetPensumByIdAsync(id);

            result.Should().BeEquivalentTo(pensum);
        }

        [Fact]
        public async Task GetPensumByIdAsync_ShouldReturnNull_WhenNotFound()
        {
            _mockRepo.Setup(r => r.GetPensumByIdAsync(It.IsAny<Guid>())).ReturnsAsync((Pensum?)null);

            var result = await _mockRepo.Object.GetPensumByIdAsync(Guid.NewGuid());

            result.Should().BeNull();
        }

        [Fact]
        public async Task GetAllPensumIncludingDeletedAsync_ShouldReturnList()
        {
            var pensumList = new List<Pensum> { new() { PensumGrad = "Gul" } };
            _mockRepo.Setup(r => r.GetAllPensumIncludingDeletedAsync()).ReturnsAsync(pensumList);

            var result = await _mockRepo.Object.GetAllPensumIncludingDeletedAsync();

            result.Should().BeEquivalentTo(pensumList);
        }

        [Fact]
        public async Task GetPensumByIdIncludingDeletedAsync_ShouldReturnPensum_WhenExists()
        {
            var id = Guid.NewGuid();
            var pensum = new Pensum { PensumID = id };
            _mockRepo.Setup(r => r.GetPensumByIdIncludingDeletedAsync(id)).ReturnsAsync(pensum);

            var result = await _mockRepo.Object.GetPensumByIdIncludingDeletedAsync(id);

            result.Should().BeEquivalentTo(pensum);
        }

        [Fact]
        public async Task GetPensumByGradAsync_ShouldReturnPensum_WhenFound()
        {
            var grad = "Grøn";
            var pensum = new Pensum { PensumGrad = grad };
            _mockRepo.Setup(r => r.GetPensumByGradAsync(grad)).ReturnsAsync(pensum);

            var result = await _mockRepo.Object.GetPensumByGradAsync(grad);

            result.Should().BeEquivalentTo(pensum);
        }

        [Fact]
        public async Task GetPensumByGradAsync_ShouldReturnNull_WhenNotFound()
        {
            _mockRepo.Setup(r => r.GetPensumByGradAsync(It.IsAny<string>())).ReturnsAsync((Pensum?)null);

            var result = await _mockRepo.Object.GetPensumByGradAsync("NonExistent");

            result.Should().BeNull();
        }

        [Fact]
        public async Task CreatePensumAsync_ShouldReturnCreatedPensum()
        {
            var pensum = new Pensum { PensumGrad = "Blå" };
            _mockRepo.Setup(r => r.CreatePensumAsync(pensum)).ReturnsAsync(pensum);

            var result = await _mockRepo.Object.CreatePensumAsync(pensum);

            result.Should().BeEquivalentTo(pensum);
        }

        [Fact]
        public async Task UpdatePensumAsync_ShouldReturnTrue_WhenSuccessful()
        {
            var pensum = new Pensum { PensumGrad = "Lilla" };
            _mockRepo.Setup(r => r.UpdatePensumAsync(pensum)).ReturnsAsync(true);

            var result = await _mockRepo.Object.UpdatePensumAsync(pensum);

            result.Should().BeTrue();
        }

        [Fact]
        public async Task UpdatePensumAsync_ShouldReturnFalse_WhenUnsuccessful()
        {
            var pensum = new Pensum { PensumGrad = "IkkeEksisterende" };
            _mockRepo.Setup(r => r.UpdatePensumAsync(pensum)).ReturnsAsync(false);

            var result = await _mockRepo.Object.UpdatePensumAsync(pensum);

            result.Should().BeFalse();
        }

        [Fact]
        public async Task DeletePensumAsync_ShouldReturnTrue_WhenDeleted()
        {
            var id = Guid.NewGuid();
            _mockRepo.Setup(r => r.DeletePensumAsync(id)).ReturnsAsync(true);

            var result = await _mockRepo.Object.DeletePensumAsync(id);

            result.Should().BeTrue();
        }

        [Fact]
        public async Task DeletePensumAsync_ShouldReturnFalse_WhenDeleteFails()
        {
            var id = Guid.NewGuid();
            _mockRepo.Setup(r => r.DeletePensumAsync(id)).ReturnsAsync(false);

            var result = await _mockRepo.Object.DeletePensumAsync(id);

            result.Should().BeFalse();
        }

        [Fact]
        public async Task UpdatePensumIncludingDeletedAsync_ShouldReturnUpdatedPensum_WhenExists()
        {
            var id = Guid.NewGuid();
            var updated = new Pensum { PensumID = id, PensumGrad = "NyGrad" };
            _mockRepo.Setup(r => r.UpdatePensumIncludingDeletedAsync(id, updated)).ReturnsAsync(updated);

            var result = await _mockRepo.Object.UpdatePensumIncludingDeletedAsync(id, updated);

            result.Should().BeEquivalentTo(updated);
        }

        [Fact]
        public async Task UpdatePensumIncludingDeletedAsync_ShouldReturnNull_WhenNotFound()
        {
            _mockRepo.Setup(r => r.UpdatePensumIncludingDeletedAsync(It.IsAny<Guid>(), It.IsAny<Pensum>()))
                     .ReturnsAsync((Pensum?)null);

            var result = await _mockRepo.Object.UpdatePensumIncludingDeletedAsync(Guid.NewGuid(), new Pensum());

            result.Should().BeNull();
        }
        [Fact]
        public async Task CreatePensumAsync_ShouldThrowException_WhenPensumIsNull()
        {
            _mockRepo.Setup(r => r.CreatePensumAsync(null!))
                     .ThrowsAsync(new ArgumentNullException());

            Func<Task> act = async () => await _mockRepo.Object.CreatePensumAsync(null!);

            await act.Should().ThrowAsync<ArgumentNullException>();
        }

        [Fact]
        public async Task GetPensumByIdIncludingDeletedAsync_ShouldReturnNull_WhenNotFound()
        {
            _mockRepo.Setup(r => r.GetPensumByIdIncludingDeletedAsync(It.IsAny<Guid>()))
                     .ReturnsAsync((Pensum?)null);

            var result = await _mockRepo.Object.GetPensumByIdIncludingDeletedAsync(Guid.NewGuid());

            result.Should().BeNull();
        }

        [Fact]
        public async Task GetAllPensumAsync_ShouldReturnEmptyList_WhenNoPensumExists()
        {
            _mockRepo.Setup(r => r.GetAllPensumAsync()).ReturnsAsync(new List<Pensum>());

            var result = await _mockRepo.Object.GetAllPensumAsync();

            result.Should().BeEmpty();
        }

    }
}
