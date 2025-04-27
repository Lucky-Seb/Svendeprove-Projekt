using AutoMapper;
using TaekwondoApp.Shared.DTO;
using TaekwondoApp.Shared.Models;
using TaekwondoOrchestration.ApiService.Helpers;
using TaekwondoOrchestration.ApiService.RepositorieInterfaces;
using TaekwondoOrchestration.ApiService.ServiceInterfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

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

        #region CRUD Operations

        // Get All KlubQuizzer
        public async Task<Result<IEnumerable<KlubQuizDTO>>> GetAllKlubQuizzerAsync()
        {
            var klubQuizzer = await _klubQuizRepository.GetAllKlubQuizzerAsync();
            var mapped = _mapper.Map<IEnumerable<KlubQuizDTO>>(klubQuizzer);
            return Result<IEnumerable<KlubQuizDTO>>.Ok(mapped);
        }

        // Get KlubQuiz by ID
        public async Task<Result<KlubQuizDTO>> GetKlubQuizByIdAsync(Guid klubId, Guid quizId)
        {
            var klubQuiz = await _klubQuizRepository.GetKlubQuizByIdAsync(klubId, quizId);
            if (klubQuiz == null)
                return Result<KlubQuizDTO>.Fail("KlubQuiz not found.");

            var mapped = _mapper.Map<KlubQuizDTO>(klubQuiz);
            return Result<KlubQuizDTO>.Ok(mapped);
        }

        // Create New KlubQuiz
        public async Task<Result<KlubQuizDTO>> CreateKlubQuizAsync(KlubQuizDTO klubQuizDto)
        {
            if (klubQuizDto == null || klubQuizDto.KlubID == Guid.Empty || klubQuizDto.QuizID == Guid.Empty)
                return Result<KlubQuizDTO>.Fail("Invalid KlubQuiz data.");

            var klubQuiz = _mapper.Map<KlubQuiz>(klubQuizDto);
            var createdKlubQuiz = await _klubQuizRepository.CreateKlubQuizAsync(klubQuiz);

            if (createdKlubQuiz == null)
                return Result<KlubQuizDTO>.Fail("Failed to create KlubQuiz.");

            var mapped = _mapper.Map<KlubQuizDTO>(createdKlubQuiz);
            return Result<KlubQuizDTO>.Ok(mapped);
        }

        // Delete KlubQuiz
        public async Task<Result<bool>> DeleteKlubQuizAsync(Guid klubId, Guid quizId)
        {
            var success = await _klubQuizRepository.DeleteKlubQuizAsync(klubId, quizId);
            if (!success)
                return Result<bool>.Fail("Failed to delete KlubQuiz.");

            return Result<bool>.Ok(true);
        }

        #endregion
    }
}
