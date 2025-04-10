using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaekwondoApp.Shared.DTO;
using TaekwondoApp.Shared.Services;
using System.Net.Http;
using System.Net.Http.Json;

namespace TaekwondoApp.Services
{
    public class SyncService : ISyncService
    {
        private readonly ISQLiteService _sqliteService;
        private readonly HttpClient _httpClient;

        public SyncService(ISQLiteService sqliteService, HttpClient httpClient)
        {
            _sqliteService = sqliteService;
            _httpClient = httpClient;
        }

        // Sync local unsynced changes to the server
        public async Task SyncLocalChangesToServerAsync()
        {
            var unsyncedEntries = await _sqliteService.GetUnsyncedEntriesAsync();

            foreach (var entry in unsyncedEntries)
            {
                var response = await _httpClient.PostAsJsonAsync("https://localhost:7478/api/ordbog", entry);

                if (response.IsSuccessStatusCode)
                {
                    // Mark as synced
                    await _sqliteService.MarkAsSyncedAsync(entry.OrdbogId);
                }
            }
        }

        // Sync new data from the server into local DB
        public async Task SyncDataFromServerAsync()
        {
            var response = await _httpClient.GetFromJsonAsync<List<OrdbogDTO>>("https://localhost:7478/api/ordbog");

            if (response != null)
            {
                foreach (var serverEntry in response)
                {
                    var localEntry = await _sqliteService.GetEntryByIdAsync(serverEntry.OrdbogId);

                    if (localEntry == null)
                    {
                        // Entry doesn't exist locally, add it as synced
                        serverEntry.IsSync = true;
                        await _sqliteService.AddEntryAsync(serverEntry);
                    }
                    else if (!localEntry.IsSync)
                    {
                        // Local entry exists but is not synced — skip updating to avoid overwriting local changes
                        continue;
                    }
                    else
                    {
                        // If entry exists and is synced, update it from server
                        serverEntry.IsSync = true;
                        await _sqliteService.UpdateEntryAsync(serverEntry);
                    }
                }
            }
        }
    }
}
