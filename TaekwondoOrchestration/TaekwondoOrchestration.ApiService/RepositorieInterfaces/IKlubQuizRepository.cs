using TaekwondoApp.Shared.Models;

namespace TaekwondoOrchestration.ApiService.RepositorieInterfaces
{
    public interface IKlubQuizRepository
    {
        Task<List<KlubQuiz>> GetAllKlubQuizzerAsync();
        Task<KlubQuiz?> GetKlubQuizByIdAsync(Guid klubId, Guid quizId);
        Task<KlubQuiz> CreateKlubQuizAsync(KlubQuiz klubQuiz);
        Task<bool> DeleteKlubQuizAsync(Guid klubId, Guid quizId);
    }
}
