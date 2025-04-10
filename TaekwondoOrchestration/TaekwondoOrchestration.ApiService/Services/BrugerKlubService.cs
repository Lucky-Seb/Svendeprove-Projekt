using TaekwondoApp.Shared.DTO;
using TaekwondoOrchestration.ApiService.Models;
using TaekwondoOrchestration.ApiService.Repositories;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaekwondoOrchestration.ApiService.RepositorieInterfaces;

namespace TaekwondoOrchestration.ApiService.Services
{
    public class BrugerKlubService
    {
        private readonly IBrugerKlubRepository _brugerKlubRepository;

        public BrugerKlubService(IBrugerKlubRepository brugerKlubRepository)
        {
            _brugerKlubRepository = brugerKlubRepository;
        }

        public async Task<List<BrugerKlubDTO>> GetAllBrugerKlubsAsync()
        {
            var brugerKlubber = await _brugerKlubRepository.GetAllBrugerKlubberAsync();
            return brugerKlubber.Select(brugerKlub => new BrugerKlubDTO
            {
                BrugerID = brugerKlub.BrugerID,
                KlubID = brugerKlub.KlubID
            }).ToList();
        }

        public async Task<BrugerKlubDTO?> GetBrugerKlubByIdAsync(int brugerId, int klubId)
        {
            var brugerKlub = await _brugerKlubRepository.GetBrugerKlubByIdAsync(brugerId, klubId);
            if (brugerKlub == null)
                return null;

            return new BrugerKlubDTO
            {
                BrugerID = brugerKlub.BrugerID,
                KlubID = brugerKlub.KlubID
            };
        }

        public async Task<BrugerKlubDTO?> CreateBrugerKlubAsync(BrugerKlubDTO brugerKlubDto)
        {
            // Check if the DTO is null
            if (brugerKlubDto == null) return null;

            //// Validate required fields
            //if (brugerKlubDto.BrugerID <= 0) return null;  // BrugerID must be a positive integer
            //if (brugerKlubDto.KlubID <= 0) return null;    // KlubID must be a positive integer

            // Create new BrugerKlub entity
            var newBrugerKlub = new BrugerKlub
            {
                BrugerID = brugerKlubDto.BrugerID,
                KlubID = brugerKlubDto.KlubID
            };

            // Save the new BrugerKlub entity to the repository
            var createdBrugerKlub = await _brugerKlubRepository.CreateBrugerKlubAsync(newBrugerKlub);
            if (createdBrugerKlub == null) return null;  // Return null if creation fails

            // Return the newly created BrugerKlubDTO
            return new BrugerKlubDTO
            {
                BrugerID = createdBrugerKlub.BrugerID,
                KlubID = createdBrugerKlub.KlubID
            };
        }


        public async Task<bool> DeleteBrugerKlubAsync(int brugerId, int klubId)
        {
            return await _brugerKlubRepository.DeleteBrugerKlubAsync(brugerId, klubId);
        }
    }
}
