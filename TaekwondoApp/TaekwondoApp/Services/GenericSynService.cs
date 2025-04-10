using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using TaekwondoApp.Shared.Services;
using TaekwondoApp.Shared.Model;

namespace TaekwondoApp.Services
{
    public class GenericSyncService : IGenericSyncService
    {
        private readonly HttpClient _httpClient;
        private readonly ISQLiteService _sqliteService;

        public GenericSyncService(HttpClient httpClient, ISQLiteService sqliteService)
        {
            _httpClient = httpClient;
            _sqliteService = sqliteService;
        }

        public async Task SyncDataFromServerAsync<T>(Func<Task<List<T>>> getServerData, Func<T, Task<T>> getLocalData, Func<T, Task> saveLocalData, Func<T, Task> updateLocalData) where T : SyncableEntity
        {
            var serverEntries = await getServerData();

            foreach (var serverEntry in serverEntries)
            {
                var localEntry = await getLocalData(serverEntry);

                if (localEntry == null)
                {
                    // Entry doesn't exist locally, add it
                    await saveLocalData(serverEntry);
                }
                else
                {
                    // Conflict resolution based on timestamps
                    if (serverEntry.LastModified > localEntry.LastModified)
                    {
                        // Server data is newer
                        await updateLocalData(serverEntry);
                        serverEntry.ConflictStatus = ConflictResolutionStatus.ServerWins;
                    }
                    else if (serverEntry.LastModified < localEntry.LastModified)
                    {
                        // Local data is newer
                        await _httpClient.PostAsJsonAsync("https://localhost:7478/api/", localEntry);
                        localEntry.ConflictStatus = ConflictResolutionStatus.LocalWins;
                    }
                    else
                    {
                        // Timestamps are equal, no conflict
                        localEntry.ConflictStatus = ConflictResolutionStatus.NoConflict;
                    }
                    await updateLocalData(localEntry);
                }
            }
        }

        public async Task SyncLocalChangesToServerAsync<T>(Func<Task<List<T>>> getUnsyncedEntries, Func<T, Task> postToServer, Func<T, Task> markAsSynced) where T : SyncableEntity
        {
            var unsyncedEntries = await getUnsyncedEntries();

            foreach (var entry in unsyncedEntries)
            {
                var response = await _httpClient.PostAsJsonAsync("https://localhost:7478/api/", entry);

                if (response.IsSuccessStatusCode)
                {
                    await markAsSynced(entry);
                }
                else
                {
                    entry.ConflictStatus = ConflictResolutionStatus.ManualResolve; // Flag for manual resolution
                }
            }
        }
    }

}
