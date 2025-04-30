using System;
using System.Threading.Tasks;
using TaekwondoApp.Shared.Models;

namespace TaekwondoApp.Shared.ServiceInterfaces
{
    public interface ISQLiteService
    {
        Task<int> AddEntryAsync(Ordbog entry);  // Use Ordbog instead of OrdbogDTO
        Task<Ordbog[]> GetAllEntriesAsync();    // Use Ordbog[] instead of OrdbogDTO[]
        Task<int> DeleteEntryAsync(Guid OrdbogId);  // Mark as deleted instead of physically deleting
        Task<int> UpdateEntryAsync(Ordbog entry);  // Use Ordbog instead of OrdbogDTO
        Task<Ordbog> GetEntryByIdAsync(Guid OrdbogId);  // Return Ordbog instead of OrdbogDTO
        void InitializeDatabase();
        Task<int> UpdateEntryWithServerIdAsync(Ordbog entry);  // Use Ordbog instead of OrdbogDTO

        // Get entries that need to be synced (Pending or Failed)
        Task<Ordbog[]> GetUnsyncedEntriesAsync();  // Use Ordbog[] instead of OrdbogDTO[]

        // Mark an entry as synced after pushing to the server
        Task<int> MarkAsSyncedAsync(Guid OrdbogId);

        // Mark an entry as failed when an error occurs during sync
        Task MarkAsFailedAsync(Guid OrdbogId);

        // Mark an entry as deleted (logical deletion)
        Task<int> MarkAsDeletedAsync(Guid OrdbogId);  // New method to mark entry as deleted
        Task<List<Ordbog>> GetLocallyDeletedEntriesAsync();
        Task MarkAsRestoredAsync(Guid OrdbogId);  // New method to mark entry as restored
    }
}
