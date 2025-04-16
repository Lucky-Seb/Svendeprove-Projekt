using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FluentAssertions;
using Moq;
using TaekwondoApp.Shared.DTO;
using TaekwondoOrchestration.ApiService.ServiceInterfaces;
using Xunit;
using AutoMapper;

namespace TaekwondoOrchestration.Tests
{
    public class OrdbogServiceTests
    {
        private readonly Mock<IOrdbogService> _mockOrdbogService;
        private readonly IMapper _mapper;

        public OrdbogServiceTests()
        {
            _mockOrdbogService = new Mock<IOrdbogService>();
        }

        [Fact]
        public async Task GetAllOrdbogAsync_ShouldReturnListOfOrdbogDTO()
        {
            // Arrange
            var expected = new List<OrdbogDTO>
            {
                new OrdbogDTO { OrdbogId = Guid.NewGuid(), DanskOrd = "Hej", KoranskOrd = "안녕", Beskrivelse = "Hello" },
                new OrdbogDTO { OrdbogId = Guid.NewGuid(), DanskOrd = "Tak", KoranskOrd = "감사", Beskrivelse = "Thank you" }
            };

            _mockOrdbogService.Setup(s => s.GetAllOrdbogAsync()).ReturnsAsync(expected);

            // Act
            var result = await _mockOrdbogService.Object.GetAllOrdbogAsync();

            // Assert
            result.Should().BeOfType<List<OrdbogDTO>>();
            result.Should().HaveCount(2);
            result.Should().BeEquivalentTo(expected);
        }

        [Fact]
        public async Task GetOrdbogByIdAsync_ShouldReturnCorrectItem()
        {
            // Arrange
            var id = Guid.NewGuid();
            var expected = new OrdbogDTO { OrdbogId = id, DanskOrd = "Hej", KoranskOrd = "안녕", Beskrivelse = "Hello" };

            _mockOrdbogService.Setup(s => s.GetOrdbogByIdAsync(id)).ReturnsAsync(expected);

            // Act
            var result = await _mockOrdbogService.Object.GetOrdbogByIdAsync(id);

            // Assert
            result.Should().NotBeNull();
            result?.OrdbogId.Should().Be(id);
            result?.DanskOrd.Should().Be("Hej");
        }

        [Fact]
        public async Task CreateOrdbogAsync_ShouldReturnCreatedDto()
        {
            // Arrange
            var dto = new OrdbogDTO { DanskOrd = "Hej", KoranskOrd = "안녕", Beskrivelse = "Hello" };
            var created = _mapper.Map<OrdbogDTO>(dto);
            created.OrdbogId = Guid.NewGuid();
            _mockOrdbogService.Setup(s => s.CreateOrdbogAsync(dto)).ReturnsAsync(created);

            // Act
            var result = await _mockOrdbogService.Object.CreateOrdbogAsync(dto);

            // Assert
            result.Should().NotBeNull();
            result.OrdbogId.Should().NotBeEmpty();
            result.DanskOrd.Should().Be(dto.DanskOrd);
        }

        [Fact]
        public async Task DeleteOrdbogAsync_ShouldReturnTrue_WhenItemExists()
        {
            // Arrange
            var id = Guid.NewGuid();
            _mockOrdbogService.Setup(s => s.DeleteOrdbogAsync(id)).ReturnsAsync(true);

            // Act
            var result = await _mockOrdbogService.Object.DeleteOrdbogAsync(id);

            // Assert
            result.Should().BeTrue();
        }

        [Fact]
        public async Task UpdateOrdbogAsync_ShouldReturnTrue_WhenUpdateIsSuccessful()
        {
            // Arrange
            var id = Guid.NewGuid();
            var updatedDto = new OrdbogDTO
            {
                OrdbogId = id,
                DanskOrd = "Opdateret",
                KoranskOrd = "수정됨",
                Beskrivelse = "Updated description"
            };

            _mockOrdbogService.Setup(s => s.UpdateOrdbogAsync(id, updatedDto)).ReturnsAsync(true);

            // Act
            var result = await _mockOrdbogService.Object.UpdateOrdbogAsync(id, updatedDto);

            // Assert
            result.Should().BeTrue();
        }
    }
}
