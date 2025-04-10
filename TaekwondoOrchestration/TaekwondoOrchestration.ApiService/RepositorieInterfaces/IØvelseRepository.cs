using TaekwondoOrchestration.ApiService.Models;

namespace TaekwondoOrchestration.ApiService.RepositorieInterfaces
{
    public interface IØvelseRepository
    {
        Task<List<Øvelse>> GetAllØvelserAsync();
        Task<Øvelse?> GetØvelseByIdAsync(Guid øvelseId);
        Task<List<Øvelse>> GetØvelserBySværhedAsync(string sværhed);
        Task<List<Øvelse>> GetØvelserByBrugerAsync(Guid brugerId);
        Task<List<Øvelse>> GetØvelserByKlubAsync(Guid klubId);
        Task<List<Øvelse>> GetØvelserByNavnAsync(string navn);
        Task<Øvelse> CreateØvelseAsync(Øvelse øvelse);
        Task<bool> DeleteØvelseAsync(Guid øvelseId);
        Task<bool> UpdateØvelseAsync(Øvelse øvelse);
    }
}
