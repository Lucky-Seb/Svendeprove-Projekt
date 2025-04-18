using TaekwondoApp.Shared.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TaekwondoOrchestration.ApiService.RepositorieInterfaces
{
    public interface IPensumRepository
    {
        Task<List<Pensum>> GetAllPensumAsync();
        Task<Pensum?> GetPensumByIdAsync(Guid pensumId);
        Task<List<Pensum>> GetAllPensumIncludingDeletedAsync();
        Task<Pensum?> GetPensumByIdIncludingDeletedAsync(Guid pensumId);
        Task<Pensum> CreatePensumAsync(Pensum pensum);
        Task<bool> UpdatePensumAsync(Pensum pensum);
        Task<bool> DeletePensumAsync(Guid pensumId);
        Task<Pensum?> UpdatePensumIncludingDeletedAsync(Guid pensumId, Pensum pensum);
    }
}
