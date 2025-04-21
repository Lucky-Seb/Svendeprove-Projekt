using AutoMapper;
using TaekwondoApp.Shared.DTO;
using TaekwondoApp.Shared.Models;
using TaekwondoOrchestration.ApiService.Helpers;
using TaekwondoOrchestration.ApiService.RepositorieInterfaces;
using TaekwondoOrchestration.ApiService.ServiceInterfaces;

namespace TaekwondoOrchestration.ApiService.Services
{
    public class BrugerQuizService : IBrugerQuizService
    {
        private readonly IBrugerQuizRepository _brugerQuizRepository;
        private readonly IMapper _mapper;

        public BrugerQuizService(IBrugerQuizRepository brugerQuizRepository, IMapper mapper)
        {
            _brugerQuizRepository = brugerQuizRepository;
            _mapper = mapper;
        }

        public async Task<Result<IEnumerable<BrugerQuizDTO>>> GetAllBrugerQuizzesAsync()
        {
            var brugerQuizzes = await _brugerQuizRepository.GetAllBrugerQuizzesAsync();
            var mapped = _mapper.Map<IEnumerable<BrugerQuizDTO>>(brugerQuizzes);
            return Result<IEnumerable<BrugerQuizDTO>>.Ok(mapped);
        }

        public async Task<Result<BrugerQuizDTO>> GetBrugerQuizByIdAsync(Guid brugerId, Guid quizId)
        {
            var brugerQuiz = await _brugerQuizRepository.GetBrugerQuizByIdAsync(brugerId, quizId);
            if (brugerQuiz == null)
                return Result<BrugerQuizDTO>.Fail("BrugerQuiz not found.");

            var mapped = _mapper.Map<BrugerQuizDTO>(brugerQuiz);
            return Result<BrugerQuizDTO>.Ok(mapped);
        }

        public async Task<BrugerQuizDTO?> CreateBrugerQuizAsync(BrugerQuizDTO brugerQuizDto)
        {
            if (brugerQuizDto == null || brugerQuizDto.BrugerID == Guid.Empty || brugerQuizDto.QuizID == Guid.Empty)
                return null;

            var brugerQuiz = _mapper.Map<BrugerQuiz>(brugerQuizDto);
            var created = await _brugerQuizRepository.CreateBrugerQuizAsync(brugerQuiz);
            return created == null ? null : _mapper.Map<BrugerQuizDTO>(created);
        }

        public async Task<Result<bool>> DeleteBrugerQuizAsync(Guid brugerId, Guid quizId)
        {
            var success = await _brugerQuizRepository.DeleteBrugerQuizAsync(brugerId, quizId);
            return success ? Result<bool>.Ok(true) : Result<bool>.Fail("Failed to delete BrugerQuiz.");
        }
    }
}