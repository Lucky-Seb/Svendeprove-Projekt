using TaekwondoOrchestration.ApiService.Models;

namespace TaekwondoOrchestration.ApiService.RepositorieInterfaces
{
    public interface IKlubØvelseRepository
    {
        Task<List<KlubØvelse>> GetAllKlubØvelserAsync();
        Task<KlubØvelse?> GetKlubØvelseByIdAsync(Guid klubId, Guid øvelseId);
        Task<KlubØvelse> CreateKlubØvelseAsync(KlubØvelse klubØvelse);
        Task<bool> DeleteKlubØvelseAsync(Guid klubId, Guid øvelseId);
    }
}
