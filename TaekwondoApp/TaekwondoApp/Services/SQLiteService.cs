using SQLite;
using TaekwondoApp.Shared.Models;
using System;
using System.Linq;
using System.Threading.Tasks;
using TaekwondoApp.Shared.Services;
using Newtonsoft.Json;
using TaekwondoApp.Shared.DTO;

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
                return await Task.FromResult(_database.Table<Ordbog>().FirstOrDefault(e => e.OrdbogId == OrdbogId));
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
                // Ensure all necessary fields are set on the Ordbog entry
                if (entry.Status == SyncStatus.Synced)
                {
                    entry.Status = SyncStatus.Pending;  // Default sync status if not set
                }

                // If the entry is already present on the server (exists in the local database), don't generate new GUID
                if (entry.OrdbogId == Guid.Empty)
                {
                    entry.OrdbogId = Guid.NewGuid();  // Generate new Guid only for new entries
                }

                // If the entry was fetched from the server and it's marked as deleted, ensure its status is also deleted in local storage
                if (entry.IsDeleted)
                {
                    // Ensure that 'IsDeleted' is kept as true for deleted entries
                    entry.Status = SyncStatus.Synced;  // Mark as synced for deletion entities
                    entry.LastModified = DateTime.UtcNow; // Set current time for last modified
                }
                else
                {
                    // Ensure the normal fields for non-deleted entries
                    entry.LastSyncedVersion = 1;  // Set to 1 for new entries
                    entry.ETag = GenerateETag(entry);  // Generate a version identifier for the entry

                    // Set CreatedAt for new entries if it's not already set
                    if (entry.CreatedAt == default(DateTime))
                    {
                        entry.CreatedAt = DateTime.UtcNow;  // Setting CreatedAt for new entry
                    }

                    // Set the 'ModifiedBy' field (replace with actual user/device ID)
                    entry.ModifiedBy = entry.ModifiedBy;  // You can dynamically replace this with user/device ID

                    // Log the initial change (first entry creation)
                    LogChange(entry, "Initial entry creation");

                    // Default to no conflict
                    entry.ConflictStatus = ConflictResolutionStatus.NoConflict;
                }

                // Serialize ChangeHistory as a JSON string
                entry.ChangeHistoryJson = JsonConvert.SerializeObject(entry.ChangeHistory);

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

                // Set the 'ModifiedBy' field (replace with actual logic to track user/device)
                entry.ModifiedBy = entry.ModifiedBy; // You can replace this with actual user or device ID

                // Set ConflictStatus (no conflict by default)
                entry.ConflictStatus = ConflictResolutionStatus.NoConflict;

                // Logical deletion flag
                entry.IsDeleted = false;

                // Set the sync status to pending
                entry.Status = SyncStatus.Pending;

                // Update LastModified to the current time for updated entry
                entry.LastModified = DateTime.UtcNow;

                // Serialize ChangeHistory to JSON if it's not already set
                entry.ChangeHistoryJson = JsonConvert.SerializeObject(entry.ChangeHistory);

                // Log the change (this can be used for auditing or debugging)
                LogChange(entry, entry.IsDeleted ? "Marked as deleted" : "Updated entry");
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
                    return await Task.Run(() => _database.Delete(entry));  // Delete the entry in the database
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
            // Combine all relevant properties to generate a unique ETag
            var etagSource = $"{entry.OrdbogId}-{entry.DanskOrd}-{entry.KoranskOrd}-{entry.Beskrivelse}-{entry.BilledeLink}-{entry.LydLink}-{entry.VideoLink}";

            // Return a hash of the combined properties to generate the ETag
            return Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(etagSource));
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
                    entry.IsDeleted = true;
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
        public async Task<List<Ordbog>> GetLocallyDeletedEntriesAsync()
        {
            return await Task.Run(() =>
            {
                return _database.Table<Ordbog>()
                                .Where(e => e.IsDeleted == true && e.Status == SyncStatus.Deleted)
                                .ToList();
            });
        }
        public async Task MarkAsRestoredAsync(Guid OrdbogId)
        {
            var entry = _database.Table<Ordbog>().FirstOrDefault(e => e.OrdbogId == OrdbogId);
            if (entry != null)
            {
                entry.Status = SyncStatus.Synced;  // Mark status as Pending (for syncing again)
                entry.IsDeleted = false;            // Mark the entry as not deleted
                entry.LastModified = DateTime.UtcNow;  // Update LastModified to the current time
                entry.ConflictStatus = ConflictResolutionStatus.NoConflict;  // Reset any conflicts

                await Task.Run(() => _database.Update(entry));  // Update entry to restore
            }
        }

    }
}