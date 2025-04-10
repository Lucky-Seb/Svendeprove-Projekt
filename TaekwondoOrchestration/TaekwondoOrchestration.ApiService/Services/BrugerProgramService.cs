using TaekwondoApp.Shared.DTO;
using TaekwondoOrchestration.ApiService.Models;
using TaekwondoOrchestration.ApiService.Repositories;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaekwondoOrchestration.ApiService.RepositorieInterfaces;

namespace TaekwondoOrchestration.ApiService.Services
{
    public class BrugerProgramService
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
            // Check if the DTO is null
            if (brugerProgramDto == null) return null;

            //// Validate required fields
            //if (brugerProgramDto.BrugerID <= 0) return null;  // BrugerID must be a positive integer
            //if (brugerProgramDto.ProgramID <= 0) return null;  // ProgramID must be a positive integer

            // Create new BrugerProgram entity
            var newBrugerProgram = new BrugerProgram
            {
                BrugerID = brugerProgramDto.BrugerID,
                ProgramID = brugerProgramDto.ProgramID
            };

            // Save the new BrugerProgram entity to the repository
            var createdBrugerProgram = await _brugerProgramRepository.CreateBrugerProgramAsync(newBrugerProgram);
            if (createdBrugerProgram == null) return null;  // Return null if creation fails

            // Return the newly created BrugerProgramDTO
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
