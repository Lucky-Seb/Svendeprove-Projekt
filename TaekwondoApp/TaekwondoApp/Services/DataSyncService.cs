using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using TaekwondoApp.Library.DTO;
using Microsoft.Maui.Networking;

namespace TaekwondoApp.Services
{
    public class SyncService
    {
        private readonly SQLiteService _sqliteService;
        private readonly HttpClient _httpClient;

        public SyncService(SQLiteService sqliteService, HttpClient httpClient)
        {
            _sqliteService = sqliteService;
            _httpClient = httpClient;
        }

        // Method to sync local changes to the server
        public async Task SyncLocalChangesToServerAsync()
        {
            // Get the list of all local entries
            var localEntries = _sqliteService.GetAllEntries();

            // Post each local entry to the server
            foreach (var entry in localEntries)
            {
                var response = await _httpClient.PostAsJsonAsync("https://localhost:7478/api/ordbog", entry);

                if (response.IsSuccessStatusCode)
                {
                    // Successfully synced the entry, you may want to mark it as synced locally or delete it
                    // For simplicity, we'll just delete it here (you can extend this logic)
                    _sqliteService.DeleteEntry(entry.Id);
                }
            }
        }

        // Method to retrieve fresh data from the server and update the local SQLite DB
        public async Task SyncDataFromServerAsync()
        {
            var response = await _httpClient.GetFromJsonAsync<List<OrdbogDTO>>("https://localhost:7478/api/ordbog");

            if (response != null)
            {
                foreach (var entry in response)
                {
                    // Check if the entry exists locally, and if not, insert it
                    var localEntry = _sqliteService.GetAllEntries().FirstOrDefault(e => e.Id == entry.Id);
                    if (localEntry == null)
                    {
                        _sqliteService.AddOrdbogEntry(entry);
                    }
                }
            }
        }

        // Sync method if there's internet access
        public async Task SyncIfOnlineAsync()
        {
            if (Connectivity.NetworkAccess == NetworkAccess.Internet)
            {
                // Directly calling methods within this class, no need for a _syncService reference.
                await SyncLocalChangesToServerAsync(); // Sync local changes
                await SyncDataFromServerAsync(); // Sync from server
            }
        }
    }
}
