using TaekwondoApp.Shared.DTO;
using TaekwondoApp.Shared.Models;
using TaekwondoOrchestration.ApiService.Helpers;

namespace TaekwondoOrchestration.ApiService.ServiceInterfaces
{
    public interface IKlubQuizService
    {
        Task<Result<IEnumerable<KlubQuizDTO>>> GetAllKlubQuizzerAsync();
        Task<Result<KlubQuizDTO>> GetKlubQuizByIdAsync(Guid klubId, Guid quizId);
        Task<KlubQuizDTO?> CreateKlubQuizAsync(KlubQuizDTO klubQuizDto);
        Task<Result<bool>> DeleteKlubQuizAsync(Guid klubId, Guid quizId);
    }
}
