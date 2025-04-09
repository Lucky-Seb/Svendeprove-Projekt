using SQLite;
using TaekwondoApp.Shared.Services;
using System;
using System.IO;
using System.Threading.Tasks;
using System.Collections.Generic;
using TaekwondoApp.Shared.DTO;

namespace TaekwondoApp.Services
{
    public class SQLiteService : ISQLiteService
    {
        private readonly SQLiteConnection _database;

        public SQLiteService()
        {
            // Database path
            var dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "ordbog.db3");
            _database = new SQLiteConnection(dbPath);

            // Create table if it doesn't exist
            _database.CreateTable<OrdbogDTO>();
        }

        // Add a new entry to the database
        public async Task<int> AddEntryAsync(OrdbogDTO entry)
        {
            return await Task.FromResult(_database.Insert(entry));  // Insert entry asynchronously
        }

        // Get all entries from the database
        public async Task<List<OrdbogDTO>> GetAllEntriesAsync()
        {
            return await Task.FromResult(_database.Table<OrdbogDTO>().ToList());  // Fetch all entries asynchronously
        }

        // Delete an entry by ID
        public async Task<int> DeleteEntryAsync(int id)
        {
            return await Task.FromResult(_database.Table<OrdbogDTO>().Delete(entry => entry.Id == id));  // Delete by ID
        }

        // Update an existing entry
        public async Task<int> UpdateEntryAsync(OrdbogDTO entry)
        {
            return await Task.FromResult(_database.Update(entry));  // Update entry asynchronously
        }

        // Initialize the database by creating tables (if not already created)
        public void InitializeDatabase()
        {
            _database.CreateTable<OrdbogDTO>();  // Create table for OrdbogDTO
        }
    }
}
