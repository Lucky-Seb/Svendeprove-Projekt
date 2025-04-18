using TaekwondoApp.Shared.DTO;
using TaekwondoApp.Shared.Models;
using TaekwondoOrchestration.ApiService.Repositories;
using TaekwondoOrchestration.ApiService.RepositorieInterfaces;
using AutoMapper;
using TaekwondoOrchestration.ApiService.ServiceInterfaces;
using TaekwondoOrchestration.ApiService.Helpers;

namespace TaekwondoOrchestration.ApiService.Services
{
    public class SpørgsmålService : ISpørgsmålService
    {
        private readonly ISpørgsmålRepository _spørgsmålRepository;
        private readonly IMapper _mapper;

        public SpørgsmålService(ISpørgsmålRepository spørgsmålRepository, IMapper mapper)
        {
            _spørgsmålRepository = spørgsmålRepository;
            _mapper = mapper;
        }

        #region CRUD Operations

        // Get All Spørgsmål
        public async Task<Result<IEnumerable<SpørgsmålDTO>>> GetAllSpørgsmålAsync()
        {
            var spørgsmålList = await _spørgsmålRepository.GetAllAsync();
            var mapped = _mapper.Map<IEnumerable<SpørgsmålDTO>>(spørgsmålList);
            return Result<IEnumerable<SpørgsmålDTO>>.Ok(mapped);
        }

        // Get Spørgsmål by ID
        public async Task<Result<SpørgsmålDTO>> GetSpørgsmålByIdAsync(Guid id)
        {
            var spørgsmål = await _spørgsmålRepository.GetByIdAsync(id);
            if (spørgsmål == null)
                return Result<SpørgsmålDTO>.Fail("Spørgsmål not found.");

            var mapped = _mapper.Map<SpørgsmålDTO>(spørgsmål);
            return Result<SpørgsmålDTO>.Ok(mapped);
        }

        // Create New Spørgsmål
        public async Task<Result<SpørgsmålDTO>> CreateSpørgsmålAsync(SpørgsmålDTO spørgsmålDto)
        {
            // Validation
            if (spørgsmålDto == null)
            {
                return Result<SpørgsmålDTO>.Fail("Invalid Spørgsmål data.");
            }

            var newSpørgsmål = _mapper.Map<Spørgsmål>(spørgsmålDto);
            EntityHelper.InitializeEntity(newSpørgsmål, spørgsmålDto.ModifiedBy, "Created new Spørgsmål.");

            var createdSpørgsmål = await _spørgsmålRepository.CreateAsync(newSpørgsmål);
            var mapped = _mapper.Map<SpørgsmålDTO>(createdSpørgsmål);

            return Result<SpørgsmålDTO>.Ok(mapped);
        }

        // Update Existing Spørgsmål
        public async Task<Result<SpørgsmålDTO>> UpdateSpørgsmålAsync(Guid id, SpørgsmålDTO spørgsmålDto)
        {
            if (spørgsmålDto == null || id != spørgsmålDto.SpørgsmålID)
                return Result<SpørgsmålDTO>.Fail("Invalid Spørgsmål data.");

            var existingSpørgsmål = await _spørgsmålRepository.GetByIdAsync(id);
            if (existingSpørgsmål == null)
                return Result<SpørgsmålDTO>.Fail("Spørgsmål not found.");

            _mapper.Map(spørgsmålDto, existingSpørgsmål);
            EntityHelper.UpdateCommonFields(existingSpørgsmål, spørgsmålDto.ModifiedBy);

            var updateSuccess = await _spørgsmålRepository.UpdateAsync(existingSpørgsmål);
            return updateSuccess ? Result<SpørgsmålDTO>.Ok(_mapper.Map<SpørgsmålDTO>(existingSpørgsmål)) : Result<SpørgsmålDTO>.Fail("Failed to update Spørgsmål.");
        }

        // Soft Delete Spørgsmål
        public async Task<Result<bool>> DeleteSpørgsmålAsync(Guid id)
        {
            var spørgsmål = await _spørgsmålRepository.GetByIdAsync(id);
            if (spørgsmål == null)
                return Result<bool>.Fail("Spørgsmål not found.");

            // Soft delete logic
            string modifiedBy = spørgsmål.ModifiedBy; // Assuming user context
            EntityHelper.SetDeletedOrRestoredProperties(spørgsmål, "Soft-deleted Spørgsmål", modifiedBy);

            var success = await _spørgsmålRepository.UpdateAsync(spørgsmål);
            return success ? Result<bool>.Ok(true) : Result<bool>.Fail("Failed to delete Spørgsmål.");
        }

        // Restore Spørgsmål from Soft-Delete
        public async Task<Result<bool>> RestoreSpørgsmålAsync(Guid id, SpørgsmålDTO spørgsmålDto)
        {
            var spørgsmål = await _spørgsmålRepository.GetByIdIncludingDeletedAsync(id);
            if (spørgsmål == null || !spørgsmål.IsDeleted)
                return Result<bool>.Fail("Spørgsmål not found or not deleted.");

            spørgsmål.IsDeleted = false;
            spørgsmål.Status = SyncStatus.Synced;
            spørgsmål.ModifiedBy = spørgsmålDto.ModifiedBy;
            spørgsmål.LastSyncedVersion++;

            // Set properties for restored entry
            EntityHelper.SetDeletedOrRestoredProperties(spørgsmål, "Restored Spørgsmål", spørgsmålDto.ModifiedBy);

            var success = await _spørgsmålRepository.UpdateAsync(spørgsmål);
            return success ? Result<bool>.Ok(true) : Result<bool>.Fail("Failed to restore Spørgsmål.");
        }

        #endregion

        #region Get Operations

        // Get Spørgsmål by QuizId
        public async Task<Result<IEnumerable<SpørgsmålDTO>>> GetSpørgsmålByQuizIdAsync(Guid quizId)
        {
            var spørgsmålList = await _spørgsmålRepository.GetByQuizIdAsync(quizId);
            var mapped = _mapper.Map<IEnumerable<SpørgsmålDTO>>(spørgsmålList);
            return Result<IEnumerable<SpørgsmålDTO>>.Ok(mapped);
        }

        // Get All Spørgsmål Including Deleted (for soft deletes)
        public async Task<Result<IEnumerable<SpørgsmålDTO>>> GetAllSpørgsmålIncludingDeletedAsync()
        {
            var spørgsmålList = await _spørgsmålRepository.GetAllIncludingDeletedAsync();
            var mapped = _mapper.Map<IEnumerable<SpørgsmålDTO>>(spørgsmålList);
            return Result<IEnumerable<SpørgsmålDTO>>.Ok(mapped);
        }

        // Get Spørgsmål by ID Including Deleted
        public async Task<Result<SpørgsmålDTO>> GetSpørgsmålByIdIncludingDeletedAsync(Guid id)
        {
            var spørgsmål = await _spørgsmålRepository.GetByIdIncludingDeletedAsync(id);
            if (spørgsmål == null)
                return Result<SpørgsmålDTO>.Fail("Spørgsmål not found.");

            var mapped = _mapper.Map<SpørgsmålDTO>(spørgsmål);
            return Result<SpørgsmålDTO>.Ok(mapped);
        }

        #endregion
    }
}
