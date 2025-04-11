using Microsoft.EntityFrameworkCore;
using TaekwondoOrchestration.ApiService.Data;
using TaekwondoApp.Shared.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using TaekwondoOrchestration.ApiService.RepositorieInterfaces;

namespace TaekwondoOrchestration.ApiService.Repositories
{
    public class KlubØvelseRepository : IKlubØvelseRepository
    {
        private readonly ApiDbContext _context;

        public KlubØvelseRepository(ApiDbContext context)
        {
            _context = context;
        }

        public async Task<List<KlubØvelse>> GetAllKlubØvelserAsync()
        {
            return await _context.KlubØvelser.ToListAsync();
        }

        public async Task<KlubØvelse?> GetKlubØvelseByIdAsync(Guid klubId, Guid øvelseId)
        {
            return await _context.KlubØvelser
                .FirstOrDefaultAsync(k => k.KlubID == klubId && k.ØvelseID == øvelseId);
        }

        public async Task<KlubØvelse> CreateKlubØvelseAsync(KlubØvelse klubØvelse)
        {
            _context.KlubØvelser.Add(klubØvelse);
            await _context.SaveChangesAsync();
            return klubØvelse;
        }

        public async Task<bool> DeleteKlubØvelseAsync(Guid klubId, Guid øvelseId)
        {
            var klubØvelse = await _context.KlubØvelser
                .FirstOrDefaultAsync(k => k.KlubID == klubId && k.ØvelseID == øvelseId);
            if (klubØvelse == null)
                return false;

            _context.KlubØvelser.Remove(klubØvelse);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
