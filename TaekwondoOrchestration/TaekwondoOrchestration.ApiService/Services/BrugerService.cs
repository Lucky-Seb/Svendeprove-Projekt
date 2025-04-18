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

        // Constructor should include the IJwtHelper parameter
        public BrugerService(IBrugerRepository brugerRepository, IMapper mapper, IJwtHelper jwtHelper)
        {
            _brugerRepository = brugerRepository;
            _mapper = mapper;
            _jwtHelper = jwtHelper; // Correct assignment
        }

        public async Task<Result<IEnumerable<BrugerDTO>>> GetAllBrugereAsync()
        {
            try
            {
                var brugere = await _brugerRepository.GetAllBrugereAsync();

                // If no users were returned, handle as failure
                if (brugere == null || !brugere.Any())
                {
                    return Result<IEnumerable<BrugerDTO>>.Fail("No users found.");
                }

                return Result<IEnumerable<BrugerDTO>>.Ok(_mapper.Map<IEnumerable<BrugerDTO>>(brugere));
            }
            catch (Exception ex)
            {
                // Log the exception (optional, depending on your logging strategy)
                return Result<IEnumerable<BrugerDTO>>.Fail($"Error occurred: {ex.Message}");
            }
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

            // Check if creation failed (created is null)
            if (created == null)
            {
                return Result<BrugerDTO>.Fail("Failed to create Bruger.");
            }

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

            if (brugere == null || !brugere.Any())
            {
                return Result<IEnumerable<BrugerDTO>>.Fail("No users found with this role.");
            }

            return Result<IEnumerable<BrugerDTO>>.Ok(_mapper.Map<IEnumerable<BrugerDTO>>(brugere));
        }

        public async Task<Result<IEnumerable<BrugerDTO>>> GetBrugerByBælteAsync(string bæltegrad)
        {
            var brugere = await _brugerRepository.GetBrugerByBælteAsync(bæltegrad);

            if (brugere == null || !brugere.Any())
            {
                return Result<IEnumerable<BrugerDTO>>.Fail("No users found with this bæltegrad.");
            }

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

            if (brugere == null || !brugere.Any())
            {
                return Result<IEnumerable<BrugerDTO>>.Fail("No users found with this name.");
            }

            return Result<IEnumerable<BrugerDTO>>.Ok(_mapper.Map<IEnumerable<BrugerDTO>>(brugere));
        }

        public async Task<Result<BrugerDTO>> AuthenticateBrugerAsync(LoginDTO loginDto)
        {
            var bruger = await _brugerRepository.GetBrugerByEmailOrBrugernavnAsync(loginDto.EmailOrBrugernavn);

            if (bruger == null)
            {
                return Result<BrugerDTO>.Fail("Invalid credentials.");
            }

            bool passwordMatch = BCrypt.Net.BCrypt.Verify(loginDto.Brugerkode, bruger.Brugerkode);
            if (!passwordMatch)
            {
                return Result<BrugerDTO>.Fail("Invalid credentials.");
            }

            var jwt = _jwtHelper.GenerateToken(bruger);
            if (jwt == null)
            {
                return Result<BrugerDTO>.Fail("Failed to generate JWT token.");
            }

            var userDto = _mapper.Map<BrugerDTO>(bruger);
            if (userDto == null)
            {
                return Result<BrugerDTO>.Fail("Failed to map Bruger to BrugerDTO.");
            }

            userDto.Token = jwt; // This is where it fails, ensure it's not null

            return Result<BrugerDTO>.Ok(userDto);
        }
    }
}
