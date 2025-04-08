using Api.DTOs;
using Api.Models;
using Api.Repositories;
using Humanizer;

namespace Api.Services
{
    public class ØvelseService
    {
        private readonly IØvelseRepository _øvelseRepository;
        private readonly IBrugerØvelseRepository _brugerØvelseRepository;
        private readonly IKlubØvelseRepository _klubØvelseRepository;

        public ØvelseService(IØvelseRepository øvelseRepository, IBrugerØvelseRepository brugerØvelseRepository, IKlubØvelseRepository klubØvelseRepository)
        {
            _øvelseRepository = øvelseRepository;
            _brugerØvelseRepository = brugerØvelseRepository ?? throw new ArgumentNullException(nameof(brugerØvelseRepository));
            _klubØvelseRepository = klubØvelseRepository ?? throw new ArgumentNullException(nameof(klubØvelseRepository));
        }

        private static ØvelseDTO MapToDTO(Øvelse o)
        {
            return new ØvelseDTO
            {
                ØvelseID = o.ØvelseID,
                ØvelseNavn = o.ØvelseNavn,
                ØvelseBeskrivelse = o.ØvelseBeskrivelse,
                ØvelseBillede = o.ØvelseBillede,
                ØvelseVideo = o.ØvelseVideo,
                ØvelseTid = o.ØvelseTid,
                ØvelseSværhed = o.ØvelseSværhed,
                PensumID = o.PensumID,
                BrugerID = o.BrugerØvelses?.FirstOrDefault()?.BrugerID,  // Mapping user if available
                KlubID = o.KlubØvelses?.FirstOrDefault()?.KlubID  // Mapping club if available
            };
        }

        public async Task<List<ØvelseDTO>> GetAllØvelserAsync()
        {
            var øvelser = await _øvelseRepository.GetAllØvelserAsync();
            return øvelser.Select(MapToDTO).ToList();
        }

        public async Task<ØvelseDTO?> GetØvelseByIdAsync(int id)
        {
            var øvelse = await _øvelseRepository.GetØvelseByIdAsync(id);
            return øvelse == null ? null : MapToDTO(øvelse);
        }

        public async Task<ØvelseDTO?> CreateØvelseAsync(ØvelseDTO øvelseDto)
        {
            if (øvelseDto == null ||
                string.IsNullOrEmpty(øvelseDto.ØvelseNavn) ||
                string.IsNullOrEmpty(øvelseDto.ØvelseBeskrivelse) ||
                øvelseDto.ØvelseTid <= 0 ||
                string.IsNullOrEmpty(øvelseDto.ØvelseSværhed))
            {
                return null; // Validation failed
            }

            var newØvelse = new Øvelse
            {
                ØvelseNavn = øvelseDto.ØvelseNavn,
                ØvelseBeskrivelse = øvelseDto.ØvelseBeskrivelse,
                ØvelseBillede = øvelseDto.ØvelseBillede,
                ØvelseVideo = øvelseDto.ØvelseVideo,
                ØvelseTid = øvelseDto.ØvelseTid,
                ØvelseSværhed = øvelseDto.ØvelseSværhed,
                PensumID = øvelseDto.PensumID // Set PensumID
            };

            var createdØvelse = await _øvelseRepository.CreateØvelseAsync(newØvelse);

            // Handle user relationship
            if (øvelseDto.BrugerID.HasValue)
            {
                var newBrugerØvelse = new BrugerØvelse
                {
                    BrugerID = øvelseDto.BrugerID.Value,
                    ØvelseID = createdØvelse.ØvelseID,
                };
                await _brugerØvelseRepository.CreateBrugerØvelseAsync(newBrugerØvelse);
            }

            // Handle club relationship
            if (øvelseDto.KlubID.HasValue)
            {
                var newKlubØvelse = new KlubØvelse
                {
                    KlubID = øvelseDto.KlubID.Value,
                    ØvelseID = createdØvelse.ØvelseID,
                };
                await _klubØvelseRepository.CreateKlubØvelseAsync(newKlubØvelse);
            }

            return MapToDTO(createdØvelse);
        }

        public async Task<bool> DeleteØvelseAsync(int id)
        {
            var result = await _øvelseRepository.DeleteØvelseAsync(id);
            return result;
        }

        public async Task<bool> UpdateØvelseAsync(int id, ØvelseDTO øvelseDto)
        {
            var existingØvelse = await _øvelseRepository.GetØvelseByIdAsync(id);
            if (existingØvelse == null)
                return false;

            existingØvelse.ØvelseNavn = øvelseDto.ØvelseNavn;
            existingØvelse.ØvelseBeskrivelse = øvelseDto.ØvelseBeskrivelse;
            existingØvelse.ØvelseBillede = øvelseDto.ØvelseBillede;
            existingØvelse.ØvelseVideo = øvelseDto.ØvelseVideo;
            existingØvelse.ØvelseTid = øvelseDto.ØvelseTid;
            existingØvelse.ØvelseSværhed = øvelseDto.ØvelseSværhed;
            existingØvelse.PensumID = øvelseDto.PensumID;

            return await _øvelseRepository.UpdateØvelseAsync(existingØvelse);
        }

        public async Task<List<ØvelseDTO>> GetØvelserBySværhedAsync(string sværhed)
        {
            var øvelser = await _øvelseRepository.GetØvelserBySværhedAsync(sværhed);
            return øvelser.Select(MapToDTO).ToList();
        }

        public async Task<List<ØvelseDTO>> GetØvelserByBrugerAsync(int brugerId)
        {
            var øvelser = await _øvelseRepository.GetØvelserByBrugerAsync(brugerId);

            return øvelser.Select(o => new ØvelseDTO
            {
                ØvelseID = o.ØvelseID,
                ØvelseNavn = o.ØvelseNavn,
                ØvelseBeskrivelse = o.ØvelseBeskrivelse,
                ØvelseBillede = o.ØvelseBillede,
                ØvelseVideo = o.ØvelseVideo,
                ØvelseTid = o.ØvelseTid,
                ØvelseSværhed = o.ØvelseSværhed,
                BrugerID = o.BrugerØvelses?.FirstOrDefault(b => b.BrugerID == brugerId)?.BrugerID
            }).ToList();
        }

        public async Task<List<ØvelseDTO>> GetØvelserByKlubAsync(int klubId)
        {
            var øvelser = await _øvelseRepository.GetØvelserByKlubAsync(klubId);

            return øvelser.Select(o => new ØvelseDTO
            {
                ØvelseID = o.ØvelseID,
                ØvelseNavn = o.ØvelseNavn,
                ØvelseBeskrivelse = o.ØvelseBeskrivelse,
                ØvelseBillede = o.ØvelseBillede,
                ØvelseVideo = o.ØvelseVideo,
                ØvelseTid = o.ØvelseTid,
                ØvelseSværhed = o.ØvelseSværhed,
                KlubID = o.KlubØvelses?.FirstOrDefault(k => k.KlubID == klubId)?.KlubID
            }).ToList();
        }

        public async Task<List<ØvelseDTO>> GetØvelserByNavnAsync(string navn)
        {
            var øvelser = await _øvelseRepository.GetØvelserByNavnAsync(navn);
            return øvelser.Select(MapToDTO).ToList();
        }
    }
}
