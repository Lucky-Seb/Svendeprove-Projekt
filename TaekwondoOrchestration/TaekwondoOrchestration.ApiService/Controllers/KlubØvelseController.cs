using Microsoft.AspNetCore.Mvc;
using TaekwondoOrchestration.ApiService.Services;
using TaekwondoOrchestration.ApiService.DTO;


namespace TaekwondoOrchestration.ApiService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class KlubØvelseController : ControllerBase
    {
        private readonly KlubØvelseService _klubØvelseService;

        public KlubØvelseController(KlubØvelseService klubØvelseService)
        {
            _klubØvelseService = klubØvelseService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<KlubØvelseDTO>>> GetKlubØvelser()
        {
            return Ok(await _klubØvelseService.GetAllKlubØvelserAsync());
        }

        [HttpGet("{klubId}/{øvelseId}")]
        public async Task<ActionResult<KlubØvelseDTO>> GetKlubØvelse(int klubId, int øvelseId)
        {
            var klubØvelse = await _klubØvelseService.GetKlubØvelseByIdAsync(klubId, øvelseId);
            if (klubØvelse == null)
                return NotFound();
            return Ok(klubØvelse);
        }

        [HttpPost]
        public async Task<ActionResult<KlubØvelseDTO>> PostKlubØvelse(KlubØvelseDTO klubØvelseDto)
        {
            var createdKlubØvelse = await _klubØvelseService.CreateKlubØvelseAsync(klubØvelseDto);
            return CreatedAtAction(nameof(GetKlubØvelse), new { klubId = createdKlubØvelse.KlubID, øvelseId = createdKlubØvelse.ØvelseID }, createdKlubØvelse);
        }

        [HttpDelete("{klubId}/{øvelseId}")]
        public async Task<IActionResult> DeleteKlubØvelse(int klubId, int øvelseId)
        {
            var success = await _klubØvelseService.DeleteKlubØvelseAsync(klubId, øvelseId);
            if (!success)
                return NotFound();
            return NoContent();
        }
    }
}
