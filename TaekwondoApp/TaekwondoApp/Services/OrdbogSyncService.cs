using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using TaekwondoApp.Shared.Models;
using TaekwondoApp.Shared.Services;
using TaekwondoApp.Shared.DTO;

namespace TaekwondoApp.Services
{
    public class OrdbogSyncService : IOrdbogSyncService
    {
        private readonly ISQLiteService _sqliteService;
        private readonly HttpClient _httpClient;
        private readonly IGenericSyncService _syncService;
        private readonly IMapper _mapper;  // Inject AutoMapper

        // Modify constructor to accept AutoMapper
        public OrdbogSyncService(IGenericSyncService syncService, ISQLiteService sqliteService, IHttpClientFactory httpClientFactory, IMapper mapper)
        {
            _syncService = syncService;
            _sqliteService = sqliteService;
            _httpClient = httpClientFactory.CreateClient();  // Initialize HttpClient using IHttpClientFactory
            _mapper = mapper;  // Inject AutoMapper
        }

        // Sync data from the server to the local database
        public async Task SyncDataAsync()
        {
            try
            {
                // Sync from server to local
                var serverData = await _httpClient.GetFromJsonAsync<List<OrdbogDTO>>("https://localhost:7478/api/ordbog");
                Console.WriteLine($"Fetched {serverData.Count} entries from the server.");

                foreach (var entryDTO in serverData)
                {
                    try
                    {
                        var localEntry = await _sqliteService.GetEntryByIdAsync(entryDTO.OrdbogId);

                        if (localEntry == null)
                        {
                            // No entry exists locally, add it
                            Console.WriteLine($"Adding new entry: {entryDTO.OrdbogId}");
                            var newEntry = _mapper.Map<Ordbog>(entryDTO);
                            await _sqliteService.AddEntryAsync(newEntry);
                            await _sqliteService.MarkAsSyncedAsync(newEntry.OrdbogId);  // Mark as synced locally

                        }
                        else
                        {
                            // Entry exists, check for differences and update if necessary
                            Console.WriteLine($"Checking for updates for entry: {entryDTO.OrdbogId}");

                            if (entryDTO.ETag != localEntry.ETag)
                            {
                                // ETag mismatch: compare LastModified dates
                                if (entryDTO.LastModified > localEntry.LastModified)
                                {
                                    // Server data is newer, update the local database
                                    var updatedEntry = _mapper.Map<Ordbog>(entryDTO);
                                    await _sqliteService.UpdateEntryAsync(updatedEntry);
                                    await _sqliteService.MarkAsSyncedAsync(entryDTO.OrdbogId);  // Mark as synced locally
                                }
                                else if (entryDTO.LastModified == localEntry.LastModified)
                                {
                                    Console.WriteLine($"Local data for {entryDTO.OrdbogId} has been updated at the samme time.");
                                }
                                else if (entryDTO.LastModified < localEntry.LastModified)
                                {

                                    // Local data is newer; handle accordingly (e.g., upload or notify user)
                                    Console.WriteLine($"Local data for {localEntry.OrdbogId} is more recent.");
                                    var updatedEntry = _mapper.Map<Ordbog>(localEntry);
                                    await _httpClient.PutAsJsonAsync($"https://localhost:7478/api/ordbog/{updatedEntry.OrdbogId}", updatedEntry);
                                    await _sqliteService.MarkAsSyncedAsync(entryDTO.OrdbogId);  // Mark as synced locally

                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Error processing server entry with ID {entryDTO.OrdbogId}: {ex.Message}");
                    }
                }

                // Sync from local to server (upload local changes)
                var unsyncedEntries = await _sqliteService.GetUnsyncedEntriesAsync();
                var unsyncedEntriesDTO = _mapper.Map<List<OrdbogDTO>>(unsyncedEntries);

                foreach (var entryDTO in unsyncedEntriesDTO)
                {
                    try
                    {
                        // Check if the entry already exists on the server using its OrdbogId
                        HttpResponseMessage response = await _httpClient.GetAsync($"https://localhost:7478/api/ordbog/{entryDTO.OrdbogId}");

                        if (response.IsSuccessStatusCode)
                        {
                            var etag = response.Headers.ETag?.ToString();
                            var existingEntry = await response.Content.ReadFromJsonAsync<OrdbogDTO>();

                            if (etag != null && entryDTO.ETag != etag)
                            {
                                // ETag mismatch, compare LastModified dates
                                if (entryDTO.LastModified > existingEntry.LastModified)
                                {
                                    // Local data is newer, update the server
                                    await _httpClient.PutAsJsonAsync("https://localhost:7478/api/ordbog", entryDTO);
                                    await _sqliteService.MarkAsSyncedAsync(entryDTO.OrdbogId);  // Mark as synced locally
                                }
                                else
                                {
                                    // Server data is newer; handle accordingly (e.g., fetch new data or notify user)
                                    Console.WriteLine($"Server data for {entryDTO.OrdbogId} is more recent.");
                                    // Server data is newer, update the local database
                                    var updatedEntry = _mapper.Map<Ordbog>(entryDTO);
                                    await _sqliteService.UpdateEntryAsync(updatedEntry);
                                    await _sqliteService.MarkAsSyncedAsync(entryDTO.OrdbogId);  // Mark as synced locally
                                }
                            }
                        }
                        else
                        {
                            // Entry does not exist on the server, so upload as new
                            await _httpClient.PostAsJsonAsync("https://localhost:7478/api/ordbog", entryDTO);
                            await _sqliteService.MarkAsSyncedAsync(entryDTO.OrdbogId);  // Mark as synced locally
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Error syncing local entry with ID {entryDTO.OrdbogId} to the server: {ex.Message}");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error during sync process: {ex.Message}");
            }
        }
    }
}
