using Microsoft.EntityFrameworkCore;
using TaekwondoOrchestration.ApiService.Data;
using TaekwondoApp.Shared.Models;
using TaekwondoApp.Shared.DTO;
using TaekwondoOrchestration.ApiService.RepositorieInterfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TaekwondoOrchestration.ApiService.Repositories
{
    public class BrugerRepository : IBrugerRepository
    {
        private readonly ApiDbContext _context;
        private readonly IMapper _mapper;

        public BrugerRepository(ApiDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<Bruger>> GetAllBrugereAsync()
        {
            return await _context.Brugere.ToListAsync();
        }

        public async Task<Bruger?> GetBrugerByIdAsync(Guid brugerId)
        {
            return await _context.Brugere.FindAsync(brugerId);
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

        public async Task<List<BrugerDTO>> GetBrugereByKlubAsync(Guid klubId)
        {
            return await _context.BrugerKlubber
                .Where(bk => bk.KlubID == klubId)
                .Select(bk => bk.Bruger)
                .Include(b => b.BrugerKlubber)
                .ThenInclude(bk => bk.Klub)
                .ProjectTo<BrugerDTO>(_mapper.ConfigurationProvider)
                .ToListAsync();
        }

        public async Task<List<BrugerDTO>> GetBrugereByKlubAndBæltegradAsync(Guid klubId, string bæltegrad)
        {
            return await _context.BrugerKlubber
                .Where(bk => bk.KlubID == klubId && bk.Bruger.Bæltegrad == bæltegrad)
                .Select(bk => bk.Bruger)
                .Include(b => b.BrugerKlubber)
                .ThenInclude(bk => bk.Klub)
                .ProjectTo<BrugerDTO>(_mapper.ConfigurationProvider)
                .ToListAsync();
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

        public async Task<bool> DeleteBrugerAsync(Guid brugerId)
        {
            var bruger = await _context.Brugere.FindAsync(brugerId);
            if (bruger == null) return false;

            _context.Brugere.Remove(bruger);
            return await _context.SaveChangesAsync() > 0;
        }
        public async Task<BrugerDTO?> AuthenticateBrugerAsync(LoginDTO loginDto)
        {
            var bruger = await _context.Brugere
                .FirstOrDefaultAsync(b =>
                    (b.Email == loginDto.EmailOrBrugernavn || b.Brugernavn == loginDto.EmailOrBrugernavn) &&
                    b.Brugerkode == loginDto.Brugerkode);

            if (bruger == null)
                return null;

            return _mapper.Map<BrugerDTO>(bruger);
        }
    }
}
