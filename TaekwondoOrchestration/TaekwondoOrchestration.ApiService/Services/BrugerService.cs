using AutoMapper;
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
        private readonly IMapper _mapper;

        public BrugerService(IBrugerRepository brugerRepository, IMapper mapper)
        {
            _brugerRepository = brugerRepository;
            _mapper = mapper;
        }

        public async Task<List<BrugerDTO>> GetAllBrugereAsync()
        {
            var brugere = await _brugerRepository.GetAllBrugereAsync();
            return _mapper.Map<List<BrugerDTO>>(brugere);
        }

        public async Task<BrugerDTO?> GetBrugerByIdAsync(Guid id)
        {
            var bruger = await _brugerRepository.GetBrugerByIdAsync(id);
            return bruger == null ? null : _mapper.Map<BrugerDTO>(bruger);
        }

        public async Task<List<BrugerDTO>> GetBrugerByRoleAsync(string role)
        {
            var brugere = await _brugerRepository.GetBrugerByRoleAsync(role);
            return _mapper.Map<List<BrugerDTO>>(brugere);
        }

        public async Task<List<BrugerDTO>> GetBrugerByBælteAsync(string bæltegrad)
        {
            var brugere = await _brugerRepository.GetBrugerByBælteAsync(bæltegrad);
            return _mapper.Map<List<BrugerDTO>>(brugere);
        }

        public async Task<List<BrugerDTO>> GetBrugereByKlubAsync(Guid klubId)
        {
            return await _brugerRepository.GetBrugereByKlubAsync(klubId);
        }

        public async Task<List<BrugerDTO>> GetBrugereByKlubAndBæltegradAsync(Guid klubId, string bæltegrad)
        {
            return await _brugerRepository.GetBrugereByKlubAndBæltegradAsync(klubId, bæltegrad);
        }

        public async Task<BrugerDTO?> GetBrugerByBrugernavnAsync(string brugernavn)
        {
            var bruger = await _brugerRepository.GetBrugerByBrugernavnAsync(brugernavn);
            return bruger == null ? null : _mapper.Map<BrugerDTO>(bruger);
        }

        public async Task<List<BrugerDTO>> GetBrugerByFornavnEfternavnAsync(string fornavn, string efternavn)
        {
            var brugere = await _brugerRepository.GetBrugerByFornavnEfternavnAsync(fornavn, efternavn);
            return _mapper.Map<List<BrugerDTO>>(brugere);
        }

        public async Task<BrugerDTO?> CreateBrugerAsync(BrugerDTO brugerDto)
        {
            var brugerEntity = _mapper.Map<Bruger>(brugerDto);
            var createdBruger = await _brugerRepository.CreateBrugerAsync(brugerEntity);
            return _mapper.Map<BrugerDTO>(createdBruger);
        }

        public async Task<bool> UpdateBrugerAsync(Guid id, BrugerDTO brugerDto)
        {
            var bruger = await _brugerRepository.GetBrugerByIdAsync(id);
            if (bruger == null) return false;

            _mapper.Map(brugerDto, bruger); // Updates the entity with the values from DTO

            return await _brugerRepository.UpdateBrugerAsync(bruger);
        }

        public async Task<bool> DeleteBrugerAsync(Guid id)
        {
            return await _brugerRepository.DeleteBrugerAsync(id);
        }

        public async Task<BrugerDTO?> AuthenticateBrugerAsync(LoginDTO loginDto)
        {
            return await _brugerRepository.AuthenticateBrugerAsync(loginDto.EmailOrBrugernavn, loginDto.Brugerkode);
        }
    }
}
