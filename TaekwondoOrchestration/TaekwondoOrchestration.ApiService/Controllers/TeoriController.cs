using Microsoft.AspNetCore.Mvc;
using TaekwondoOrchestration.ApiService.Services;
using TaekwondoApp.Shared.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TaekwondoOrchestration.ApiService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeoriController : ControllerBase
    {
        private readonly TeoriService _teoriService;

        public TeoriController(TeoriService teoriService)
        {
            _teoriService = teoriService;
        }

        // Get all Teori records
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TeoriDTO>>> GetTeorier()
        {
            var teorier = await _teoriService.GetAllTeoriAsync();
            return Ok(teorier);
        }

        // Get Teori by ID
        [HttpGet("{id}")]
        public async Task<ActionResult<TeoriDTO>> GetTeori(int id)
        {
            var teori = await _teoriService.GetTeoriByIdAsync(id);
            if (teori == null)
                return NotFound();
            return Ok(teori);
        }

        // Get all Teori by Pensum ID
        [HttpGet("pensum/{pensumId}")]
        public async Task<ActionResult<IEnumerable<TeoriDTO>>> GetTeorierByPensum(int pensumId)
        {
            var teorier = await _teoriService.GetTeoriByPensumAsync(pensumId);
            if (teorier == null || teorier.Count == 0)
                return NotFound();
            return Ok(teorier);
        }

        // Get Teori by TeoriNavn
        [HttpGet("navn/{teoriNavn}")]
        public async Task<ActionResult<TeoriDTO>> GetTeoriByNavn(string teoriNavn)
        {
            var teori = await _teoriService.GetTeoriByTeoriNavnAsync(teoriNavn);
            if (teori == null)
                return NotFound();
            return Ok(teori);
        }

        // Create a new Teori
        [HttpPost]
        public async Task<ActionResult<TeoriDTO>> PostTeori([FromBody] TeoriDTO teoriDTO)
        {
            if (teoriDTO == null)
                return BadRequest("Invalid data.");

            var createdTeori = await _teoriService.CreateTeoriAsync(teoriDTO);
            return CreatedAtAction(nameof(GetTeori), new { id = createdTeori.TeoriID }, createdTeori);
        }

        // Update an existing Teori
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTeori(int id, [FromBody] TeoriDTO teoriDTO)
        {
            var success = await _teoriService.UpdateTeoriAsync(id, teoriDTO);
            if (!success)
                return BadRequest();
            return NoContent();
        }

        // Delete a Teori by ID
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTeori(int id)
        {
            var success = await _teoriService.DeleteTeoriAsync(id);
            if (!success)
                return NotFound();
            return NoContent();
        }
    }
}
