using Moq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TaekwondoApp.Shared.DTO;
using TaekwondoApp.Shared.Models;
using TaekwondoOrchestration.ApiService.Repositories;
using TaekwondoOrchestration.ApiService.ServiceInterfaces;
using TaekwondoOrchestration.ApiService.Services;
using TaekwondoOrchestration.ApiService.Helpers;
using FluentAssertions;
using Xunit;
using AutoMapper;
using TaekwondoOrchestration.ApiService.RepositorieInterfaces;

namespace TaekwondoOrchestration.ApiService.PensumTests
{
    public class PensumServiceTests
    {
        private readonly Mock<IPensumRepository> _mockRepo;
        private readonly Mock<IMapper> _mockMapper;
        private readonly IPensumService _service;

        public PensumServiceTests()
        {
            _mockRepo = new Mock<IPensumRepository>();
            _mockMapper = new Mock<IMapper>();
            _service = new PensumService(_mockRepo.Object, _mockMapper.Object);
        }

        #region GetAllPensumAsync Tests

        [Fact]
        public async Task GetAllPensumAsync_ShouldReturnMappedResult_WhenPensumExists()
        {
            var pensums = new List<Pensum> { new Pensum() };
            var pensumDtos = new List<PensumDTO> { new PensumDTO() };

            _mockRepo.Setup(r => r.GetAllPensumAsync()).ReturnsAsync(pensums);
            _mockMapper.Setup(m => m.Map<IEnumerable<PensumDTO>>(pensums)).Returns(pensumDtos);

            var result = await _service.GetAllPensumAsync();

            result.Success.Should().BeTrue();
            result.Value.Should().BeEquivalentTo(pensumDtos);
        }

        [Fact]
        public async Task GetAllPensumAsync_ShouldReturnEmptyList_WhenNoPensumExists()
        {
            _mockRepo.Setup(r => r.GetAllPensumAsync()).ReturnsAsync(new List<Pensum>());
            _mockMapper.Setup(m => m.Map<IEnumerable<PensumDTO>>(It.IsAny<IEnumerable<Pensum>>()))
                       .Returns(new List<PensumDTO>());

            var result = await _service.GetAllPensumAsync();

            result.Success.Should().BeTrue();
            result.Value.Should().BeEmpty();
        }

        #endregion

        #region GetPensumByIdAsync Tests

        [Fact]
        public async Task GetPensumByIdAsync_ShouldReturnMappedPensum_WhenFound()
        {
            var pensum = new Pensum();
            var dto = new PensumDTO();

            _mockRepo.Setup(r => r.GetPensumByIdAsync(It.IsAny<Guid>())).ReturnsAsync(pensum);
            _mockMapper.Setup(m => m.Map<PensumDTO>(pensum)).Returns(dto);

            var result = await _service.GetPensumByIdAsync(Guid.NewGuid());

            result.Success.Should().BeTrue();
            result.Value.Should().Be(dto);
        }

        [Fact]
        public async Task GetPensumByIdAsync_ShouldReturnFailure_WhenNotFound()
        {
            _mockRepo.Setup(r => r.GetPensumByIdAsync(It.IsAny<Guid>())).ReturnsAsync((Pensum?)null);

            var result = await _service.GetPensumByIdAsync(Guid.NewGuid());

            result.Success.Should().BeFalse();
            result.Errors.Should().AllBe("Pensum not found.");
        }

        #endregion

        #region CreatePensumAsync Tests

        [Fact]
        public async Task CreatePensumAsync_ShouldCreateAndReturnPensum_WhenValid()
        {
            var dto = new PensumDTO { PensumGrad = "Black", ModifiedBy = "admin" };
            var pensum = new Pensum();
            var createdPensum = new Pensum();
            var mappedDto = new PensumDTO();

            _mockMapper.Setup(m => m.Map<Pensum>(dto)).Returns(pensum);
            _mockRepo.Setup(r => r.CreatePensumAsync(pensum)).ReturnsAsync(createdPensum);
            _mockMapper.Setup(m => m.Map<PensumDTO>(createdPensum)).Returns(mappedDto);

            var result = await _service.CreatePensumAsync(dto);

            result.Success.Should().BeTrue();
            result.Value.Should().Be(mappedDto);
        }

        [Fact]
        public async Task CreatePensumAsync_ShouldFail_WhenPensumGradIsMissing()
        {
            var dto = new PensumDTO { PensumGrad = null };

            var result = await _service.CreatePensumAsync(dto);

            result.Success.Should().BeFalse();
            result.Errors.Should().AllBe("PensumGrad is required.");
        }

        #endregion

        #region UpdatePensumAsync Tests

        [Fact]
        public async Task UpdatePensumAsync_ShouldUpdate_WhenDataIsValid()
        {
            var id = Guid.NewGuid();
            var dto = new PensumDTO { PensumGrad = "Black", ModifiedBy = "user" };
            var pensum = new Pensum();

            _mockRepo.Setup(r => r.GetPensumByIdAsync(id)).ReturnsAsync(pensum);
            _mockRepo.Setup(r => r.UpdatePensumAsync(pensum)).ReturnsAsync(true);

            var result = await _service.UpdatePensumAsync(id, dto);

            result.Success.Should().BeTrue();
        }

        [Fact]
        public async Task UpdatePensumAsync_ShouldFail_WhenPensumNotFound()
        {
            _mockRepo.Setup(r => r.GetPensumByIdAsync(It.IsAny<Guid>())).ReturnsAsync((Pensum?)null);

            var result = await _service.UpdatePensumAsync(Guid.NewGuid(), new PensumDTO { PensumGrad = "Red" });

            result.Success.Should().BeFalse();
            result.Errors.Should().AllBe("Pensum not found.");
        }

        #endregion

        #region DeletePensumAsync Tests

        [Fact]
        public async Task DeletePensumAsync_ShouldSoftDelete_WhenPensumExists()
        {
            var pensum = new Pensum { ModifiedBy = "admin" };

            _mockRepo.Setup(r => r.GetPensumByIdAsync(It.IsAny<Guid>())).ReturnsAsync(pensum);
            _mockRepo.Setup(r => r.UpdatePensumAsync(It.IsAny<Pensum>())).ReturnsAsync(true);

            var result = await _service.DeletePensumAsync(Guid.NewGuid());

            result.Success.Should().BeTrue();
        }

        [Fact]
        public async Task DeletePensumAsync_ShouldFail_WhenPensumNotFound()
        {
            _mockRepo.Setup(r => r.GetPensumByIdAsync(It.IsAny<Guid>())).ReturnsAsync((Pensum?)null);

            var result = await _service.DeletePensumAsync(Guid.NewGuid());

            result.Success.Should().BeFalse();
            result.Errors.Should().AllBe("Pensum not found.");
        }

        #endregion

        #region RestorePensumAsync Tests

        [Fact]
        public async Task RestorePensumAsync_ShouldRestore_WhenValid()
        {
            var pensum = new Pensum { IsDeleted = true, LastSyncedVersion = 1 };
            var dto = new PensumDTO { ModifiedBy = "admin" };

            _mockRepo.Setup(r => r.GetPensumByIdIncludingDeletedAsync(It.IsAny<Guid>())).ReturnsAsync(pensum);
            _mockRepo.Setup(r => r.UpdatePensumAsync(pensum)).ReturnsAsync(true);

            var result = await _service.RestorePensumAsync(Guid.NewGuid(), dto);

            result.Success.Should().BeTrue();
        }

        [Fact]
        public async Task RestorePensumAsync_ShouldFail_WhenPensumNotFoundOrNotDeleted()
        {
            var pensum = new Pensum { IsDeleted = false };
            var dto = new PensumDTO { ModifiedBy = "admin" };

            _mockRepo.Setup(r => r.GetPensumByIdIncludingDeletedAsync(It.IsAny<Guid>())).ReturnsAsync(pensum);

            var result = await _service.RestorePensumAsync(Guid.NewGuid(), dto);

            result.Success.Should().BeFalse();
            result.Errors.Should().AllBe("Pensum not found or not deleted.");
        }

        #endregion
    }
}
