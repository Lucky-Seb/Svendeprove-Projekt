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

        public SQLiteService(string dbPath)
        {
            _database = new SQLiteConnection(dbPath);
            InitializeDatabase(); // Ensure the database is initialized
        }

        public void InitializeDatabase()
        {
            // Create the table if it doesn't exist
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
                return await Task.Run(() => _database.Delete<OrdbogDTO>(id));
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error deleting entry: {ex.Message}");
                throw;
            }
        }
    }
}