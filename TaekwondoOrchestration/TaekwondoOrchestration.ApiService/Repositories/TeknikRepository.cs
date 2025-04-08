using TaekwondoOrchestration.ApiService.Data;
using TaekwondoOrchestration.ApiService.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using TaekwondoOrchestration.ApiService.RepositorieInterfaces;

namespace TaekwondoOrchestration.ApiService.Repositories
{
    public class TeknikRepository : ITeknikRepository
    {
        private readonly ApiDbContext _context;

        public TeknikRepository(ApiDbContext context)
        {
            _context = context;
        }

        public async Task<List<Teknik>> GetAllTekniksAsync()
        {
            return await _context.Teknikker.ToListAsync();
        }

        public async Task<Teknik?> GetTeknikByIdAsync(int id)
        {
            return await _context.Teknikker.FindAsync(id);
        }

        public async Task<Teknik> CreateTeknikAsync(Teknik teknik)
        {
            _context.Teknikker.Add(teknik);
            await _context.SaveChangesAsync();
            return teknik;
        }

        public async Task<bool> DeleteTeknikAsync(int id)
        {
            var teknik = await _context.Teknikker.FindAsync(id);
            if (teknik == null)
                return false;

            _context.Teknikker.Remove(teknik);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> UpdateTeknikAsync(Teknik teknik)
        {
            _context.Entry(teknik).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return true;
        }
        public async Task<List<Teknik>> GetTekniksByPensumAsync(int pensumId)
        {
            return await _context.Teknikker
                                 .Where(t => t.PensumID == pensumId)
                                 .ToListAsync();
        }

        public async Task<Teknik?> GetTeknikByTeknikNavnAsync(string teknikNavn)
        {
            return await _context.Teknikker
                                 .FirstOrDefaultAsync(t => t.TeknikNavn == teknikNavn);
        }
    }
}
