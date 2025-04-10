﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TaekwondoApp.Shared.DTO;

namespace TaekwondoApp.Shared.Services
{
    public interface ISQLiteService
    {
        Task<int> AddEntryAsync(OrdbogDTO entry);
        Task<List<OrdbogDTO>> GetAllEntriesAsync();
        Task<int> DeleteEntryAsync(Guid OrdbogId);
        Task<int> UpdateEntryAsync(OrdbogDTO entry);
        Task<OrdbogDTO> GetEntryByIdAsync(Guid OrdbogId);
        void InitializeDatabase();
        Task<int> UpdateEntryWithServerIdAsync(OrdbogDTO entry);
        // NEW: Only get entries that need to be synced
        Task<List<OrdbogDTO>> GetUnsyncedEntriesAsync();

        // NEW: Mark an entry as synced after pushing to server
        Task<int> MarkAsSyncedAsync(Guid OrdbogId);
    }
}
