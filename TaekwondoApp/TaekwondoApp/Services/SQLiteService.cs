using SQLite;
using TaekwondoApp.Shared.Models;
using System;
using System.Linq;
using System.Threading.Tasks;
using TaekwondoApp.Shared.Services;

namespace TaekwondoApp.Services
{
    public class SQLiteService : ISQLiteService
    {
        private readonly SQLiteConnection _database;

        public SQLiteService(string dbPath)
        {
            _database = new SQLiteConnection(dbPath);
            InitializeDatabase();  // Ensure the database is initialized
        }

        public void InitializeDatabase()
        {
            _database.CreateTable<Ordbog>();  // Create table using the model Ordbog
        }

        // Fetch all entries (includes LastSyncedVersion, ETag, etc.)
        public async Task<Ordbog[]> GetAllEntriesAsync()
        {
            try
            {
                var list = _database.Table<Ordbog>().ToList();
                return await Task.FromResult(list.ToArray());  // Safe for AOT/WinRT
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching entries: {ex.Message}");
                throw;
            }
        }
        public async Task<Ordbog> GetEntryByIdAsync(Guid OrdbogId)
        {
            try
            {
                // Fetch the Ordbog entry by OrdbogId, excluding deleted entries
                return await Task.FromResult(_database.Table<Ordbog>().FirstOrDefault(e => e.OrdbogId == OrdbogId && !e.IsDeleted));
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching entry by OrdbogId: {ex.Message}");
                throw;
            }
        }

        // Add entry with versioning, syncing, and conflict detection
        public async Task<int> AddEntryAsync(Ordbog entry)
        {
            try
            {
                // Set default sync status and initialize version if not set
                if (entry.Status == SyncStatus.Pending)
                {
                    entry.Status = SyncStatus.Pending;
                }

                entry.LastSyncedVersion = 1;  // New entry, start at version 1
                entry.ETag = GenerateETag(entry);  // Generate an initial ETag
                entry.CreatedAt = DateTime.UtcNow;  // Setting CreatedAt for new entry
                // Set the 'ModifiedBy' field (could be a user or device ID)
                entry.ModifiedBy = "System"; // Replace with actual logic to track user/device

                // Log the initial change (first entry creation)
                LogChange(entry, "Initial entry creation");

                // Insert the new entry into the database
                return await Task.Run(() => _database.Insert(entry));
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error adding entry: {ex.Message}");
                throw;
            }
        }

        public async Task<int> UpdateEntryAsync(Ordbog entry)
        {
            try
            {
                // Increment version on every update
                entry.LastSyncedVersion++;

                // Update ETag based on version and content
                entry.ETag = GenerateETag(entry);

                // Set the 'ModifiedBy' field (could be a user or device ID)
                entry.ModifiedBy = "System"; // Replace with actual logic to track user/device

                // Set ConflictStatus (no conflict by default)
                entry.ConflictStatus = ConflictResolutionStatus.NoConflict;

                // Log the change
                LogChange(entry, "Updated entry");

                // Update the entry in the database
                return await Task.Run(() => _database.Update(entry));
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error updating entry: {ex.Message}");
                throw;
            }
        }



        // Delete entry (logical deletion)
        public async Task<int> DeleteEntryAsync(Guid OrdbogId)
        {
            try
            {
                // Get the Ordbog entry by its OrdbogId
                var entry = _database.Table<Ordbog>().FirstOrDefault(e => e.OrdbogId == OrdbogId);
                if (entry != null)
                {
                    entry.IsDeleted = true; // Mark the entry as deleted (logical deletion)
                    return await Task.Run(() => _database.Update(entry));  // Update the entry in the database
                }
                return 0; // Return 0 if entry was not found
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error marking entry as deleted: {ex.Message}");
                throw;
            }
        }

        // Generate ETag based on entity versioning and other fields
        private string GenerateETag(Ordbog entry)
        {
            return $"{entry.OrdbogId}-{entry.LastSyncedVersion}-{entry.DanskOrd}-{entry.KoranskOrd}";
        }

        // Get entries with unsynced status (Pending or Failed)
        public Task<Ordbog[]> GetUnsyncedEntriesAsync()
        {
            try
            {
                var unsynced = _database
                    .Table<Ordbog>()
                    .Where(e => e.Status == SyncStatus.Pending || e.Status == SyncStatus.Failed)
                    .ToArray();  // Synchronously get unsynced entries

                return Task.FromResult(unsynced);  // Return as Task<Ordbog[]>
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching unsynced entries: {ex.Message}");
                throw;
            }
        }

        // Mark an entry as synced
        public async Task<int> MarkAsSyncedAsync(Guid OrdbogId)
        {
            try
            {
                var entry = _database.Table<Ordbog>().FirstOrDefault(e => e.OrdbogId == OrdbogId);
                if (entry != null)
                {
                    entry.Status = SyncStatus.Synced;  // Mark as synced
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
                var entry = _database.Table<Ordbog>().FirstOrDefault(e => e.OrdbogId == OrdbogId);
                if (entry != null)
                {
                    entry.Status = SyncStatus.Failed;  // Mark as failed
                    entry.ConflictStatus = ConflictResolutionStatus.ManualResolve; // Set conflict status
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
                var entry = _database.Table<Ordbog>().FirstOrDefault(e => e.OrdbogId == OrdbogId);
                if (entry != null)
                {
                    entry.Status = SyncStatus.Deleted;  // Mark status as deleted
                    return await Task.Run(() => _database.Update(entry));  // Update entry to mark as deleted
                }

                return 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error marking entry as deleted: {ex.Message}");
                throw;
            }
        }

        // Update entry with server-assigned ID and status
        public async Task<int> UpdateEntryWithServerIdAsync(Ordbog entry)
        {
            try
            {
                var existingEntry = _database.Table<Ordbog>().FirstOrDefault(e => e.OrdbogId == entry.OrdbogId);
                if (existingEntry != null)
                {
                    // Update with server-assigned ID and sync status
                    existingEntry.DanskOrd = entry.DanskOrd;
                    existingEntry.KoranskOrd = entry.KoranskOrd;
                    existingEntry.Beskrivelse = entry.Beskrivelse;
                    existingEntry.BilledeLink = entry.BilledeLink;
                    existingEntry.LydLink = entry.LydLink;
                    existingEntry.VideoLink = entry.VideoLink;
                    existingEntry.Status = SyncStatus.Synced;

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
        private void LogChange(Ordbog entry, string changeDescription)
        {
            var changeRecord = new ChangeRecord
            {
                ChangedAt = DateTime.UtcNow,
                ChangedBy = entry.ModifiedBy,  // Who made the change (user/device)
                ChangeDescription = changeDescription
            };

            // Add the change record to the ChangeHistory of the entry
            entry.ChangeHistory.Add(changeRecord);
        }
    }
}