using TaekwondoApp.Shared.DTO;
using TaekwondoApp.Shared.Models;
using TaekwondoOrchestration.ApiService.Repositories;
using TaekwondoOrchestration.ApiService.RepositorieInterfaces;
using AutoMapper;
using Newtonsoft.Json;
using Microsoft.EntityFrameworkCore;
using TaekwondoOrchestration.ApiService.ServiceInterfaces;
using TaekwondoOrchestration.ApiService.Helpers;

namespace TaekwondoOrchestration.ApiService.Services
{
    public class OrdbogService : IOrdbogService
    {
        private readonly IOrdbogRepository _ordbogRepository;
        private readonly IMapper _mapper;

        // Constructor: Dependency Injection of Repository and Mapper
        public OrdbogService(IOrdbogRepository ordbogRepository, IMapper mapper)
        {
            _ordbogRepository = ordbogRepository;
            _mapper = mapper;
        }

        #region CRUD Operations

        // Get All Ordbog Entries
        public async Task<Result<IEnumerable<OrdbogDTO>>> GetAllOrdbogAsync()
        {
            var ordboger = await _ordbogRepository.GetAllOrdbogAsync();
            var mapped = _mapper.Map<IEnumerable<OrdbogDTO>>(ordboger);
            return Result<IEnumerable<OrdbogDTO>>.Ok(mapped);
        }

        // Get Ordbog by ID
        public async Task<Result<OrdbogDTO>> GetOrdbogByIdAsync(Guid id)
        {
            var ordbog = await _ordbogRepository.GetOrdbogByIdAsync(id);
            if (ordbog == null)
                return Result<OrdbogDTO>.Fail("Ordbog not found.");

            var mapped = _mapper.Map<OrdbogDTO>(ordbog);
            return Result<OrdbogDTO>.Ok(mapped);
        }

        // Create New Ordbog Entry
        public async Task<Result<OrdbogDTO>> CreateOrdbogAsync(OrdbogDTO ordbogDto)
        {
            var newOrdbog = _mapper.Map<Ordbog>(ordbogDto);
            EntityHelper.InitializeEntity(newOrdbog, ordbogDto.ModifiedBy, "Created new Ordbog entry.");
            var createdOrdbog = await _ordbogRepository.CreateOrdbogAsync(newOrdbog);

            var mapped = _mapper.Map<OrdbogDTO>(createdOrdbog);
            return Result<OrdbogDTO>.Ok(mapped);
        }

        // Update Existing Ordbog Entry
        public async Task<Result<bool>> UpdateOrdbogAsync(Guid id, OrdbogDTO ordbogDto)
        {
            if (string.IsNullOrEmpty(ordbogDto.DanskOrd) ||
                string.IsNullOrEmpty(ordbogDto.KoranskOrd) ||
                string.IsNullOrEmpty(ordbogDto.Beskrivelse))
                return Result<bool>.Fail("Invalid input data.");

            var existingOrdbog = await _ordbogRepository.GetOrdbogByIdAsync(id);
            if (existingOrdbog == null)
                return Result<bool>.Fail("Ordbog not found.");

            _mapper.Map(ordbogDto, existingOrdbog);
            EntityHelper.UpdateCommonFields(existingOrdbog, ordbogDto.ModifiedBy);
            var updateSuccess = await _ordbogRepository.UpdateOrdbogAsync(existingOrdbog);

            if (!updateSuccess)
                return Result<bool>.Fail("Failed to update Ordbog.");

            return Result<bool>.Ok(true);
        }

        // Update Ordbog Entry, including soft-deleted entries
        public async Task<Result<OrdbogDTO>> UpdateOrdbogIncludingDeletedByIdAsync(Guid id, OrdbogDTO ordbogDto)
        {
            var existingOrdbog = await _ordbogRepository.GetOrdbogByIdIncludingDeletedAsync(id);
            if (existingOrdbog == null)
                return Result<OrdbogDTO>.Fail("Ordbog not found.");

            _mapper.Map(ordbogDto, existingOrdbog);
            EntityHelper.UpdateCommonFields(existingOrdbog, ordbogDto.ModifiedBy);
            var updatedOrdbog = await _ordbogRepository.UpdateOrdbogAsync(existingOrdbog);

            var mapped = _mapper.Map<OrdbogDTO>(existingOrdbog);
            return Result<OrdbogDTO>.Ok(mapped);
        }

        // Delete Ordbog Entry (Soft-Delete)
        public async Task<Result<bool>> DeleteOrdbogAsync(Guid id)
        {
            var ordbog = await _ordbogRepository.GetOrdbogByIdAsync(id);
            if (ordbog == null || ordbog.IsDeleted)
                return Result<bool>.Fail("Ordbog not found or already deleted.");

            // Assuming ModifiedBy is coming from the current context or user, you can also pass it explicitly.
            string modifiedBy = ordbog.ModifiedBy; // or get it from context if available

            // Pass the modifiedBy to the helper method
            EntityHelper.SetDeletedOrRestoredProperties(ordbog, "Soft-deleted Ordbog entry", modifiedBy);

            var success = await _ordbogRepository.UpdateOrdbogAsync(ordbog);

            return success ? Result<bool>.Ok(true) : Result<bool>.Fail("Failed to delete Ordbog.");
        }

        // Restore Ordbog Entry from Soft-Delete
        public async Task<Result<bool>> RestoreOrdbogAsync(Guid id, OrdbogDTO dto)
        {
            var ordbog = await _ordbogRepository.GetOrdbogByIdIncludingDeletedAsync(id);
            if (ordbog == null || !ordbog.IsDeleted)
                return Result<bool>.Fail("Ordbog not found or not deleted.");

            // Set properties for the restored entry
            ordbog.IsDeleted = false;
            ordbog.Status = SyncStatus.Synced;
            ordbog.ModifiedBy = dto.ModifiedBy;
            ordbog.LastSyncedVersion++;

            // Pass the modifiedBy along with changeDescription to the helper method
            EntityHelper.SetDeletedOrRestoredProperties(ordbog, "Restored Ordbog entry", dto.ModifiedBy);

            var success = await _ordbogRepository.UpdateAsync(ordbog);

            return success ? Result<bool>.Ok(true) : Result<bool>.Fail("Failed to restore Ordbog.");
        }

        #endregion

        #region Search Operations

        // Get Ordbog by Dansk Ord
        public async Task<Result<OrdbogDTO>> GetOrdbogByDanskOrdAsync(string danskOrd)
        {
            var ordbog = await _ordbogRepository.GetOrdbogByDanskOrdAsync(danskOrd);
            if (ordbog == null)
                return Result<OrdbogDTO>.Fail("Ordbog not found.");

            var mapped = _mapper.Map<OrdbogDTO>(ordbog);
            return Result<OrdbogDTO>.Ok(mapped);
        }

        // Get Ordbog by Koran Ord
        public async Task<Result<OrdbogDTO>> GetOrdbogByKoranOrdAsync(string koranOrd)
        {
            var ordbog = await _ordbogRepository.GetOrdbogByKoranskOrdAsync(koranOrd);
            if (ordbog == null)
                return Result<OrdbogDTO>.Fail("Ordbog not found.");

            var mapped = _mapper.Map<OrdbogDTO>(ordbog);
            return Result<OrdbogDTO>.Ok(mapped);
        }

        // Get All Ordbog Entries including Deleted Ones
        public async Task<Result<IEnumerable<OrdbogDTO>>> GetAllOrdbogIncludingDeletedAsync()
        {
            var ordboger = await _ordbogRepository.GetAllOrdbogIncludingDeletedAsync();
            var mapped = _mapper.Map<IEnumerable<OrdbogDTO>>(ordboger);
            return Result<IEnumerable<OrdbogDTO>>.Ok(mapped);
        }

        #endregion
    }
}