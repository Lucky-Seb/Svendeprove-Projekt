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
            return Result<IEnumerable<OrdbogDTO>>.Success(mapped);
        }

        // Get Ordbog by ID
        public async Task<Result<OrdbogDTO>> GetOrdbogByIdAsync(Guid id)
        {
            var ordbog = await _ordbogRepository.GetOrdbogByIdAsync(id);
            if (ordbog == null)
                return Result<OrdbogDTO>.Failure("Ordbog not found.");

            var mapped = _mapper.Map<OrdbogDTO>(ordbog);
            return Result<OrdbogDTO>.Success(mapped);
        }

        // Create New Ordbog Entry
        public async Task<Result<OrdbogDTO>> CreateOrdbogAsync(OrdbogDTO ordbogDto)
        {
            var newOrdbog = _mapper.Map<Ordbog>(ordbogDto);
            InitializeOrdbog(newOrdbog, "Created new Ordbog entry.");
            var createdOrdbog = await _ordbogRepository.CreateOrdbogAsync(newOrdbog);

            var mapped = _mapper.Map<OrdbogDTO>(createdOrdbog);
            return Result<OrdbogDTO>.Success(mapped);
        }

        // Update Existing Ordbog Entry
        public async Task<Result<bool>> UpdateOrdbogAsync(Guid id, OrdbogDTO ordbogDto)
        {
            if (string.IsNullOrEmpty(ordbogDto.DanskOrd) ||
                string.IsNullOrEmpty(ordbogDto.KoranskOrd) ||
                string.IsNullOrEmpty(ordbogDto.Beskrivelse))
                return Result<bool>.Failure("Invalid input data.");

            var existingOrdbog = await _ordbogRepository.GetOrdbogByIdAsync(id);
            if (existingOrdbog == null)
                return Result<bool>.Failure("Ordbog not found.");

            _mapper.Map(ordbogDto, existingOrdbog);
            UpdateCommonFields(existingOrdbog, ordbogDto.ModifiedBy);
            var updateSuccess = await _ordbogRepository.UpdateOrdbogAsync(existingOrdbog);

            if (!updateSuccess)
                return Result<bool>.Failure("Failed to update Ordbog.");

            return Result<bool>.Success(true);
        }

        // Update Ordbog Entry, including soft-deleted entries
        public async Task<Result<OrdbogDTO>> UpdateOrdbogIncludingDeletedByIdAsync(Guid id, OrdbogDTO ordbogDto)
        {
            var existingOrdbog = await _ordbogRepository.GetOrdbogByIdIncludingDeletedAsync(id);
            if (existingOrdbog == null)
                return Result<OrdbogDTO>.Failure("Ordbog not found.");

            _mapper.Map(ordbogDto, existingOrdbog);
            UpdateCommonFields(existingOrdbog, ordbogDto.ModifiedBy);
            var updatedOrdbog = await _ordbogRepository.UpdateOrdbogAsync(existingOrdbog);

            var mapped = _mapper.Map<OrdbogDTO>(updatedOrdbog);
            return Result<OrdbogDTO>.Success(mapped);
        }

        // Delete Ordbog Entry (Soft-Delete)
        public async Task<Result<bool>> DeleteOrdbogAsync(Guid id)
        {
            var ordbog = await _ordbogRepository.GetOrdbogByIdAsync(id);
            if (ordbog == null || ordbog.IsDeleted)
                return Result<bool>.Failure("Ordbog not found or already deleted.");

            SetDeletedOrRestoredProperties(ordbog, "Soft-deleted Ordbog entry");
            var success = await _ordbogRepository.UpdateOrdbogAsync(ordbog);

            return success ? Result<bool>.Success(true) : Result<bool>.Failure("Failed to delete Ordbog.");
        }

        // Restore Ordbog Entry from Soft-Delete
        public async Task<Result<bool>> RestoreOrdbogAsync(Guid id, OrdbogDTO dto)
        {
            var ordbog = await _ordbogRepository.GetOrdbogByIdIncludingDeletedAsync(id);
            if (ordbog == null || !ordbog.IsDeleted)
                return Result<bool>.Failure("Ordbog not found or not deleted.");

            ordbog.IsDeleted = false;
            ordbog.Status = SyncStatus.Synced;
            ordbog.ModifiedBy = dto.ModifiedBy;
            ordbog.LastSyncedVersion++;
            SetDeletedOrRestoredProperties(ordbog, "Restored Ordbog entry");
            var success = await _ordbogRepository.UpdateAsync(ordbog);

            return success ? Result<bool>.Success(true) : Result<bool>.Failure("Failed to restore Ordbog.");
        }

        #endregion

        #region Search Operations

        // Get Ordbog by Dansk Ord
        public async Task<Result<OrdbogDTO>> GetOrdbogByDanskOrdAsync(string danskOrd)
        {
            var ordbog = await _ordbogRepository.GetOrdbogByDanskOrdAsync(danskOrd);
            if (ordbog == null)
                return Result<OrdbogDTO>.Failure("Ordbog not found.");

            var mapped = _mapper.Map<OrdbogDTO>(ordbog);
            return Result<OrdbogDTO>.Success(mapped);
        }

        // Get Ordbog by Koran Ord
        public async Task<Result<OrdbogDTO>> GetOrdbogByKoranOrdAsync(string koranOrd)
        {
            var ordbog = await _ordbogRepository.GetOrdbogByKoranOrdAsync(koranOrd);
            if (ordbog == null)
                return Result<OrdbogDTO>.Failure("Ordbog not found.");

            var mapped = _mapper.Map<OrdbogDTO>(ordbog);
            return Result<OrdbogDTO>.Success(mapped);
        }

        // Get All Ordbog Entries including Deleted Ones
        public async Task<Result<IEnumerable<OrdbogDTO>>> GetAllOrdbogIncludingDeletedAsync()
        {
            var ordboger = await _ordbogRepository.GetAllOrdbogIncludingDeletedAsync();
            var mapped = _mapper.Map<IEnumerable<OrdbogDTO>>(ordboger);
            return Result<IEnumerable<OrdbogDTO>>.Success(mapped);
        }

        #endregion

        #region Helper Methods

        // Generate ETag based on Dansk and Koran Ord
        private string GenerateETag(Ordbog entry)
        {
            var etagSource = $"{entry.OrdbogId}-{entry.DanskOrd}-{entry.KoranskOrd}-{entry.Beskrivelse}-{entry.BilledeLink}-{entry.LydLink}-{entry.VideoLink}";
            return Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(etagSource));
        }

        // Initialize Ordbog Entry (common setup)
        private void InitializeOrdbog(Ordbog newOrdbog, string changeDescription)
        {
            newOrdbog.ETag = GenerateETag(newOrdbog);
            newOrdbog.CreatedAt = DateTime.UtcNow;
            newOrdbog.LastModified = DateTime.UtcNow;
            newOrdbog.Status = SyncStatus.Synced;
            newOrdbog.ConflictStatus = ConflictResolutionStatus.NoConflict;
            newOrdbog.LastSyncedVersion = 0;
            newOrdbog.ModifiedBy = newOrdbog.ModifiedBy;
            newOrdbog.IsDeleted = false;
            newOrdbog.ChangeHistory = new List<ChangeRecord>
            {
                new ChangeRecord
                {
                    ChangedAt = DateTime.UtcNow,
                    ChangedBy = newOrdbog.ModifiedBy,
                    ChangeDescription = changeDescription
                }
            };
            newOrdbog.ChangeHistoryJson = JsonConvert.SerializeObject(newOrdbog.ChangeHistory);
        }

        // Set properties for deleted or restored entries
        private void SetDeletedOrRestoredProperties(Ordbog ordbog, string changeDescription)
        {
            ordbog.LastModified = DateTime.UtcNow;
            ordbog.ModifiedBy = ordbog.ModifiedBy;
            ordbog.ConflictStatus = ConflictResolutionStatus.NoConflict;
            ordbog.LastSyncedVersion++;
            ordbog.ChangeHistory.Add(new ChangeRecord
            {
                ChangedAt = DateTime.UtcNow,
                ChangedBy = ordbog.ModifiedBy,
                ChangeDescription = changeDescription
            });
            ordbog.ChangeHistoryJson = JsonConvert.SerializeObject(ordbog.ChangeHistory);
            ordbog.ETag = GenerateETag(ordbog);
        }

        public void UpdateCommonFields(Ordbog existingOrdbog, string modifiedBy)
        {
            existingOrdbog.CreatedAt = existingOrdbog.CreatedAt;
            existingOrdbog.LastModified = DateTime.UtcNow;
            existingOrdbog.ETag = GenerateETag(existingOrdbog);
            existingOrdbog.ModifiedBy = modifiedBy;
            existingOrdbog.LastSyncedVersion++;
            existingOrdbog.ConflictStatus = ConflictResolutionStatus.NoConflict;

            existingOrdbog.ChangeHistory.Add(new ChangeRecord
            {
                ChangedAt = DateTime.UtcNow,
                ChangedBy = existingOrdbog.ModifiedBy,
                ChangeDescription = $"Updated Ordbog entry with ID: {existingOrdbog.OrdbogId}"
            });

            existingOrdbog.ChangeHistoryJson = JsonConvert.SerializeObject(existingOrdbog.ChangeHistory);
        }
        #endregion
    }
}
