using TaekwondoApp.Shared.DTO;
using TaekwondoApp.Shared.Models;
using TaekwondoOrchestration.ApiService.Repositories;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaekwondoOrchestration.ApiService.RepositorieInterfaces;

namespace TaekwondoOrchestration.ApiService.Services
{
    public class BrugerService
    {
        private readonly IBrugerRepository _brugerRepository;

        public BrugerService(IBrugerRepository brugerRepository)
        {
            _brugerRepository = brugerRepository;
        }

        private BrugerDTO MapToDTO(Bruger bruger)
        {
            return new BrugerDTO
            {
                BrugerID = bruger.BrugerID,
                Email = bruger.Email,
                Brugernavn = bruger.Brugernavn,
                Fornavn = bruger.Fornavn,
                Efternavn = bruger.Efternavn,
                Brugerkode = bruger.Brugerkode,
                Bæltegrad = bruger.Bæltegrad,
                Address = bruger.Address,
                Role = bruger.Role,
            };
        }

        public async Task<List<BrugerDTO>> GetAllBrugereAsync()
        {
            var brugere = await _brugerRepository.GetAllBrugereAsync();
            return brugere.Select(MapToDTO).ToList();
        }

        public async Task<BrugerDTO?> GetBrugerByIdAsync(Guid id)
        {
            var bruger = await _brugerRepository.GetBrugerByIdAsync(id);
            return bruger == null ? null : MapToDTO(bruger);
        }

        public async Task<List<BrugerDTO>> GetBrugerByRoleAsync(string role)
        {
            var brugere = await _brugerRepository.GetBrugerByRoleAsync(role);
            return brugere.Select(MapToDTO).ToList();
        }

        public async Task<List<BrugerDTO>> GetBrugerByBælteAsync(string bæltegrad)
        {
            var brugere = await _brugerRepository.GetBrugerByBælteAsync(bæltegrad);
            return brugere.Select(MapToDTO).ToList();
        }

        public async Task<List<BrugerDTO>> GetBrugereByKlubAsync(Guid klubId)
        {
            // Get the list of BrugerDTOs from the repository
            var brugere = await _brugerRepository.GetBrugereByKlubAsync(klubId);

            // Return the list directly (no need for Select(MapToDTO))
            return brugere;
        }

        public async Task<List<BrugerDTO>> GetBrugereByKlubAndBæltegradAsync(Guid klubId, string bæltegrad)
        {
            // Get the list of BrugerDTOs from the repository
            var brugere = await _brugerRepository.GetBrugereByKlubAndBæltegradAsync(klubId, bæltegrad);

            // Return the list directly (no need for Select(MapToDTO))
            return brugere;
        }

        public async Task<BrugerDTO?> GetBrugerByBrugernavnAsync(string brugernavn)
        {
            var bruger = await _brugerRepository.GetBrugerByBrugernavnAsync(brugernavn);
            return bruger == null ? null : MapToDTO(bruger);
        }

        public async Task<List<BrugerDTO>> GetBrugerByFornavnEfternavnAsync(string fornavn, string efternavn)
        {
            var brugere = await _brugerRepository.GetBrugerByFornavnEfternavnAsync(fornavn, efternavn);
            return brugere.Select(MapToDTO).ToList();
        }

        public async Task<BrugerDTO?> CreateBrugerAsync(BrugerDTO brugerDto)
        {
            // Validate and create a new Bruger entity
            var newBruger = new Bruger
            {
                Brugernavn = brugerDto.Brugernavn,
                Email = brugerDto.Email,
                Fornavn = brugerDto.Fornavn,
                Efternavn = brugerDto.Efternavn,
                Brugerkode = brugerDto.Brugerkode,
                Bæltegrad = brugerDto.Bæltegrad,
                Address = brugerDto.Address,
                Role = brugerDto.Role
            };

            var createdBruger = await _brugerRepository.CreateBrugerAsync(newBruger);
            if (createdBruger == null) return null;

            brugerDto.BrugerID = createdBruger.BrugerID;
            return brugerDto;
        }

        public async Task<bool> UpdateBrugerAsync(Guid id, BrugerDTO brugerDto)
        {
            var bruger = await _brugerRepository.GetBrugerByIdAsync(id);
            if (bruger == null) return false;

            // Update the user entity
            bruger.Brugernavn = brugerDto.Brugernavn;
            bruger.Email = brugerDto.Email;
            bruger.Fornavn = brugerDto.Fornavn;
            bruger.Efternavn = brugerDto.Efternavn;
            bruger.Brugerkode = brugerDto.Brugerkode;
            bruger.Bæltegrad = brugerDto.Bæltegrad;
            bruger.Address = brugerDto.Address;
            bruger.Role = brugerDto.Role;

            return await _brugerRepository.UpdateBrugerAsync(bruger);
        }

        public async Task<bool> DeleteBrugerAsync(Guid id)
        {
            return await _brugerRepository.DeleteBrugerAsync(id);
        }
    }
}
