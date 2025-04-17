using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaekwondoApp.Shared.Models;
using TaekwondoApp.Shared.ServiceInterfaces;

namespace TaekwondoApp.Services
{
    public class BrugerSQLiteService : GenericSQLiteService<Bruger>, IBrugerSQLiteService
    {
        private readonly SQLiteConnection _database;

        public BrugerSQLiteService(string dbPath) : base(dbPath)
        {
            _database = new SQLiteConnection(dbPath);
            InitializeDatabase();
        }

        // Custom method to get a user by their email
        public async Task<Bruger> GetBrugerByEmailAsync(string email)
        {
            try
            {
                var bruger = _database.Table<Bruger>().FirstOrDefault(b => b.Email == email);
                return await Task.FromResult(bruger);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching user by email: {ex.Message}");
                throw;
            }
        }

        // Custom method to get a user by their Brugernavn
        public async Task<Bruger> GetBrugerByBrugernavnAsync(string brugernavn)
        {
            try
            {
                var bruger = _database.Table<Bruger>().FirstOrDefault(b => b.Brugernavn == brugernavn);
                return await Task.FromResult(bruger);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching user by brugernavn: {ex.Message}");
                throw;
            }
        }

        // Custom method to get all users with a specific Bæltegrad
        public async Task<List<Bruger>> GetBrugersByBæltegradAsync(string bæltegrad)
        {
            try
            {
                var brugers = _database.Table<Bruger>().Where(b => b.Bæltegrad == bæltegrad).ToList();
                return await Task.FromResult(brugers);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching users by Bæltegrad: {ex.Message}");
                throw;
            }
        }

        // Custom method to get all users with a specific role
        public async Task<List<Bruger>> GetBrugersByRoleAsync(string role)
        {
            try
            {
                var brugers = _database.Table<Bruger>().Where(b => b.Role == role).ToList();
                return await Task.FromResult(brugers);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching users by role: {ex.Message}");
                throw;
            }
        }

        // Custom method to get a user by their unique Brugerkode
        public async Task<Bruger> GetBrugerByBrugerkodeAsync(string brugerkode)
        {
            try
            {
                var bruger = _database.Table<Bruger>().FirstOrDefault(b => b.Brugerkode == brugerkode);
                return await Task.FromResult(bruger);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching user by brugerkode: {ex.Message}");
                throw;
            }
        }

        // Method to get all users who are currently part of a certain club
        public async Task<List<Bruger>> GetBrugersByKlubAsync(Guid klubId)
        {
            try
            {
                var brugers = _database.Table<BrugerKlub>()
                    .Where(bk => bk.KlubID == klubId) // assuming you have KlubId in the BrugerKlub model
                    .Select(bk => bk.Bruger)
                    .ToList();

                return await Task.FromResult(brugers);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching users by klub: {ex.Message}");
                throw;
            }
        }

        // Optionally, a method to load a user's related entities (e.g., programs, quizzes, etc.)
        public async Task<Bruger> GetBrugerWithRelatedDataAsync(Guid brugerId)
        {
            try
            {
                var bruger = await GetEntryByIdAsync(brugerId);
                if (bruger != null)
                {
                    // You could load the related entities such as:
                    // bruger.BrugerKlubber = GetBrugerKlubber(brugerId);
                    // Or you can ensure that the related data is fetched.
                    return bruger;
                }
                return null;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching user with related data: {ex.Message}");
                throw;
            }
        }
    }
}
