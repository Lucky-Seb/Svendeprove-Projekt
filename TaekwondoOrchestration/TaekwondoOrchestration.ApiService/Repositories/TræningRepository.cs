using TaekwondoOrchestration.ApiService.Data;
using TaekwondoApp.Shared.DTO;
using TaekwondoOrchestration.ApiService.Models;
using TaekwondoOrchestration.ApiService.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaekwondoOrchestration.ApiService.RepositorieInterfaces;

namespace TaekwondoOrchestration.ApiService.Repositories
{
    public class TræningRepository : ITræningRepository
    {
        private readonly ApiDbContext _context;

        public TræningRepository(ApiDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Træning>> GetByProgramIdAsync(Guid træningId)
        {
            return await _context.Træninger
                .Where(t => t.ProgramID == træningId)
                .ToListAsync();
        }
        public async Task<List<Træning>> GetAllTræningAsync()
        {
            return await _context.Træninger.ToListAsync();
        }

        public async Task<Træning> GetTræningByIdAsync(Guid træningId)
        {
            return await _context.Træninger.FindAsync(træningId);
        }
        public async Task<Træning> CreateTræningAsync(Træning træning)
        {
            _context.Træninger.Add(træning);
            await _context.SaveChangesAsync();

            return new Træning{};
        }

        public async Task<bool> DeleteTræningAsync(Guid træningId)
        {
            var træning = await _context.Træninger.FindAsync(træningId);
            if (træning == null)
                return false;

            _context.Træninger.Remove(træning);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> UpdateTræningAsync(Træning træning)
        {
            var existingTræning = await _context.Træninger.FindAsync(træning.TræningID);
            if (existingTræning == null)
                return false;

            _context.Entry(existingTræning).CurrentValues.SetValues(træning);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
