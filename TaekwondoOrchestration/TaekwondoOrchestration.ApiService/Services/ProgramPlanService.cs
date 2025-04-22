using TaekwondoApp.Shared.DTO;
using TaekwondoApp.Shared.Models;
using TaekwondoOrchestration.ApiService.Repositories;
using TaekwondoOrchestration.ApiService.RepositorieInterfaces;
using AutoMapper;
using TaekwondoOrchestration.ApiService.ServiceInterfaces;
using TaekwondoOrchestration.ApiService.Helpers;

namespace TaekwondoOrchestration.ApiService.Services
{
    public class ProgramPlanService : IProgramPlanService
    {
        private readonly IProgramPlanRepository _programPlanRepository;
        private readonly IMapper _mapper;
        private readonly IBrugerProgramService _brugerProgramService;
        private readonly IKlubProgramService _klubProgramService;
        private readonly ITræningService _træningService;
        public ProgramPlanService(
            IProgramPlanRepository programPlanRepository,
            IMapper mapper,
            IBrugerProgramService brugerProgramService,
            IKlubProgramService klubProgramService,
            ITræningService træningService)
        {
            _programPlanRepository = programPlanRepository;
            _mapper = mapper;
            _brugerProgramService = brugerProgramService;
            _klubProgramService = klubProgramService;
            _træningService = træningService;
        }

        #region CRUD Operations

        // Get All Program Plans
        public async Task<Result<IEnumerable<ProgramPlanDTO>>> GetAllProgramPlansAsync()
        {
            var programPlansList = await _programPlanRepository.GetAllProgramPlansAsync();
            var mapped = _mapper.Map<IEnumerable<ProgramPlanDTO>>(programPlansList);
            return Result<IEnumerable<ProgramPlanDTO>>.Ok(mapped);
        }

        // Create New Program Plan with associated Bruger or Klub and Træning entities
        public async Task<Result<ProgramPlanDTO>> CreateProgramPlanWithBrugerOrKlubAsync(ProgramPlanDTO programPlanDto)
        {
            // Perform validation or any necessary checks on the DTO
            if (string.IsNullOrEmpty(programPlanDto.ProgramNavn))
            {
                return Result<ProgramPlanDTO>.Fail("ProgramPlan Name is required.");
            }

            var newProgramPlan = _mapper.Map<ProgramPlan>(programPlanDto);
            EntityHelper.InitializeEntity(newProgramPlan, programPlanDto.ModifiedBy, "Created new Program Plan.");

            // Create the ProgramPlan
            var createdProgramPlan = await _programPlanRepository.CreateProgramPlanAsync(newProgramPlan);

            // Create the appropriate Program (BrugerProgram or KlubProgram) based on who created it
            if (programPlanDto.BrugerID != Guid.Empty)
            {
                // If a Bruger is creating the Program Plan, create a BrugerProgram
                var brugerProgramDto = new BrugerProgramDTO
                {
                    BrugerID = programPlanDto.BrugerID,
                    ProgramID = createdProgramPlan.ProgramID
                };

                var createdBrugerProgram = await _brugerProgramService.CreateBrugerProgramAsync(brugerProgramDto);
                if (createdBrugerProgram == null)
                {
                    return Result<ProgramPlanDTO>.Fail("Failed to create BrugerProgram.");
                }
            }
            else if (programPlanDto.KlubID != Guid.Empty)
            {
                // If a Klub is creating the Program Plan, create a KlubProgram
                var klubProgramDto = new KlubProgramDTO
                {
                    KlubID = programPlanDto.KlubID,
                    ProgramID = createdProgramPlan.ProgramID
                };

                var createdKlubProgram = await _klubProgramService.CreateKlubProgramAsync(klubProgramDto);
                if (createdKlubProgram == null)
                {
                    return Result<ProgramPlanDTO>.Fail("Failed to create KlubProgram.");
                }
            }
            else
            {
                return Result<ProgramPlanDTO>.Fail("Either BrugerID or KlubID must be provided.");
            }

            // Create the associated Træning entities
            if (programPlanDto.Træninger != null && programPlanDto.Træninger.Any())
            {
                foreach (var træningDto in programPlanDto.Træninger)
                {
                    var træningResult = await _træningService.CreateTræningAsync(træningDto);
                    if (træningResult.Failure)
                    {
                        return Result<ProgramPlanDTO>.Fail($"Failed to create Træning for ProgramPlan. Error: {træningResult.Failure}");
                    }
                }
            }

            var mappedProgramPlan = _mapper.Map<ProgramPlanDTO>(createdProgramPlan);
            return Result<ProgramPlanDTO>.Ok(mappedProgramPlan);
        }

        public async Task<Result<ProgramPlanDTO>> UpdateProgramPlanAsync(Guid programId,ProgramPlanDTO updatedDto)
        {
            // 1. Validate ProgramNavn
            if (string.IsNullOrEmpty(updatedDto.ProgramNavn))
                return Result<ProgramPlanDTO>.Fail("ProgramPlan Name is required.");

            // 2. Fetch existing ProgramPlan from DB
            var existingPlan = await _programPlanRepository.GetProgramPlanByIdAsync(programId);
            if (existingPlan == null)
                return Result<ProgramPlanDTO>.Fail("ProgramPlan not found.");

            // 3. Update ProgramPlan fields
            existingPlan.ProgramNavn = updatedDto.ProgramNavn;
            existingPlan.Beskrivelse = updatedDto.Beskrivelse;
            existingPlan.Længde = updatedDto.Længde;
            EntityHelper.InitializeEntity(existingPlan, updatedDto.ModifiedBy, "Updated Program Plan");

            await _programPlanRepository.UpdateProgramPlanAsync(existingPlan);

            // 4. Get existing træninger from DB
            var træningerResult = await _træningService.GetTræningByProgramIdAsync(programId);
            if (træningerResult.Failure)
            {
                return Result<ProgramPlanDTO>.Fail("Failed to retrieve existing træninger.");
            }

            var existingTræninger = træningerResult.Value.ToList();

            var updatedTræninger = updatedDto.Træninger ?? new List<TræningDTO>();

            // 5. Handle Deletions
            var træningIdsToKeep = updatedTræninger
                .Where(t => t.TræningID != Guid.Empty)
                .Select(t => t.TræningID)
                .ToHashSet();

            var træningerToDelete = existingTræninger
                .Where(et => !træningIdsToKeep.Contains(et.TræningID))
                .ToList();

            foreach (var træning in træningerToDelete)
            {
                await _træningService.DeleteTræningAsync(træning.TræningID);
            }

            // 6. Handle Additions & Updates
            foreach (var træningDto in updatedTræninger)
            {
                if (træningDto.TræningID == Guid.Empty)
                {
                    // New træning
                    træningDto.ProgramID = updatedDto.ProgramID;
                    var createResult = await _træningService.CreateTræningAsync(træningDto);
                    if (createResult.Failure)
                        return Result<ProgramPlanDTO>.Fail($"Failed to add træning: {createResult.Failure}");
                }
                else
                {
                    // Update existing træning
                    var existing = existingTræninger.FirstOrDefault(t => t.TræningID == træningDto.TræningID);
                    if (existing != null)
                    {
                        await _træningService.UpdateTræningAsync(træningDto.TræningID, træningDto);
                    }
                }
            }

            // 7. Return updated ProgramPlanDTO
            var mapped = _mapper.Map<ProgramPlanDTO>(existingPlan);
            return Result<ProgramPlanDTO>.Ok(mapped);
        }


        // Update Existing Program Plan
        public async Task<Result<ProgramPlanDTO>> UpdateProgramPlanWithBrugerOrKlubAsync(Guid id, ProgramPlanDTO programPlanDto)
        {
            if (string.IsNullOrEmpty(programPlanDto.ProgramNavn))
            {
                return Result<ProgramPlanDTO>.Fail("ProgramPlan Name is required.");
            }

            var existingProgramPlan = await _programPlanRepository.GetProgramPlanByIdAsync(id);
            if (existingProgramPlan == null)
                return Result<ProgramPlanDTO>.Fail("Program Plan not found.");

            _mapper.Map(programPlanDto, existingProgramPlan);
            EntityHelper.UpdateCommonFields(existingProgramPlan, programPlanDto.ModifiedBy);
            var updateSuccess = await _programPlanRepository.UpdateProgramPlanAsync(existingProgramPlan);

            return updateSuccess ? Result<ProgramPlanDTO>.Ok(_mapper.Map<ProgramPlanDTO>(existingProgramPlan)) : Result<ProgramPlanDTO>.Fail("Failed to update Program Plan.");
        }

        // Soft Delete Program Plan
        public async Task<Result<bool>> DeleteProgramPlanAsync(Guid id)
        {
            var programPlan = await _programPlanRepository.GetProgramPlanByIdAsync(id);
            if (programPlan == null)
                return Result<bool>.Fail("Program Plan not found.");

            // Soft delete logic
            string modifiedBy = programPlan.ModifiedBy; // Assuming user context
            EntityHelper.SetDeletedOrRestoredProperties(programPlan, "Soft-deleted Program Plan", modifiedBy);

            var success = await _programPlanRepository.UpdateProgramPlanAsync(programPlan);
            return success ? Result<bool>.Ok(true) : Result<bool>.Fail("Failed to delete Program Plan.");
        }

        // Restore Program Plan from Soft-Delete
        public async Task<Result<bool>> RestoreProgramPlanAsync(Guid id, ProgramPlanDTO dto)
        {
            var programPlan = await _programPlanRepository.GetProgramPlanByIdIncludingDeletedAsync(id);
            if (programPlan == null || !programPlan.IsDeleted)
                return Result<bool>.Fail("Program Plan not found or not deleted.");

            programPlan.IsDeleted = false;
            programPlan.Status = SyncStatus.Synced;
            programPlan.ModifiedBy = dto.ModifiedBy;
            programPlan.LastSyncedVersion++;

            // Set properties for restored entry
            EntityHelper.SetDeletedOrRestoredProperties(programPlan, "Restored Program Plan", dto.ModifiedBy);

            var success = await _programPlanRepository.UpdateProgramPlanAsync(programPlan);
            return success ? Result<bool>.Ok(true) : Result<bool>.Fail("Failed to restore Program Plan.");
        }

        #endregion

        #region Get Operations

        // Get Programs by User (Bruger)
        public async Task<Result<IEnumerable<ProgramPlanDTO>>> GetAllProgramPlansByBrugerIdAsync(Guid brugerId)
        {
            var programPlans = await _programPlanRepository.GetAllProgramPlansByBrugerIdAsync(brugerId);
            var mapped = _mapper.Map<IEnumerable<ProgramPlanDTO>>(programPlans);
            return Result<IEnumerable<ProgramPlanDTO>>.Ok(mapped);
        }

        // Get Programs by Club (Klub)
        public async Task<Result<IEnumerable<ProgramPlanDTO>>> GetAllProgramPlansByKlubIdAsync(Guid klubId)
        {
            var programPlans = await _programPlanRepository.GetAllProgramPlansByKlubIdAsync(klubId);
            var mapped = _mapper.Map<IEnumerable<ProgramPlanDTO>>(programPlans);
            return Result<IEnumerable<ProgramPlanDTO>>.Ok(mapped);
        }

        // Get All Programs
        public async Task<Result<IEnumerable<ProgramPlanDTO>>> GetAllProgramsAsync()
        {
            var programs = await _programPlanRepository.GetAllProgramPlansAsync();
            var mapped = _mapper.Map<IEnumerable<ProgramPlanDTO>>(programs);
            return Result<IEnumerable<ProgramPlanDTO>>.Ok(mapped);
        }

        // Get Program Plan by ID
        public async Task<Result<ProgramPlanDTO>> GetProgramPlanByIdAsync(Guid id)
        {
            var programPlan = await _programPlanRepository.GetProgramPlanByIdAsync(id);
            if (programPlan == null)
                return Result<ProgramPlanDTO>.Fail("Program Plan not found.");

            var mapped = _mapper.Map<ProgramPlanDTO>(programPlan);
            return Result<ProgramPlanDTO>.Ok(mapped);
        }
        // Get Program Plan with all details by ID
        public async Task<Result<ProgramPlanDTO>> GetProgramPlanWithDetailsAsync(Guid id)
        {
            var programPlan = await _programPlanRepository.GetProgramPlanWithDetailsAsync(id);
            if (programPlan == null)
                return Result<ProgramPlanDTO>.Fail("Program Plan not found.");

            var mapped = _mapper.Map<ProgramPlanDTO>(programPlan);
            return Result<ProgramPlanDTO>.Ok(mapped);
        }

        #endregion
    }
}
