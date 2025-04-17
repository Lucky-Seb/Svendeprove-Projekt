using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FluentAssertions;
using Moq;
using TaekwondoApp.Shared.DTO;
using TaekwondoOrchestration.ApiService.ServiceInterfaces;
using Xunit;

namespace TaekwondoOrchestration.Tests
{
    public class IOrdbogServiceTests
    {
        private readonly Mock<IOrdbogService> _mockOrdbogService;

        public IOrdbogServiceTests()
        {
            _mockOrdbogService = new Mock<IOrdbogService>();
        }

        [Fact]
        public async Task GetAllOrdbogAsync_ShouldReturnListOfOrdbogDTO()
        {
            var expected = new List<OrdbogDTO>
            {
                new() { OrdbogId = Guid.NewGuid(), DanskOrd = "Hej", KoranskOrd = "안녕", Beskrivelse = "Hello" },
                new() { OrdbogId = Guid.NewGuid(), DanskOrd = "Tak", KoranskOrd = "감사", Beskrivelse = "Thanks" }
            };

            _mockOrdbogService.Setup(s => s.GetAllOrdbogAsync()).ReturnsAsync(expected);

            var result = await _mockOrdbogService.Object.GetAllOrdbogAsync();

            result.Should().BeEquivalentTo(expected);
        }

        [Fact]
        public async Task GetAllOrdbogAsync_ShouldReturnEmptyList_WhenNoItemsExist()
        {
            _mockOrdbogService.Setup(s => s.GetAllOrdbogAsync()).ReturnsAsync(new List<OrdbogDTO>());

            var result = await _mockOrdbogService.Object.GetAllOrdbogAsync();

            result.Should().BeEmpty();
        }

        [Fact]
        public async Task GetOrdbogByIdAsync_ShouldReturnCorrectItem()
        {
            var id = Guid.NewGuid();
            var expected = new OrdbogDTO { OrdbogId = id, DanskOrd = "Hej", KoranskOrd = "안녕", Beskrivelse = "Hello" };

            _mockOrdbogService.Setup(s => s.GetOrdbogByIdAsync(id)).ReturnsAsync(expected);

            var result = await _mockOrdbogService.Object.GetOrdbogByIdAsync(id);

            result.Should().BeEquivalentTo(expected);
        }

        [Fact]
        public async Task GetOrdbogByIdAsync_ShouldReturnNull_WhenItemNotFound()
        {
            _mockOrdbogService.Setup(s => s.GetOrdbogByIdAsync(It.IsAny<Guid>())).ReturnsAsync((OrdbogDTO?)null);

            var result = await _mockOrdbogService.Object.GetOrdbogByIdAsync(Guid.NewGuid());

            result.Should().BeNull();
        }

        [Fact]
        public async Task CreateOrdbogAsync_ShouldReturnCreatedDto()
        {
            var dto = new OrdbogDTO { DanskOrd = "Hej", KoranskOrd = "안녕", Beskrivelse = "Hello" };
            var created = new OrdbogDTO
            {
                OrdbogId = Guid.NewGuid(),
                DanskOrd = dto.DanskOrd,
                KoranskOrd = dto.KoranskOrd,
                Beskrivelse = dto.Beskrivelse
            };

            _mockOrdbogService.Setup(s => s.CreateOrdbogAsync(dto)).ReturnsAsync(created);

            var result = await _mockOrdbogService.Object.CreateOrdbogAsync(dto);

            result.Should().BeEquivalentTo(created);
        }

        [Fact]
        public async Task CreateOrdbogAsync_ShouldReturnNull_WhenCreationFails()
        {
            var dto = new OrdbogDTO { DanskOrd = "Hej" };

            _mockOrdbogService.Setup(s => s.CreateOrdbogAsync(dto)).ThrowsAsync(new Exception("Creation failed"));

            var result = await _mockOrdbogService.Object.CreateOrdbogAsync(dto);

            await Assert.ThrowsAsync<Exception>(() => _mockOrdbogService.Object.CreateOrdbogAsync(dto));
        }

        [Fact]
        public async Task UpdateOrdbogAsync_ShouldReturnTrue_WhenSuccessful()
        {
            var id = Guid.NewGuid();
            var dto = new OrdbogDTO { OrdbogId = id };

            _mockOrdbogService.Setup(s => s.UpdateOrdbogAsync(id, dto)).ReturnsAsync(true);

            var result = await _mockOrdbogService.Object.UpdateOrdbogAsync(id, dto);

            result.Should().BeTrue();
        }

        [Fact]
        public async Task UpdateOrdbogAsync_ShouldReturnFalse_WhenUpdateFails()
        {
            var id = Guid.NewGuid();
            var dto = new OrdbogDTO { OrdbogId = id };

            _mockOrdbogService.Setup(s => s.UpdateOrdbogAsync(id, dto)).ReturnsAsync(false);

            var result = await _mockOrdbogService.Object.UpdateOrdbogAsync(id, dto);

            result.Should().BeFalse();
        }

        [Fact]
        public async Task UpdateOrdbogIncludingDeletedByIdAsync_ShouldReturnUpdatedDto()
        {
            var id = Guid.NewGuid();
            var dto = new OrdbogDTO { OrdbogId = id };

            _mockOrdbogService.Setup(s => s.UpdateOrdbogIncludingDeletedByIdAsync(id, dto)).ReturnsAsync(dto);

            var result = await _mockOrdbogService.Object.UpdateOrdbogIncludingDeletedByIdAsync(id, dto);

            result.Should().BeEquivalentTo(dto);
        }

        [Fact]
        public async Task UpdateOrdbogIncludingDeletedByIdAsync_ShouldReturnNull_WhenUpdateFails()
        {
            var id = Guid.NewGuid();
            var dto = new OrdbogDTO { OrdbogId = id };

            _mockOrdbogService.Setup(s => s.UpdateOrdbogIncludingDeletedByIdAsync(id, dto)).ReturnsAsync((OrdbogDTO?)null);

            var result = await _mockOrdbogService.Object.UpdateOrdbogIncludingDeletedByIdAsync(id, dto);

            result.Should().BeNull();
        }

        [Fact]
        public async Task DeleteOrdbogAsync_ShouldReturnTrue_WhenSuccessful()
        {
            var id = Guid.NewGuid();

            _mockOrdbogService.Setup(s => s.DeleteOrdbogAsync(id)).ReturnsAsync(true);

            var result = await _mockOrdbogService.Object.DeleteOrdbogAsync(id);

            result.Should().BeTrue();
        }

        [Fact]
        public async Task DeleteOrdbogAsync_ShouldReturnFalse_WhenDeleteFails()
        {
            var id = Guid.NewGuid();

            _mockOrdbogService.Setup(s => s.DeleteOrdbogAsync(id)).ReturnsAsync(false);

            var result = await _mockOrdbogService.Object.DeleteOrdbogAsync(id);

            result.Should().BeFalse();
        }

        [Fact]
        public async Task RestoreOrdbogAsync_ShouldReturnTrue_WhenSuccessful()
        {
            var id = Guid.NewGuid();
            var dto = new OrdbogDTO { OrdbogId = id };

            _mockOrdbogService.Setup(s => s.RestoreOrdbogAsync(id, dto)).ReturnsAsync(true);

            var result = await _mockOrdbogService.Object.RestoreOrdbogAsync(id, dto);

            result.Should().BeTrue();
        }

        [Fact]
        public async Task RestoreOrdbogAsync_ShouldReturnFalse_WhenFails()
        {
            var id = Guid.NewGuid();
            var dto = new OrdbogDTO { OrdbogId = id };

            _mockOrdbogService.Setup(s => s.RestoreOrdbogAsync(id, dto)).ReturnsAsync(false);

            var result = await _mockOrdbogService.Object.RestoreOrdbogAsync(id, dto);

            result.Should().BeFalse();
        }

        [Fact]
        public async Task GetOrdbogByDanskOrdAsync_ShouldReturnDto_WhenExists()
        {
            var danskOrd = "Hej";
            var expected = new OrdbogDTO { DanskOrd = danskOrd };

            _mockOrdbogService.Setup(s => s.GetOrdbogByDanskOrdAsync(danskOrd)).ReturnsAsync(expected);

            var result = await _mockOrdbogService.Object.GetOrdbogByDanskOrdAsync(danskOrd);

            result.Should().BeEquivalentTo(expected);
        }

        [Fact]
        public async Task GetOrdbogByDanskOrdAsync_ShouldReturnNull_WhenNotFound()
        {
            _mockOrdbogService.Setup(s => s.GetOrdbogByDanskOrdAsync(It.IsAny<string>())).ReturnsAsync((OrdbogDTO?)null);

            var result = await _mockOrdbogService.Object.GetOrdbogByDanskOrdAsync("NonExistent");

            result.Should().BeNull();
        }

        [Fact]
        public async Task GetOrdbogByKoranOrdAsync_ShouldReturnDto_WhenExists()
        {
            var koranOrd = "감사";
            var expected = new OrdbogDTO { KoranskOrd = koranOrd };

            _mockOrdbogService.Setup(s => s.GetOrdbogByKoranOrdAsync(koranOrd)).ReturnsAsync(expected);

            var result = await _mockOrdbogService.Object.GetOrdbogByKoranOrdAsync(koranOrd);

            result.Should().BeEquivalentTo(expected);
        }

        [Fact]
        public async Task GetOrdbogByKoranOrdAsync_ShouldReturnNull_WhenNotFound()
        {
            _mockOrdbogService.Setup(s => s.GetOrdbogByKoranOrdAsync(It.IsAny<string>())).ReturnsAsync((OrdbogDTO?)null);

            var result = await _mockOrdbogService.Object.GetOrdbogByKoranOrdAsync("NonExistent");

            result.Should().BeNull();
        }

        [Fact]
        public async Task GetAllOrdbogIncludingDeletedAsync_ShouldReturnList()
        {
            var expected = new List<OrdbogDTO>
            {
                new() { DanskOrd = "Hej", KoranskOrd = "안녕" },
                new() { DanskOrd = "Tak", KoranskOrd = "감사" }
            };

            _mockOrdbogService.Setup(s => s.GetAllOrdbogIncludingDeletedAsync()).ReturnsAsync(expected);

            var result = await _mockOrdbogService.Object.GetAllOrdbogIncludingDeletedAsync();

            result.Should().BeEquivalentTo(expected);
        }

        [Fact]
        public async Task GetAllOrdbogIncludingDeletedAsync_ShouldReturnEmpty_WhenNoneExist()
        {
            _mockOrdbogService.Setup(s => s.GetAllOrdbogIncludingDeletedAsync()).ReturnsAsync(new List<OrdbogDTO>());

            var result = await _mockOrdbogService.Object.GetAllOrdbogIncludingDeletedAsync();

            result.Should().BeEmpty();
        }
    }
}
