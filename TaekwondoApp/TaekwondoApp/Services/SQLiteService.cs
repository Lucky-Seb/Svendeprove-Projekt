using SQLite;
using TaekwondoApp.Shared.Services;
using System;
using System.IO;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using TaekwondoApp.Shared.DTO;

namespace TaekwondoApp.Services
{
    public class SQLiteService : ISQLiteService
    {
        private readonly SQLiteConnection _database;

        public SQLiteService(string dbPath)
        {
            _database = new SQLiteConnection(dbPath);
            InitializeDatabase(); // Ensure the database is initialized
        }

        public void InitializeDatabase()
        {
            _database.CreateTable<OrdbogDTO>();
        }

        public async Task<OrdbogDTO[]> GetAllEntriesAsync()
        {
            try
            {
                var list = _database.Table<OrdbogDTO>().ToList();
                return await Task.FromResult(list.ToArray()); // Safe for AOT/WinRT
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching entries: {ex.Message}");
                throw;
            }
        }

        public async Task<int> AddEntryAsync(OrdbogDTO entry)
        {
            try
            {
                // Make sure the entry's IsSync is correctly set (if it's not set from the Blazor component)
                if (entry.Status == SyncStatus.Pending)
                {
                    entry.Status = SyncStatus.Pending; // Default to Pending if it's not set
                }

                return await Task.Run(() => _database.Insert(entry));
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error adding entry: {ex.Message}");
                throw;
            }
        }

        public async Task<int> UpdateEntryAsync(OrdbogDTO entry)
        {
            try
            {
                return await Task.Run(() => _database.Update(entry));
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error updating entry: {ex.Message}");
                throw;
            }
        }

        // Physical deletion: deletes the entry from the database
        public async Task<int> DeleteEntryAsync(Guid OrdbogId)
        {
            try
            {
                var entry = _database.Table<OrdbogDTO>().FirstOrDefault(e => e.OrdbogId == OrdbogId);
                if (entry != null)
                {
                    return await Task.Run(() => _database.Delete(entry)); // Physically delete the record
                }
                return 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error deleting entry: {ex.Message}");
                throw;
            }
        }

        public async Task<OrdbogDTO> GetEntryByIdAsync(Guid OrdbogId)
        {
            try
            {
                return await Task.FromResult(_database.Table<OrdbogDTO>().FirstOrDefault(e => e.OrdbogId == OrdbogId && !e.IsDeleted));
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching entry by OrdbogId: {ex.Message}");
                throw;
            }
        }

        // Get only entries that are unsynced (Pending or Failed)
        public Task<OrdbogDTO[]> GetUnsyncedEntriesAsync()
        {
            try
            {
                var unsynced = _database
                    .Table<OrdbogDTO>()
                    .Where(e => e.Status == SyncStatus.Pending || e.Status == SyncStatus.Failed)
                    .ToArray();  // This is synchronous, no need for await

                return Task.FromResult(unsynced);  // Return as Task<OrdbogDTO[]>
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching unsynced entries: {ex.Message}");
                throw;
            }
        }

        // Mark an entry as synced after pushing to the server
        public async Task<int> MarkAsSyncedAsync(Guid OrdbogId)
        {
            try
            {
                var entry = _database.Table<OrdbogDTO>().FirstOrDefault(e => e.OrdbogId == OrdbogId);
                if (entry != null)
                {
                    entry.Status = SyncStatus.Synced; // Set Status to Synced
                    return await Task.Run(() => _database.Update(entry));
                }

                return 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error marking entry as synced: {ex.Message}");
                throw;
            }
        }

        // Mark an entry as failed (during sync failure)
        public async Task MarkAsFailedAsync(Guid OrdbogId)
        {
            try
            {
                var entry = _database.Table<OrdbogDTO>().FirstOrDefault(e => e.OrdbogId == OrdbogId);
                if (entry != null)
                {
                    entry.Status = SyncStatus.Failed; // Set Status to Failed
                    await Task.Run(() => _database.Update(entry));
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error marking entry as failed: {ex.Message}");
                throw;
            }
        }

        // Mark an entry as deleted (logical deletion)
        public async Task<int> MarkAsDeletedAsync(Guid OrdbogId)
        {
            try
            {
                var entry = _database.Table<OrdbogDTO>().FirstOrDefault(e => e.OrdbogId == OrdbogId);
                if (entry != null)
                {
                    entry.IsDeleted = true;  // Set IsDeleted flag to true
                    return await Task.Run(() => _database.Update(entry));  // Update the entry to mark as deleted
                }

                return 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error marking entry as deleted: {ex.Message}");
                throw;
            }
        }

        public async Task<int> UpdateEntryWithServerIdAsync(OrdbogDTO entry)
        {
            try
            {
                var existingEntry = _database.Table<OrdbogDTO>().FirstOrDefault(e => e.OrdbogId == entry.OrdbogId);
                if (existingEntry != null)
                {
                    // Update the entry with the server-assigned ID and sync status
                    existingEntry.DanskOrd = entry.DanskOrd;
                    existingEntry.KoranskOrd = entry.KoranskOrd;
                    existingEntry.Beskrivelse = entry.Beskrivelse;
                    existingEntry.BilledeLink = entry.BilledeLink;
                    existingEntry.LydLink = entry.LydLink;
                    existingEntry.VideoLink = entry.VideoLink;
                    existingEntry.Status = SyncStatus.Synced; // Mark as synced with server

                    return await Task.Run(() => _database.Update(existingEntry));
                }

                return 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error updating entry with server ID: {ex.Message}");
                throw;
            }
        }
    }
}
