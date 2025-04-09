using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;
using System.IO;
using TaekwondoApp.Library.DTO;

namespace TaekwondoApp.Services
{
    public class SQLiteService
    {
        private SQLiteConnection _database;
        private readonly string _dbPath;

        public SQLiteService()
        {
            // Set the SQLite database path (on the device storage)
            _dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "ordbog.db3");

            // Open the SQLite connection
            _database = new SQLiteConnection(_dbPath);

            // Create tables for storing offline data (this can be extended as needed)
            _database.CreateTable<OrdbogDTO>();
        }

        // Method to add new entry to the SQLite database
        public int AddOrdbogEntry(OrdbogDTO entry)
        {
            return _database.Insert(entry);
        }

        // Method to get all entries from SQLite database
        public List<OrdbogDTO> GetAllEntries()
        {
            return _database.Table<OrdbogDTO>().ToList();
        }

        // Method to delete entry by ID
        public int DeleteEntry(int id)
        {
            return _database.Table<OrdbogDTO>().Delete(entry => entry.Id == id);
        }

        // Method to update existing entry
        public int UpdateEntry(OrdbogDTO entry)
        {
            return _database.Update(entry);
        }
    }
}
