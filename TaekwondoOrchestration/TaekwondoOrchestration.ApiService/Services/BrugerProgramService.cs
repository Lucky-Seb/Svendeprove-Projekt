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
    public class BrugerProgramService : IBrugerProgramService
    {
        private readonly IBrugerProgramRepository _brugerProgramRepository;
        private readonly IMapper _mapper;

        // Constructor: Dependency Injection of Repository and Mapper
        public BrugerProgramService(IBrugerProgramRepository brugerProgramRepository, IMapper mapper)
        {
            _brugerProgramRepository = brugerProgramRepository;
            _mapper = mapper;
        }

        #region CRUD Operations

        // Get All BrugerPrograms
        public async Task<Result<IEnumerable<BrugerProgramDTO>>> GetAllBrugerProgramsAsync()
        {
            var brugerPrograms = await _brugerProgramRepository.GetAllBrugerProgramsAsync();
            var mapped = _mapper.Map<IEnumerable<BrugerProgramDTO>>(brugerPrograms);
            return Result<IEnumerable<BrugerProgramDTO>>.Ok(mapped);
        }

        // Get BrugerProgram by ID
        public async Task<Result<BrugerProgramDTO>> GetBrugerProgramByIdAsync(Guid brugerId, Guid programId)
        {
            var brugerProgram = await _brugerProgramRepository.GetBrugerProgramByIdAsync(brugerId, programId);
            if (brugerProgram == null)
                return Result<BrugerProgramDTO>.Fail("BrugerProgram not found.");

            var mapped = _mapper.Map<BrugerProgramDTO>(brugerProgram);
            return Result<BrugerProgramDTO>.Ok(mapped);
        }

        // Create New BrugerProgram
        public async Task<Result<BrugerProgramDTO>> CreateBrugerProgramAsync(BrugerProgramDTO brugerProgramDto)
        {
            if (brugerProgramDto == null)
                return Result<BrugerProgramDTO>.Fail("Invalid BrugerProgram data.");

            // Check if BrugerProgram already exists
            var existingBrugerProgram = await _brugerProgramRepository.GetBrugerProgramByIdAsync(brugerProgramDto.BrugerID, brugerProgramDto.ProgramID);
            if (existingBrugerProgram != null)
                return Result<BrugerProgramDTO>.Fail("BrugerProgram already exists.");

            // Map the DTO to the BrugerProgram entity
            var newBrugerProgram = _mapper.Map<BrugerProgram>(brugerProgramDto);
            var createdBrugerProgram = await _brugerProgramRepository.CreateBrugerProgramAsync(newBrugerProgram);
            if (createdBrugerProgram == null)
                return Result<BrugerProgramDTO>.Fail("Failed to create BrugerProgram.");

            var mapped = _mapper.Map<BrugerProgramDTO>(createdBrugerProgram);
            return Result<BrugerProgramDTO>.Ok(mapped);
        }

        // Delete BrugerProgram
        public async Task<Result<bool>> DeleteBrugerProgramAsync(Guid brugerId, Guid programId)
        {
            var brugerProgram = await _brugerProgramRepository.GetBrugerProgramByIdAsync(brugerId, programId);
            if (brugerProgram == null)
                return Result<bool>.Fail("BrugerProgram not found.");

            var success = await _brugerProgramRepository.DeleteBrugerProgramAsync(brugerId, programId);
            return success ? Result<bool>.Ok(true) : Result<bool>.Fail("Failed to delete BrugerProgram.");
        }

        #endregion
    }
}
