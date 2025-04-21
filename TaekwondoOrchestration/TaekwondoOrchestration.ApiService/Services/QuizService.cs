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
        private readonly ISpørgsmålService _spørgsmålService;
        private readonly IBrugerQuizService _brugerQuizService;
        private readonly IKlubQuizService _klubQuizService;
        private readonly IMapper _mapper;

        public QuizService(IQuizRepository quizRepository, ISpørgsmålService spørgsmålService, IBrugerQuizService brugerQuizService, IKlubQuizService klubQuizService, IMapper mapper)
        {
            _quizRepository = quizRepository;
            _spørgsmålService = spørgsmålService;
            _brugerQuizService = brugerQuizService;
            _klubQuizService = klubQuizService;
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
            // Validate DTO
            if (string.IsNullOrEmpty(quizDto.QuizNavn))
            {
                return Result<QuizDTO>.Fail("Quiz name is required.");
            }

            var newQuiz = _mapper.Map<Quiz>(quizDto);
            EntityHelper.InitializeEntity(newQuiz, quizDto.ModifiedBy, "Created new Quiz.");

            // Create the Quiz
            var createdQuiz = await _quizRepository.CreateQuizAsync(newQuiz);

            // Create the appropriate relation (BrugerQuiz or KlubQuiz)
            if (quizDto.BrugerID.HasValue && quizDto.BrugerID.Value != Guid.Empty)
            {
                var brugerQuizDto = new BrugerQuizDTO
                {
                    BrugerID = quizDto.BrugerID.Value,
                    QuizID = createdQuiz.QuizID
                };

                var createdBrugerQuiz = await _brugerQuizService.CreateBrugerQuizAsync(brugerQuizDto);
                if (createdBrugerQuiz == null)
                {
                    return Result<QuizDTO>.Fail("Failed to create BrugerQuiz.");
                }
            }
            else if (quizDto.KlubID.HasValue && quizDto.KlubID.Value != Guid.Empty)
            {
                var klubQuizDto = new KlubQuizDTO
                {
                    KlubID = quizDto.KlubID.Value,
                    QuizID = createdQuiz.QuizID
                };

                var createdKlubQuiz = await _klubQuizService.CreateKlubQuizAsync(klubQuizDto);
                if (createdKlubQuiz == null)
                {
                    return Result<QuizDTO>.Fail("Failed to create KlubQuiz.");
                }
            }
            else
            {
                return Result<QuizDTO>.Fail("Either BrugerID or KlubID must be provided.");
            }

            // Create associated Spørgsmål entities
            if (quizDto.Spørgsmål != null && quizDto.Spørgsmål.Any())
            {
                foreach (var spørgsmålDto in quizDto.Spørgsmål)
                {
                    spørgsmålDto.QuizID = createdQuiz.QuizID; // Ensure correct foreign key
                    var spørgsmålResult = await _spørgsmålService.CreateSpørgsmålAsync(spørgsmålDto);
                    if (spørgsmålResult.Failure)
                    {
                        return Result<QuizDTO>.Fail($"Failed to create Spørgsmål. Error: {spørgsmålResult.Failure}");
                    }
                }
            }

            var mappedQuiz = _mapper.Map<QuizDTO>(createdQuiz);
            return Result<QuizDTO>.Ok(mappedQuiz);
        }

        // Update Existing Quiz
        public async Task<Result<QuizDTO>> UpdateQuizAsync(Guid quizId, QuizDTO quizDto)
        {
            // 1. Validate QuizNavn
            if (string.IsNullOrEmpty(updatedDto.QuizNavn))
                return Result<QuizDTO>.Fail("Quiz name is required.");

            // 2. Fetch existing Quiz from DB
            var existingQuiz = await _quizRepository.GetQuizByIdAsync(quizId);
            if (existingQuiz == null)
                return Result<QuizDTO>.Fail("Quiz not found.");

            // 3. Update Quiz fields
            existingQuiz.QuizNavn = updatedDto.QuizNavn;
            existingQuiz.QuizBeskrivelse = updatedDto.QuizBeskrivelse;
            existingQuiz.PensumID = updatedDto.PensumID;
            EntityHelper.InitializeEntity(existingQuiz, updatedDto.ModifiedBy, "Updated Quiz");

            await _quizRepository.UpdateQuizAsync(existingQuiz);

            // 4. Get existing spørgsmål from DB
            var spørgsmålResult = await _spørgsmålService.GetSpørgsmålByQuizIdAsync(quizId);
            if (spørgsmålResult.Failure)
            {
                return Result<QuizDTO>.Fail("Failed to retrieve existing spørgsmål.");
            }

            var existingSpørgsmål = spørgsmålResult.Value.ToList();
            var updatedSpørgsmål = updatedDto.Spørgsmål ?? new List<SpørgsmålDTO>();

            // 5. Handle Deletions
            var spørgsmålIdsToKeep = updatedSpørgsmål
                .Where(s => s.SpørgsmålID != Guid.Empty)
                .Select(s => s.SpørgsmålID)
                .ToHashSet();

            var spørgsmålToDelete = existingSpørgsmål
                .Where(es => !spørgsmålIdsToKeep.Contains(es.SpørgsmålID))
                .ToList();

            foreach (var spørgsmål in spørgsmålToDelete)
            {
                await _spørgsmålService.DeleteSpørgsmålAsync(spørgsmål.SpørgsmålID);
            }

            // 6. Handle Additions & Updates
            foreach (var spørgsmålDto in updatedSpørgsmål)
            {
                if (spørgsmålDto.SpørgsmålID == Guid.Empty)
                {
                    // New spørgsmål
                    spørgsmålDto.QuizID = updatedDto.QuizID;
                    var createResult = await _spørgsmålService.CreateSpørgsmålAsync(spørgsmålDto);
                    if (createResult.Failure)
                        return Result<QuizDTO>.Fail($"Failed to add spørgsmål: {createResult.Failure}");
                }
                else
                {
                    // Update existing spørgsmål
                    var existing = existingSpørgsmål.FirstOrDefault(s => s.SpørgsmålID == spørgsmålDto.SpørgsmålID);
                    if (existing != null)
                    {
                        await _spørgsmålService.UpdateSpørgsmålAsync(spørgsmålDto.SpørgsmålID, spørgsmålDto);
                    }
                }
            }

            // 7. Return updated QuizDTO
            var mappedQuiz = _mapper.Map<QuizDTO>(existingQuiz);
            return Result<QuizDTO>.Ok(mappedQuiz);
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
