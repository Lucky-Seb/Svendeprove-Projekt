using TaekwondoOrchestration.ApiService.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TaekwondoOrchestration.ApiService.Repositories
{
    public interface ITeoriRepository
    {
        // Get all Teori records
        Task<List<Teori>> GetAllTeoriAsync();

        // Get a Teori by its ID
        Task<Teori?> GetTeoriByIdAsync(int id);

        // Get all Teori records by Pensum ID
        Task<List<Teori>> GetTeoriByPensumAsync(int pensumId);

        // Get a Teori by its name
        Task<Teori?> GetTeoriByTeoriNavnAsync(string teoriNavn);

        // Create a new Teori record
        Task CreateTeoriAsync(Teori teori);

        // Delete a Teori record by its ID
        Task<bool> DeleteTeoriAsync(int id);

        // Update an existing Teori record
        Task<bool> UpdateTeoriAsync(Teori teori);
    }
}
