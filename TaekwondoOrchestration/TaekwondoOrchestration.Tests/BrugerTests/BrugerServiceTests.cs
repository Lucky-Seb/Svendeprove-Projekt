using AutoMapper;
using TaekwondoApp.Shared.DTO;
using TaekwondoApp.Shared.Models;
using TaekwondoOrchestration.ApiService.Repositories;
using TaekwondoOrchestration.ApiService.RepositorieInterfaces;
using TaekwondoOrchestration.ApiService.ServiceInterfaces;
using TaekwondoOrchestration.ApiService.Helpers;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace TaekwondoOrchestration.ApiService.Services
{
    public class BrugerService : IBrugerService
    {
        private readonly IBrugerRepository _brugerRepository;
        private readonly IMapper _mapper;
        private readonly IJwtHelper _jwtHelper;

        // Fixed constructor - assigning jwtHelper parameter correctly
        public BrugerService(IBrugerRepository brugerRepository, IMapper mapper, IJwtHelper jwtHelper)
        {
            _brugerRepository = brugerRepository;
            _mapper = mapper;
            _jwtHelper = jwtHelper; // Correct assignment
        }

        public async Task<Result<IEnumerable<BrugerDTO>>> GetAllBrugereAsync()
        {
            var brugere = await _brugerRepository.GetAllBrugereAsync();
            return Result<IEnumerable<BrugerDTO>>.Ok(_mapper.Map<IEnumerable<BrugerDTO>>(brugere));
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
            var brugerEntity = _mapper.Map<Bruger>(brugerDto);
            EntityHelper.InitializeEntity(brugerEntity, brugerDto.ModifiedBy, "Created new Bruger.");
            var created = await _brugerRepository.CreateBrugerAsync(brugerEntity);

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
            if (bruger == null || bruger.IsDeleted)
                return Result<bool>.Fail("Bruger not found or already deleted.");

            EntityHelper.SetDeletedOrRestoredProperties(bruger, bruger.ModifiedBy ?? "system", "Soft-deleted Bruger");
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

            EntityHelper.SetDeletedOrRestoredProperties(bruger, dto.ModifiedBy, "Restored Bruger");
            var success = await _brugerRepository.UpdateBrugerAsync(bruger);

            return success ? Result<bool>.Ok(true) : Result<bool>.Fail("Failed to restore Bruger.");
        }

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
            return Result<IEnumerable<BrugerDTO>>.Ok(brugere); // Assuming already DTOs
        }

        public async Task<Result<IEnumerable<BrugerDTO>>> GetBrugereByKlubAndBæltegradAsync(Guid klubId, string bæltegrad)
        {
            var brugere = await _brugerRepository.GetBrugereByKlubAndBæltegradAsync(klubId, bæltegrad);
            return Result<IEnumerable<BrugerDTO>>.Ok(brugere); // Assuming already DTOs
        }

        public async Task<Result<BrugerDTO>> GetBrugerByBrugernavnAsync(string brugernavn)
        {
            var bruger = await _brugerRepository.GetBrugerByBrugernavnAsync(brugernavn);
            if (bruger == null)
                return Result<BrugerDTO>.Fail("Bruger not found.");

            return Result<BrugerDTO>.Ok(_mapper.Map<BrugerDTO>(bruger));
        }

        public async Task<Result<IEnumerable<BrugerDTO>>> GetBrugerByFornavnEfternavnAsync(string fornavn, string efternavn)
        {
            var brugere = await _brugerRepository.GetBrugerByFornavnEfternavnAsync(fornavn, efternavn);
            return Result<IEnumerable<BrugerDTO>>.Ok(_mapper.Map<IEnumerable<BrugerDTO>>(brugere));
        }

        public async Task<Result<BrugerDTO>> AuthenticateBrugerAsync(LoginDTO loginDto)
        {
            // Get the user by email or username
            var bruger = await _brugerRepository.GetBrugerByEmailOrBrugernavnAsync(loginDto.EmailOrBrugernavn);
            if (bruger == null)
            {
                return Result<BrugerDTO>.Fail("Invalid credentials.");
            }

            // Verify the password using BCrypt
            bool passwordMatch = BCrypt.Net.BCrypt.Verify(loginDto.Brugerkode, bruger.Brugerkode);
            if (!passwordMatch)
            {
                return Result<BrugerDTO>.Fail("Invalid credentials.");
            }

            // Use the injected JwtHelper to create the JWT token
            var jwt = _jwtHelper.GenerateToken(bruger);  // Now using the IJwtHelper to generate the token

            // Map the Bruger to BrugerDTO and include the generated token
            var userDto = _mapper.Map<BrugerDTO>(bruger);
            userDto.Token = jwt;

            return Result<BrugerDTO>.Ok(userDto);
        }
    }
}
