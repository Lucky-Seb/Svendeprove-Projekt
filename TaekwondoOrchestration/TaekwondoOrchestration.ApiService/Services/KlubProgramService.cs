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
    public class KlubProgramService : IKlubProgramService
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
            if (klubProgramDto == null) return null;

            var newKlubProgram = new KlubProgram
            {
                KlubID = klubProgramDto.KlubID,
                ProgramID = klubProgramDto.ProgramID
            };

            var createdKlubProgram = await _klubProgramRepository.CreateKlubProgramAsync(newKlubProgram);

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
