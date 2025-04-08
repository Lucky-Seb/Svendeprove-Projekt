using Api.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Api.Repositories
{
    public interface IBrugerKlubRepository
    {
        Task<List<BrugerKlub>> GetAllBrugerKlubberAsync();
        Task<BrugerKlub?> GetBrugerKlubByIdAsync(int brugerId, int klubId);
        Task<BrugerKlub> CreateBrugerKlubAsync(BrugerKlub brugerKlub);
        Task<bool> DeleteBrugerKlubAsync(int brugerId, int klubId);
    }
}
