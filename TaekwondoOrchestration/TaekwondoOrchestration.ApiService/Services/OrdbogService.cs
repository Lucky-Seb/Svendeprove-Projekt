﻿using TaekwondoApp.Shared.DTO;
using TaekwondoApp.Shared.Models;
using TaekwondoOrchestration.ApiService.Repositories;
using TaekwondoOrchestration.ApiService.RepositorieInterfaces;
using AutoMapper;

namespace TaekwondoOrchestration.ApiService.Services
{
    public class OrdbogService
    {
        private readonly IOrdbogRepository _ordbogRepository;
        private readonly IMapper _mapper;  // Declare IMapper

        // Correct the constructor to inject IMapper
        public OrdbogService(IOrdbogRepository ordbogRepository, IMapper mapper)
        {
            _ordbogRepository = ordbogRepository;
            _mapper = mapper;  // Initialize IMapper via the constructor
        }

        public async Task<List<OrdbogDTO>> GetAllOrdbogAsync()
        {
            var ordboger = await _ordbogRepository.GetAllOrdbogAsync();
            return ordboger.Select(o => new OrdbogDTO
            {
                OrdbogId = o.OrdbogId,
                DanskOrd = o.DanskOrd,
                KoranskOrd = o.KoranskOrd,
                Beskrivelse = o.Beskrivelse,
                BilledeLink = o.BilledeLink,
                LydLink = o.LydLink,
                VideoLink = o.VideoLink
            }).ToList();
        }

        public async Task<OrdbogDTO?> GetOrdbogByIdAsync(Guid id)
        {
            var ordbog = await _ordbogRepository.GetOrdbogByIdAsync(id);
            if (ordbog == null)
                return null;

            return new OrdbogDTO
            {
                OrdbogId = ordbog.OrdbogId,
                DanskOrd = ordbog.DanskOrd,
                KoranskOrd = ordbog.KoranskOrd,
                Beskrivelse = ordbog.Beskrivelse,
                BilledeLink = ordbog.BilledeLink,
                LydLink = ordbog.LydLink,
                VideoLink = ordbog.VideoLink
            };
        }

        public async Task<OrdbogDTO> CreateOrdbogAsync(OrdbogDTO ordbogDto)
        {
            // Map DTO to Ordbog entity using AutoMapper
            var newOrdbog = _mapper.Map<Ordbog>(ordbogDto);

            // Manually set ETag for new entry
            newOrdbog.ETag = ordbogDto.DanskOrd + ordbogDto.KoranskOrd;

            // Create the Ordbog in the repository
            var createdOrdbog = await _ordbogRepository.CreateOrdbogAsync(newOrdbog);

            // Return the DTO back with the mapped values
            return _mapper.Map<OrdbogDTO>(createdOrdbog);
        }


        public async Task<bool> UpdateOrdbogAsync(Guid id, OrdbogDTO ordbogDto)
        {
            // Validate required fields
            if (string.IsNullOrEmpty(ordbogDto.DanskOrd)) return false;
            if (string.IsNullOrEmpty(ordbogDto.KoranskOrd)) return false;
            if (string.IsNullOrEmpty(ordbogDto.Beskrivelse)) return false;

            var existingOrdbog = await _ordbogRepository.GetOrdbogByIdAsync(id);
            if (existingOrdbog == null) return false;

            // Update fields
            existingOrdbog.DanskOrd = ordbogDto.DanskOrd;
            existingOrdbog.KoranskOrd = ordbogDto.KoranskOrd;
            existingOrdbog.Beskrivelse = ordbogDto.Beskrivelse;
            existingOrdbog.BilledeLink = ordbogDto.BilledeLink;
            existingOrdbog.LydLink = ordbogDto.LydLink;
            existingOrdbog.VideoLink = ordbogDto.VideoLink;

            // Regenerate ETag for updated entry
            existingOrdbog.ETag = GenerateETag(existingOrdbog);

            // Update LastModified field
            existingOrdbog.LastModified = DateTime.UtcNow;

            // You can set sync status based on your sync logic (e.g., mark it as 'Pending' until it's successfully synced)
            existingOrdbog.Status = SyncStatus.Pending;

            // Update conflict status as per the sync logic
            existingOrdbog.ConflictStatus = ConflictResolutionStatus.NoConflict;

            return await _ordbogRepository.UpdateOrdbogAsync(existingOrdbog);
        }


        public async Task<bool> DeleteOrdbogAsync(Guid id)
        {
            return await _ordbogRepository.DeleteOrdbogAsync(id);
        }

        // Get Ordbog by DanskOrd
        public async Task<OrdbogDTO?> GetOrdbogByDanskOrdAsync(string danskOrd)
        {
            var ordbog = await _ordbogRepository.GetOrdbogByDanskOrdAsync(danskOrd);
            if (ordbog == null)
                return null;

            return new OrdbogDTO
            {
                OrdbogId = ordbog.OrdbogId,
                DanskOrd = ordbog.DanskOrd,
                KoranskOrd = ordbog.KoranskOrd,
                Beskrivelse = ordbog.Beskrivelse,
                BilledeLink = ordbog.BilledeLink,
                LydLink = ordbog.LydLink,
                VideoLink = ordbog.VideoLink
            };
        }

        // Get Ordbog by KoranOrd
        public async Task<OrdbogDTO?> GetOrdbogByKoranOrdAsync(string koranOrd)
        {
            var ordbog = await _ordbogRepository.GetOrdbogByKoranOrdAsync(koranOrd);
            if (ordbog == null)
                return null;

            return new OrdbogDTO
            {
                OrdbogId = ordbog.OrdbogId,
                DanskOrd = ordbog.DanskOrd,
                KoranskOrd = ordbog.KoranskOrd,
                Beskrivelse = ordbog.Beskrivelse,
                BilledeLink = ordbog.BilledeLink,
                LydLink = ordbog.LydLink,
                VideoLink = ordbog.VideoLink
            };
        }
        private string GenerateETag(Ordbog entry)
        {
            return $"{entry.DanskOrd}-{entry.KoranskOrd}";
        }
    }
}
