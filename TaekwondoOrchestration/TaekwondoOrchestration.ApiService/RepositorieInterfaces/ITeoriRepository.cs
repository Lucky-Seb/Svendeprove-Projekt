using TaekwondoApp.Shared.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TaekwondoOrchestration.ApiService.RepositorieInterfaces
{
    public interface ITeoriRepository
    {
        // Get all Teori records
        Task<List<Teori>> GetAllTeoriAsync();

        // Get a Teori by its ID
        Task<Teori?> GetTeoriByIdAsync(Guid teoriId);

        // Get all Teori records by Pensum ID
        Task<List<Teori>> GetTeoriByPensumAsync(Guid pensumId);

        // Get a Teori by its name
        Task<Teori?> GetTeoriByTeoriNavnAsync(string teoriNavn);

        // Create a new Teori record
        Task CreateTeoriAsync(Teori teori);

        // Delete a Teori record by its ID
        Task<bool> DeleteTeoriAsync(Guid teoriId);

        // Update an existing Teori record
        Task<bool> UpdateTeoriAsync(Teori teori);
    }
}
