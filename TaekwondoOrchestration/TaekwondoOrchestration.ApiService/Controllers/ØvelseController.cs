using Microsoft.AspNetCore.Mvc;
using TaekwondoOrchestration.ApiService.Services;
using TaekwondoOrchestration.ApiService.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TaekwondoOrchestration.ApiService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ØvelseController : ControllerBase
    {
        private readonly ØvelseService _øvelseService;

        public ØvelseController(ØvelseService øvelseService)
        {
            _øvelseService = øvelseService;
        }

        // GET: api/Øvelse
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ØvelseDTO>>> GetØvelser()
        {
            var øvelser = await _øvelseService.GetAllØvelserAsync();
            return Ok(øvelser);
        }

        // GET: api/Øvelse/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ØvelseDTO>> GetØvelse(int id)
        {
            var øvelse = await _øvelseService.GetØvelseByIdAsync(id);
            if (øvelse == null)
                return NotFound();
            return Ok(øvelse);
        }

        // GET: api/Øvelse/sværhed/{sværhed}
        [HttpGet("sværhed/{sværhed}")]
        public async Task<ActionResult<IEnumerable<ØvelseDTO>>> GetØvelserBySværhed(string sværhed)
        {
            var øvelser = await _øvelseService.GetØvelserBySværhedAsync(sværhed);
            return Ok(øvelser);
        }

        // GET: api/Øvelse/bruger/{brugerId}
        [HttpGet("bruger/{brugerId}")]
        public async Task<ActionResult<IEnumerable<ØvelseDTO>>> GetØvelserByBruger(int brugerId)
        {
            var øvelser = await _øvelseService.GetØvelserByBrugerAsync(brugerId);
            return Ok(øvelser);
        }

        // GET: api/Øvelse/klub/{klubId}
        [HttpGet("klub/{klubId}")]
        public async Task<ActionResult<IEnumerable<ØvelseDTO>>> GetØvelserByKlub(int klubId)
        {
            var øvelser = await _øvelseService.GetØvelserByKlubAsync(klubId);
            return Ok(øvelser);
        }

        // GET: api/Øvelse/navn/{navn}
        [HttpGet("navn/{navn}")]
        public async Task<ActionResult<IEnumerable<ØvelseDTO>>> GetØvelserByNavn(string navn)
        {
            var øvelser = await _øvelseService.GetØvelserByNavnAsync(navn);
            return Ok(øvelser);
        }

        // POST: api/Øvelse
        [HttpPost]
        public async Task<ActionResult<ØvelseDTO>> PostØvelse(ØvelseDTO øvelseDto)
        {
            var createdØvelse = await _øvelseService.CreateØvelseAsync(øvelseDto);
            return CreatedAtAction(nameof(GetØvelse), new { id = createdØvelse.ØvelseID }, createdØvelse);
        }

        // DELETE: api/Øvelse/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteØvelse(int id)
        {
            var success = await _øvelseService.DeleteØvelseAsync(id);
            if (!success)
                return NotFound();
            return NoContent();
        }
    }
}
