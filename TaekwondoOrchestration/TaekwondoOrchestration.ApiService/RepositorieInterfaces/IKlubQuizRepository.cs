using TaekwondoOrchestration.ApiService.Models;

namespace TaekwondoOrchestration.ApiService.RepositorieInterfaces
{
    public interface IKlubQuizRepository
    {
        Task<List<KlubQuiz>> GetAllKlubQuizzerAsync();
        Task<KlubQuiz?> GetKlubQuizByIdAsync(int klubId, int quizId);
        Task<KlubQuiz> CreateKlubQuizAsync(KlubQuiz klubQuiz);
        Task<bool> DeleteKlubQuizAsync(int klubId, int quizId);
    }
}
