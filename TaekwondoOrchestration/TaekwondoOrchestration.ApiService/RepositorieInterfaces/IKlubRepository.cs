using TaekwondoApp.Shared.Models;
using System.Threading.Tasks;
using System.Collections.Generic;
using TaekwondoApp.Shared.DTO;

namespace TaekwondoOrchestration.ApiService.RepositorieInterfaces
{
    public interface IKlubRepository
    {
        Task<List<Klub>> GetAllKlubberAsync();
        Task<Klub?> GetKlubByIdAsync(Guid klubId);
        Task<Klub?> GetKlubByNavnAsync(string klubNavn); // Add this method
        Task<Klub> CreateKlubAsync(Klub klub);
        Task<bool> UpdateKlubAsync(Klub klub);
        Task<bool> DeleteKlubAsync(Guid klubId);
        Task<KlubDTO?> GetKlubWithDetailsAsync(Guid klubId);
    }
}
