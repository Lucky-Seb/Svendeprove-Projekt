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
            var dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "ordbog.db3");
            _database = new SQLiteConnection(dbPath);
            _database.CreateTable<OrdbogDTO>();
        }

        public async Task<int> AddEntryAsync(OrdbogDTO entry)
        {
            return await Task.FromResult(_database.Insert(entry));
        }

        public async Task<List<OrdbogDTO>> GetAllEntriesAsync()
        {
            return await Task.FromResult(_database.Table<OrdbogDTO>().ToList());
        }

        public void InitializeDatabase()
        {
            // You can initialize tables here
            _database.CreateTable<OrdbogDTO>();
        }
    }
}
