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
        public async Task SyncOrdbogDataFromServerAsync()
        {
            await _syncService.SyncDataFromServerAsync<OrdbogDTO>(
                getServerData: async () => await _httpClient.GetFromJsonAsync<List<OrdbogDTO>>("https://localhost:7478/api/ordbog"),

                // Map Ordbog to OrdbogDTO here
                getLocalData: async (entryDTO) =>
                {
                    // Fetch Ordbog from SQLite using its ID
                    var ordbogModel = await _sqliteService.GetEntryByIdAsync(entryDTO.OrdbogId);

                    // Map Ordbog model to OrdbogDTO
                    var mappedEntry = _mapper.Map<OrdbogDTO>(ordbogModel);

                    return mappedEntry;
                },

                saveLocalData: async (entryDTO) =>
                {
                    // Map OrdbogDTO to Ordbog model before saving
                    var ordbogModel = _mapper.Map<Ordbog>(entryDTO);
                    await _sqliteService.AddEntryAsync(ordbogModel);
                },

                updateLocalData: async (entryDTO) =>
                {
                    // Map OrdbogDTO to Ordbog model before updating
                    var ordbogModel = _mapper.Map<Ordbog>(entryDTO);
                    await _sqliteService.UpdateEntryAsync(ordbogModel);
                }
            );
        }

        public async Task SyncOrdbogLocalChangesToServerAsync()
        {
            // Fetch unsynced entries (these are Ordbog models)
            var unsyncedEntries = await _sqliteService.GetUnsyncedEntriesAsync();

            // Map Ordbog model entries to OrdbogDTO before passing to sync service
            var unsyncedEntriesDTO = _mapper.Map<List<OrdbogDTO>>(unsyncedEntries);

            // Pass the mapped List<OrdbogDTO> to the Sync service
            await _syncService.SyncLocalChangesToServerAsync<OrdbogDTO>(
                getUnsyncedEntries: () => Task.FromResult(unsyncedEntriesDTO),  // Return the List<OrdbogDTO> here
                postToServer: async (entryDTO) =>
                {
                    // Directly post OrdbogDTO to the server, no need to map to Ordbog model
                    await _httpClient.PostAsJsonAsync("https://localhost:7478/api/ordbog", entryDTO);
                },
                markAsSynced: async (entryDTO) => await _sqliteService.MarkAsSyncedAsync(entryDTO.OrdbogId)
            );
        }
    }
}
