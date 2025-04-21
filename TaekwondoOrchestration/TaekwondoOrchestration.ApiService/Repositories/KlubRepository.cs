// In KlubRepository.cs
using Microsoft.EntityFrameworkCore;
using TaekwondoOrchestration.ApiService.Data;
using TaekwondoApp.Shared.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaekwondoOrchestration.ApiService.RepositorieInterfaces;
using TaekwondoApp.Shared.DTO;
using AutoMapper;

namespace TaekwondoOrchestration.ApiService.Repositories
{
    public class KlubRepository : IKlubRepository
    {
        private readonly ApiDbContext _context;
        private readonly IMapper _mapper;

        public KlubRepository(ApiDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<Klub>> GetAllKlubberAsync()
        {
            return await _context.Klubber.ToListAsync();
        }

        public async Task<Klub?> GetKlubByIdAsync(Guid klubId)
        {
            return await _context.Klubber.FindAsync(klubId);
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

        public async Task<bool> DeleteKlubAsync(Guid klubId)
        {
            var klub = await _context.Klubber.FindAsync(klubId);
            if (klub == null) return false;

            _context.Klubber.Remove(klub);
            return await _context.SaveChangesAsync() > 0;
        }
        public async Task<KlubDTO?> GetKlubWithDetailsAsync(Guid klubId)
        {
            var klubDetails = await _context.Klubber
                .Where(k => k.KlubID == klubId)
                .Include(k => k.KlubProgrammer)
                    .ThenInclude(kp => kp.Plan)
                .Include(k => k.KlubQuizzer)
                    .ThenInclude(kq => kq.Quiz)
                .Include(k => k.KlubØvelser)
                    .ThenInclude(kø => kø.Øvelse)
                .Include(k => k.BrugerKlubber)
                    .ThenInclude(bk => bk.Bruger)
                .FirstOrDefaultAsync();

            if (klubDetails == null)
                return null;

            var klubDTO = _mapper.Map<KlubDTO>(klubDetails);

            return klubDTO;
        }

    }
}
