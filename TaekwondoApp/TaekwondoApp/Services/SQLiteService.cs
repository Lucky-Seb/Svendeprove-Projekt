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

        public async Task<List<OrdbogDTO>> GetAllEntriesAsync()
        {
            try
            {
                return await Task.FromResult(_database.Table<OrdbogDTO>().ToList());
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
                if (entry.IsSync == null)
                {
                    entry.IsSync = false; // Default to false if it's not set
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

        public async Task<int> DeleteEntryAsync(int id)
        {
            try
            {
                // Log the ID being passed for deletion
                Console.WriteLine($"Attempting to delete entry with ID: {id}");
                var deletedCount = await Task.Run(() => _database.Delete<OrdbogDTO>(id));
                Console.WriteLine($"Deleted {deletedCount} entries with ID: {id}");
                return deletedCount;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error deleting entry: {ex.Message}");
                throw;
            }
        }


        public async Task<OrdbogDTO> GetEntryByIdAsync(int id)
        {
            try
            {
                return await Task.FromResult(_database.Table<OrdbogDTO>().FirstOrDefault(e => e.Id == id));
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching entry by id: {ex.Message}");
                throw;
            }
        }

        public async Task<List<OrdbogDTO>> GetUnsyncedEntriesAsync()
        {
            try
            {
                return await Task.FromResult(
                    _database.Table<OrdbogDTO>().Where(e => !e.IsSync).ToList()
                );
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching unsynced entries: {ex.Message}");
                throw;
            }
        }

        public async Task<int> MarkAsSyncedAsync(int id)
        {
            try
            {
                var entry = _database.Table<OrdbogDTO>().FirstOrDefault(e => e.Id == id);
                if (entry != null)
                {
                    entry.IsSync = true;
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
        public async Task<int> UpdateEntryWithServerIdAsync(OrdbogDTO entry)
        {
            try
            {
                var existingEntry = _database.Table<OrdbogDTO>().FirstOrDefault(e => e.Id == entry.Id);
                if (existingEntry != null)
                {
                    // Update the entry with the server-assigned ID and sync status
                    existingEntry.DanskOrd = entry.DanskOrd;
                    existingEntry.KoranskOrd = entry.KoranskOrd;
                    existingEntry.Beskrivelse = entry.Beskrivelse;
                    existingEntry.BilledeLink = entry.BilledeLink;
                    existingEntry.LydLink = entry.LydLink;
                    existingEntry.VideoLink = entry.VideoLink;
                    existingEntry.IsSync = true; // Mark as synced with server

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
