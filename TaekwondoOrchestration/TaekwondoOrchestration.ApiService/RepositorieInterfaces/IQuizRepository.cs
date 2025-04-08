using TaekwondoOrchestration.ApiService.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TaekwondoOrchestration.ApiService.RepositorieInterfaces
{
    public interface IQuizRepository
    {
        Task<List<Quiz>> GetAllAsync();
        Task<Quiz?> GetByIdAsync(int id);
        Task<Quiz> CreateAsync(Quiz quiz);
        Task<bool> UpdateAsync(Quiz quiz);
        Task<bool> DeleteAsync(int id);
        Task<List<Quiz>> GetAllByBrugerAsync(int brugerId);
        Task<List<Quiz>> GetAllByKlubAsync(int klubId);
        Task<List<Quiz>> GetAllByPensumAsync(int pensumId);
    }
}
