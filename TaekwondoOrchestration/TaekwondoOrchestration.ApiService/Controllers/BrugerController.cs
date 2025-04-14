using Microsoft.AspNetCore.Mvc;
using TaekwondoOrchestration.ApiService.Services;
using TaekwondoApp.Shared.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TaekwondoOrchestration.ApiService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BrugerController : ControllerBase
    {
        private readonly BrugerService _brugerService;

        public BrugerController(BrugerService brugerService)
        {
            _brugerService = brugerService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<BrugerDTO>>> GetBrugere()
        {
            var brugere = await _brugerService.GetAllBrugereAsync();
            return Ok(brugere);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<BrugerDTO>> GetBruger(Guid id)
        {
            var bruger = await _brugerService.GetBrugerByIdAsync(id);
            if (bruger == null)
                return NotFound();
            return Ok(bruger);
        }

        // Get Bruger by Role
        [HttpGet("role/{role}")]
        public async Task<ActionResult<IEnumerable<BrugerDTO>>> GetBrugerByRole(string role)
        {
            var brugere = await _brugerService.GetBrugerByRoleAsync(role);
            if (brugere == null || brugere.Count == 0)
                return NotFound();
            return Ok(brugere);
        }

        // Get Bruger by Bæltegrad
        [HttpGet("bælte/{bæltegrad}")]
        public async Task<ActionResult<IEnumerable<BrugerDTO>>> GetBrugerByBælte(string bæltegrad)
        {
            var brugere = await _brugerService.GetBrugerByBælteAsync(bæltegrad);
            if (brugere == null || brugere.Count == 0)
                return NotFound();
            return Ok(brugere);
        }

        // Get Brugere by KlubID
        [HttpGet("klub/{klubId}")]
        public async Task<ActionResult<List<BrugerDTO>>> GetBrugereByKlubAsync(Guid klubId)
        {
            var brugere = await _brugerService.GetBrugereByKlubAsync(klubId);
            if (brugere == null || brugere.Count == 0)
                return NotFound();
            return Ok(brugere);
        }

        // Get Brugere by KlubID and Bæltegrad
        [HttpGet("klub/{klubId}/bæltegrad/{bæltegrad}")]
        public async Task<ActionResult<List<BrugerDTO>>> GetBrugereByKlubAndBæltegrad(Guid klubId, string bæltegrad)
        {
            var brugere = await _brugerService.GetBrugereByKlubAndBæltegradAsync(klubId, bæltegrad);
            if (brugere == null || brugere.Count == 0)
                return NotFound();
            return Ok(brugere);
        }

        // Get Bruger by Brugernavn
        [HttpGet("brugernavn/{brugernavn}")]
        public async Task<ActionResult<BrugerDTO>> GetBrugerByBrugernavn(string brugernavn)
        {
            var bruger = await _brugerService.GetBrugerByBrugernavnAsync(brugernavn);
            if (bruger == null)
                return NotFound();
            return Ok(bruger);
        }

        // Get Bruger by Fornavn and Efternavn
        [HttpGet("navn/{fornavn}/{efternavn}")]
        public async Task<ActionResult<IEnumerable<BrugerDTO>>> GetBrugerByFornavnEfternavn(string fornavn, string efternavn)
        {
            var brugere = await _brugerService.GetBrugerByFornavnEfternavnAsync(fornavn, efternavn);
            if (brugere == null || brugere.Count == 0)
                return NotFound();
            return Ok(brugere);
        }

        [HttpPost]
        public async Task<ActionResult<BrugerDTO>> PostBruger([FromBody] BrugerDTO brugerDTO)
        {
            if (brugerDTO == null)
                return BadRequest("Invalid data.");

            var createdBruger = await _brugerService.CreateBrugerAsync(brugerDTO);
            return CreatedAtAction(nameof(GetBruger), new { id = createdBruger.BrugerID }, createdBruger);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutBruger(Guid id, [FromBody] BrugerDTO brugerDTO)
        {
            var success = await _brugerService.UpdateBrugerAsync(id, brugerDTO);
            if (!success)
                return BadRequest();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBruger(Guid id)
        {
            var success = await _brugerService.DeleteBrugerAsync(id);
            if (!success)
                return NotFound();
            return NoContent();
        }
        [HttpPost("login")]
        public async Task<ActionResult<BrugerDTO>> Login([FromBody] LoginDTO loginDto)
        {
            if (string.IsNullOrWhiteSpace(loginDto.EmailOrBrugernavn) || string.IsNullOrWhiteSpace(loginDto.Brugerkode))
                return BadRequest("Email/Username and password are required.");

            var bruger = await _brugerService.AuthenticateBrugerAsync(loginDto);

            if (bruger == null)
                return Unauthorized("Invalid login credentials.");

            return Ok(bruger);
        }
    }
}
