using TaekwondoApp.Shared.DTO;
using TaekwondoApp.Shared.Models;
using TaekwondoOrchestration.ApiService.Repositories;
using TaekwondoOrchestration.ApiService.RepositorieInterfaces;
using AutoMapper;
using TaekwondoOrchestration.ApiService.ServiceInterfaces;
using TaekwondoOrchestration.ApiService.Helpers;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TaekwondoOrchestration.ApiService.Services
{
    public class BrugerKlubService : IBrugerKlubService
    {
        private readonly IBrugerKlubRepository _brugerKlubRepository;
        private readonly IMapper _mapper;

        // Constructor: Dependency Injection of Repository and Mapper
        public BrugerKlubService(IBrugerKlubRepository brugerKlubRepository, IMapper mapper)
        {
            _brugerKlubRepository = brugerKlubRepository;
            _mapper = mapper;
        }

        #region CRUD Operations

        // Get All BrugerKlubber
        public async Task<Result<IEnumerable<BrugerKlubDTO>>> GetAllBrugerKlubsAsync()
        {
            var brugerKlubber = await _brugerKlubRepository.GetAllBrugerKlubberAsync();
            var mapped = _mapper.Map<IEnumerable<BrugerKlubDTO>>(brugerKlubber);
            return Result<IEnumerable<BrugerKlubDTO>>.Ok(mapped);
        }

        // Get BrugerKlub by ID
        public async Task<Result<BrugerKlubDTO>> GetBrugerKlubByIdAsync(Guid brugerId, Guid klubId)
        {
            var brugerKlub = await _brugerKlubRepository.GetBrugerKlubByIdAsync(brugerId, klubId);
            if (brugerKlub == null)
                return Result<BrugerKlubDTO>.Fail("BrugerKlub not found.");

            var mapped = _mapper.Map<BrugerKlubDTO>(brugerKlub);
            return Result<BrugerKlubDTO>.Ok(mapped);
        }

        public async Task<Result<BrugerKlubDTO>> CreateBrugerKlubAsync(BrugerKlubDTO brugerKlubDto)
        {
            if (brugerKlubDto == null)
                return Result<BrugerKlubDTO>.Fail("Invalid BrugerKlub data.");

            // First, check if the user is already connected to the club
            var existingBrugerKlub = await _brugerKlubRepository.GetBrugerKlubByIdAsync(brugerKlubDto.BrugerID, brugerKlubDto.KlubID);

            // If the connection already exists, return a failure message
            if (existingBrugerKlub != null)
            {
                return Result<BrugerKlubDTO>.Fail("Bruger is already connected to this Klub.");
            }

            // Proceed with creating the new connection
            var newBrugerKlub = _mapper.Map<BrugerKlub>(brugerKlubDto);
            var createdBrugerKlub = await _brugerKlubRepository.CreateBrugerKlubAsync(newBrugerKlub);

            if (createdBrugerKlub == null)
                return Result<BrugerKlubDTO>.Fail("Failed to create BrugerKlub.");

            var mapped = _mapper.Map<BrugerKlubDTO>(createdBrugerKlub);
            return Result<BrugerKlubDTO>.Ok(mapped);
        }


        // Delete BrugerKlub
        public async Task<Result<bool>> DeleteBrugerKlubAsync(Guid brugerId, Guid klubId)
        {
            var brugerKlub = await _brugerKlubRepository.GetBrugerKlubByIdAsync(brugerId, klubId);
            if (brugerKlub == null)
                return Result<bool>.Fail("BrugerKlub not found.");

            var success = await _brugerKlubRepository.DeleteBrugerKlubAsync(brugerId, klubId);
            return success ? Result<bool>.Ok(true) : Result<bool>.Fail("Failed to delete BrugerKlub.");
        }

        #endregion
    }
}
