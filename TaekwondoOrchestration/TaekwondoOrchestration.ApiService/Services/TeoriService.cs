using TaekwondoApp.Shared.DTO;
using TaekwondoApp.Shared.Models;
using TaekwondoOrchestration.ApiService.Repositories;
using TaekwondoOrchestration.ApiService.RepositorieInterfaces;
using AutoMapper;
using TaekwondoOrchestration.ApiService.ServiceInterfaces;
using TaekwondoOrchestration.ApiService.Helpers;

namespace TaekwondoOrchestration.ApiService.Services
{
    public class TeoriService : ITeoriService
    {
        private readonly ITeoriRepository _teoriRepository;
        private readonly IMapper _mapper;

        public TeoriService(ITeoriRepository teoriRepository, IMapper mapper)
        {
            _teoriRepository = teoriRepository;
            _mapper = mapper;
        }

        #region CRUD Operations

        // Get All Teorier
        public async Task<Result<IEnumerable<TeoriDTO>>> GetAllTeoriAsync()
        {
            var teoriList = await _teoriRepository.GetAllAsync();
            if (teoriList == null || !teoriList.Any())
                return Result<IEnumerable<TeoriDTO>>.Fail("No teorier found.");

            var mapped = _mapper.Map<IEnumerable<TeoriDTO>>(teoriList);
            return Result<IEnumerable<TeoriDTO>>.Ok(mapped);
        }

        // Get Teori by ID
        public async Task<Result<TeoriDTO>> GetTeoriByIdAsync(Guid id)
        {
            var teori = await _teoriRepository.GetByIdAsync(id);
            if (teori == null)
                return Result<TeoriDTO>.Fail("Teori not found.");

            var mapped = _mapper.Map<TeoriDTO>(teori);
            return Result<TeoriDTO>.Ok(mapped);
        }

        // Create New Teori
        public async Task<Result<TeoriDTO>> CreateTeoriAsync(TeoriDTO teoriDto)
        {
            if (teoriDto == null)
                return Result<TeoriDTO>.Fail("Invalid Teori data.");

            var newTeori = _mapper.Map<Teori>(teoriDto);
            EntityHelper.InitializeEntity(newTeori, teoriDto.ModifiedBy, "Created new Teori.");

            var createdTeori = await _teoriRepository.CreateAsync(newTeori);
            var mapped = _mapper.Map<TeoriDTO>(createdTeori);

            return Result<TeoriDTO>.Ok(mapped);
        }

        // Update Existing Teori
        public async Task<Result<TeoriDTO>> UpdateTeoriAsync(Guid id, TeoriDTO teoriDto)
        {
            if (teoriDto == null || id != teoriDto.TeoriID)
                return Result<TeoriDTO>.Fail("Invalid Teori data.");

            var existingTeori = await _teoriRepository.GetByIdAsync(id);
            if (existingTeori == null)
                return Result<TeoriDTO>.Fail("Teori not found.");

            _mapper.Map(teoriDto, existingTeori);
            EntityHelper.UpdateCommonFields(existingTeori, teoriDto.ModifiedBy);

            var updateSuccess = await _teoriRepository.UpdateAsync(existingTeori);
            return updateSuccess ? Result<TeoriDTO>.Ok(_mapper.Map<TeoriDTO>(existingTeori)) : Result<TeoriDTO>.Fail("Failed to update Teori.");
        }

        // Soft Delete Teori
        public async Task<Result<bool>> DeleteTeoriAsync(Guid id)
        {
            var teori = await _teoriRepository.GetByIdAsync(id);
            if (teori == null)
                return Result<bool>.Fail("Teori not found.");

            // Soft delete logic
            string modifiedBy = teori.ModifiedBy; // Assuming user context
            EntityHelper.SetDeletedOrRestoredProperties(teori, "Soft-deleted Teori", modifiedBy);

            var success = await _teoriRepository.UpdateAsync(teori);
            return success ? Result<bool>.Ok(true) : Result<bool>.Fail("Failed to delete Teori.");
        }

        // Restore Teori from Soft-Delete
        public async Task<Result<bool>> RestoreTeoriAsync(Guid id, TeoriDTO teoriDto)
        {
            var teori = await _teoriRepository.GetByIdIncludingDeletedAsync(id);
            if (teori == null || !teori.IsDeleted)
                return Result<bool>.Fail("Teori not found or not deleted.");

            teori.IsDeleted = false;
            teori.Status = SyncStatus.Synced;
            teori.ModifiedBy = teoriDto.ModifiedBy;
            teori.LastSyncedVersion++;

            // Set properties for restored entry
            EntityHelper.SetDeletedOrRestoredProperties(teori, "Restored Teori", teoriDto.ModifiedBy);

            var success = await _teoriRepository.UpdateAsync(teori);
            return success ? Result<bool>.Ok(true) : Result<bool>.Fail("Failed to restore Teori.");
        }

        #endregion

        #region Get Operations

        // Get Teori by Pensum ID
        public async Task<Result<IEnumerable<TeoriDTO>>> GetTeoriByPensumAsync(Guid pensumId)
        {
            var teoriList = await _teoriRepository.GetByPensumIdAsync(pensumId);
            if (teoriList == null || !teoriList.Any())
                return Result<IEnumerable<TeoriDTO>>.Fail("No teorier found for this Pensum.");

            var mapped = _mapper.Map<IEnumerable<TeoriDTO>>(teoriList);
            return Result<IEnumerable<TeoriDTO>>.Ok(mapped);
        }

        // Get Teori by TeoriNavn
        public async Task<Result<TeoriDTO>> GetTeoriByTeoriNavnAsync(string teoriNavn)
        {
            if (string.IsNullOrWhiteSpace(teoriNavn))
                return Result<TeoriDTO>.Fail("Teori name cannot be empty.");

            var teori = await _teoriRepository.GetByTeoriNavnAsync(teoriNavn);
            if (teori == null)
                return Result<TeoriDTO>.Fail("Teori not found.");

            var mapped = _mapper.Map<TeoriDTO>(teori);
            return Result<TeoriDTO>.Ok(mapped);
        }

        // Get all Teori, including deleted ones
        public async Task<Result<IEnumerable<TeoriDTO>>> GetAllTeoriIncludingDeletedAsync()
        {
            var teoriList = await _teoriRepository.GetAllIncludingDeletedAsync();
            if (teoriList == null || !teoriList.Any())
                return Result<IEnumerable<TeoriDTO>>.Fail("No teorier found, including deleted ones.");

            var mapped = _mapper.Map<IEnumerable<TeoriDTO>>(teoriList);
            return Result<IEnumerable<TeoriDTO>>.Ok(mapped);
        }

        // Get Teori by ID, including deleted ones
        public async Task<Result<TeoriDTO>> GetTeoriByIdIncludingDeletedAsync(Guid id)
        {
            var teori = await _teoriRepository.GetByIdIncludingDeletedAsync(id);
            if (teori == null)
                return Result<TeoriDTO>.Fail("Teori not found.");

            var mapped = _mapper.Map<TeoriDTO>(teori);
            return Result<TeoriDTO>.Ok(mapped);
        }

        #endregion
    }
}
