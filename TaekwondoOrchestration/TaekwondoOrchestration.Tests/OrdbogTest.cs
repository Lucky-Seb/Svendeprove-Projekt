using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FluentAssertions;
using Moq;
using TaekwondoApp.Shared.DTO;
using TaekwondoOrchestration.ApiService.ServiceInterfaces;
using Xunit;

namespace TaekwondoApp.Tests.Services
{
    public class OrdbogServiceTests
    {
        private readonly Mock<IOrdbogService> _ordbogServiceMock;

        public OrdbogServiceTests()
        {
            _ordbogServiceMock = new Mock<IOrdbogService>();
        }

        [Fact]
        public async Task GetAllOrdbogAsync_ShouldReturnList()
        {
            // Arrange
            var expected = new List<OrdbogDTO>
            {
                new() { Id = Guid.NewGuid(), DanskOrd = "Hej", KoranOrd = "안녕하세요" },
                new() { Id = Guid.NewGuid(), DanskOrd = "Farvel", KoranOrd = "안녕히 가세요" }
            };

            _ordbogServiceMock.Setup(s => s.GetAllOrdbogAsync()).ReturnsAsync(expected);

            // Act
            var result = await _ordbogServiceMock.Object.GetAllOrdbogAsync();

            // Assert
            result.Should().BeEquivalentTo(expected);
            result.Should().HaveCount(2);
        }

        [Fact]
        public async Task GetOrdbogByIdAsync_ShouldReturnCorrectItem()
        {
            // Arrange
            var id = Guid.NewGuid();
            var expected = new OrdbogDTO { Id = id, DanskOrd = "Hej", KoranOrd = "안녕하세요" };

            _ordbogServiceMock.Setup(s => s.GetOrdbogByIdAsync(id)).ReturnsAsync(expected);

            // Act
            var result = await _ordbogServiceMock.Object.GetOrdbogByIdAsync(id);

            // Assert
            result.Should().NotBeNull();
            result!.Id.Should().Be(id);
        }

        [Fact]
        public async Task CreateOrdbogAsync_ShouldReturnCreatedItem()
        {
            // Arrange
            var dto = new OrdbogDTO { DanskOrd = "Ven", KoranOrd = "친구" };
            var expected = new OrdbogDTO { Id = Guid.NewGuid(), DanskOrd = dto.DanskOrd, KoranOrd = dto.KoranOrd };

            _ordbogServiceMock.Setup(s => s.CreateOrdbogAsync(dto)).ReturnsAsync(expected);

            // Act
            var result = await _ordbogServiceMock.Object.CreateOrdbogAsync(dto);

            // Assert
            result.Should().NotBeNull();
            result.DanskOrd.Should().Be(dto.DanskOrd);
            result.KoranOrd.Should().Be(dto.KoranOrd);
        }
    }
}
