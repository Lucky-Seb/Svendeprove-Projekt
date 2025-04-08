using Microsoft.AspNetCore.Mvc;
using TaekwondoOrchestration.ApiService.Services;
using TaekwondoOrchestration.ApiService.DTO;

namespace TaekwondoOrchestration.ApiService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BrugerØvelseController : ControllerBase
    {
        private readonly BrugerØvelseService _brugerØvelseService;

        public BrugerØvelseController(BrugerØvelseService brugerØvelseService)
        {
            _brugerØvelseService = brugerØvelseService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<BrugerØvelseDTO>>> GetBrugerØvelser()
        {
            return Ok(await _brugerØvelseService.GetAllBrugerØvelserAsync());
        }

        [HttpGet("{brugerId}/{øvelseId}")]
        public async Task<ActionResult<BrugerØvelseDTO>> GetBrugerØvelse(int brugerId, int øvelseId)
        {
            var brugerØvelse = await _brugerØvelseService.GetBrugerØvelseByIdAsync(brugerId, øvelseId);
            if (brugerØvelse == null)
                return NotFound();
            return Ok(brugerØvelse);
        }

        [HttpPost]
        public async Task<ActionResult<BrugerØvelseDTO>> PostBrugerØvelse(BrugerØvelseDTO brugerØvelseDto)
        {
            var createdBrugerØvelse = await _brugerØvelseService.CreateBrugerØvelseAsync(brugerØvelseDto);
            return CreatedAtAction(nameof(GetBrugerØvelse), new { brugerId = createdBrugerØvelse.BrugerID, øvelseId = createdBrugerØvelse.ØvelseID }, createdBrugerØvelse);
        }

        [HttpDelete("{brugerId}/{øvelseId}")]
        public async Task<IActionResult> DeleteBrugerØvelse(int brugerId, int øvelseId)
        {
            var success = await _brugerØvelseService.DeleteBrugerØvelseAsync(brugerId, øvelseId);
            if (!success)
                return NotFound();
            return NoContent();
        }
    }
}
