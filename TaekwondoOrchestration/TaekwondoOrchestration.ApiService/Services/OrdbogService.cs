using TaekwondoApp.Shared.DTO;
using TaekwondoApp.Shared.Models;
using TaekwondoOrchestration.ApiService.Repositories;
using TaekwondoOrchestration.ApiService.RepositorieInterfaces;
using AutoMapper;
using Newtonsoft.Json;
using Microsoft.EntityFrameworkCore;
using TaekwondoOrchestration.ApiService.ServiceInterfaces;

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
        public async Task<List<OrdbogDTO>> GetAllOrdbogAsync()
        {
            var ordboger = await _ordbogRepository.GetAllOrdbogAsync();
            return _mapper.Map<List<OrdbogDTO>>(ordboger);
        }

        // Get Ordbog by ID
        public async Task<OrdbogDTO?> GetOrdbogByIdAsync(Guid id)
        {
            var ordbog = await _ordbogRepository.GetOrdbogByIdAsync(id);
            return _mapper.Map<OrdbogDTO>(ordbog);
        }

        // Create New Ordbog Entry
        public async Task<OrdbogDTO> CreateOrdbogAsync(OrdbogDTO ordbogDto)
        {
            var newOrdbog = _mapper.Map<Ordbog>(ordbogDto);
            InitializeOrdbog(newOrdbog, "Created new Ordbog entry.");
            var createdOrdbog = await _ordbogRepository.CreateOrdbogAsync(newOrdbog);
            return _mapper.Map<OrdbogDTO>(createdOrdbog);
        }

        // Update Existing Ordbog Entry
        public async Task<bool> UpdateOrdbogAsync(Guid id, OrdbogDTO ordbogDto)
        {
            if (string.IsNullOrEmpty(ordbogDto.DanskOrd) ||
                string.IsNullOrEmpty(ordbogDto.KoranskOrd) ||
                string.IsNullOrEmpty(ordbogDto.Beskrivelse)) return false;

            var existingOrdbog = await _ordbogRepository.GetOrdbogByIdAsync(id);
            if (existingOrdbog == null) return false;

            // Preserve non-editable fields before mapping
            var preservedChangeHistory = existingOrdbog.ChangeHistory ?? new List<ChangeRecord>();

            // Use AutoMapper to map from DTO to the existing Ordbog entity
            _mapper.Map(ordbogDto, existingOrdbog);

            // Now call the helper to update common fields and Change History
            UpdateCommonFields(existingOrdbog, ordbogDto.ModifiedBy);  // You can replace "System" with current user

            return await _ordbogRepository.UpdateOrdbogAsync(existingOrdbog);
        }

        // Update Ordbog Entry, including soft-deleted entries
        public async Task<OrdbogDTO> UpdateOrdbogIncludingDeletedByIdAsync(Guid id, OrdbogDTO ordbogDto)
        {
            // Validate and retrieve the existing Ordbog entity
            var existingOrdbog = await _ordbogRepository.GetOrdbogByIdIncludingDeletedAsync(id);
            if (existingOrdbog == null) return null;  // Return null if not found

            // Map the DTO to the existing Ordbog
            _mapper.Map(ordbogDto, existingOrdbog);

            // Update the Ordbog fields
            UpdateCommonFields(existingOrdbog, ordbogDto.ModifiedBy);

            // Save the updated Ordbog entity
            var updatedOrdbog = await _ordbogRepository.UpdateOrdbogAsync(existingOrdbog);

            // Return the updated Ordbog as DTO
            return _mapper.Map<OrdbogDTO>(updatedOrdbog);
        }

        // Delete Ordbog Entry (Soft-Delete)
        public async Task<bool> DeleteOrdbogAsync(Guid id)
        {
            var ordbog = await _ordbogRepository.GetOrdbogByIdAsync(id);
            if (ordbog == null || ordbog.IsDeleted) return false;

            SetDeletedOrRestoredProperties(ordbog, "Soft-deleted Ordbog entry");
            return await _ordbogRepository.UpdateOrdbogAsync(ordbog);
        }

        // Restore Ordbog Entry from Soft-Delete
        public async Task<bool> RestoreOrdbogAsync(Guid id, OrdbogDTO dto)
        {
            var ordbog = await _ordbogRepository.GetOrdbogByIdIncludingDeletedAsync(id);
            if (ordbog == null || !ordbog.IsDeleted) return false;

            ordbog.IsDeleted = false;
            ordbog.Status = SyncStatus.Synced;
            ordbog.ModifiedBy = dto.ModifiedBy ;
            ordbog.LastSyncedVersion++;
            SetDeletedOrRestoredProperties(ordbog, "Restored Ordbog entry");
            return await _ordbogRepository.UpdateAsync(ordbog);
        }

        #endregion

        #region Search Operations

        // Get Ordbog by Dansk Ord
        public async Task<OrdbogDTO?> GetOrdbogByDanskOrdAsync(string danskOrd)
        {
            var ordbog = await _ordbogRepository.GetOrdbogByDanskOrdAsync(danskOrd);
            return _mapper.Map<OrdbogDTO>(ordbog);
        }

        // Get Ordbog by Koran Ord
        public async Task<OrdbogDTO?> GetOrdbogByKoranOrdAsync(string koranOrd)
        {
            var ordbog = await _ordbogRepository.GetOrdbogByKoranOrdAsync(koranOrd);
            return _mapper.Map<OrdbogDTO>(ordbog);
        }

        // Get All Ordbog Entries including Deleted Ones
        public async Task<IEnumerable<OrdbogDTO>> GetAllOrdbogIncludingDeletedAsync()
        {
            var ordboger = await _ordbogRepository.GetAllOrdbogIncludingDeletedAsync();
            return _mapper.Map<IEnumerable<OrdbogDTO>>(ordboger);
        }

        #endregion

        #region Helper Methods

        // Generate ETag based on Dansk and Koran Ord
        private string GenerateETag(Ordbog entry)
        {
            // Combine all relevant properties to generate a unique ETag
            var etagSource = $"{entry.OrdbogId}-{entry.DanskOrd}-{entry.KoranskOrd}-{entry.Beskrivelse}-{entry.BilledeLink}-{entry.LydLink}-{entry.VideoLink}";

            // Return a hash of the combined properties to generate the ETag
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
            ordbog.ModifiedBy = ordbog.ModifiedBy;  // Can be changed to current user if necessary
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
            // Preserve and restore fields
            existingOrdbog.CreatedAt = existingOrdbog.CreatedAt; // Ensure CreatedAt is not overwritten
            existingOrdbog.LastModified = DateTime.UtcNow;
            existingOrdbog.ETag = GenerateETag(existingOrdbog); // Static method call
            existingOrdbog.ModifiedBy = modifiedBy;  // Set the user who made the modification
            existingOrdbog.LastSyncedVersion++;       // Increment sync version after update
            existingOrdbog.ConflictStatus = ConflictResolutionStatus.NoConflict;

            // Update Change History
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
