using AutoMapper;
using TaekwondoApp.Shared.DTO;
using TaekwondoApp.Shared.Models;
using TaekwondoOrchestration.ApiService.Helpers;
using TaekwondoOrchestration.ApiService.RepositorieInterfaces;
using TaekwondoOrchestration.ApiService.ServiceInterfaces;

namespace TaekwondoOrchestration.ApiService.Services
{
    public class KlubQuizService : IKlubQuizService
    {
        private readonly IKlubQuizRepository _klubQuizRepository;
        private readonly IMapper _mapper;

        public KlubQuizService(IKlubQuizRepository klubQuizRepository, IMapper mapper)
        {
            _klubQuizRepository = klubQuizRepository;
            _mapper = mapper;
        }

        public async Task<Result<IEnumerable<KlubQuizDTO>>> GetAllKlubQuizzerAsync()
        {
            var klubQuizzer = await _klubQuizRepository.GetAllKlubQuizzerAsync();
            var mapped = _mapper.Map<IEnumerable<KlubQuizDTO>>(klubQuizzer);
            return Result<IEnumerable<KlubQuizDTO>>.Ok(mapped);
        }

        public async Task<Result<KlubQuizDTO>> GetKlubQuizByIdAsync(Guid klubId, Guid quizId)
        {
            var klubQuiz = await _klubQuizRepository.GetKlubQuizByIdAsync(klubId, quizId);
            if (klubQuiz == null)
                return Result<KlubQuizDTO>.Fail("KlubQuiz not found.");

            var mapped = _mapper.Map<KlubQuizDTO>(klubQuiz);
            return Result<KlubQuizDTO>.Ok(mapped);
        }

        public async Task<KlubQuizDTO?> CreateKlubQuizAsync(KlubQuizDTO klubQuizDto)
        {
            if (klubQuizDto == null || klubQuizDto.KlubID == Guid.Empty || klubQuizDto.QuizID == Guid.Empty)
                return null;

            var klubQuiz = _mapper.Map<KlubQuiz>(klubQuizDto);
            var created = await _klubQuizRepository.CreateKlubQuizAsync(klubQuiz);
            return created == null ? null : _mapper.Map<KlubQuizDTO>(created);
        }

        public async Task<Result<bool>> DeleteKlubQuizAsync(Guid klubId, Guid quizId)
        {
            var success = await _klubQuizRepository.DeleteKlubQuizAsync(klubId, quizId);
            return success ? Result<bool>.Ok(true) : Result<bool>.Fail("Failed to delete KlubQuiz.");
        }
    }
}
