using Microsoft.EntityFrameworkCore;
using TaekwondoOrchestration.ApiService.Data;
using TaekwondoOrchestration.ApiService.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaekwondoOrchestration.ApiService.DTO;

namespace TaekwondoOrchestration.ApiService.Repositories
{
    public class BrugerRepository : IBrugerRepository
    {
        private readonly ApiDbContext _context;

        public BrugerRepository(ApiDbContext context)
        {
            _context = context;
        }

        public async Task<List<Bruger>> GetAllBrugereAsync()
        {
            return await _context.Brugere.ToListAsync();
        }

        public async Task<Bruger?> GetBrugerByIdAsync(int id)
        {
            return await _context.Brugere.FindAsync(id);
        }

        public async Task<List<Bruger>> GetBrugerByRoleAsync(string role)
        {
            return await _context.Brugere
                .Where(b => b.Role == role)
                .ToListAsync();
        }

        public async Task<List<Bruger>> GetBrugerByBælteAsync(string bæltegrad)
        {
            return await _context.Brugere
                .Where(b => b.Bæltegrad == bæltegrad)
                .ToListAsync();
        }

        public async Task<List<BrugerDTO>> GetBrugereByKlubAsync(int klubId)
        {
            var result = await _context.Brugere
                .Join(_context.BrugerKlubber,
                      b => b.BrugerID,
                      bk => bk.BrugerID,
                      (b, bk) => new { b, bk })
                .Join(_context.Klubber,
                      temp => temp.bk.KlubID,
                      k => k.KlubID,
                      (temp, k) => new { temp.b, temp.bk, k })
                .Where(x => x.k.KlubID == klubId)
                .Select(x => new BrugerDTO
                {
                    BrugerID = x.b.BrugerID,
                    Email = x.b.Email,
                    Brugernavn = x.b.Brugernavn,
                    Fornavn = x.b.Fornavn,
                    Efternavn = x.b.Efternavn,
                    Brugerkode = x.b.Brugerkode,
                    Bæltegrad = x.b.Bæltegrad,
                    Address = x.b.Address,
                    Role = x.b.Role,
                    Klub = new KlubDTO
                    {
                        KlubID = x.k.KlubID,
                        KlubNavn = x.k.KlubNavn
                    }
                })
                .ToListAsync();

            return result;
        }

        // Get all Brugere by KlubID and Bæltegrad
        public async Task<List<BrugerDTO>> GetBrugereByKlubAndBæltegradAsync(int klubId, string bæltegrad)
        {
            var result = await _context.Brugere
                .Join(_context.BrugerKlubber,
                      b => b.BrugerID,
                      bk => bk.BrugerID,
                      (b, bk) => new { b, bk })
                .Join(_context.Klubber,
                      temp => temp.bk.KlubID,
                      k => k.KlubID,
                      (temp, k) => new { temp.b, temp.bk, k })
                .Where(x => x.k.KlubID == klubId && x.b.Bæltegrad == bæltegrad)
                .Select(x => new BrugerDTO
                {
                    BrugerID = x.b.BrugerID,
                    Email = x.b.Email,
                    Brugernavn = x.b.Brugernavn,
                    Fornavn = x.b.Fornavn,
                    Efternavn = x.b.Efternavn,
                    Brugerkode = x.b.Brugerkode,
                    Bæltegrad = x.b.Bæltegrad,
                    Address = x.b.Address,
                    Role = x.b.Role,
                    Klub = new KlubDTO
                    {
                        KlubID = x.k.KlubID,
                        KlubNavn = x.k.KlubNavn
                    }
                })
                .ToListAsync();

            return result;
        }

        public async Task<Bruger?> GetBrugerByBrugernavnAsync(string brugernavn)
        {
            return await _context.Brugere
                .FirstOrDefaultAsync(b => b.Brugernavn == brugernavn);
        }

        public async Task<List<Bruger>> GetBrugerByFornavnEfternavnAsync(string fornavn, string efternavn)
        {
            return await _context.Brugere
                .Where(b => b.Fornavn == fornavn && b.Efternavn == efternavn)
                .ToListAsync();
        }

        public async Task<Bruger> CreateBrugerAsync(Bruger bruger)
        {
            _context.Brugere.Add(bruger);
            await _context.SaveChangesAsync();
            return bruger;
        }

        public async Task<bool> UpdateBrugerAsync(Bruger bruger)
        {
            _context.Brugere.Update(bruger);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteBrugerAsync(int id)
        {
            var bruger = await _context.Brugere.FindAsync(id);
            if (bruger == null) return false;

            _context.Brugere.Remove(bruger);
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
