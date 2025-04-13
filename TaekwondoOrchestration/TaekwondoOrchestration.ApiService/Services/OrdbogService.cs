using TaekwondoApp.Shared.DTO;
using TaekwondoApp.Shared.Models;
using TaekwondoOrchestration.ApiService.Repositories;
using TaekwondoOrchestration.ApiService.RepositorieInterfaces;
using AutoMapper;
using Newtonsoft.Json;
using Microsoft.EntityFrameworkCore;

namespace TaekwondoOrchestration.ApiService.Services
{
    public class OrdbogService
    {
        private readonly IOrdbogRepository _ordbogRepository;
        private readonly IMapper _mapper;

        public OrdbogService(IOrdbogRepository ordbogRepository, IMapper mapper)
        {
            _ordbogRepository = ordbogRepository;
            _mapper = mapper;
        }

        public async Task<List<OrdbogDTO>> GetAllOrdbogAsync()
        {
            var ordboger = await _ordbogRepository.GetAllOrdbogAsync();
            return _mapper.Map<List<OrdbogDTO>>(ordboger);
        }

        public async Task<OrdbogDTO?> GetOrdbogByIdAsync(Guid id)
        {
            var ordbog = await _ordbogRepository.GetOrdbogByIdAsync(id);
            return ordbog == null ? null : _mapper.Map<OrdbogDTO>(ordbog);
        }

        public async Task<OrdbogDTO> CreateOrdbogAsync(OrdbogDTO ordbogDto)
        {
            var newOrdbog = _mapper.Map<Ordbog>(ordbogDto);

            newOrdbog.ETag = GenerateETag(newOrdbog);
            newOrdbog.CreatedAt = DateTime.UtcNow;
            newOrdbog.LastModified = DateTime.UtcNow;
            newOrdbog.Status = SyncStatus.Synced;
            newOrdbog.ConflictStatus = ConflictResolutionStatus.NoConflict;
            newOrdbog.LastSyncedVersion = 0;
            newOrdbog.ModifiedBy = "System";
            newOrdbog.IsDeleted = false;
            newOrdbog.ChangeHistory = new List<ChangeRecord>
            {
                new ChangeRecord
                {
                    ChangedAt = DateTime.UtcNow,
                    ChangedBy = newOrdbog.ModifiedBy,
                    ChangeDescription = "Created new Ordbog entry."
                }
            };
            newOrdbog.ChangeHistoryJson = JsonConvert.SerializeObject(newOrdbog.ChangeHistory);

            var createdOrdbog = await _ordbogRepository.CreateOrdbogAsync(newOrdbog);

            return _mapper.Map<OrdbogDTO>(createdOrdbog);
        }

        public async Task<bool> UpdateOrdbogAsync(Guid id, OrdbogDTO ordbogDto)
        {
            if (string.IsNullOrEmpty(ordbogDto.DanskOrd) ||
                string.IsNullOrEmpty(ordbogDto.KoranskOrd) ||
                string.IsNullOrEmpty(ordbogDto.Beskrivelse))
            {
                return false;
            }

            var existingOrdbog = await _ordbogRepository.GetOrdbogByIdAsync(id);
            if (existingOrdbog == null) return false;

            // Preserve non-editable fields
            var createdAt = existingOrdbog.CreatedAt;
            existingOrdbog.IsDeleted = ordbogDto.IsDeleted;
            existingOrdbog.Status = ordbogDto.Status;

            // Update editable fields
            existingOrdbog.DanskOrd = ordbogDto.DanskOrd;
            existingOrdbog.KoranskOrd = ordbogDto.KoranskOrd;
            existingOrdbog.Beskrivelse = ordbogDto.Beskrivelse;
            existingOrdbog.BilledeLink = ordbogDto.BilledeLink;
            existingOrdbog.LydLink = ordbogDto.LydLink;
            existingOrdbog.VideoLink = ordbogDto.VideoLink;

            existingOrdbog.LastModified = DateTime.UtcNow;
            existingOrdbog.ETag = GenerateETag(existingOrdbog);
            existingOrdbog.ModifiedBy = "System";
            existingOrdbog.ConflictStatus = ConflictResolutionStatus.NoConflict;
            existingOrdbog.LastSyncedVersion++; // optional: increment version

            existingOrdbog.ChangeHistory.Add(new ChangeRecord
            {
                ChangedAt = DateTime.UtcNow,
                ChangedBy = existingOrdbog.ModifiedBy,
                ChangeDescription = $"Updated Ordbog entry with ID: {existingOrdbog.OrdbogId}"
            });

            existingOrdbog.ChangeHistoryJson = JsonConvert.SerializeObject(existingOrdbog.ChangeHistory);

            return await _ordbogRepository.UpdateOrdbogAsync(existingOrdbog);
        }


        public async Task<bool> DeleteOrdbogAsync(Guid id)
        {
            var ordbog = await _ordbogRepository.GetOrdbogByIdAsync(id);
            if (ordbog == null || ordbog.IsDeleted) return false;

            ordbog.IsDeleted = true;
            ordbog.LastModified = DateTime.UtcNow;
            ordbog.ModifiedBy = "System"; // or current user
            ordbog.Status = SyncStatus.Deleted;
            ordbog.ConflictStatus = ConflictResolutionStatus.NoConflict;
            ordbog.LastSyncedVersion++;

            ordbog.ChangeHistory.Add(new ChangeRecord
            {
                ChangedAt = DateTime.UtcNow,
                ChangedBy = ordbog.ModifiedBy,
                ChangeDescription = $"Soft-deleted Ordbog entry with ID: {ordbog.OrdbogId}"
            });

            ordbog.ChangeHistoryJson = JsonConvert.SerializeObject(ordbog.ChangeHistory);
            ordbog.ETag = GenerateETag(ordbog);

            return await _ordbogRepository.UpdateOrdbogAsync(ordbog);
        }
        public async Task<bool> RestoreOrdbogAsync(Guid id, OrdbogDTO dto)
        {
            var ordbog = await _ordbogRepository.GetOrdbogByIdIncludingDeletedAsync(id);
            if (ordbog == null || !ordbog.IsDeleted)
                return false;

            ordbog.IsDeleted = false;
            ordbog.Status = SyncStatus.Synced;
            ordbog.LastModified = DateTime.UtcNow;
            ordbog.ModifiedBy = dto?.ModifiedBy ?? "System";
            ordbog.LastSyncedVersion++;

            ordbog.ChangeHistory.Add(new ChangeRecord
            {
                ChangedAt = DateTime.UtcNow,
                ChangedBy = ordbog.ModifiedBy,
                ChangeDescription = $"Restored Ordbog entry with ID: {id}"
            });

            ordbog.ChangeHistoryJson = JsonConvert.SerializeObject(ordbog.ChangeHistory);
            ordbog.ETag = GenerateETag(ordbog);

            return await _ordbogRepository.UpdateAsync(ordbog);
        }
        public async Task<OrdbogDTO?> GetOrdbogByDanskOrdAsync(string danskOrd)
        {
            var ordbog = await _ordbogRepository.GetOrdbogByDanskOrdAsync(danskOrd);
            return ordbog == null ? null : _mapper.Map<OrdbogDTO>(ordbog);
        }
        public async Task<OrdbogDTO?> UpdateOrdbogIncludingDeletedByIdAsync(Guid id, OrdbogDTO dto)
        {
            var existing = await _ordbogRepository.GetOrdbogByIdAsync(id);
            if (existing == null) return null;

            // Preserve non-editable fields (CreatedAt, ChangeHistory)
            var preservedCreatedAt = existing.CreatedAt;
            var preservedChangeHistory = existing.ChangeHistory ?? new List<ChangeRecord>();

            // Map incoming DTO onto the existing entity
            var updated = _mapper.Map(dto, existing);

            // Preserve fields from the existing entity
            updated.CreatedAt = preservedCreatedAt;
            updated.LastModified = DateTime.UtcNow;
            updated.ModifiedBy = "System";  // You can replace "System" with the actual current user
            updated.LastSyncedVersion++;  // Increment the sync version number
            updated.ConflictStatus = ConflictResolutionStatus.NoConflict;

            // Update Change History
            preservedChangeHistory.Add(new ChangeRecord
            {
                ChangedAt = DateTime.UtcNow,
                ChangedBy = updated.ModifiedBy,
                ChangeDescription = $"Force-updated (including deleted) Ordbog with ID: {updated.OrdbogId}"
            });

            updated.ChangeHistory = preservedChangeHistory;
            updated.ChangeHistoryJson = JsonConvert.SerializeObject(updated.ChangeHistory);
            updated.ETag = GenerateETag(updated);  // Generate a new ETag to indicate changes

            // Call the repository's update method (which is much simpler now)
            var result = await _ordbogRepository.UpdateOrdbogIncludingDeletedAsync(id, updated);

            // Return the updated entity mapped to DTO
            return result == null ? null : _mapper.Map<OrdbogDTO>(result);
        }

        public async Task<OrdbogDTO?> GetOrdbogByKoranOrdAsync(string koranOrd)
        {
            var ordbog = await _ordbogRepository.GetOrdbogByKoranOrdAsync(koranOrd);
            return ordbog == null ? null : _mapper.Map<OrdbogDTO>(ordbog);
        }

        private string GenerateETag(Ordbog entry)
        {
            return $"{entry.DanskOrd}-{entry.KoranskOrd}";
        }
        public async Task<IEnumerable<OrdbogDTO>> GetAllOrdbogIncludingDeletedAsync()
        {
            var ordboger = await _ordbogRepository.GetAllOrdbogIncludingDeletedAsync();
            return _mapper.Map<IEnumerable<OrdbogDTO>>(ordboger);
        }
    }
}
