using TaekwondoApp.Shared.DTO;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TaekwondoOrchestration.ApiService.Helpers;

namespace TaekwondoOrchestration.ApiService.ServiceInterfaces
{
    public interface IBrugerØvelseService
    {
        // Get all BrugerØvelser
        Task<Result<IEnumerable<BrugerØvelseDTO>>> GetAllBrugerØvelserAsync();

        // Get BrugerØvelse by ID
        Task<Result<BrugerØvelseDTO>> GetBrugerØvelseByIdAsync(Guid brugerId, Guid øvelseId);

        // Create a new BrugerØvelse
        Task<Result<BrugerØvelseDTO>> CreateBrugerØvelseAsync(BrugerØvelseDTO brugerØvelseDto);

        // Delete a BrugerØvelse
        Task<Result<bool>> DeleteBrugerØvelseAsync(Guid brugerId, Guid øvelseId);
    }
}
