// In KlubRepository.cs
using Microsoft.EntityFrameworkCore;
using TaekwondoOrchestration.ApiService.Data;
using TaekwondoOrchestration.ApiService.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TaekwondoOrchestration.ApiService.Repositories
{
    public class KlubRepository : IKlubRepository
    {
        private readonly ApiDbContext _context;

        public KlubRepository(ApiDbContext context)
        {
            _context = context;
        }

        public async Task<List<Klub>> GetAllKlubberAsync()
        {
            return await _context.Klubber.ToListAsync();
        }

        public async Task<Klub?> GetKlubByIdAsync(int id)
        {
            return await _context.Klubber.FindAsync(id);
        }

        public async Task<Klub?> GetKlubByNavnAsync(string klubNavn)
        {
            return await _context.Klubber
                .FirstOrDefaultAsync(k => k.KlubNavn == klubNavn);
        }

        public async Task<Klub> CreateKlubAsync(Klub klub)
        {
            _context.Klubber.Add(klub);
            await _context.SaveChangesAsync();
            return klub;
        }

        public async Task<bool> UpdateKlubAsync(Klub klub)
        {
            _context.Klubber.Update(klub);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteKlubAsync(int id)
        {
            var klub = await _context.Klubber.FindAsync(id);
            if (klub == null) return false;

            _context.Klubber.Remove(klub);
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
