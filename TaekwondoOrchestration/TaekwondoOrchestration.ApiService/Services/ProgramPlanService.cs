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

        public ProgramPlanService(IProgramPlanRepository programPlanRepository, IMapper mapper)
        {
            _programPlanRepository = programPlanRepository;
            _mapper = mapper;
        }

        #region CRUD Operations

        // Get All Program Plans
        public async Task<Result<IEnumerable<ProgramPlanDTO>>> GetAllProgramPlansAsync()
        {
            var programPlansList = await _programPlanRepository.GetAllProgramPlansAsync();
            var mapped = _mapper.Map<IEnumerable<ProgramPlanDTO>>(programPlansList);
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

        // Create New Program Plan
        public async Task<Result<ProgramPlanDTO>> CreateProgramPlanWithBrugerAndKlubAsync(ProgramPlanDTO programPlanDto)
        {
            // Perform validation or any necessary checks on the DTO
            if (string.IsNullOrEmpty(programPlanDto.ProgramNavn))
            {
                return Result<ProgramPlanDTO>.Fail("ProgramPlan Name is required.");
            }

            var newProgramPlan = _mapper.Map<ProgramPlan>(programPlanDto);
            EntityHelper.InitializeEntity(newProgramPlan, programPlanDto.ModifiedBy, "Created new Program Plan.");
            var createdProgramPlan = await _programPlanRepository.CreateProgramPlanAsync(newProgramPlan);

            var mapped = _mapper.Map<ProgramPlanDTO>(createdProgramPlan);
            return Result<ProgramPlanDTO>.Ok(mapped);
        }
        public async Task<Result<bool>> UpdateProgramPlanAsync(Guid id, ProgramPlanDTO programPlanDto)
        {
            // Validate input
            if (string.IsNullOrEmpty(programPlanDto.ProgramNavn))
            {
                return Result<bool>.Fail("ProgramPlan Name is required.");
            }

            // Retrieve the existing program plan by ID
            var existingProgramPlan = await _programPlanRepository.GetProgramPlanByIdAsync(id);
            if (existingProgramPlan == null)
            {
                return Result<bool>.Fail("Program Plan not found.");
            }

            // Map the DTO to the existing program plan entity
            _mapper.Map(programPlanDto, existingProgramPlan);

            // Update common fields (e.g., ModifiedBy, ModifiedDate)
            EntityHelper.UpdateCommonFields(existingProgramPlan, programPlanDto.ModifiedBy);

            // Save the changes in the repository
            var updateSuccess = await _programPlanRepository.UpdateProgramPlanAsync(existingProgramPlan);

            // Return the result of the update operation
            return updateSuccess ? Result<bool>.Ok(true) : Result<bool>.Fail("Failed to update Program Plan.");
        }

        // Update Existing Program Plan
        public async Task<Result<ProgramPlanDTO>> UpdateProgramPlanWithBrugerAndKlubAsync(Guid id, ProgramPlanDTO programPlanDto)
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

        // Get Program by ID
        public async Task<Result<ProgramPlanDTO>> GetProgramByIdAsync(Guid id)
        {
            var program = await _programPlanRepository.GetProgramPlanByIdAsync(id);
            if (program == null)
                return Result<ProgramPlanDTO>.Fail("Program not found.");

            var mapped = _mapper.Map<ProgramPlanDTO>(program);
            return Result<ProgramPlanDTO>.Ok(mapped);
        }

        #endregion
    }
}
