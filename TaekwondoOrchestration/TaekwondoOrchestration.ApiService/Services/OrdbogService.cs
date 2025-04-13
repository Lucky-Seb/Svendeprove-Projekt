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
            var isDeleted = existingOrdbog.IsDeleted;

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
            existingOrdbog.Status = SyncStatus.Synced;
            existingOrdbog.ConflictStatus = ConflictResolutionStatus.NoConflict;
            existingOrdbog.LastSyncedVersion++; // optional: increment version

            existingOrdbog.ChangeHistory.Add(new ChangeRecord
            {
                ChangedAt = DateTime.UtcNow,
                ChangedBy = existingOrdbog.ModifiedBy,
                ChangeDescription = $"Updated Ordbog entry with ID: {existingOrdbog.OrdbogId}"
            });

            existingOrdbog.ChangeHistoryJson = JsonConvert.SerializeObject(existingOrdbog.ChangeHistory);

            // Re-apply preserved fields
            existingOrdbog.CreatedAt = createdAt;
            existingOrdbog.IsDeleted = isDeleted;

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
        public async Task<bool> RestoreOrdbogAsync(Guid id)
        {
            var ordbog = await _ordbogRepository.GetOrdbogByIdAsync(id);
            if (ordbog == null || !ordbog.IsDeleted) return false;

            ordbog.IsDeleted = false;
            ordbog.LastModified = DateTime.UtcNow;
            ordbog.ModifiedBy = "System";
            ordbog.Status = SyncStatus.Synced;
            ordbog.LastSyncedVersion++;

            ordbog.ChangeHistory.Add(new ChangeRecord
            {
                ChangedAt = DateTime.UtcNow,
                ChangedBy = ordbog.ModifiedBy,
                ChangeDescription = $"Restored Ordbog entry with ID: {ordbog.OrdbogId}"
            });

            ordbog.ChangeHistoryJson = JsonConvert.SerializeObject(ordbog.ChangeHistory);
            ordbog.ETag = GenerateETag(ordbog);

            return await _ordbogRepository.UpdateOrdbogAsync(ordbog);
        }
        public async Task<OrdbogDTO?> GetOrdbogByDanskOrdAsync(string danskOrd)
        {
            var ordbog = await _ordbogRepository.GetOrdbogByDanskOrdAsync(danskOrd);
            return ordbog == null ? null : _mapper.Map<OrdbogDTO>(ordbog);
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
