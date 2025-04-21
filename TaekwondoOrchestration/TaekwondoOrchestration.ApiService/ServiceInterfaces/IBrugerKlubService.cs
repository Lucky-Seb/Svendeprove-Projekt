using TaekwondoApp.Shared.DTO;
using TaekwondoOrchestration.ApiService.Helpers;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TaekwondoApp.Shared.Helper;

namespace TaekwondoOrchestration.ApiService.ServiceInterfaces
{
    public interface IBrugerKlubService
    {
        // Get all BrugerKlubber
        Task<Result<IEnumerable<BrugerKlubDTO>>> GetAllBrugerKlubsAsync();

        // Get BrugerKlub by ID
        Task<Result<BrugerKlubDTO>> GetBrugerKlubByIdAsync(Guid brugerId, Guid klubId);

        // Create new BrugerKlub
        Task<Result<BrugerKlubDTO>> CreateBrugerKlubAsync(BrugerKlubDTO brugerKlubDto);

        // Delete BrugerKlub
        Task<Result<bool>> DeleteBrugerKlubAsync(Guid brugerId, Guid klubId);
        Task<Result<bool>> CheckIfUserIsAdminAsync(Guid brugerId, Guid klubId);

    }
}
