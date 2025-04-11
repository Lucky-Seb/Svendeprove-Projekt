// In KlubService.cs
using TaekwondoApp.Shared.DTO;
using TaekwondoApp.Shared.Models;
using TaekwondoOrchestration.ApiService.Repositories;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaekwondoOrchestration.ApiService.RepositorieInterfaces;

namespace TaekwondoOrchestration.ApiService.Services
{
    public class KlubService
    {
        private readonly IKlubRepository _klubRepository;

        public KlubService(IKlubRepository klubRepository)
        {
            _klubRepository = klubRepository;
        }

        public async Task<List<KlubDTO>> GetAllKlubberAsync()
        {
            var klubber = await _klubRepository.GetAllKlubberAsync();
            return klubber.Select(klub => new KlubDTO
            {
                KlubID = klub.KlubID,
                KlubNavn = klub.KlubNavn
            }).ToList();
        }

        public async Task<KlubDTO?> GetKlubByIdAsync(Guid id)
        {
            var klub = await _klubRepository.GetKlubByIdAsync(id);
            if (klub == null)
                return null;

            return new KlubDTO
            {
                KlubID = klub.KlubID,
                KlubNavn = klub.KlubNavn
            };
        }

        // Add GetKlubByNavnAsync
        public async Task<KlubDTO?> GetKlubByNavnAsync(string klubNavn)
        {
            var klub = await _klubRepository.GetKlubByNavnAsync(klubNavn);
            if (klub == null)
                return null;

            return new KlubDTO
            {
                KlubID = klub.KlubID,
                KlubNavn = klub.KlubNavn
            };
        }

        public async Task<KlubDTO?> CreateKlubAsync(KlubDTO klubDto)
        {
            if (klubDto == null) return null;

            if (string.IsNullOrEmpty(klubDto.KlubNavn)) return null;

            var newKlub = new Klub
            {
                KlubNavn = klubDto.KlubNavn
            };

            var createdKlub = await _klubRepository.CreateKlubAsync(newKlub);

            return new KlubDTO
            {
                KlubID = createdKlub.KlubID,
                KlubNavn = createdKlub.KlubNavn
            };
        }

        public async Task<bool> DeleteKlubAsync(Guid id)
        {
            return await _klubRepository.DeleteKlubAsync(id);
        }

        public async Task<(bool success, string message)> UpdateKlubAsync(Guid id, KlubDTO klubDto)
        {
            var existingKlub = await _klubRepository.GetKlubByIdAsync(id);
            if (existingKlub == null)
                return (false, "Klub not found.");

            if (string.IsNullOrWhiteSpace(klubDto.KlubNavn))
                return (false, "Klub name cannot be empty.");

            existingKlub.KlubNavn = klubDto.KlubNavn;

            bool updated = await _klubRepository.UpdateKlubAsync(existingKlub);
            return updated ? (true, "Klub updated successfully.") : (false, "Update failed.");
        }
    }
}
