using TaekwondoApp.Shared.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TaekwondoOrchestration.ApiService.RepositorieInterfaces
{
    public interface ITræningRepository
    {
        // GET all Træning records with related data
        Task<List<Træning>> GetAllAsync();

        // GET a Træning by ID with related data
        Task<Træning?> GetByIdAsync(Guid træningId);

        // GET a Træning by ID including deleted records (for soft deletes)
        Task<Træning?> GetByIdIncludingDeletedAsync(Guid træningId);

        // GET all Træning records including deleted ones (for soft deletes)
        Task<List<Træning>> GetAllIncludingDeletedAsync();

        // CREATE a new Træning
        Task<Træning> CreateAsync(Træning træning);

        // UPDATE an existing Træning
        Task<bool> UpdateAsync(Træning træning);

        // DELETE a Træning
        Task<bool> DeleteAsync(Guid træningId);

        // GET all Træning records by ProgramId (with related data)
        Task<List<Træning>> GetByProgramIdAsync(Guid programId);
    }
}
