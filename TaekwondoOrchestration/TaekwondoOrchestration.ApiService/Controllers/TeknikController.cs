using Microsoft.AspNetCore.Mvc;
using TaekwondoOrchestration.ApiService.Services;
using TaekwondoApp.Shared.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TaekwondoOrchestration.ApiService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeknikController : ControllerBase
    {
        private readonly TeknikService _teknikService;

        public TeknikController(TeknikService teknikService)
        {
            _teknikService = teknikService;
        }

        // Get all Tekniks
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TeknikDTO>>> GetTekniks()
        {
            return Ok(await _teknikService.GetAllTekniksAsync());
        }

        // Get Teknik by ID
        [HttpGet("{id}")]
        public async Task<ActionResult<TeknikDTO>> GetTeknik(Guid id)
        {
            var teknik = await _teknikService.GetTeknikByIdAsync(id);
            if (teknik == null)
                return NotFound();
            return Ok(teknik);
        }

        // Get all Tekniks by Pensum ID
        [HttpGet("pensum/{pensumId}")]
        public async Task<ActionResult<IEnumerable<TeknikDTO>>> GetTekniksByPensum(Guid pensumId)
        {
            var tekniks = await _teknikService.GetAllTeknikByPensumAsync(pensumId);
            if (tekniks == null || tekniks.Count == 0)
                return NotFound();
            return Ok(tekniks);
        }

        // Get Teknik by TeknikNavn
        [HttpGet("tekniknavn/{teknikNavn}")]
        public async Task<ActionResult<TeknikDTO>> GetTeknikByTeknikNavn(string teknikNavn)
        {
            var teknik = await _teknikService.GetTeknikByTeknikNavnAsync(teknikNavn);
            if (teknik == null)
                return NotFound();
            return Ok(teknik);
        }

        // Create Teknik
        [HttpPost]
        public async Task<ActionResult<TeknikDTO>> PostTeknik(TeknikDTO teknikDto)
        {
            var createdTeknik = await _teknikService.CreateTeknikAsync(teknikDto);
            return CreatedAtAction(nameof(GetTeknik), new { id = createdTeknik.TeknikID }, createdTeknik);
        }

        // Delete Teknik
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTeknik(Guid id)
        {
            var success = await _teknikService.DeleteTeknikAsync(id);
            if (!success)
                return NotFound();
            return NoContent();
        }
    }
}
