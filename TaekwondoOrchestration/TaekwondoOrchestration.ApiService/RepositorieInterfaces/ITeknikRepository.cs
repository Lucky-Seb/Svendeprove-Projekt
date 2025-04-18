using TaekwondoApp.Shared.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TaekwondoOrchestration.ApiService.RepositorieInterfaces
{
    public interface ITeknikRepository
    {
        // GET all Teknikker with related data
        Task<List<Teknik>> GetAllAsync();

        // GET a Teknik by ID with related data
        Task<Teknik?> GetByIdAsync(Guid teknikId);

        // GET a Teknik by ID including deleted records (for soft deletes)
        Task<Teknik?> GetByIdIncludingDeletedAsync(Guid teknikId);

        // GET all Teknikker including deleted records (for soft deletes)
        Task<List<Teknik>> GetAllIncludingDeletedAsync();

        // CREATE a new Teknik
        Task<Teknik> CreateAsync(Teknik teknik);

        // UPDATE an existing Teknik
        Task<bool> UpdateAsync(Teknik teknik);

        // DELETE a Teknik
        Task<bool> DeleteAsync(Guid teknikId);

        // GET Teknikker by PensumId (with related data)
        Task<List<Teknik>> GetByPensumIdAsync(Guid pensumId);

        // GET Teknik by TeknikNavn (for example, name search)
        Task<Teknik?> GetByTeknikNavnAsync(string teknikNavn);

        // GET Teknikker by PensumId including deleted records (for soft deletes)
        Task<List<Teknik>> GetByPensumIdIncludingDeletedAsync(Guid pensumId);

    }
}
