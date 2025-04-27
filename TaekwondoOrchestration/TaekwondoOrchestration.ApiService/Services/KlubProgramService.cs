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
    public class KlubProgramService : IKlubProgramService
    {
        private readonly IKlubProgramRepository _klubProgramRepository;
        private readonly IMapper _mapper;

        // Constructor for Dependency Injection (Repository and Mapper)
        public KlubProgramService(IKlubProgramRepository klubProgramRepository, IMapper mapper)
        {
            _klubProgramRepository = klubProgramRepository;
            _mapper = mapper;
        }

        #region CRUD Operations

        // Get all KlubProgrammer
        public async Task<Result<IEnumerable<KlubProgramDTO>>> GetAllKlubProgrammerAsync()
        {
            var klubProgrammer = await _klubProgramRepository.GetAllKlubProgrammerAsync();
            if (klubProgrammer == null || !klubProgrammer.Any())
                return Result<IEnumerable<KlubProgramDTO>>.Fail("No KlubProgrammer found.");

            var mapped = _mapper.Map<IEnumerable<KlubProgramDTO>>(klubProgrammer);
            return Result<IEnumerable<KlubProgramDTO>>.Ok(mapped);
        }

        // Get specific KlubProgram by its IDs (KlubID, ProgramID)
        public async Task<Result<KlubProgramDTO>> GetKlubProgramByIdAsync(Guid klubId, Guid programId)
        {
            var klubProgram = await _klubProgramRepository.GetKlubProgramByIdAsync(klubId, programId);
            if (klubProgram == null)
                return Result<KlubProgramDTO>.Fail("KlubProgram not found.");

            var mapped = _mapper.Map<KlubProgramDTO>(klubProgram);
            return Result<KlubProgramDTO>.Ok(mapped);
        }

        // Create a new KlubProgram
        public async Task<Result<KlubProgramDTO>> CreateKlubProgramAsync(KlubProgramDTO klubProgramDto)
        {
            if (klubProgramDto == null)
                return Result<KlubProgramDTO>.Fail("Invalid input data.");

            var newKlubProgram = _mapper.Map<KlubProgram>(klubProgramDto);
            var createdKlubProgram = await _klubProgramRepository.CreateKlubProgramAsync(newKlubProgram);
            if (createdKlubProgram == null)
                return Result<KlubProgramDTO>.Fail("Failed to create KlubProgram.");

            var mapped = _mapper.Map<KlubProgramDTO>(createdKlubProgram);
            return Result<KlubProgramDTO>.Ok(mapped);
        }

        // Delete a KlubProgram by its IDs (KlubID, ProgramID)
        public async Task<Result<bool>> DeleteKlubProgramAsync(Guid klubId, Guid programId)
        {
            var klubProgram = await _klubProgramRepository.GetKlubProgramByIdAsync(klubId, programId);
            if (klubProgram == null)
                return Result<bool>.Fail("KlubProgram not found.");

            var success = await _klubProgramRepository.DeleteKlubProgramAsync(klubId, programId);
            return success ? Result<bool>.Ok(true) : Result<bool>.Fail("Failed to delete KlubProgram.");
        }

        #endregion
    }
}
