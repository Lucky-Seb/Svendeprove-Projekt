// In KlubController.cs
using TaekwondoApp.Shared.DTO;
using TaekwondoOrchestration.ApiService.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace TaekwondoOrchestration.ApiService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class KlubController : ControllerBase
    {
        private readonly KlubService _klubService;

        public KlubController(KlubService klubService)
        {
            _klubService = klubService;
        }

        [HttpGet]
        public async Task<ActionResult<List<KlubDTO>>> GetAllKlubber()
        {
            var klubber = await _klubService.GetAllKlubberAsync();
            return Ok(klubber);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<KlubDTO>> GetKlubById(int id)
        {
            var klub = await _klubService.GetKlubByIdAsync(id);
            if (klub == null)
                return NotFound();

            return Ok(klub);
        }

        // Add the new endpoint to get Klub by Navn
        [HttpGet("by-name/{klubNavn}")]
        public async Task<ActionResult<KlubDTO>> GetKlubByNavn(string klubNavn)
        {
            var klub = await _klubService.GetKlubByNavnAsync(klubNavn);
            if (klub == null)
                return NotFound();

            return Ok(klub);
        }

        [HttpPost]
        public async Task<ActionResult<KlubDTO>> CreateKlub(KlubDTO klubDto)
        {
            var createdKlub = await _klubService.CreateKlubAsync(klubDto);
            if (createdKlub == null)
                return BadRequest("Invalid data.");

            return CreatedAtAction(nameof(GetKlubById), new { id = createdKlub.KlubID }, createdKlub);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateKlub(int id, KlubDTO klubDto)
        {
            var (success, message) = await _klubService.UpdateKlubAsync(id, klubDto);
            if (!success)
                return BadRequest(message);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteKlub(int id)
        {
            var success = await _klubService.DeleteKlubAsync(id);
            if (!success)
                return NotFound();

            return NoContent();
        }
    }
}
