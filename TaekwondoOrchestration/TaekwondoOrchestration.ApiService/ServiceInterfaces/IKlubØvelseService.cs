using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TaekwondoApp.Shared.DTO;
using TaekwondoOrchestration.ApiService.Helpers;

namespace TaekwondoOrchestration.ApiService.ServiceInterfaces
{
    public interface IKlubØvelseService
    {
        // Get all KlubØvelser
        Task<Result<IEnumerable<KlubØvelseDTO>>> GetAllKlubØvelserAsync();

        // Get a single KlubØvelse by its composite ID (KlubID and ØvelseID)
        Task<Result<KlubØvelseDTO>> GetKlubØvelseByIdAsync(Guid klubId, Guid øvelseId);

        // Create a new KlubØvelse
        Task<Result<KlubØvelseDTO>> CreateKlubØvelseAsync(KlubØvelseDTO klubØvelseDto);

        // Delete a KlubØvelse by its composite ID
        Task<Result<bool>> DeleteKlubØvelseAsync(Guid klubId, Guid øvelseId);
    }
}
