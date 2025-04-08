using TaekwondoOrchestration.ApiService.DTO;
using TaekwondoOrchestration.ApiService.Models;
using TaekwondoOrchestration.ApiService.Repositories;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaekwondoOrchestration.ApiService.RepositorieInterfaces;

namespace TaekwondoOrchestration.ApiService.Services
{
    public class BrugerØvelseService
    {
        private readonly IBrugerØvelseRepository _brugerØvelseRepository;

        public BrugerØvelseService(IBrugerØvelseRepository brugerØvelseRepository)
        {
            _brugerØvelseRepository = brugerØvelseRepository;
        }

        public async Task<List<BrugerØvelseDTO>> GetAllBrugerØvelserAsync()
        {
            var brugerØvelser = await _brugerØvelseRepository.GetAllBrugerØvelserAsync();
            return brugerØvelser.Select(brugerØvelse => new BrugerØvelseDTO
            {
                BrugerID = brugerØvelse.BrugerID,
                ØvelseID = brugerØvelse.ØvelseID
            }).ToList();
        }

        public async Task<BrugerØvelseDTO?> GetBrugerØvelseByIdAsync(int brugerId, int øvelseId)
        {
            var brugerØvelse = await _brugerØvelseRepository.GetBrugerØvelseByIdAsync(brugerId, øvelseId);
            if (brugerØvelse == null)
                return null;

            return new BrugerØvelseDTO
            {
                BrugerID = brugerØvelse.BrugerID,
                ØvelseID = brugerØvelse.ØvelseID
            };
        }

        public async Task<BrugerØvelseDTO?> CreateBrugerØvelseAsync(BrugerØvelseDTO brugerØvelseDto)
        {
            // Check if the DTO is null
            if (brugerØvelseDto == null) return null;

            // Validate required fields
            if (brugerØvelseDto.BrugerID <= 0) return null;  // BrugerID must be a positive integer
            if (brugerØvelseDto.ØvelseID <= 0) return null;  // ØvelseID must be a positive integer

            // Create new BrugerØvelse entity
            var newBrugerØvelse = new BrugerØvelse
            {
                BrugerID = brugerØvelseDto.BrugerID,
                ØvelseID = brugerØvelseDto.ØvelseID
            };

            // Save the new BrugerØvelse entity to the repository
            var createdBrugerØvelse = await _brugerØvelseRepository.CreateBrugerØvelseAsync(newBrugerØvelse);
            if (createdBrugerØvelse == null) return null;  // Return null if creation fails

            // Return the newly created BrugerØvelseDTO
            return new BrugerØvelseDTO
            {
                BrugerID = createdBrugerØvelse.BrugerID,
                ØvelseID = createdBrugerØvelse.ØvelseID
            };
        }

        public async Task<bool> DeleteBrugerØvelseAsync(int brugerId, int øvelseId)
        {
            return await _brugerØvelseRepository.DeleteBrugerØvelseAsync(brugerId, øvelseId);
        }
    }
}
