using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FluentAssertions;
using Moq;
using TaekwondoApp.Shared.DTO;
using TaekwondoOrchestration.ApiService.Helpers;
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

            // Ensure that you're returning IEnumerable<OrdbogDTO>, which can be List<OrdbogDTO> since List implements IEnumerable
            _mockOrdbogService.Setup(s => s.GetAllOrdbogAsync()).ReturnsAsync(Result<IEnumerable<OrdbogDTO>>.Ok(expected));

            var result = await _mockOrdbogService.Object.GetAllOrdbogAsync();

            // Adjust the assertion to check for the Result type and the Value being equivalent to the expected
            result.Should().BeEquivalentTo(Result<IEnumerable<OrdbogDTO>>.Ok(expected));
        }


        [Fact]
        public async Task GetAllOrdbogAsync_ShouldReturnEmptyList_WhenNoItemsExist()
        {
            // Returning IEnumerable<OrdbogDTO> (can be an empty List<OrdbogDTO>)
            _mockOrdbogService.Setup(s => s.GetAllOrdbogAsync()).ReturnsAsync(Result<IEnumerable<OrdbogDTO>>.Ok(new List<OrdbogDTO>()));

            var result = await _mockOrdbogService.Object.GetAllOrdbogAsync();

            // Assert that the result value is an empty list
            result.Value.Should().BeEmpty();
        }


        [Fact]
        public async Task GetOrdbogByIdAsync_ShouldReturnCorrectItem()
        {
            var id = Guid.NewGuid();
            var expected = new OrdbogDTO { OrdbogId = id, DanskOrd = "Hej", KoranskOrd = "안녕", Beskrivelse = "Hello" };

            _mockOrdbogService.Setup(s => s.GetOrdbogByIdAsync(id)).ReturnsAsync(Result<OrdbogDTO>.Ok(expected));

            var result = await _mockOrdbogService.Object.GetOrdbogByIdAsync(id);

            result.Value.Should().BeEquivalentTo(expected);
        }

        [Fact]
        public async Task GetOrdbogByIdAsync_ShouldReturnNotFound_WhenItemNotFound()
        {
            // Mocking the service to return a failure result with an error message
            _mockOrdbogService.Setup(s => s.GetOrdbogByIdAsync(It.IsAny<Guid>()))
                                 .ReturnsAsync(Result<OrdbogDTO>.Fail("Ordbog not found"));

            var result = await _mockOrdbogService.Object.GetOrdbogByIdAsync(Guid.NewGuid());

            // Assert that the operation failed (check if failure)
            result.Failure.Should().BeTrue();

            // Assert that the error exists and matches
            result.Errors.Should().Contain("Ordbog not found");
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

            _mockOrdbogService.Setup(s => s.CreateOrdbogAsync(dto)).ReturnsAsync(Result<OrdbogDTO>.Ok(created));

            var result = await _mockOrdbogService.Object.CreateOrdbogAsync(dto);

            result.Value.Should().BeEquivalentTo(created);
        }

        [Fact]
        public async Task CreateOrdbogAsync_ShouldReturnError_WhenCreationFails()
        {
            var dto = new OrdbogDTO { DanskOrd = "Hej" };

            // Mocking the service to return a failure result with an error message
            _mockOrdbogService.Setup(s => s.CreateOrdbogAsync(dto))
                                 .ReturnsAsync(Result<OrdbogDTO>.Fail("Creation failed"));

            var result = await _mockOrdbogService.Object.CreateOrdbogAsync(dto);

            // Assert that the operation failed
            result.Failure.Should().BeTrue();

            // Assert that the error exists and matches
            result.Errors.Should().Contain("Creation failed");
        }


        [Fact]
        public async Task UpdateOrdbogAsync_ShouldReturnTrue_WhenSuccessful()
        {
            var id = Guid.NewGuid();
            var dto = new OrdbogDTO { OrdbogId = id };

            _mockOrdbogService.Setup(s => s.UpdateOrdbogAsync(id, dto)).ReturnsAsync(Result<bool>.Ok(true));

            var result = await _mockOrdbogService.Object.UpdateOrdbogAsync(id, dto);

            result.Value.Should().BeTrue();
        }

        [Fact]
        public async Task UpdateOrdbogAsync_ShouldReturnFalse_WhenUpdateFails()
        {
            var id = Guid.NewGuid();
            var dto = new OrdbogDTO { OrdbogId = id };

            _mockOrdbogService.Setup(s => s.UpdateOrdbogAsync(id, dto)).ReturnsAsync(Result<bool>.Ok(false));

            var result = await _mockOrdbogService.Object.UpdateOrdbogAsync(id, dto);

            result.Value.Should().BeFalse();
        }

        [Fact]
        public async Task UpdateOrdbogIncludingDeletedByIdAsync_ShouldReturnUpdatedDto()
        {
            var id = Guid.NewGuid();
            var dto = new OrdbogDTO { OrdbogId = id };

            _mockOrdbogService.Setup(s => s.UpdateOrdbogIncludingDeletedByIdAsync(id, dto)).ReturnsAsync(Result<OrdbogDTO>.Ok(dto));

            var result = await _mockOrdbogService.Object.UpdateOrdbogIncludingDeletedByIdAsync(id, dto);

            result.Value.Should().BeEquivalentTo(dto);
        }

        [Fact]
        public async Task UpdateOrdbogIncludingDeletedByIdAsync_ShouldReturnError_WhenUpdateFails()
        {
            var id = Guid.NewGuid();
            var dto = new OrdbogDTO { OrdbogId = id };

            // Mocking the service to return a failure result with an error message
            _mockOrdbogService.Setup(s => s.UpdateOrdbogIncludingDeletedByIdAsync(id, dto))
                                 .ReturnsAsync(Result<OrdbogDTO>.Fail("Update failed"));

            var result = await _mockOrdbogService.Object.UpdateOrdbogIncludingDeletedByIdAsync(id, dto);

            // Assert that the operation failed
            result.Failure.Should().BeTrue();

            // Assert that the error exists and matches
            result.Errors.Should().Contain("Update failed");
        }

        [Fact]
        public async Task DeleteOrdbogAsync_ShouldReturnTrue_WhenSuccessful()
        {
            var id = Guid.NewGuid();

            _mockOrdbogService.Setup(s => s.DeleteOrdbogAsync(id)).ReturnsAsync(Result<bool>.Ok(true));

            var result = await _mockOrdbogService.Object.DeleteOrdbogAsync(id);

            result.Value.Should().BeTrue();
        }

        [Fact]
        public async Task DeleteOrdbogAsync_ShouldReturnFalse_WhenDeleteFails()
        {
            var id = Guid.NewGuid();

            _mockOrdbogService.Setup(s => s.DeleteOrdbogAsync(id)).ReturnsAsync(Result<bool>.Ok(false));

            var result = await _mockOrdbogService.Object.DeleteOrdbogAsync(id);

            result.Value.Should().BeFalse();
        }

        [Fact]
        public async Task RestoreOrdbogAsync_ShouldReturnTrue_WhenSuccessful()
        {
            var id = Guid.NewGuid();
            var dto = new OrdbogDTO { OrdbogId = id };

            _mockOrdbogService.Setup(s => s.RestoreOrdbogAsync(id, dto)).ReturnsAsync(Result<bool>.Ok(true));

            var result = await _mockOrdbogService.Object.RestoreOrdbogAsync(id, dto);

            result.Value.Should().BeTrue();
        }

        [Fact]
        public async Task RestoreOrdbogAsync_ShouldReturnFalse_WhenFails()
        {
            var id = Guid.NewGuid();
            var dto = new OrdbogDTO { OrdbogId = id };

            _mockOrdbogService.Setup(s => s.RestoreOrdbogAsync(id, dto)).ReturnsAsync(Result<bool>.Ok(false));

            var result = await _mockOrdbogService.Object.RestoreOrdbogAsync(id, dto);

            result.Value.Should().BeFalse();
        }
        [Fact]
        public async Task GetOrdbogByDanskOrdAsync_ShouldReturnDto_WhenExists()
        {
            // Arrange
            var danskOrd = "Hej";
            var expected = new OrdbogDTO { DanskOrd = danskOrd, KoranskOrd = "안녕", Beskrivelse = "Hello" };

            _mockOrdbogService.Setup(s => s.GetOrdbogByDanskOrdAsync(danskOrd))
                              .ReturnsAsync(Result<OrdbogDTO>.Ok(expected));

            // Act
            var result = await _mockOrdbogService.Object.GetOrdbogByDanskOrdAsync(danskOrd);

            // Assert
            result.Success.Should().BeTrue();
            result.Value.Should().BeEquivalentTo(expected);
        }
        [Fact]
        public async Task GetOrdbogByDanskOrdAsync_ShouldReturnNotFound_WhenNotExists()
        {
            // Arrange
            var danskOrd = "NonExistent";
            _mockOrdbogService.Setup(s => s.GetOrdbogByDanskOrdAsync(danskOrd))
                              .ReturnsAsync(Result<OrdbogDTO>.Fail("Ordbog not found"));

            // Act
            var result = await _mockOrdbogService.Object.GetOrdbogByDanskOrdAsync(danskOrd);

            // Assert
            result.Failure.Should().BeTrue();
            result.Errors.Should().Contain("Ordbog not found");
        }
        [Fact]
        public async Task GetOrdbogByKoranOrdAsync_ShouldReturnDto_WhenExists()
        {
            // Arrange
            var koranOrd = "감사";
            var expected = new OrdbogDTO { KoranskOrd = koranOrd, DanskOrd = "Tak", Beskrivelse = "Thanks" };

            _mockOrdbogService.Setup(s => s.GetOrdbogByKoranOrdAsync(koranOrd))
                              .ReturnsAsync(Result<OrdbogDTO>.Ok(expected));

            // Act
            var result = await _mockOrdbogService.Object.GetOrdbogByKoranOrdAsync(koranOrd);

            // Assert
            result.Success.Should().BeTrue();
            result.Value.Should().BeEquivalentTo(expected);
        }
        [Fact]
        public async Task GetOrdbogByKoranOrdAsync_ShouldReturnNotFound_WhenNotExists()
        {
            // Arrange
            var koranOrd = "NonExistent";
            _mockOrdbogService.Setup(s => s.GetOrdbogByKoranOrdAsync(koranOrd))
                              .ReturnsAsync(Result<OrdbogDTO>.Fail("Ordbog not found"));

            // Act
            var result = await _mockOrdbogService.Object.GetOrdbogByKoranOrdAsync(koranOrd);

            // Assert
            result.Failure.Should().BeTrue();
            result.Errors.Should().Contain("Ordbog not found");
        }
        [Fact]
        public async Task GetAllOrdbogIncludingDeletedAsync_ShouldReturnList_WhenItemsExist()
        {
            // Arrange
            var expected = new List<OrdbogDTO>
            {
                new OrdbogDTO { DanskOrd = "Hej", KoranskOrd = "안녕", Beskrivelse = "Hello" },
                new OrdbogDTO { DanskOrd = "Tak", KoranskOrd = "감사", Beskrivelse = "Thanks" }
            };

            _mockOrdbogService.Setup(s => s.GetAllOrdbogIncludingDeletedAsync())
                              .ReturnsAsync(Result<IEnumerable<OrdbogDTO>>.Ok(expected));

            // Act
            var result = await _mockOrdbogService.Object.GetAllOrdbogIncludingDeletedAsync();

            // Assert
            result.Success.Should().BeTrue();
            result.Value.Should().BeEquivalentTo(expected);
        }
        [Fact]
        public async Task GetAllOrdbogIncludingDeletedAsync_ShouldReturnEmpty_WhenNoItemsExist()
        {
            // Arrange
            _mockOrdbogService.Setup(s => s.GetAllOrdbogIncludingDeletedAsync())
                              .ReturnsAsync(Result<IEnumerable<OrdbogDTO>>.Ok(new List<OrdbogDTO>()));

            // Act
            var result = await _mockOrdbogService.Object.GetAllOrdbogIncludingDeletedAsync();

            // Assert
            result.Success.Should().BeTrue();
            result.Value.Should().BeEmpty();
        }

    }
}
