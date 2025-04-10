using TaekwondoApp.Shared.DTO;
using TaekwondoOrchestration.ApiService.Models;
using TaekwondoOrchestration.ApiService.Repositories;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaekwondoOrchestration.ApiService.RepositorieInterfaces;

namespace TaekwondoOrchestration.ApiService.Services
{
    public class KlubProgramService
    {
        private readonly IKlubProgramRepository _klubProgramRepository;

        public KlubProgramService(IKlubProgramRepository klubProgramRepository)
        {
            _klubProgramRepository = klubProgramRepository;
        }

        public async Task<List<KlubProgramDTO>> GetAllKlubProgrammerAsync()
        {
            var klubProgrammer = await _klubProgramRepository.GetAllKlubProgrammerAsync();
            return klubProgrammer.Select(k => new KlubProgramDTO
            {
                KlubID = k.KlubID,
                ProgramID = k.ProgramID
            }).ToList();
        }

        public async Task<KlubProgramDTO?> GetKlubProgramByIdAsync(Guid klubId, Guid programId)
        {
            var klubProgram = await _klubProgramRepository.GetKlubProgramByIdAsync(klubId, programId);
            if (klubProgram == null)
                return null;

            return new KlubProgramDTO
            {
                KlubID = klubProgram.KlubID,
                ProgramID = klubProgram.ProgramID
            };
        }

        public async Task<KlubProgramDTO?> CreateKlubProgramAsync(KlubProgramDTO klubProgramDto)
        {
            // Check if the DTO is null
            if (klubProgramDto == null) return null;

            // Validate required fields
            //if (klubProgramDto.KlubID <= 0) return null;  // KlubID must be a positive integer
            //if (klubProgramDto.ProgramID <= 0) return null;  // ProgramID must be a positive integer

            // Create new KlubProgram entity
            var newKlubProgram = new KlubProgram
            {
                KlubID = klubProgramDto.KlubID,
                ProgramID = klubProgramDto.ProgramID
            };

            // Save the new KlubProgram entity
            var createdKlubProgram = await _klubProgramRepository.CreateKlubProgramAsync(newKlubProgram);

            // Return the newly created KlubProgramDTO
            return new KlubProgramDTO
            {
                KlubID = createdKlubProgram.KlubID,
                ProgramID = createdKlubProgram.ProgramID
            };
        }


        public async Task<bool> DeleteKlubProgramAsync(Guid klubId, Guid programId)
        {
            return await _klubProgramRepository.DeleteKlubProgramAsync(klubId, programId);
        }
    }
}
