using TaekwondoApp.Shared.DTO;
using TaekwondoApp.Shared.Models;
using TaekwondoOrchestration.ApiService.Repositories;
using TaekwondoOrchestration.ApiService.RepositorieInterfaces;
using AutoMapper;
using TaekwondoOrchestration.ApiService.ServiceInterfaces;
using TaekwondoOrchestration.ApiService.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TaekwondoOrchestration.ApiService.Services
{
    public class BrugerØvelseService : IBrugerØvelseService
    {
        private readonly IBrugerØvelseRepository _brugerØvelseRepository;
        private readonly IMapper _mapper;

        // Constructor: Dependency Injection of Repository and Mapper
        public BrugerØvelseService(IBrugerØvelseRepository brugerØvelseRepository, IMapper mapper)
        {
            _brugerØvelseRepository = brugerØvelseRepository;
            _mapper = mapper;
        }

        #region CRUD Operations

        // Get All BrugerØvelser
        public async Task<Result<IEnumerable<BrugerØvelseDTO>>> GetAllBrugerØvelserAsync()
        {
            var brugerØvelser = await _brugerØvelseRepository.GetAllBrugerØvelserAsync();
            var mapped = _mapper.Map<IEnumerable<BrugerØvelseDTO>>(brugerØvelser);
            return Result<IEnumerable<BrugerØvelseDTO>>.Ok(mapped);
        }

        // Get BrugerØvelse by ID
        public async Task<Result<BrugerØvelseDTO>> GetBrugerØvelseByIdAsync(Guid brugerId, Guid øvelseId)
        {
            var brugerØvelse = await _brugerØvelseRepository.GetBrugerØvelseByIdAsync(brugerId, øvelseId);
            if (brugerØvelse == null)
                return Result<BrugerØvelseDTO>.Fail("BrugerØvelse not found.");

            var mapped = _mapper.Map<BrugerØvelseDTO>(brugerØvelse);
            return Result<BrugerØvelseDTO>.Ok(mapped);
        }

        // Create BrugerØvelse
        public async Task<Result<BrugerØvelseDTO>> CreateBrugerØvelseAsync(BrugerØvelseDTO brugerØvelseDto)
        {
            if (brugerØvelseDto == null)
                return Result<BrugerØvelseDTO>.Fail("Invalid BrugerØvelse data.");

            // Check if the BrugerØvelse already exists
            var existingBrugerØvelse = await _brugerØvelseRepository.GetBrugerØvelseByIdAsync(brugerØvelseDto.BrugerID, brugerØvelseDto.ØvelseID);
            if (existingBrugerØvelse != null)
            {
                return Result<BrugerØvelseDTO>.Fail("BrugerØvelse already exists.");
            }

            // Proceed with creating the new BrugerØvelse
            var newBrugerØvelse = _mapper.Map<BrugerØvelse>(brugerØvelseDto);
            var createdBrugerØvelse = await _brugerØvelseRepository.CreateBrugerØvelseAsync(newBrugerØvelse);

            if (createdBrugerØvelse == null)
                return Result<BrugerØvelseDTO>.Fail("Failed to create BrugerØvelse.");

            var mapped = _mapper.Map<BrugerØvelseDTO>(createdBrugerØvelse);
            return Result<BrugerØvelseDTO>.Ok(mapped);
        }

        // Delete BrugerØvelse
        public async Task<Result<bool>> DeleteBrugerØvelseAsync(Guid brugerId, Guid øvelseId)
        {
            var brugerØvelse = await _brugerØvelseRepository.GetBrugerØvelseByIdAsync(brugerId, øvelseId);
            if (brugerØvelse == null)
                return Result<bool>.Fail("BrugerØvelse not found.");

            var success = await _brugerØvelseRepository.DeleteBrugerØvelseAsync(brugerId, øvelseId);
            return success ? Result<bool>.Ok(true) : Result<bool>.Fail("Failed to delete BrugerØvelse.");
        }

        #endregion
    }
}
