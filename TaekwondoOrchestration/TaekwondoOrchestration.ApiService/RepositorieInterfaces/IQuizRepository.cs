using TaekwondoApp.Shared.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TaekwondoOrchestration.ApiService.RepositorieInterfaces
{
    public interface IQuizRepository
    {
        Task<List<Quiz>> GetAllAsync();
        Task<Quiz?> GetByIdAsync(Guid quizId);
        Task<Quiz> CreateAsync(Quiz quiz);
        Task<bool> UpdateAsync(Quiz quiz);
        Task<bool> DeleteAsync(Guid quizId);
        Task<List<Quiz>> GetAllByBrugerAsync(Guid brugerId);
        Task<List<Quiz>> GetAllByKlubAsync(Guid klubId);
        Task<List<Quiz>> GetAllByPensumAsync(Guid pensumId);
    }
}
