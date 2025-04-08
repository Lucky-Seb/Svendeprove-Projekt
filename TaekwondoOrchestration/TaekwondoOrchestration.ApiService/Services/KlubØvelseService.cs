﻿using Api.DTOs;
using Api.Models;
using Api.Repositories;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Services
{
    public class KlubØvelseService
    {
        private readonly IKlubØvelseRepository _klubØvelseRepository;

        public KlubØvelseService(IKlubØvelseRepository klubØvelseRepository)
        {
            _klubØvelseRepository = klubØvelseRepository;
        }

        public async Task<List<KlubØvelseDTO>> GetAllKlubØvelserAsync()
        {
            var klubØvelser = await _klubØvelseRepository.GetAllKlubØvelserAsync();
            return klubØvelser.Select(k => new KlubØvelseDTO
            {
                KlubID = k.KlubID,
                ØvelseID = k.ØvelseID
            }).ToList();
        }

        public async Task<KlubØvelseDTO?> GetKlubØvelseByIdAsync(int klubId, int øvelseId)
        {
            var klubØvelse = await _klubØvelseRepository.GetKlubØvelseByIdAsync(klubId, øvelseId);
            if (klubØvelse == null)
                return null;

            return new KlubØvelseDTO
            {
                KlubID = klubØvelse.KlubID,
                ØvelseID = klubØvelse.ØvelseID
            };
        }

        public async Task<KlubØvelseDTO?> CreateKlubØvelseAsync(KlubØvelseDTO klubØvelseDto)
        {
            // Check if the DTO is null
            if (klubØvelseDto == null) return null;

            // Validate required fields
            if (klubØvelseDto.KlubID <= 0) return null;  // KlubID must be a positive integer
            if (klubØvelseDto.ØvelseID <= 0) return null;  // ØvelseID must be a positive integer

            // Create new KlubØvelse entity
            var newKlubØvelse = new KlubØvelse
            {
                KlubID = klubØvelseDto.KlubID,
                ØvelseID = klubØvelseDto.ØvelseID
            };

            // Save the new KlubØvelse entity
            var createdKlubØvelse = await _klubØvelseRepository.CreateKlubØvelseAsync(newKlubØvelse);

            // Return the newly created KlubØvelseDTO
            return new KlubØvelseDTO
            {
                KlubID = createdKlubØvelse.KlubID,
                ØvelseID = createdKlubØvelse.ØvelseID
            };
        }


        public async Task<bool> DeleteKlubØvelseAsync(int klubId, int øvelseId)
        {
            return await _klubØvelseRepository.DeleteKlubØvelseAsync(klubId, øvelseId);
        }
    }
}
