using TaekwondoApp.Shared.DTO;
using TaekwondoApp.Shared.Models;
using TaekwondoOrchestration.ApiService.Repositories;
using TaekwondoOrchestration.ApiService.RepositorieInterfaces;
using AutoMapper;
using TaekwondoOrchestration.ApiService.ServiceInterfaces;
using TaekwondoOrchestration.ApiService.Helpers;

namespace TaekwondoOrchestration.ApiService.Services
{
    public class QuizService : IQuizService
    {
        private readonly IQuizRepository _quizRepository;
        private readonly ISpørgsmålRepository _spørgsmålRepository;
        private readonly IBrugerQuizRepository _brugerQuizRepository;
        private readonly IKlubQuizRepository _klubQuizRepository;
        private readonly IMapper _mapper;

        public QuizService(IQuizRepository quizRepository, ISpørgsmålRepository spørgsmålRepository, IBrugerQuizRepository brugerQuizRepository, IKlubQuizRepository klubQuizRepository, IMapper mapper)
        {
            _quizRepository = quizRepository;
            _spørgsmålRepository = spørgsmålRepository;
            _brugerQuizRepository = brugerQuizRepository;
            _klubQuizRepository = klubQuizRepository;
            _mapper = mapper;
        }

        #region CRUD Operations

        // Get All Quizzes
        public async Task<Result<IEnumerable<QuizDTO>>> GetAllQuizzesAsync()
        {
            var quizzesList = await _quizRepository.GetAllQuizzesAsync();
            var mapped = _mapper.Map<IEnumerable<QuizDTO>>(quizzesList);
            return Result<IEnumerable<QuizDTO>>.Ok(mapped);
        }

        // Get Quiz by ID
        public async Task<Result<QuizDTO>> GetQuizByIdAsync(Guid quizId)
        {
            var quiz = await _quizRepository.GetQuizByIdAsync(quizId);
            if (quiz == null)
                return Result<QuizDTO>.Fail("Quiz not found.");

            var mapped = _mapper.Map<QuizDTO>(quiz);
            return Result<QuizDTO>.Ok(mapped);
        }

        // Create New Quiz
        public async Task<Result<QuizDTO>> CreateQuizAsync(QuizDTO quizDto)
        {
            // Validation if necessary
            if (string.IsNullOrEmpty(quizDto.QuizNavn))
            {
                return Result<QuizDTO>.Fail("Quiz Name is required.");
            }

            var newQuiz = _mapper.Map<Quiz>(quizDto);
            EntityHelper.InitializeEntity(newQuiz, quizDto.ModifiedBy, "Created new Quiz.");
            var createdQuiz = await _quizRepository.CreateQuizAsync(newQuiz);

            var mapped = _mapper.Map<QuizDTO>(createdQuiz);
            return Result<QuizDTO>.Ok(mapped);
        }

        // Update Existing Quiz
        public async Task<Result<QuizDTO>> UpdateQuizAsync(Guid quizId, QuizDTO quizDto)
        {
            if (string.IsNullOrEmpty(quizDto.QuizNavn))
            {
                return Result<QuizDTO>.Fail("Quiz Name is required.");
            }

            var existingQuiz = await _quizRepository.GetQuizByIdAsync(quizId);
            if (existingQuiz == null)
                return Result<QuizDTO>.Fail("Quiz not found.");

            _mapper.Map(quizDto, existingQuiz);
            EntityHelper.UpdateCommonFields(existingQuiz, quizDto.ModifiedBy);
            var updateSuccess = await _quizRepository.UpdateQuizAsync(existingQuiz);

            return updateSuccess ? Result<QuizDTO>.Ok(_mapper.Map<QuizDTO>(existingQuiz)) : Result<QuizDTO>.Fail("Failed to update Quiz.");
        }

        // Soft Delete Quiz
        public async Task<Result<bool>> DeleteQuizAsync(Guid quizId)
        {
            var quiz = await _quizRepository.GetQuizByIdAsync(quizId);
            if (quiz == null)
                return Result<bool>.Fail("Quiz not found.");

            // Soft delete logic
            string modifiedBy = quiz.ModifiedBy; // Assuming user context
            EntityHelper.SetDeletedOrRestoredProperties(quiz, "Soft-deleted Quiz", modifiedBy);

            var success = await _quizRepository.UpdateQuizAsync(quiz);
            return success ? Result<bool>.Ok(true) : Result<bool>.Fail("Failed to delete Quiz.");
        }

        // Restore Quiz from Soft-Delete
        public async Task<Result<bool>> RestoreQuizAsync(Guid quizId, QuizDTO quizDto)
        {
            var quiz = await _quizRepository.GetQuizByIdIncludingDeletedAsync(quizId);
            if (quiz == null || !quiz.IsDeleted)
                return Result<bool>.Fail("Quiz not found or not deleted.");

            quiz.IsDeleted = false;
            quiz.Status = SyncStatus.Synced;
            quiz.ModifiedBy = quizDto.ModifiedBy;
            quiz.LastSyncedVersion++;

            // Set properties for restored entry
            EntityHelper.SetDeletedOrRestoredProperties(quiz, "Restored Quiz", quizDto.ModifiedBy);

            var success = await _quizRepository.UpdateQuizAsync(quiz);
            return success ? Result<bool>.Ok(true) : Result<bool>.Fail("Failed to restore Quiz.");
        }

        #endregion

        #region Get Operations

        // Get Quizzes by User (Bruger)
        public async Task<Result<IEnumerable<QuizDTO>>> GetAllQuizzesByBrugerIdAsync(Guid brugerId)
        {
            var quizzes = await _quizRepository.GetAllQuizzesByBrugerAsync(brugerId);
            var mapped = _mapper.Map<IEnumerable<QuizDTO>>(quizzes);
            return Result<IEnumerable<QuizDTO>>.Ok(mapped);
        }

        // Get Quizzes by Club (Klub)
        public async Task<Result<IEnumerable<QuizDTO>>> GetAllQuizzesByKlubIdAsync(Guid klubId)
        {
            var quizzes = await _quizRepository.GetAllQuizzesByKlubAsync(klubId);
            var mapped = _mapper.Map<IEnumerable<QuizDTO>>(quizzes);
            return Result<IEnumerable<QuizDTO>>.Ok(mapped);
        }

        // Get All Quizzes
        public async Task<Result<IEnumerable<QuizDTO>>> GetAllQuizzesIncludingDeletedAsync()
        {
            var quizzes = await _quizRepository.GetAllQuizzesIncludingDeletedAsync();
            var mapped = _mapper.Map<IEnumerable<QuizDTO>>(quizzes);
            return Result<IEnumerable<QuizDTO>>.Ok(mapped);
        }

        // Get Quiz by ID
        public async Task<Result<QuizDTO>> GetQuizByIdIncludingDeletedAsync(Guid quizId)
        {
            var quiz = await _quizRepository.GetQuizByIdIncludingDeletedAsync(quizId);
            if (quiz == null)
                return Result<QuizDTO>.Fail("Quiz not found.");

            var mapped = _mapper.Map<QuizDTO>(quiz);
            return Result<QuizDTO>.Ok(mapped);
        }

        // Get Quizzes by Pensum
        public async Task<Result<IEnumerable<QuizDTO>>> GetAllQuizzesByPensumIdAsync(Guid pensumId)
        {
            var quizzes = await _quizRepository.GetAllQuizzesByPensumAsync(pensumId);
            var mapped = _mapper.Map<IEnumerable<QuizDTO>>(quizzes);
            return Result<IEnumerable<QuizDTO>>.Ok(mapped);
        }

        #endregion
    }
}
