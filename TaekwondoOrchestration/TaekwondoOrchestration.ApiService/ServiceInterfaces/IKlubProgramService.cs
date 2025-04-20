using TaekwondoApp.Shared.DTO;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TaekwondoOrchestration.ApiService.ServiceInterfaces
{
    public interface IKlubProgramService
    {
        // Asynchronously retrieves a list of all KlubProgrammer
        Task<List<KlubProgramDTO>> GetAllKlubProgrammerAsync();

        // Asynchronously retrieves a KlubProgram by its KlubID and ProgramID
        Task<KlubProgramDTO?> GetKlubProgramByIdAsync(Guid klubId, Guid programId);

        // Asynchronously creates a new KlubProgram and returns the created DTO
        Task<KlubProgramDTO?> CreateKlubProgramAsync(KlubProgramDTO klubProgramDto);

        // Asynchronously deletes a KlubProgram by its KlubID and ProgramID
        Task<bool> DeleteKlubProgramAsync(Guid klubId, Guid programId);
    }
}
