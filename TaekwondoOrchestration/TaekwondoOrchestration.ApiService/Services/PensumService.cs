using TaekwondoApp.Shared.DTO;
using TaekwondoApp.Shared.Models;
using TaekwondoOrchestration.ApiService.Repositories;
using TaekwondoOrchestration.ApiService.RepositorieInterfaces;
using AutoMapper;
using TaekwondoOrchestration.ApiService.ServiceInterfaces;
using TaekwondoOrchestration.ApiService.Helpers;

namespace TaekwondoOrchestration.ApiService.Services
{
    public class PensumService : IPensumService
    {
        private readonly IPensumRepository _pensumRepository;
        private readonly IMapper _mapper;

        public PensumService(IPensumRepository pensumRepository, IMapper mapper)
        {
            _pensumRepository = pensumRepository;
            _mapper = mapper;
        }

        #region CRUD Operations

        // Get All Pensum
        public async Task<Result<IEnumerable<PensumDTO>>> GetAllPensumAsync()
        {
            var pensumList = await _pensumRepository.GetAllPensumAsync();
            var mapped = _mapper.Map<IEnumerable<PensumDTO>>(pensumList);
            return Result<IEnumerable<PensumDTO>>.Ok(mapped);
        }

        // Get Pensum by ID
        public async Task<Result<PensumDTO>> GetPensumByIdAsync(Guid id)
        {
            var pensum = await _pensumRepository.GetPensumByIdAsync(id);
            if (pensum == null)
                return Result<PensumDTO>.Fail("Pensum not found.");

            var mapped = _mapper.Map<PensumDTO>(pensum);
            return Result<PensumDTO>.Ok(mapped);
        }

        // Create New Pensum
        public async Task<Result<PensumDTO>> CreatePensumAsync(PensumDTO pensumDto)
        {
            if (string.IsNullOrEmpty(pensumDto.PensumGrad))
            {
                return Result<PensumDTO>.Fail("PensumGrad is required.");
            }

            var newPensum = _mapper.Map<Pensum>(pensumDto);
            EntityHelper.InitializeEntity(newPensum, pensumDto.ModifiedBy, "Created new Pensum.");
            var createdPensum = await _pensumRepository.CreatePensumAsync(newPensum);

            var mapped = _mapper.Map<PensumDTO>(createdPensum);
            return Result<PensumDTO>.Ok(mapped);
        }

        // Update Existing Pensum
        public async Task<Result<bool>> UpdatePensumAsync(Guid id, PensumDTO pensumDto)
        {
            if (string.IsNullOrEmpty(pensumDto.PensumGrad))
            {
                return Result<bool>.Fail("PensumGrad is required.");
            }

            var existingPensum = await _pensumRepository.GetPensumByIdAsync(id);
            if (existingPensum == null)
                return Result<bool>.Fail("Pensum not found.");

            _mapper.Map(pensumDto, existingPensum);
            EntityHelper.UpdateCommonFields(existingPensum, pensumDto.ModifiedBy);
            var updateSuccess = await _pensumRepository.UpdatePensumAsync(existingPensum);

            return updateSuccess ? Result<bool>.Ok(true) : Result<bool>.Fail("Failed to update Pensum.");
        }

        // Soft Delete Pensum
        public async Task<Result<bool>> DeletePensumAsync(Guid id)
        {
            var pensum = await _pensumRepository.GetPensumByIdAsync(id);
            if (pensum == null)
                return Result<bool>.Fail("Pensum not found.");

            // Soft delete
            string modifiedBy = pensum.ModifiedBy; // Assuming we pass user context here
            EntityHelper.SetDeletedOrRestoredProperties(pensum, "Soft-deleted Pensum entry", modifiedBy);

            var success = await _pensumRepository.UpdatePensumAsync(pensum);
            return success ? Result<bool>.Ok(true) : Result<bool>.Fail("Failed to delete Pensum.");
        }

        // Restore Pensum from Soft-Delete
        public async Task<Result<bool>> RestorePensumAsync(Guid id, PensumDTO dto)
        {
            var pensum = await _pensumRepository.GetPensumByIdIncludingDeletedAsync(id);
            if (pensum == null || !pensum.IsDeleted)
                return Result<bool>.Fail("Pensum not found or not deleted.");

            pensum.IsDeleted = false;
            pensum.Status = SyncStatus.Synced;
            pensum.ModifiedBy = dto.ModifiedBy;
            pensum.LastSyncedVersion++;

            // Set properties for restored entry
            EntityHelper.SetDeletedOrRestoredProperties(pensum, "Restored Pensum entry", dto.ModifiedBy);

            var success = await _pensumRepository.UpdatePensumAsync(pensum);
            return success ? Result<bool>.Ok(true) : Result<bool>.Fail("Failed to restore Pensum.");
        }

        #endregion

        #region Search Operations

        // Get Pensum by Grad (Difficulty)
        public async Task<Result<IEnumerable<PensumDTO>>> GetPensumByGradAsync(string grad)
        {
            var pensumList = await _pensumRepository.GetPensumByGradAsync(grad);
            var mapped = _mapper.Map<IEnumerable<PensumDTO>>(pensumList);
            return Result<IEnumerable<PensumDTO>>.Ok(mapped);
        }

        #endregion
    }
}
