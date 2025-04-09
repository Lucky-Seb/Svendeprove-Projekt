using Microsoft.AspNetCore.Mvc;
using TaekwondoOrchestration.ApiService.Services;
using TaekwondoApp.Shared.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TaekwondoOrchestration.ApiService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProgramPlanController : ControllerBase
    {
        private readonly ProgramPlanService _programPlanService;

        public ProgramPlanController(ProgramPlanService programPlanService)
        {
            _programPlanService = programPlanService;
        }

        // GET: api/programplan
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProgramPlanDTO>>> GetProgramPlans()
        {
            var programPlanList = await _programPlanService.GetAllProgramPlansAsync();
            return Ok(programPlanList);
        }

        // GET: api/programplan/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ProgramPlanDTO>> GetProgramPlan(int id)
        {
            var programPlan = await _programPlanService.GetProgramPlanByIdAsync(id);
            if (programPlan == null)
                return NotFound();
            return Ok(programPlan);
        }

        // POST: api/programplan
        //[HttpPost]
        //public async Task<ActionResult<ProgramPlanDTO>> PostProgramPlan(ProgramPlanCreateDTO programPlanDto)
        //{
        //    var createdProgramPlan = await _programPlanService.CreateProgramPlanAsync(programPlanDto);
        //    return CreatedAtAction(nameof(GetProgramPlan), new { id = createdProgramPlan.ProgramID }, createdProgramPlan);
        //}
        [HttpPost]
        public async Task<ActionResult<ProgramPlanDTO>> PostQuiz(ProgramPlanDTO programPlanDTO)
        {
            if (programPlanDTO == null)
            {
                return BadRequest("Invalid data");
            }
            var createdProgramPlan = await _programPlanService.CreateProgramPlanWithBrugerAndKlubAsync(programPlanDTO);
            return CreatedAtAction(nameof(GetProgramPlan), new { id = createdProgramPlan.ProgramID }, createdProgramPlan);
        }

        // PUT: api/programplan/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProgramPlan(int id, ProgramPlanDTO programPlanDto)
        {
            var success = await _programPlanService.UpdateProgramPlanAsync(id, programPlanDto);
            if (!success)
                return BadRequest();
            return NoContent();
        }

        // DELETE: api/programplan/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProgramPlan(int id)
        {
            var success = await _programPlanService.DeleteProgramPlanAsync(id);
            if (!success)
                return NotFound();
            return NoContent();
        }
        [HttpPut("træning/{id}")]
        public async Task<IActionResult> PutProgramPlanwithtræning(int id, ProgramPlanDTO programPlanDto)
        {
            var updatedProgramPlan = await _programPlanService.UpdateProgramPlanWithBrugerAndKlubAsync(id, programPlanDto);
            if (updatedProgramPlan == null)
                return NotFound();

            // Return the updated ProgramPlanDTO with a 200 OK response
            return Ok(updatedProgramPlan);
        }
        [HttpGet("by-bruger/{brugerId}")]
        public async Task<ActionResult<List<ProgramPlanDTO>>> GetAllProgrammmerByBruger(int brugerId)
        {
            var programs = await _programPlanService.GetProgramsByBrugerAsync(brugerId);
            return Ok(programs);
        }
        [HttpGet("by-klub/{klubId}")]
        public async Task<ActionResult<List<ProgramPlanDTO>>> GetAllProgrammerByKlub(int klubId)
        {
            var programs = await _programPlanService.GetProgramsByBrugerAsync(klubId);
            return Ok(programs);
        }
        // Get all programs
        [HttpGet("all")]
        public async Task<ActionResult<List<ProgramPlanDTO>>> GetAllPrograms()
        {
            var programs = await _programPlanService.GetAllProgramsAsync();
            return Ok(programs);
        }

        // Get program by ID
        [HttpGet("/{id}")]
        public async Task<ActionResult<ProgramPlanDTO>> GetProgramById(int id)
        {
            var program = await _programPlanService.GetProgramByIdAsync(id);
            if (program == null)
            {
                return NotFound($"Program with ID {id} not found.");
            }
            return Ok(program);
        }
    }
}
