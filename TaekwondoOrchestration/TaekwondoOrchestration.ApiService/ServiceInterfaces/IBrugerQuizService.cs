using TaekwondoApp.Shared.DTO;
using TaekwondoApp.Shared.Models;
using TaekwondoOrchestration.ApiService.Helpers;

namespace TaekwondoOrchestration.ApiService.ServiceInterfaces
{
    public interface IBrugerQuizService
    {
        Task<Result<IEnumerable<BrugerQuizDTO>>> GetAllBrugerQuizzesAsync();
        Task<Result<BrugerQuizDTO>> GetBrugerQuizByIdAsync(Guid brugerId, Guid quizId);
        Task<BrugerQuizDTO?> CreateBrugerQuizAsync(BrugerQuizDTO brugerQuizDto);
        Task<Result<bool>> DeleteBrugerQuizAsync(Guid brugerId, Guid quizId);
    }
}
