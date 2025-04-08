using TaekwondoOrchestration.ApiService.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TaekwondoOrchestration.ApiService.Repositories
{
    public interface IBrugerQuizRepository
    {
        Task<List<BrugerQuiz>> GetAllBrugerQuizzesAsync();
        Task<BrugerQuiz?> GetBrugerQuizByIdAsync(int brugerId, int quizId);
        Task<BrugerQuiz> CreateBrugerQuizAsync(BrugerQuiz brugerQuiz);
        Task<bool> DeleteBrugerQuizAsync(int brugerId, int quizId);
    }
}

