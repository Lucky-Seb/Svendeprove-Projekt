using TaekwondoOrchestration.ApiService.Models;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace TaekwondoOrchestration.ApiService.Repositories
{
    public interface IKlubRepository
    {
        Task<List<Klub>> GetAllKlubberAsync();
        Task<Klub?> GetKlubByIdAsync(int id);
        Task<Klub?> GetKlubByNavnAsync(string klubNavn); // Add this method
        Task<Klub> CreateKlubAsync(Klub klub);
        Task<bool> UpdateKlubAsync(Klub klub);
        Task<bool> DeleteKlubAsync(int id);
    }
}
