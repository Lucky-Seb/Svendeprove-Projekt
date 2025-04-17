using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using TaekwondoApp.Shared.Models;
using TaekwondoApp.Shared.ServiceInterfaces;

namespace TaekwondoApp.Services
{
    public class GenericSQLiteService<T> : IGenericSQLiteService<T> where T : SyncableEntity, new()
    {
        private readonly SQLiteConnection _database;

        public GenericSQLiteService(string dbPath)
        {
            _database = new SQLiteConnection(dbPath);
            InitializeDatabase();
        }

        // Initialize the table dynamically based on the model type
        public void InitializeDatabase()
        {
            _database.CreateTable<T>();
        }

        // Get all entries of type T
        public async Task<T[]> GetAllEntriesAsync()
        {
            try
            {
                var list = _database.Table<T>().ToList();
                return await Task.FromResult(list.ToArray());
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching entries: {ex.Message}");
                throw;
            }
        }

        // Get a single entry by its primary key, dynamically determined using reflection
        public async Task<T> GetEntryByIdAsync(Guid entryId)
        {
            try
            {
                var primaryKeyProperty = GetPrimaryKeyProperty();
                var query = _database.Table<T>().FirstOrDefault(e => primaryKeyProperty.GetValue(e).Equals(entryId));
                return await Task.FromResult(query);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching entry by ID: {ex.Message}");
                throw;
            }
        }

        // Add a new entry
        public async Task<int> AddEntryAsync(T entry)
        {
            try
            {
                // Handle versioning, conflict detection, and setting default values from SyncableEntity
                if (entry.Status == SyncStatus.Synced)
                {
                    entry.Status = SyncStatus.Pending;  // Default sync status if not set
                }

                if (entry.CreatedAt == default(DateTime))
                {
                    entry.CreatedAt = DateTime.UtcNow;  // Set created date if not already set
                }

                // Ensure LastModified is always set when adding
                entry.LastModified = DateTime.UtcNow;

                return await Task.Run(() => _database.Insert(entry));
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error adding entry: {ex.Message}");
                throw;
            }
        }

        // Update an existing entry
        public async Task<int> UpdateEntryAsync(T entry)
        {
            try
            {
                // Handle versioning, conflict detection, and update logic
                entry.LastSyncedVersion++;
                entry.LastModified = DateTime.UtcNow;

                return await Task.Run(() => _database.Update(entry));
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error updating entry: {ex.Message}");
                throw;
            }
        }

        // Delete an entry by its primary key
        public async Task<int> DeleteEntryAsync(Guid entryId)
        {
            try
            {
                var primaryKeyProperty = GetPrimaryKeyProperty();
                var entry = _database.Table<T>().FirstOrDefault(e => primaryKeyProperty.GetValue(e).Equals(entryId));
                if (entry != null)
                {
                    return await Task.Run(() => _database.Delete(entry));
                }
                return 0;  // Entry not found
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error deleting entry: {ex.Message}");
                throw;
            }
        }

        // Mark an entry as synced
        public async Task<int> MarkAsSyncedAsync(Guid entryId)
        {
            try
            {
                var entry = await GetEntryByIdAsync(entryId);
                if (entry != null)
                {
                    entry.Status = SyncStatus.Synced;
                    return await Task.Run(() => _database.Update(entry));
                }
                return 0;  // Entry not found
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error marking entry as synced: {ex.Message}");
                throw;
            }
        }

        // Mark an entry as deleted
        public async Task<int> MarkAsDeletedAsync(Guid entryId)
        {
            try
            {
                var entry = await GetEntryByIdAsync(entryId);
                if (entry != null)
                {
                    entry.Status = SyncStatus.Deleted;
                    return await Task.Run(() => _database.Update(entry));
                }
                return 0;  // Entry not found
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error marking entry as deleted: {ex.Message}");
                throw;
            }
        }

        // Mark an entry as restored
        public async Task<int> MarkAsRestoredAsync(Guid entryId)
        {
            try
            {
                var entry = await GetEntryByIdAsync(entryId);
                if (entry != null)
                {
                    entry.Status = SyncStatus.Synced;
                    return await Task.Run(() => _database.Update(entry));
                }
                return 0;  // Entry not found
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error restoring entry: {ex.Message}");
                throw;
            }
        }

        // Retrieve locally deleted entries
        public async Task<List<T>> GetLocallyDeletedEntriesAsync()
        {
            try
            {
                var deletedEntries = _database.Table<T>().Where(e => e.IsDeleted).ToList();
                return await Task.FromResult(deletedEntries);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching deleted entries: {ex.Message}");
                throw;
            }
        }


        // Get unsynced entries
        public async Task<T[]> GetUnsyncedEntriesAsync()
        {
            try
            {
                var unsyncedEntries = _database.Table<T>()
                    .Where(e => e.Status == SyncStatus.Pending || e.Status == SyncStatus.Failed)
                    .ToArray();
                return await Task.FromResult(unsyncedEntries);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching unsynced entries: {ex.Message}");
                throw;
            }
        }

        // Helper method to retrieve the primary key property via reflection
        private PropertyInfo GetPrimaryKeyProperty()
        {
            // Assuming the primary key is annotated with [PrimaryKey]
            var primaryKeyProperty = typeof(T).GetProperties().FirstOrDefault(p => p.GetCustomAttribute<PrimaryKeyAttribute>() != null);
            if (primaryKeyProperty == null)
            {
                throw new Exception($"No primary key property found for {typeof(T).Name}");
            }
            return primaryKeyProperty;
        }
    }
}
