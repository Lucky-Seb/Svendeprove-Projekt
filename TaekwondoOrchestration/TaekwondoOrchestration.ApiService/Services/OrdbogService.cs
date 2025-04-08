using TaekwondoOrchestration.ApiService.DTO;
using TaekwondoOrchestration.ApiService.Models;
using TaekwondoOrchestration.ApiService.Repositories;

namespace TaekwondoOrchestration.ApiService.Services
{
    public class OrdbogService
    {
        private readonly IOrdbogRepository _ordbogRepository;

        public OrdbogService(IOrdbogRepository ordbogRepository)
        {
            _ordbogRepository = ordbogRepository;
        }

        public async Task<List<OrdbogDTO>> GetAllOrdbogAsync()
        {
            var ordboger = await _ordbogRepository.GetAllOrdbogAsync();
            return ordboger.Select(o => new OrdbogDTO
            {
                Id = o.Id,
                DanskOrd = o.DanskOrd,
                KoranskOrd = o.KoranskOrd,
                Beskrivelse = o.Beskrivelse,
                BilledeLink = o.BilledeLink,
                LydLink = o.LydLink,
                VideoLink = o.VideoLink
            }).ToList();
        }

        public async Task<OrdbogDTO?> GetOrdbogByIdAsync(int id)
        {
            var ordbog = await _ordbogRepository.GetOrdbogByIdAsync(id);
            if (ordbog == null)
                return null;

            return new OrdbogDTO
            {
                Id = ordbog.Id,
                DanskOrd = ordbog.DanskOrd,
                KoranskOrd = ordbog.KoranskOrd,
                Beskrivelse = ordbog.Beskrivelse,
                BilledeLink = ordbog.BilledeLink,
                LydLink = ordbog.LydLink,
                VideoLink = ordbog.VideoLink
            };
        }

        public async Task<OrdbogDTO> CreateOrdbogAsync(OrdbogDTO ordbogDto)
        {
            var newOrdbog = new Ordbog
            {
                DanskOrd = ordbogDto.DanskOrd,
                KoranskOrd = ordbogDto.KoranskOrd,
                Beskrivelse = ordbogDto.Beskrivelse,
                BilledeLink = ordbogDto.BilledeLink,
                LydLink = ordbogDto.LydLink,
                VideoLink = ordbogDto.VideoLink
            };

            var createdOrdbog = await _ordbogRepository.CreateOrdbogAsync(newOrdbog);

            return new OrdbogDTO
            {
                Id = createdOrdbog.Id,
                DanskOrd = createdOrdbog.DanskOrd,
                KoranskOrd = createdOrdbog.KoranskOrd,
                Beskrivelse = createdOrdbog.Beskrivelse,
                BilledeLink = createdOrdbog.BilledeLink,
                LydLink = createdOrdbog.LydLink,
                VideoLink = createdOrdbog.VideoLink
            };
        }

        public async Task<bool> UpdateOrdbogAsync(int id, OrdbogDTO ordbogDto)
        {
            if (id <= 0 || ordbogDto == null || id != ordbogDto.Id) return false;

            // Validate required fields
            if (string.IsNullOrEmpty(ordbogDto.DanskOrd)) return false;  // Ord cannot be null or empty
            if (string.IsNullOrEmpty(ordbogDto.KoranskOrd)) return false;  // Ord cannot be null or empty
            if (string.IsNullOrEmpty(ordbogDto.Beskrivelse)) return false;  // Beskrivelse cannot be null or empty

            var existingOrdbog = await _ordbogRepository.GetOrdbogByIdAsync(id);
            if (existingOrdbog == null) return false;

            existingOrdbog.DanskOrd = ordbogDto.DanskOrd;
            existingOrdbog.KoranskOrd = ordbogDto.KoranskOrd;
            existingOrdbog.Beskrivelse = ordbogDto.Beskrivelse;
            existingOrdbog.BilledeLink = ordbogDto.BilledeLink;
            existingOrdbog.LydLink = ordbogDto.LydLink;
            existingOrdbog.VideoLink = ordbogDto.VideoLink;

            return await _ordbogRepository.UpdateOrdbogAsync(existingOrdbog);
        }

        public async Task<bool> DeleteOrdbogAsync(int id)
        {
            return await _ordbogRepository.DeleteOrdbogAsync(id);
        }

        // Get Ordbog by DanskOrd
        public async Task<OrdbogDTO?> GetOrdbogByDanskOrdAsync(string danskOrd)
        {
            var ordbog = await _ordbogRepository.GetOrdbogByDanskOrdAsync(danskOrd);
            if (ordbog == null)
                return null;

            return new OrdbogDTO
            {
                Id = ordbog.Id,
                DanskOrd = ordbog.DanskOrd,
                KoranskOrd = ordbog.KoranskOrd,
                Beskrivelse = ordbog.Beskrivelse,
                BilledeLink = ordbog.BilledeLink,
                LydLink = ordbog.LydLink,
                VideoLink = ordbog.VideoLink
            };
        }

        // Get Ordbog by KoranOrd
        public async Task<OrdbogDTO?> GetOrdbogByKoranOrdAsync(string koranOrd)
        {
            var ordbog = await _ordbogRepository.GetOrdbogByKoranOrdAsync(koranOrd);
            if (ordbog == null)
                return null;

            return new OrdbogDTO
            {
                Id = ordbog.Id,
                DanskOrd = ordbog.DanskOrd,
                KoranskOrd = ordbog.KoranskOrd,
                Beskrivelse = ordbog.Beskrivelse,
                BilledeLink = ordbog.BilledeLink,
                LydLink = ordbog.LydLink,
                VideoLink = ordbog.VideoLink
            };
        }
    }
}
