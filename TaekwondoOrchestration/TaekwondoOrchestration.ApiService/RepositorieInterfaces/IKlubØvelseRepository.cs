using TaekwondoOrchestration.ApiService.Models;

namespace TaekwondoOrchestration.ApiService.Repositories
{
    public interface IKlubØvelseRepository
    {
        Task<List<KlubØvelse>> GetAllKlubØvelserAsync();
        Task<KlubØvelse?> GetKlubØvelseByIdAsync(int klubId, int øvelseId);
        Task<KlubØvelse> CreateKlubØvelseAsync(KlubØvelse klubØvelse);
        Task<bool> DeleteKlubØvelseAsync(int klubId, int øvelseId);
    }
}
