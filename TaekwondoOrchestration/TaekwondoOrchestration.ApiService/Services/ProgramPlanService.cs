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
        private readonly ITræningRepository _træningRepository;
        private readonly IKlubProgramRepository _klubProgramRepository;
        private readonly IBrugerProgramRepository _brugerProgramRepository;
        private readonly IMapper _mapper;

        public ProgramPlanService(
            IProgramPlanRepository programPlanRepository,
            ITræningRepository træningRepository,
            IKlubProgramRepository klubProgramRepository,
            IBrugerProgramRepository brugerProgramRepository,
            IMapper mapper)
        {
            _programPlanRepository = programPlanRepository ?? throw new ArgumentNullException(nameof(programPlanRepository));
            _træningRepository = træningRepository ?? throw new ArgumentNullException(nameof(træningRepository));
            _klubProgramRepository = klubProgramRepository ?? throw new ArgumentNullException(nameof(klubProgramRepository));
            _brugerProgramRepository = brugerProgramRepository ?? throw new ArgumentNullException(nameof(brugerProgramRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        #region CRUD Operations

        // Get All Program Plans
        public async Task<List<ProgramPlanDTO>> GetAllProgramPlansAsync()
        {
            var programPlans = await _programPlanRepository.GetAllProgramPlansAsync();
            return _mapper.Map<List<ProgramPlanDTO>>(programPlans);
        }

        // Get Program Plan by ID
        public async Task<ProgramPlanDTO?> GetProgramPlanByIdAsync(Guid id)
        {
            var programPlan = await _programPlanRepository.GetProgramPlanByIdAsync(id);
            if (programPlan == null)
                return null;

            return _mapper.Map<ProgramPlanDTO>(programPlan);
        }

        // Create New Program Plan
        public async Task<ProgramPlanDTO> CreateProgramPlanWithBrugerAndKlubAsync(ProgramPlanDTO dto)
        {
            if (string.IsNullOrEmpty(dto.ProgramNavn))
                throw new ArgumentException("Program name is required.");

            var programPlan = _mapper.Map<ProgramPlan>(dto);
            EntityHelper.InitializeEntity(programPlan, dto.ModifiedBy, "Created new Program Plan.");

            var createdProgramPlan = await _programPlanRepository.CreateProgramPlanAsync(programPlan);

            // Handle BrugerProgram creation
            if (dto.BrugerID != null)
            {
                var brugerProgram = new BrugerProgram
                {
                    BrugerID = dto.BrugerID,
                    ProgramID = createdProgramPlan.ProgramID
                };
                await _brugerProgramRepository.CreateBrugerProgramAsync(brugerProgram);
            }

            // Handle KlubProgram creation
            if (dto.KlubID != null)
            {
                var klubProgram = new KlubProgram
                {
                    KlubID = dto.KlubID,
                    ProgramID = createdProgramPlan.ProgramID
                };
                await _klubProgramRepository.CreateKlubProgramAsync(klubProgram);
            }

            // Handle Træning creation
            foreach (var træningDTO in dto.Træninger)
            {
                var træning = _mapper.Map<Træning>(træningDTO);
                træning.ProgramID = createdProgramPlan.ProgramID;
                await _træningRepository.CreateTræningAsync(træning);
            }

            return _mapper.Map<ProgramPlanDTO>(createdProgramPlan);
        }

        // Update Existing Program Plan
        public async Task<ProgramPlanDTO> UpdateProgramPlanWithBrugerAndKlubAsync(Guid id, ProgramPlanDTO dto)
        {
            var existingProgramPlan = await _programPlanRepository.GetProgramPlanByIdAsync(id);
            if (existingProgramPlan == null)
                return null;

            _mapper.Map(dto, existingProgramPlan);
            EntityHelper.UpdateCommonFields(existingProgramPlan, dto.ModifiedBy);

            // Handle Træning update
            var existingTræninger = await _træningRepository.GetByProgramIdAsync(id);
            foreach (var træningDTO in dto.Træninger)
            {
                var existingTræning = existingTræninger.FirstOrDefault(t => t.TræningID == træningDTO.TræningID);
                if (existingTræning != null)
                {
                    _mapper.Map(træningDTO, existingTræning);
                    await _træningRepository.UpdateTræningAsync(existingTræning);
                }
                else
                {
                    var newTræning = _mapper.Map<Træning>(træningDTO);
                    newTræning.ProgramID = existingProgramPlan.ProgramID;
                    await _træningRepository.CreateTræningAsync(newTræning);
                }
            }

            var updateSuccess = await _programPlanRepository.UpdateProgramPlanAsync(existingProgramPlan);
            if (updateSuccess)
            {
                return _mapper.Map<ProgramPlanDTO>(existingProgramPlan);
            }
            return null;
        }

        // Update Program Plan (generic update with boolean return)
        public async Task<bool> UpdateProgramPlanAsync(Guid id, ProgramPlanDTO programPlanDto)
        {
            var existingProgramPlan = await _programPlanRepository.GetProgramPlanByIdAsync(id);
            if (existingProgramPlan == null)
                return false;

            _mapper.Map(programPlanDto, existingProgramPlan);
            EntityHelper.UpdateCommonFields(existingProgramPlan, programPlanDto.ModifiedBy);

            // Handle Træning update
            var existingTræninger = await _træningRepository.GetByProgramIdAsync(id);
            foreach (var træningDTO in programPlanDto.Træninger)
            {
                var existingTræning = existingTræninger.FirstOrDefault(t => t.TræningID == træningDTO.TræningID);
                if (existingTræning != null)
                {
                    _mapper.Map(træningDTO, existingTræning);
                    await _træningRepository.UpdateTræningAsync(existingTræning);
                }
                else
                {
                    var newTræning = _mapper.Map<Træning>(træningDTO);
                    newTræning.ProgramID = existingProgramPlan.ProgramID;
                    await _træningRepository.CreateTræningAsync(newTræning);
                }
            }

            return await _programPlanRepository.UpdateProgramPlanAsync(existingProgramPlan);
        }

        // Delete Program Plan
        public async Task<bool> DeleteProgramPlanAsync(Guid id)
        {
            var programPlan = await _programPlanRepository.GetProgramPlanByIdAsync(id);
            if (programPlan == null)
                return false;

            string modifiedBy = programPlan.ModifiedBy;
            EntityHelper.SetDeletedOrRestoredProperties(programPlan, "Soft-deleted program plan", modifiedBy);

            return await _programPlanRepository.UpdateProgramPlanAsync(programPlan);
        }

        // Get All Programs with related entities (e.g., training, quiz, etc.)
        public async Task<List<ProgramPlanDTO>> GetAllProgramsAsync()
        {
            var programPlans = await _programPlanRepository.GetAllProgramPlansAsync();
            return _mapper.Map<List<ProgramPlanDTO>>(programPlans);
        }

        // Get Program by ID
        public async Task<ProgramPlanDTO?> GetProgramByIdAsync(Guid id)
        {
            var programPlan = await _programPlanRepository.GetProgramPlanByIdAsync(id);
            if (programPlan == null)
                return null;

            return _mapper.Map<ProgramPlanDTO>(programPlan);
        }

        // Get Programs by Bruger
        public async Task<List<ProgramPlanDTO>> GetAllProgramPlansByBrugerIdAsync(Guid brugerId)
        {
            var programPlans = await _programPlanRepository.GetAllProgramPlansByBrugerIdAsync(brugerId);
            return _mapper.Map<List<ProgramPlanDTO>>(programPlans);
        }

        // Get Programs by Klub
        public async Task<List<ProgramPlanDTO>> GetAllProgramPlansByKlubIdAsync(Guid klubId)
        {
            var programPlans = await _programPlanRepository.GetAllProgramPlansByKlubIdAsync(klubId);
            return _mapper.Map<List<ProgramPlanDTO>>(programPlans);
        }

        #endregion
    }
}
