using TaekwondoApp.Shared.DTO;
using TaekwondoApp.Shared.Models;
using TaekwondoOrchestration.ApiService.Repositories;
using TaekwondoOrchestration.ApiService.RepositorieInterfaces;
using AutoMapper;
using TaekwondoOrchestration.ApiService.ServiceInterfaces;
using TaekwondoOrchestration.ApiService.Helpers;

namespace TaekwondoOrchestration.ApiService.Services
{
    public class ØvelseService : IØvelseService
    {
        private readonly IØvelseRepository _øvelseRepository;
        private readonly IBrugerØvelseRepository _brugerØvelseRepository;
        private readonly IKlubØvelseRepository _klubØvelseRepository;
        private readonly IMapper _mapper;

        // Constructor: Dependency Injection of Repository, Mapper, and other services
        public ØvelseService(IØvelseRepository øvelseRepository,
                             IBrugerØvelseRepository brugerØvelseRepository,
                             IKlubØvelseRepository klubØvelseRepository,
                             IMapper mapper)
        {
            _øvelseRepository = øvelseRepository;
            _brugerØvelseRepository = brugerØvelseRepository;
            _klubØvelseRepository = klubØvelseRepository;
            _mapper = mapper;
        }

        #region CRUD Operations

        // Get All Øvelser
        public async Task<Result<IEnumerable<ØvelseDTO>>> GetAllØvelserAsync()
        {
            var øvelser = await _øvelseRepository.GetAllØvelserAsync();
            var mapped = _mapper.Map<IEnumerable<ØvelseDTO>>(øvelser);
            return Result<IEnumerable<ØvelseDTO>>.Ok(mapped);
        }

        // Get Øvelse by ID
        public async Task<Result<ØvelseDTO>> GetØvelseByIdAsync(Guid id)
        {
            var øvelse = await _øvelseRepository.GetØvelseByIdAsync(id);
            if (øvelse == null)
                return Result<ØvelseDTO>.Fail("Øvelse not found.");

            var mapped = _mapper.Map<ØvelseDTO>(øvelse);
            return Result<ØvelseDTO>.Ok(mapped);
        }

        // Create New Øvelse
        public async Task<Result<ØvelseDTO>> CreateØvelseAsync(ØvelseDTO øvelseDto)
        {
            var newØvelse = _mapper.Map<Øvelse>(øvelseDto);
            EntityHelper.InitializeEntity(newØvelse, øvelseDto.ModifiedBy, "Created new Øvelse.");
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

            var mapped = _mapper.Map<ØvelseDTO>(createdØvelse);
            return Result<ØvelseDTO>.Ok(mapped);
        }

        // Update Existing Øvelse
        public async Task<Result<bool>> UpdateØvelseAsync(Guid id, ØvelseDTO øvelseDto)
        {
            if (string.IsNullOrEmpty(øvelseDto.ØvelseNavn) ||
                string.IsNullOrEmpty(øvelseDto.ØvelseBeskrivelse) ||
                øvelseDto.ØvelseTid <= 0 ||
                string.IsNullOrEmpty(øvelseDto.ØvelseSværhed))
            {
                return Result<bool>.Fail("Invalid input data.");
            }

            var existingØvelse = await _øvelseRepository.GetØvelseByIdAsync(id);
            if (existingØvelse == null)
                return Result<bool>.Fail("Øvelse not found.");

            _mapper.Map(øvelseDto, existingØvelse);
            EntityHelper.UpdateCommonFields(existingØvelse, øvelseDto.ModifiedBy);
            var updateSuccess = await _øvelseRepository.UpdateØvelseAsync(existingØvelse);

            return updateSuccess ? Result<bool>.Ok(true) : Result<bool>.Fail("Failed to update Øvelse.");
        }

        // Delete Øvelse (Soft-Delete)
        public async Task<Result<bool>> DeleteØvelseAsync(Guid id)
        {
            var øvelse = await _øvelseRepository.GetØvelseByIdAsync(id);
            if (øvelse == null || øvelse.IsDeleted)
                return Result<bool>.Fail("Øvelse not found or already deleted.");

            // Soft delete
            string modifiedBy = øvelse.ModifiedBy; // Pass user context for ModifiedBy
            EntityHelper.SetDeletedOrRestoredProperties(øvelse, "Soft-deleted Øvelse entry", modifiedBy);

            var success = await _øvelseRepository.UpdateØvelseAsync(øvelse);
            return success ? Result<bool>.Ok(true) : Result<bool>.Fail("Failed to delete Øvelse.");
        }

        // Restore Øvelse from Soft-Delete
        public async Task<Result<bool>> RestoreØvelseAsync(Guid id, ØvelseDTO dto)
        {
            var øvelse = await _øvelseRepository.GetØvelseByIdIncludingDeletedAsync(id);
            if (øvelse == null || !øvelse.IsDeleted)
                return Result<bool>.Fail("Øvelse not found or not deleted.");

            // Restore the Øvelse entry
            øvelse.IsDeleted = false;
            øvelse.Status = SyncStatus.Synced;
            øvelse.ModifiedBy = dto.ModifiedBy;
            øvelse.LastSyncedVersion++;

            // Set properties for restored entry
            EntityHelper.SetDeletedOrRestoredProperties(øvelse, "Restored Øvelse entry", dto.ModifiedBy);

            var success = await _øvelseRepository.UpdateØvelseAsync(øvelse);
            return success ? Result<bool>.Ok(true) : Result<bool>.Fail("Failed to restore Øvelse.");
        }

        #endregion

        #region Search Operations

        // Get Øvelser by Sværhed (Difficulty)
        public async Task<Result<IEnumerable<ØvelseDTO>>> GetØvelserBySværhedAsync(string sværhed)
        {
            var øvelser = await _øvelseRepository.GetØvelserBySværhedAsync(sværhed);
            var mapped = _mapper.Map<IEnumerable<ØvelseDTO>>(øvelser);
            return Result<IEnumerable<ØvelseDTO>>.Ok(mapped);
        }

        // Get Øvelser by Bruger (User)
        public async Task<Result<IEnumerable<ØvelseDTO>>> GetØvelserByBrugerAsync(Guid brugerId)
        {
            var øvelser = await _øvelseRepository.GetØvelserByBrugerAsync(brugerId);
            var mapped = _mapper.Map<IEnumerable<ØvelseDTO>>(øvelser);
            return Result<IEnumerable<ØvelseDTO>>.Ok(mapped);
        }

        // Get Øvelser by Klub (Club)
        public async Task<Result<IEnumerable<ØvelseDTO>>> GetØvelserByKlubAsync(Guid klubId)
        {
            var øvelser = await _øvelseRepository.GetØvelserByKlubAsync(klubId);
            var mapped = _mapper.Map<IEnumerable<ØvelseDTO>>(øvelser);
            return Result<IEnumerable<ØvelseDTO>>.Ok(mapped);
        }

        // Get Øvelser by Navn (Name)
        public async Task<Result<IEnumerable<ØvelseDTO>>> GetØvelserByNavnAsync(string navn)
        {
            var øvelser = await _øvelseRepository.GetØvelserByNavnAsync(navn);
            var mapped = _mapper.Map<IEnumerable<ØvelseDTO>>(øvelser);
            return Result<IEnumerable<ØvelseDTO>>.Ok(mapped);
        }

        #endregion
    }
}
