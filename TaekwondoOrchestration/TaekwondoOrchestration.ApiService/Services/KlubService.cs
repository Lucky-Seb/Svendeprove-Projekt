using TaekwondoApp.Shared.DTO;
using TaekwondoApp.Shared.Models;
using TaekwondoOrchestration.ApiService.Repositories;
using TaekwondoOrchestration.ApiService.RepositorieInterfaces;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaekwondoOrchestration.ApiService.ServiceInterfaces;
using TaekwondoOrchestration.ApiService.Helpers;

namespace TaekwondoOrchestration.ApiService.Services
{
    public class KlubService : IKlubService
    {
        private readonly IKlubRepository _klubRepository;
        private readonly IMapper _mapper;

        // Constructor: Dependency Injection of Repository and Mapper
        public KlubService(IKlubRepository klubRepository, IMapper mapper)
        {
            _klubRepository = klubRepository;
            _mapper = mapper;
        }

        #region CRUD Operations

        // Get All Klubber
        public async Task<Result<IEnumerable<KlubDTO>>> GetAllKlubberAsync()
        {
            var klubber = await _klubRepository.GetAllKlubberAsync();
            var mapped = _mapper.Map<IEnumerable<KlubDTO>>(klubber);
            return Result<IEnumerable<KlubDTO>>.Ok(mapped);
        }

        // Get Klub by ID
        public async Task<Result<KlubDTO>> GetKlubByIdAsync(Guid id)
        {
            var klub = await _klubRepository.GetKlubByIdAsync(id);
            if (klub == null)
                return Result<KlubDTO>.Fail("Klub not found.");

            var mapped = _mapper.Map<KlubDTO>(klub);
            return Result<KlubDTO>.Ok(mapped);
        }

        // Get Klub by Navn
        public async Task<Result<KlubDTO>> GetKlubByNavnAsync(string klubNavn)
        {
            var klub = await _klubRepository.GetKlubByNavnAsync(klubNavn);
            if (klub == null)
                return Result<KlubDTO>.Fail("Klub not found.");

            var mapped = _mapper.Map<KlubDTO>(klub);
            return Result<KlubDTO>.Ok(mapped);
        }
        public async Task<Result<KlubDTO>> GetKlubWithDetailsAsync(Guid klubId)
        {
            var klubDTO = await _klubRepository.GetKlubWithDetailsAsync(klubId);

            if (klubDTO == null)
            {
                return Result<KlubDTO>.Fail("Klub not found.");
            }

            return Result<KlubDTO>.Ok(klubDTO);
        }

        // Create New Klub
        public async Task<Result<KlubDTO>> CreateKlubAsync(KlubDTO klubDto)
        {
            if (klubDto == null || string.IsNullOrEmpty(klubDto.KlubNavn))
                return Result<KlubDTO>.Fail("Invalid Klub data.");

            var newKlub = _mapper.Map<Klub>(klubDto);
            var createdKlub = await _klubRepository.CreateKlubAsync(newKlub);

            var mapped = _mapper.Map<KlubDTO>(createdKlub);
            return Result<KlubDTO>.Ok(mapped);
        }

        // Update Existing Klub
        public async Task<Result<bool>> UpdateKlubAsync(Guid id, KlubDTO klubDto)
        {
            if (string.IsNullOrWhiteSpace(klubDto.KlubNavn))
                return Result<bool>.Fail("Klub name cannot be empty.");

            var existingKlub = await _klubRepository.GetKlubByIdAsync(id);
            if (existingKlub == null)
                return Result<bool>.Fail("Klub not found.");

            _mapper.Map(klubDto, existingKlub);
            var updateSuccess = await _klubRepository.UpdateKlubAsync(existingKlub);

            return updateSuccess ? Result<bool>.Ok(true) : Result<bool>.Fail("Failed to update Klub.");
        }

        // Delete Klub
        public async Task<Result<bool>> DeleteKlubAsync(Guid id)
        {
            var klub = await _klubRepository.GetKlubByIdAsync(id);
            if (klub == null)
                return Result<bool>.Fail("Klub not found.");

            var success = await _klubRepository.DeleteKlubAsync(id);
            return success ? Result<bool>.Ok(true) : Result<bool>.Fail("Failed to delete Klub.");
        }

        #endregion

        #region Search Operations

        // Search Klubber by Name
        public async Task<Result<IEnumerable<KlubDTO>>> GetKlubberByNavnAsync(string klubNavn)
        {
            var klubber = await _klubRepository.GetKlubByNavnAsync(klubNavn);
            if (klubber == null)
                return Result<IEnumerable<KlubDTO>>.Fail("No Klubber found.");

            var mapped = _mapper.Map<IEnumerable<KlubDTO>>(klubber);
            return Result<IEnumerable<KlubDTO>>.Ok(mapped);
        }

        #endregion
    }
}
