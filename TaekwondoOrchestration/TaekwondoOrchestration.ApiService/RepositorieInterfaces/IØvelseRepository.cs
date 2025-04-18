using TaekwondoApp.Shared.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TaekwondoOrchestration.ApiService.RepositorieInterfaces
{
    public interface IØvelseRepository
    {
        Task<List<Øvelse>> GetAllØvelserAsync(); // Fetch all Øvelser excluding deleted ones
        Task<Øvelse?> GetØvelseByIdAsync(Guid øvelseId); // Fetch Øvelse by Id excluding deleted ones
        Task<List<Øvelse>> GetØvelserBySværhedAsync(string sværhed); // Fetch Øvelser by difficulty excluding deleted ones
        Task<List<Øvelse>> GetØvelserByBrugerAsync(Guid brugerId); // Fetch Øvelser by BrugerId excluding deleted ones
        Task<List<Øvelse>> GetØvelserByKlubAsync(Guid klubId); // Fetch Øvelser by KlubId excluding deleted ones
        Task<List<Øvelse>> GetØvelserByNavnAsync(string navn); // Fetch Øvelser by name excluding deleted ones

        Task<Øvelse> CreateØvelseAsync(Øvelse øvelse); // Create a new Øvelse
        Task<bool> UpdateØvelseAsync(Øvelse øvelse); // Update an existing Øvelse
        Task<bool> DeleteØvelseAsync(Guid øvelseId); // Soft delete an Øvelse

        // Methods to handle soft deletes and fetch deleted entities
        Task<List<Øvelse>> GetAllØvelserIncludingDeletedAsync(); // Fetch all Øvelser including deleted ones
        Task<Øvelse?> GetØvelseByIdIncludingDeletedAsync(Guid id); // Fetch Øvelse by Id including deleted ones
        Task<Øvelse?> UpdateØvelseIncludingDeletedAsync(Guid id, Øvelse øvelse); // Update Øvelse including deleted ones
    }
}
