using AutoMapper;
using TaekwondoApp.Shared.DTO;
using TaekwondoApp.Shared.Models;
using TaekwondoOrchestration.ApiService.Helpers;
using TaekwondoOrchestration.ApiService.RepositorieInterfaces;
using TaekwondoOrchestration.ApiService.ServiceInterfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
            try
            {
                var quizzes = await _brugerQuizRepository.GetAllBrugerQuizzesAsync();

                if (quizzes == null || !quizzes.Any())
                {
                    return Result<IEnumerable<BrugerQuizDTO>>.Fail("No quizzes found.");
                }

                var mappedQuizzes = _mapper.Map<IEnumerable<BrugerQuizDTO>>(quizzes);
                return Result<IEnumerable<BrugerQuizDTO>>.Ok(mappedQuizzes);
            }
            catch (Exception ex)
            {
                // Log the exception (optional, depending on your logging strategy)
                return Result<IEnumerable<BrugerQuizDTO>>.Fail($"Error occurred: {ex.Message}");
            }
        }

        public async Task<Result<BrugerQuizDTO>> GetBrugerQuizByIdAsync(Guid brugerId, Guid quizId)
        {
            try
            {
                var brugerQuiz = await _brugerQuizRepository.GetBrugerQuizByIdAsync(brugerId, quizId);
                if (brugerQuiz == null)
                {
                    return Result<BrugerQuizDTO>.Fail("BrugerQuiz not found.");
                }

                var mapped = _mapper.Map<BrugerQuizDTO>(brugerQuiz);
                return Result<BrugerQuizDTO>.Ok(mapped);
            }
            catch (Exception ex)
            {
                return Result<BrugerQuizDTO>.Fail($"Error occurred: {ex.Message}");
            }
        }

        public async Task<Result<BrugerQuizDTO>> CreateBrugerQuizAsync(BrugerQuizDTO brugerQuizDto)
        {
            try
            {
                if (brugerQuizDto == null || brugerQuizDto.BrugerID == Guid.Empty || brugerQuizDto.QuizID == Guid.Empty)
                    return Result<BrugerQuizDTO>.Fail("Invalid data.");

                var brugerQuiz = _mapper.Map<BrugerQuiz>(brugerQuizDto);
                var created = await _brugerQuizRepository.CreateBrugerQuizAsync(brugerQuiz);

                if (created == null)
                    return Result<BrugerQuizDTO>.Fail("Failed to create BrugerQuiz.");

                var mapped = _mapper.Map<BrugerQuizDTO>(created);
                return Result<BrugerQuizDTO>.Ok(mapped);
            }
            catch (Exception ex)
            {
                return Result<BrugerQuizDTO>.Fail($"Error occurred: {ex.Message}");
            }
        }

        public async Task<Result<bool>> DeleteBrugerQuizAsync(Guid brugerId, Guid quizId)
        {
            try
            {
                var success = await _brugerQuizRepository.DeleteBrugerQuizAsync(brugerId, quizId);

                if (!success)
                {
                    return Result<bool>.Fail("Failed to delete BrugerQuiz.");
                }

                return Result<bool>.Ok(true);
            }
            catch (Exception ex)
            {
                return Result<bool>.Fail($"Error occurred: {ex.Message}");
            }
        }
    }
}
