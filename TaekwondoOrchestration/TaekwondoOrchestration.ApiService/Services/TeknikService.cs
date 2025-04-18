using TaekwondoApp.Shared.DTO;
using TaekwondoApp.Shared.Models;
using TaekwondoOrchestration.ApiService.Repositories;
using TaekwondoOrchestration.ApiService.RepositorieInterfaces;
using AutoMapper;
using TaekwondoOrchestration.ApiService.ServiceInterfaces;
using TaekwondoOrchestration.ApiService.Helpers;

namespace TaekwondoOrchestration.ApiService.Services
{
    public class TeknikService : ITeknikService
    {
        private readonly ITeknikRepository _teknikRepository;
        private readonly IMapper _mapper;

        public TeknikService(ITeknikRepository teknikRepository, IMapper mapper)
        {
            _teknikRepository = teknikRepository;
            _mapper = mapper;
        }

        #region CRUD Operations

        // Get All Teknikker
        public async Task<Result<IEnumerable<TeknikDTO>>> GetAllTekniksAsync()
        {
            var teknikList = await _teknikRepository.GetAllAsync();
            var mapped = _mapper.Map<IEnumerable<TeknikDTO>>(teknikList);
            return Result<IEnumerable<TeknikDTO>>.Ok(mapped);
        }

        // Get Teknik by ID
        public async Task<Result<TeknikDTO>> GetTeknikByIdAsync(Guid id)
        {
            var teknik = await _teknikRepository.GetByIdAsync(id);
            if (teknik == null)
                return Result<TeknikDTO>.Fail("Teknik not found.");

            var mapped = _mapper.Map<TeknikDTO>(teknik);
            return Result<TeknikDTO>.Ok(mapped);
        }

        // Create New Teknik
        public async Task<Result<TeknikDTO>> CreateTeknikAsync(TeknikDTO teknikDto)
        {
            if (teknikDto == null)
            {
                return Result<TeknikDTO>.Fail("Invalid Teknik data.");
            }

            var newTeknik = _mapper.Map<Teknik>(teknikDto);
            EntityHelper.InitializeEntity(newTeknik, teknikDto.ModifiedBy, "Created new Teknik.");

            var createdTeknik = await _teknikRepository.CreateAsync(newTeknik);
            var mapped = _mapper.Map<TeknikDTO>(createdTeknik);

            return Result<TeknikDTO>.Ok(mapped);
        }

        // Update Existing Teknik
        public async Task<Result<TeknikDTO>> UpdateTeknikAsync(Guid id, TeknikDTO teknikDto)
        {
            if (teknikDto == null || id != teknikDto.TeknikID)
                return Result<TeknikDTO>.Fail("Invalid Teknik data.");

            var existingTeknik = await _teknikRepository.GetByIdAsync(id);
            if (existingTeknik == null)
                return Result<TeknikDTO>.Fail("Teknik not found.");

            _mapper.Map(teknikDto, existingTeknik);
            EntityHelper.UpdateCommonFields(existingTeknik, teknikDto.ModifiedBy);

            var updateSuccess = await _teknikRepository.UpdateAsync(existingTeknik);
            return updateSuccess ? Result<TeknikDTO>.Ok(_mapper.Map<TeknikDTO>(existingTeknik)) : Result<TeknikDTO>.Fail("Failed to update Teknik.");
        }

        // Soft Delete Teknik
        public async Task<Result<bool>> DeleteTeknikAsync(Guid id)
        {
            var teknik = await _teknikRepository.GetByIdAsync(id);
            if (teknik == null)
                return Result<bool>.Fail("Teknik not found.");

            // Soft delete logic
            string modifiedBy = teknik.ModifiedBy; // Assuming user context
            EntityHelper.SetDeletedOrRestoredProperties(teknik, "Soft-deleted Teknik", modifiedBy);

            var success = await _teknikRepository.UpdateAsync(teknik);
            return success ? Result<bool>.Ok(true) : Result<bool>.Fail("Failed to delete Teknik.");
        }

        // Restore Teknik from Soft-Delete
        public async Task<Result<bool>> RestoreTeknikAsync(Guid id, TeknikDTO teknikDto)
        {
            var teknik = await _teknikRepository.GetByIdIncludingDeletedAsync(id);
            if (teknik == null || !teknik.IsDeleted)
                return Result<bool>.Fail("Teknik not found or not deleted.");

            teknik.IsDeleted = false;
            teknik.Status = SyncStatus.Synced;
            teknik.ModifiedBy = teknikDto.ModifiedBy;
            teknik.LastSyncedVersion++;

            // Set properties for restored entry
            EntityHelper.SetDeletedOrRestoredProperties(teknik, "Restored Teknik", teknikDto.ModifiedBy);

            var success = await _teknikRepository.UpdateAsync(teknik);
            return success ? Result<bool>.Ok(true) : Result<bool>.Fail("Failed to restore Teknik.");
        }

        #endregion

        #region Get Operations

        // Get Teknikker by Pensum ID
        public async Task<Result<IEnumerable<TeknikDTO>>> GetAllTeknikByPensumAsync(Guid pensumId)
        {
            var teknikList = await _teknikRepository.GetByPensumIdAsync(pensumId);
            var mapped = _mapper.Map<IEnumerable<TeknikDTO>>(teknikList);
            return Result<IEnumerable<TeknikDTO>>.Ok(mapped);
        }

        // Get Teknik by TeknikNavn
        public async Task<Result<TeknikDTO>> GetTeknikByTeknikNavnAsync(string teknikNavn)
        {
            if (string.IsNullOrWhiteSpace(teknikNavn))
                return Result<TeknikDTO>.Fail("Teknik name cannot be empty.");

            var teknik = await _teknikRepository.GetByTeknikNavnAsync(teknikNavn);
            if (teknik == null)
                return Result<TeknikDTO>.Fail("Teknik not found.");

            var mapped = _mapper.Map<TeknikDTO>(teknik);
            return Result<TeknikDTO>.Ok(mapped);
        }

        #endregion
    }
}
