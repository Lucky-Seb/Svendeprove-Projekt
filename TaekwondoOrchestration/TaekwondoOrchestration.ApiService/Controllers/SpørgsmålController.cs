using Microsoft.AspNetCore.Mvc;
using TaekwondoOrchestration.ApiService.Services;
using TaekwondoOrchestration.ApiService.DTO;
namespace TaekwondoOrchestration.ApiService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SpørgsmålController : ControllerBase
    {
        private readonly SpørgsmålService _spørgsmålService;

        public SpørgsmålController(SpørgsmålService spørgsmålService)
        {
            _spørgsmålService = spørgsmålService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<SpørgsmålDTO>>> GetSpørgsmål()
        {
            return Ok(await _spørgsmålService.GetAllSpørgsmålAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<SpørgsmålDTO>> GetSpørgsmål(int id)
        {
            var spørgsmål = await _spørgsmålService.GetSpørgsmålByIdAsync(id);
            if (spørgsmål == null)
                return NotFound();
            return Ok(spørgsmål);
        }

        [HttpPost]
        public async Task<ActionResult<SpørgsmålDTO>> PostSpørgsmål(SpørgsmålDTO spørgsmålDto)
        {
            var createdSpørgsmål = await _spørgsmålService.CreateSpørgsmålAsync(spørgsmålDto);
            return CreatedAtAction(nameof(GetSpørgsmål), new { id = createdSpørgsmål.SpørgsmålID }, createdSpørgsmål);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSpørgsmål(int id)
        {
            var success = await _spørgsmålService.DeleteSpørgsmålAsync(id);
            if (!success)
                return NotFound();
            return NoContent();
        }
    }
}
