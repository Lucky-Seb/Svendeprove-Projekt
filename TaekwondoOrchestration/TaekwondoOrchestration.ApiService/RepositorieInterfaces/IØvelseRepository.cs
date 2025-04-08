using TaekwondoOrchestration.ApiService.Models;

namespace TaekwondoOrchestration.ApiService.Repositories
{
    public interface IØvelseRepository
    {
        Task<List<Øvelse>> GetAllØvelserAsync();
        Task<Øvelse?> GetØvelseByIdAsync(int id);
        Task<List<Øvelse>> GetØvelserBySværhedAsync(string sværhed);
        Task<List<Øvelse>> GetØvelserByBrugerAsync(int brugerId);
        Task<List<Øvelse>> GetØvelserByKlubAsync(int klubId);
        Task<List<Øvelse>> GetØvelserByNavnAsync(string navn);
        Task<Øvelse> CreateØvelseAsync(Øvelse øvelse);
        Task<bool> DeleteØvelseAsync(int id);
        Task<bool> UpdateØvelseAsync(Øvelse øvelse);
    }
}
