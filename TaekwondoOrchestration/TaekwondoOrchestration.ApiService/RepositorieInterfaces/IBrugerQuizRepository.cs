﻿using TaekwondoApp.Shared.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TaekwondoOrchestration.ApiService.RepositorieInterfaces
{
    public interface IBrugerQuizRepository
    {
        Task<List<BrugerQuiz>> GetAllBrugerQuizzesAsync();
        Task<BrugerQuiz?> GetBrugerQuizByIdAsync(Guid brugerId, Guid quizId);
        Task<BrugerQuiz> CreateBrugerQuizAsync(BrugerQuiz brugerQuiz);
        Task<bool> DeleteBrugerQuizAsync(Guid brugerId, Guid quizId);
    }
}

