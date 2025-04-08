using Microsoft.AspNetCore.Mvc;
using TaekwondoOrchestration.ApiService.Services;
using TaekwondoOrchestration.ApiService.DTO;

namespace TaekwondoOrchestration.ApiService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdbogController : ControllerBase
    {
        private readonly OrdbogService _ordbogService;

        public OrdbogController(OrdbogService ordbogService)
        {
            _ordbogService = ordbogService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrdbogDTO>>> GetOrdboger()
        {
            return Ok(await _ordbogService.GetAllOrdbogAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<OrdbogDTO>> GetOrdbog(int id)
        {
            var ordbog = await _ordbogService.GetOrdbogByIdAsync(id);
            if (ordbog == null)
                return NotFound();
            return Ok(ordbog);
        }

        [HttpPost]
        public async Task<ActionResult<OrdbogDTO>> PostOrdbog(OrdbogDTO ordbogDto)
        {
            var createdOrdbog = await _ordbogService.CreateOrdbogAsync(ordbogDto);
            return CreatedAtAction(nameof(GetOrdbog), new { id = createdOrdbog.Id }, createdOrdbog);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrdbog(int id)
        {
            var success = await _ordbogService.DeleteOrdbogAsync(id);
            if (!success)
                return NotFound();
            return NoContent();
        }
        // GET: api/Ordbog/by-danskord/{danskOrd}
        [HttpGet("by-danskord/{danskOrd}")]
        public async Task<ActionResult<OrdbogDTO>> GetOrdbogByDanskOrd(string danskOrd)
        {
            var ordbog = await _ordbogService.GetOrdbogByDanskOrdAsync(danskOrd);
            if (ordbog == null)
                return NotFound();

            return Ok(ordbog);
        }

        // GET: api/Ordbog/by-koranord/{koranOrd}
        [HttpGet("by-koranord/{koranOrd}")]
        public async Task<ActionResult<OrdbogDTO>> GetOrdbogByKoranOrd(string koranOrd)
        {
            var ordbog = await _ordbogService.GetOrdbogByKoranOrdAsync(koranOrd);
            if (ordbog == null)
                return NotFound();

            return Ok(ordbog);
        }
    }
}
