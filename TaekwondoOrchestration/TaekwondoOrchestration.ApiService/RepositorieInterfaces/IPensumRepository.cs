using TaekwondoOrchestration.ApiService.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TaekwondoOrchestration.ApiService.RepositorieInterfaces
{
    public interface IPensumRepository
    {
        Task<List<Pensum>> GetAllAsync();
        Task<Pensum?> GetByIdAsync(int id);
        Task<Pensum> CreateAsync(Pensum pensum);
        Task<bool> UpdateAsync(Pensum pensum);
        Task<bool> DeleteAsync(int id);
    }
}
