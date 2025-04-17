using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TaekwondoApp.Shared.Models;

namespace TaekwondoApp.Shared.ServiceInterfaces
{
    public interface IBrugerSQLiteService
    {
        // Get a user by their email
        Task<Bruger> GetBrugerByEmailAsync(string email);

        // Get a user by their Brugernavn (username)
        Task<Bruger> GetBrugerByBrugernavnAsync(string brugernavn);

        // Get a list of users with a specific Bæltegrad (belt grade)
        Task<List<Bruger>> GetBrugersByBæltegradAsync(string bæltegrad);

        // Get a list of users with a specific role
        Task<List<Bruger>> GetBrugersByRoleAsync(string role);

        // Get a user by their unique Brugerkode
        Task<Bruger> GetBrugerByBrugerkodeAsync(string brugerkode);

        // Get a list of users who are part of a certain club
        Task<List<Bruger>> GetBrugersByKlubAsync(Guid klubId);

        // Get a user by their ID and load their related data (like clubs, programs, etc.)
        Task<Bruger> GetBrugerWithRelatedDataAsync(Guid brugerId);

        // Inherit methods from the GenericSQLiteService interface (if needed)
        Task<Bruger[]> GetAllEntriesAsync();
        Task<Bruger> GetEntryByIdAsync(Guid entryId);
        Task<int> AddEntryAsync(Bruger entry);
        Task<int> UpdateEntryAsync(Bruger entry);
        Task<int> DeleteEntryAsync(Guid entryId);
        Task<int> MarkAsSyncedAsync(Guid entryId);
        Task<int> MarkAsDeletedAsync(Guid entryId);
        Task<int> MarkAsRestoredAsync(Guid entryId);
        Task<List<Bruger>> GetLocallyDeletedEntriesAsync();
        Task<Bruger[]> GetUnsyncedEntriesAsync();
    }
}
