using TaekwondoOrchestration.ApiService.Data;
using TaekwondoApp.Shared.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaekwondoOrchestration.ApiService.RepositorieInterfaces;

namespace TaekwondoOrchestration.ApiService.Repositories
{
    public class TeoriRepository : ITeoriRepository
    {
        private readonly ApiDbContext _context;

        public TeoriRepository(ApiDbContext context)
        {
            _context = context;
        }


        // Get all Teori records
        public async Task<List<Teori>> GetAllTeoriAsync()
        {
            return await _context.Teorier.ToListAsync();
        }

        // Get a Teori by its ID
        public async Task<Teori?> GetTeoriByIdAsync(Guid teoriId)
        {
            return await _context.Teorier.FindAsync(teoriId);
        }

        // Get all Teori records by Pensum ID
        public async Task<List<Teori>> GetTeoriByPensumAsync(Guid pensumId)
        {
            return await _context.Teorier
                                 .Where(t => t.PensumID == pensumId)
                                 .ToListAsync();
        }

        // Get a Teori by its name
        public async Task<Teori?> GetTeoriByTeoriNavnAsync(string teoriNavn)
        {
            return await _context.Teorier
                                 .FirstOrDefaultAsync(t => t.TeoriNavn.Equals(teoriNavn, StringComparison.OrdinalIgnoreCase));
        }

        // Create a new Teori record
        public async Task CreateTeoriAsync(Teori teori)
        {
            await _context.Teorier.AddAsync(teori);
            await _context.SaveChangesAsync();
        }

        // Delete a Teori record by its ID
        public async Task<bool> DeleteTeoriAsync(Guid teoriId)
        {
            var teori = await _context.Teorier.FindAsync(teoriId);
            if (teori == null) return false;

            _context.Teorier.Remove(teori);
            await _context.SaveChangesAsync();
            return true;
        }

        // Update an existing Teori record
        public async Task<bool> UpdateTeoriAsync(Teori teori)
        {
            var existingTeori = await _context.Teorier.FindAsync(teori.TeoriID);
            if (existingTeori == null) return false;

            _context.Entry(existingTeori).CurrentValues.SetValues(teori);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
