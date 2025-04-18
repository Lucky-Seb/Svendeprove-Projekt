using TaekwondoApp.Shared.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TaekwondoOrchestration.ApiService.RepositorieInterfaces
{
    public interface ITeoriRepository
    {
        // GET all Teori records with related data
        Task<List<Teori>> GetAllAsync();

        // GET a Teori by ID with related data
        Task<Teori?> GetByIdAsync(Guid teoriId);

        // GET a Teori by ID including deleted records (for soft deletes)
        Task<Teori?> GetByIdIncludingDeletedAsync(Guid teoriId);

        // GET all Teori records including deleted ones (for soft deletes)
        Task<List<Teori>> GetAllIncludingDeletedAsync();

        // CREATE a new Teori
        Task<Teori> CreateAsync(Teori teori);

        // UPDATE an existing Teori
        Task<bool> UpdateAsync(Teori teori);

        // DELETE a Teori
        Task<bool> DeleteAsync(Guid teoriId);

        // GET all Teori by PensumId (with related data)
        Task<List<Teori>> GetByPensumIdAsync(Guid pensumId);

        // GET Teori by TeoriNavn (for name search)
        Task<Teori?> GetByTeoriNavnAsync(string teoriNavn);

        // GET Teori by PensumId including deleted records (for soft deletes)
        Task<List<Teori>> GetByPensumIdIncludingDeletedAsync(Guid pensumId);
    }
}
