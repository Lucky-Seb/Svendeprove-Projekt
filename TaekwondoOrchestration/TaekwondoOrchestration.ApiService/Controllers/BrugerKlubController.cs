using Microsoft.AspNetCore.Mvc;
using TaekwondoOrchestration.ApiService.Services;
using TaekwondoApp.Shared.DTO;

namespace TaekwondoOrchestration.ApiService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BrugerKlubController : ControllerBase
    {
        private readonly BrugerKlubService _brugerKlubService;

        public BrugerKlubController(BrugerKlubService brugerKlubService)
        {
            _brugerKlubService = brugerKlubService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<BrugerKlubDTO>>> GetBrugerKlubs()
        {
            return Ok(await _brugerKlubService.GetAllBrugerKlubsAsync());
        }

        [HttpGet("{brugerId}/{klubId}")]
        public async Task<ActionResult<BrugerKlubDTO>> GetBrugerKlub(int brugerId, int klubId)
        {
            var brugerKlub = await _brugerKlubService.GetBrugerKlubByIdAsync(brugerId, klubId);
            if (brugerKlub == null)
                return NotFound();
            return Ok(brugerKlub);
        }

        [HttpPost]
        public async Task<ActionResult<BrugerKlubDTO>> PostBrugerKlub(BrugerKlubDTO brugerKlubDto)
        {
            var createdBrugerKlub = await _brugerKlubService.CreateBrugerKlubAsync(brugerKlubDto);
            return CreatedAtAction(nameof(GetBrugerKlub), new { brugerId = createdBrugerKlub.BrugerID, klubId = createdBrugerKlub.KlubID }, createdBrugerKlub);
        }

        [HttpDelete("{brugerId}/{klubId}")]
        public async Task<IActionResult> DeleteBrugerKlub(int brugerId, int klubId)
        {
            var success = await _brugerKlubService.DeleteBrugerKlubAsync(brugerId, klubId);
            if (!success)
                return NotFound();
            return NoContent();
        }
    }
}
