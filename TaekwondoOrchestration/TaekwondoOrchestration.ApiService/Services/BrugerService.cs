using AutoMapper;
using TaekwondoApp.Shared.DTO;
using TaekwondoApp.Shared.Models;
using TaekwondoOrchestration.ApiService.Repositories;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaekwondoOrchestration.ApiService.RepositorieInterfaces;
using TaekwondoOrchestration.ApiService.ServiceInterfaces;
using TaekwondoOrchestration.ApiService.Helpers;

namespace TaekwondoOrchestration.ApiService.Services
{
    public class BrugerService : IBrugerService
    {
        private readonly IBrugerRepository _brugerRepository;
        private readonly IMapper _mapper;

        public BrugerService(IBrugerRepository brugerRepository, IMapper mapper)
        {
            _brugerRepository = brugerRepository;
            _mapper = mapper;
        }

        #region CRUD Operations

        public async Task<Result<IEnumerable<BrugerDTO>>> GetAllBrugereAsync()
        {
            var brugere = await _brugerRepository.GetAllBrugereAsync();
            var mapped = _mapper.Map<IEnumerable<BrugerDTO>>(brugere);
            return Result<IEnumerable<BrugerDTO>>.Ok(mapped);
        }

        public async Task<Result<BrugerDTO>> GetBrugerByIdAsync(Guid id)
        {
            var bruger = await _brugerRepository.GetBrugerByIdAsync(id);
            if (bruger == null)
                return Result<BrugerDTO>.Fail("Bruger not found.");

            return Result<BrugerDTO>.Ok(_mapper.Map<BrugerDTO>(bruger));
        }

        public async Task<Result<BrugerDTO>> CreateBrugerAsync(BrugerDTO brugerDto)
        {
            var entity = _mapper.Map<Bruger>(brugerDto);
            EntityHelper.InitializeEntity(entity, brugerDto.ModifiedBy, "Created new Bruger entry.");

            var created = await _brugerRepository.CreateBrugerAsync(entity);
            return Result<BrugerDTO>.Ok(_mapper.Map<BrugerDTO>(created));
        }

        public async Task<Result<bool>> UpdateBrugerAsync(Guid id, BrugerDTO brugerDto)
        {
            var existing = await _brugerRepository.GetBrugerByIdAsync(id);
            if (existing == null)
                return Result<bool>.Fail("Bruger not found.");

            _mapper.Map(brugerDto, existing);
            EntityHelper.UpdateCommonFields(existing, brugerDto.ModifiedBy);

            var success = await _brugerRepository.UpdateBrugerAsync(existing);
            return success ? Result<bool>.Ok(true) : Result<bool>.Fail("Failed to update Bruger.");
        }

        public async Task<Result<bool>> DeleteBrugerAsync(Guid id)
        {
            var bruger = await _brugerRepository.GetBrugerByIdAsync(id);
            if (bruger == null)
                return Result<bool>.Fail("Bruger not found.");

            EntityHelper.SetDeletedOrRestoredProperties(bruger, "Soft-deleted Bruger", bruger.ModifiedBy);
            var success = await _brugerRepository.UpdateBrugerAsync(bruger);
            return success ? Result<bool>.Ok(true) : Result<bool>.Fail("Failed to delete Bruger.");
        }

        public async Task<Result<bool>> RestoreBrugerAsync(Guid id, BrugerDTO dto)
        {
            var bruger = await _brugerRepository.GetBrugerByIdIncludingDeletedAsync(id);
            if (bruger == null || !bruger.IsDeleted)
                return Result<bool>.Fail("Bruger not found or not deleted.");

            bruger.IsDeleted = false;
            bruger.Status = SyncStatus.Synced;
            bruger.ModifiedBy = dto.ModifiedBy;
            bruger.LastSyncedVersion++;

            EntityHelper.SetDeletedOrRestoredProperties(bruger, "Restored Bruger", dto.ModifiedBy);

            var success = await _brugerRepository.UpdateBrugerAsync(bruger);
            return success ? Result<bool>.Ok(true) : Result<bool>.Fail("Failed to restore Bruger.");
        }

        #endregion

        #region Search Operations

        public async Task<Result<IEnumerable<BrugerDTO>>> GetBrugerByRoleAsync(string role)
        {
            var brugere = await _brugerRepository.GetBrugerByRoleAsync(role);
            return Result<IEnumerable<BrugerDTO>>.Ok(_mapper.Map<IEnumerable<BrugerDTO>>(brugere));
        }

        public async Task<Result<IEnumerable<BrugerDTO>>> GetBrugerByBælteAsync(string bæltegrad)
        {
            var brugere = await _brugerRepository.GetBrugerByBælteAsync(bæltegrad);
            return Result<IEnumerable<BrugerDTO>>.Ok(_mapper.Map<IEnumerable<BrugerDTO>>(brugere));
        }

        public async Task<Result<IEnumerable<BrugerDTO>>> GetBrugereByKlubAsync(Guid klubId)
        {
            var brugere = await _brugerRepository.GetBrugereByKlubAsync(klubId);
            return Result<IEnumerable<BrugerDTO>>.Ok(brugere); // Assuming already mapped
        }

        public async Task<Result<IEnumerable<BrugerDTO>>> GetBrugereByKlubAndBæltegradAsync(Guid klubId, string bæltegrad)
        {
            var brugere = await _brugerRepository.GetBrugereByKlubAndBæltegradAsync(klubId, bæltegrad);
            return Result<IEnumerable<BrugerDTO>>.Ok(brugere); // Assuming already mapped
        }

        public async Task<Result<BrugerDTO>> GetBrugerByBrugernavnAsync(string brugernavn)
        {
            var bruger = await _brugerRepository.GetBrugerByBrugernavnAsync(brugernavn);
            return bruger == null
                ? Result<BrugerDTO>.Fail("Bruger not found.")
                : Result<BrugerDTO>.Ok(_mapper.Map<BrugerDTO>(bruger));
        }

        public async Task<Result<IEnumerable<BrugerDTO>>> GetBrugerByFornavnEfternavnAsync(string fornavn, string efternavn)
        {
            var brugere = await _brugerRepository.GetBrugerByFornavnEfternavnAsync(fornavn, efternavn);
            return Result<IEnumerable<BrugerDTO>>.Ok(_mapper.Map<IEnumerable<BrugerDTO>>(brugere));
        }

        public async Task<Result<BrugerDTO>> AuthenticateBrugerAsync(LoginDTO loginDto)
        {
            var bruger = await _brugerRepository.AuthenticateBrugerAsync(loginDto.EmailOrBrugernavn, loginDto.Brugerkode);
            return bruger == null
                ? Result<BrugerDTO>.Fail("Invalid credentials.")
                : Result<BrugerDTO>.Ok(_mapper.Map<BrugerDTO>(bruger));
        }

        #endregion
    }
}
