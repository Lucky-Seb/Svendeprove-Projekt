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
    public class OrdbogSyncService : IOrdbogSyncService
    {
        private readonly ISQLiteService _sqliteService;
        private readonly HttpClient _httpClient;  // Declare HttpClient
        private readonly IGenericSyncService _syncService;

        // Modify constructor to accept IHttpClientFactory
        public OrdbogSyncService(IGenericSyncService syncService, ISQLiteService sqliteService, IHttpClientFactory httpClientFactory)
        {
            _syncService = syncService;
            _sqliteService = sqliteService;
            _httpClient = httpClientFactory.CreateClient();  // Initialize HttpClient using IHttpClientFactory
        }

        public async Task SyncOrdbogDataFromServerAsync()
        {
            await _syncService.SyncDataFromServerAsync<OrdbogDTO>(
                getServerData: async () => await _httpClient.GetFromJsonAsync<List<OrdbogDTO>>("https://localhost:7478/api/ordbog"),
                getLocalData: async (entry) => await _sqliteService.GetEntryByIdAsync(entry.OrdbogId),
                saveLocalData: async (entry) => await _sqliteService.AddEntryAsync(entry),
                updateLocalData: async (entry) => await _sqliteService.UpdateEntryAsync(entry)
            );
        }

        public async Task SyncOrdbogLocalChangesToServerAsync()
        {
            // Fetch unsynced entries (this is an array)
            var unsyncedEntries = await _sqliteService.GetUnsyncedEntriesAsync();

            // Pass the array as a List using .ToList()
            await _syncService.SyncLocalChangesToServerAsync<OrdbogDTO>(
                getUnsyncedEntries: () => Task.FromResult(unsyncedEntries.ToList()),  // Convert array to List<OrdbogDTO>
                postToServer: async (entry) => await _httpClient.PostAsJsonAsync("https://localhost:7478/api/ordbog", entry),
                markAsSynced: async (entry) => await _sqliteService.MarkAsSyncedAsync(entry.OrdbogId)
            );
        }
    }
}
