using TaekwondoApp.Shared.DTO;
using TaekwondoApp.Shared.Models;
using TaekwondoOrchestration.ApiService.Repositories;
using TaekwondoOrchestration.ApiService.RepositorieInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaekwondoOrchestration.ApiService.ServiceInterfaces;

namespace TaekwondoOrchestration.ApiService.Services
{
    public class BrugerProgramService : IBrugerProgramService
    {
        private readonly IBrugerProgramRepository _brugerProgramRepository;

        public BrugerProgramService(IBrugerProgramRepository brugerProgramRepository)
        {
            _brugerProgramRepository = brugerProgramRepository;
        }

        public async Task<List<BrugerProgramDTO>> GetAllBrugerProgramsAsync()
        {
            var brugerPrograms = await _brugerProgramRepository.GetAllBrugerProgramsAsync();
            return brugerPrograms.Select(brugerProgram => new BrugerProgramDTO
            {
                BrugerID = brugerProgram.BrugerID,
                ProgramID = brugerProgram.ProgramID
            }).ToList();
        }

        public async Task<BrugerProgramDTO?> GetBrugerProgramByIdAsync(Guid brugerId, Guid programId)
        {
            var brugerProgram = await _brugerProgramRepository.GetBrugerProgramByIdAsync(brugerId, programId);
            if (brugerProgram == null)
                return null;

            return new BrugerProgramDTO
            {
                BrugerID = brugerProgram.BrugerID,
                ProgramID = brugerProgram.ProgramID
            };
        }

        public async Task<BrugerProgramDTO?> CreateBrugerProgramAsync(BrugerProgramDTO brugerProgramDto)
        {
            if (brugerProgramDto == null) return null;

            var newBrugerProgram = new BrugerProgram
            {
                BrugerID = brugerProgramDto.BrugerID,
                ProgramID = brugerProgramDto.ProgramID
            };

            var createdBrugerProgram = await _brugerProgramRepository.CreateBrugerProgramAsync(newBrugerProgram);
            if (createdBrugerProgram == null) return null;

            return new BrugerProgramDTO
            {
                BrugerID = createdBrugerProgram.BrugerID,
                ProgramID = createdBrugerProgram.ProgramID
            };
        }

        public async Task<bool> DeleteBrugerProgramAsync(Guid brugerId, Guid programId)
        {
            return await _brugerProgramRepository.DeleteBrugerProgramAsync(brugerId, programId);
        }
    }
}
