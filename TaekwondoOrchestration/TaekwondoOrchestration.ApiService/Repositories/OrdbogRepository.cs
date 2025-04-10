﻿using Microsoft.EntityFrameworkCore;
using TaekwondoOrchestration.ApiService.Data;
using TaekwondoApp.Shared.Models;
using TaekwondoOrchestration.ApiService.RepositorieInterfaces;

namespace TaekwondoOrchestration.ApiService.Repositories
{
    public class OrdbogRepository : IOrdbogRepository
    {
        private readonly ApiDbContext _context;

        public OrdbogRepository(ApiDbContext context)
        {
            _context = context;
        }

        public async Task<List<Ordbog>> GetAllOrdbogAsync()
        {
            return await _context.Ordboger.ToListAsync();
        }

        public async Task<Ordbog?> GetOrdbogByIdAsync(Guid ordbogId)
        {
            return await _context.Ordboger.FindAsync(ordbogId);
        }

        public async Task<Ordbog> CreateOrdbogAsync(Ordbog ordbog)
        {
            _context.Ordboger.Add(ordbog);
            await _context.SaveChangesAsync();
            return ordbog;
        }

        public async Task<bool> UpdateOrdbogAsync(Ordbog ordbog)
        {
            _context.Entry(ordbog).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteOrdbogAsync(Guid ordbogId)
        {
            var ordbog = await _context.Ordboger.FindAsync(ordbogId);
            if (ordbog == null)
                return false;

            _context.Ordboger.Remove(ordbog);
            await _context.SaveChangesAsync();
            return true;
        }
        public async Task<Ordbog?> GetOrdbogByDanskOrdAsync(string danskOrd)
        {
            return await _context.Ordboger
                .Where(o => o.DanskOrd.Equals(danskOrd, StringComparison.OrdinalIgnoreCase))
                .FirstOrDefaultAsync();
        }

        public async Task<Ordbog?> GetOrdbogByKoranOrdAsync(string koranOrd)
        {
            return await _context.Ordboger
                .Where(o => o.KoranskOrd.Equals(koranOrd, StringComparison.OrdinalIgnoreCase))
                .FirstOrDefaultAsync();
        }
    }
}
