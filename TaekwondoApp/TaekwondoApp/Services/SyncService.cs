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
        private readonly ISQLiteService _sqliteService;  // Updated interface for SQLiteService
        private readonly HttpClient _httpClient;

        public SyncService(ISQLiteService sqliteService, HttpClient httpClient)
        {
            _sqliteService = sqliteService;
            _httpClient = httpClient;
        }

        // Sync local changes to the server
        public async Task SyncLocalChangesToServerAsync()
        {
            var localEntries = await _sqliteService.GetAllEntriesAsync(); // Use async method to get entries

            foreach (var entry in localEntries)
            {
                var response = await _httpClient.PostAsJsonAsync("https://localhost:7478/api/ordbog", entry);

                if (response.IsSuccessStatusCode)
                {
                    await _sqliteService.DeleteEntryAsync(entry.Id);  // Use async delete method
                }
            }
        }

        // Sync data from the server to local SQLite
        public async Task SyncDataFromServerAsync()
        {
            var response = await _httpClient.GetFromJsonAsync<List<OrdbogDTO>>("https://localhost:7478/api/ordbog");

            if (response != null)
            {
                foreach (var entry in response)
                {
                    var localEntry = (await _sqliteService.GetAllEntriesAsync()).FirstOrDefault(e => e.Id == entry.Id);

                    if (localEntry == null)
                    {
                        await _sqliteService.AddEntryAsync(entry); // Use async add method
                    }
                }
            }
        }
    }
}
