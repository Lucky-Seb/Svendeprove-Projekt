using TaekwondoApp.Shared.DTO;
using TaekwondoApp.Shared.Models;
using TaekwondoOrchestration.ApiService.Repositories;
using TaekwondoOrchestration.ApiService.RepositorieInterfaces;
using AutoMapper;
using TaekwondoOrchestration.ApiService.ServiceInterfaces;
using TaekwondoOrchestration.ApiService.Helpers;

namespace TaekwondoOrchestration.ApiService.Services
{
    public class TræningService : ITræningService
    {
        private readonly ITræningRepository _træningRepository;
        private readonly IMapper _mapper;

        public TræningService(ITræningRepository træningRepository, IMapper mapper)
        {
            _træningRepository = træningRepository;
            _mapper = mapper;
        }

        #region CRUD Operations

        // Get All Træninger
        public async Task<Result<IEnumerable<TræningDTO>>> GetAllTræningAsync()
        {
            var træningList = await _træningRepository.GetAllAsync();
            if (træningList == null || !træningList.Any())
                return Result<IEnumerable<TræningDTO>>.Fail("No træning records found.");

            var mapped = _mapper.Map<IEnumerable<TræningDTO>>(træningList);
            return Result<IEnumerable<TræningDTO>>.Ok(mapped);
        }

        // Get Træning by ID
        public async Task<Result<TræningDTO>> GetTræningByIdAsync(Guid id)
        {
            var træning = await _træningRepository.GetByIdAsync(id);
            if (træning == null)
                return Result<TræningDTO>.Fail("Træning not found.");

            var mapped = _mapper.Map<TræningDTO>(træning);
            return Result<TræningDTO>.Ok(mapped);
        }

        // Create New Træning
        public async Task<Result<TræningDTO>> CreateTræningAsync(TræningDTO træningDto)
        {
            if (træningDto == null)
                return Result<TræningDTO>.Fail("Invalid træning data.");

            var newTræning = _mapper.Map<Træning>(træningDto);
            EntityHelper.InitializeEntity(newTræning, træningDto.ModifiedBy, "Created new Træning.");

            var createdTræning = await _træningRepository.CreateAsync(newTræning);
            var mapped = _mapper.Map<TræningDTO>(createdTræning);

            return Result<TræningDTO>.Ok(mapped);
        }

        // Update Existing Træning
        public async Task<Result<TræningDTO>> UpdateTræningAsync(Guid id, TræningDTO træningDto)
        {
            if (træningDto == null || id != træningDto.TræningID)
                return Result<TræningDTO>.Fail("Invalid træning data.");

            var existingTræning = await _træningRepository.GetByIdAsync(id);
            if (existingTræning == null)
                return Result<TræningDTO>.Fail("Træning not found.");

            _mapper.Map(træningDto, existingTræning);
            EntityHelper.UpdateCommonFields(existingTræning, træningDto.ModifiedBy);

            var updateSuccess = await _træningRepository.UpdateAsync(existingTræning);
            return updateSuccess ? Result<TræningDTO>.Ok(_mapper.Map<TræningDTO>(existingTræning)) : Result<TræningDTO>.Fail("Failed to update træning.");
        }

        // Soft Delete Træning
        public async Task<Result<bool>> DeleteTræningAsync(Guid id)
        {
            var træning = await _træningRepository.GetByIdAsync(id);
            if (træning == null)
                return Result<bool>.Fail("Træning not found.");

            // Soft delete logic
            string modifiedBy = træning.ModifiedBy; // Assuming user context
            EntityHelper.SetDeletedOrRestoredProperties(træning, "Soft-deleted træning", modifiedBy);

            var success = await _træningRepository.UpdateAsync(træning);
            return success ? Result<bool>.Ok(true) : Result<bool>.Fail("Failed to delete træning.");
        }

        // Restore Træning from Soft-Delete
        public async Task<Result<bool>> RestoreTræningAsync(Guid id, TræningDTO træningDto)
        {
            var træning = await _træningRepository.GetByIdIncludingDeletedAsync(id);
            if (træning == null || !træning.IsDeleted)
                return Result<bool>.Fail("Træning not found or not deleted.");

            træning.IsDeleted = false;
            træning.Status = SyncStatus.Synced;
            træning.ModifiedBy = træningDto.ModifiedBy;
            træning.LastSyncedVersion++;

            // Set properties for restored entry
            EntityHelper.SetDeletedOrRestoredProperties(træning, "Restored træning", træningDto.ModifiedBy);

            var success = await _træningRepository.UpdateAsync(træning);
            return success ? Result<bool>.Ok(true) : Result<bool>.Fail("Failed to restore træning.");
        }

        #endregion

        #region Get Operations

        // Get all Træning by Program ID
        public async Task<Result<IEnumerable<TræningDTO>>> GetTræningByProgramIdAsync(Guid programId)
        {
            var træningList = await _træningRepository.GetByProgramIdAsync(programId);
            if (træningList == null || !træningList.Any())
                return Result<IEnumerable<TræningDTO>>.Fail("No træning records found for this program.");

            var mapped = _mapper.Map<IEnumerable<TræningDTO>>(træningList);
            return Result<IEnumerable<TræningDTO>>.Ok(mapped);
        }

        // Get all Træning, including deleted ones
        public async Task<Result<IEnumerable<TræningDTO>>> GetAllTræningIncludingDeletedAsync()
        {
            var træningList = await _træningRepository.GetAllIncludingDeletedAsync();
            if (træningList == null || !træningList.Any())
                return Result<IEnumerable<TræningDTO>>.Fail("No træning records found, including deleted ones.");

            var mapped = _mapper.Map<IEnumerable<TræningDTO>>(træningList);
            return Result<IEnumerable<TræningDTO>>.Ok(mapped);
        }

        // Get Træning by ID, including deleted ones
        public async Task<Result<TræningDTO>> GetTræningByIdIncludingDeletedAsync(Guid id)
        {
            var træning = await _træningRepository.GetByIdIncludingDeletedAsync(id);
            if (træning == null)
                return Result<TræningDTO>.Fail("Træning not found.");

            var mapped = _mapper.Map<TræningDTO>(træning);
            return Result<TræningDTO>.Ok(mapped);
        }

        #endregion
    }
}
