using Microsoft.AspNetCore.Mvc;
using TaekwondoOrchestration.ApiService.Services;
using TaekwondoApp.Shared.DTO;


namespace TaekwondoOrchestration.ApiService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class KlubProgramController : ControllerBase
    {
        private readonly KlubProgramService _klubProgramService;

        public KlubProgramController(KlubProgramService klubProgramService)
        {
            _klubProgramService = klubProgramService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<KlubProgramDTO>>> GetKlubProgrammer()
        {
            return Ok(await _klubProgramService.GetAllKlubProgrammerAsync());
        }

        [HttpGet("{klubId}/{programId}")]
        public async Task<ActionResult<KlubProgramDTO>> GetKlubProgram(Guid klubId, Guid programId)
        {
            var klubProgram = await _klubProgramService.GetKlubProgramByIdAsync(klubId, programId);
            if (klubProgram == null)
                return NotFound();
            return Ok(klubProgram);
        }

        [HttpPost]
        public async Task<ActionResult<KlubProgramDTO>> PostKlubProgram(KlubProgramDTO klubProgramDto)
        {
            var createdKlubProgram = await _klubProgramService.CreateKlubProgramAsync(klubProgramDto);
            return CreatedAtAction(nameof(GetKlubProgram), new { klubId = createdKlubProgram.KlubID, programId = createdKlubProgram.ProgramID }, createdKlubProgram);
        }

        [HttpDelete("{klubId}/{programId}")]
        public async Task<IActionResult> DeleteKlubProgram(Guid klubId, Guid programId)
        {
            var success = await _klubProgramService.DeleteKlubProgramAsync(klubId, programId);
            if (!success)
                return NotFound();
            return NoContent();
        }
    }
}
