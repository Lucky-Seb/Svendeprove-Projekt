using Microsoft.AspNetCore.Mvc;
using TaekwondoOrchestration.ApiService.Services;
using TaekwondoApp.Shared.DTO;

namespace TaekwondoOrchestration.ApiService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PensumController : ControllerBase
    {
        private readonly PensumService _pensumService;

        public PensumController(PensumService pensumService)
        {
            _pensumService = pensumService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PensumDTO>>> GetPensum()
        {
            var pensumList = await _pensumService.GetAllPensumAsync();
            return Ok(pensumList);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<PensumDTO>> GetPensum(Guid id)
        {
            var pensum = await _pensumService.GetPensumByIdAsync(id);
            if (pensum == null)
                return NotFound();
            return Ok(pensum);
        }

        [HttpPost]
        public async Task<ActionResult<PensumDTO>> PostPensum([FromBody] PensumDTO pensumDTO)
        {
            if (pensumDTO == null)
                return BadRequest("Invalid data.");

            var createdPensum = await _pensumService.CreatePensumAsync(pensumDTO);
            return CreatedAtAction(nameof(GetPensum), new { id = createdPensum.PensumID }, createdPensum);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutPensum(Guid id, [FromBody] PensumDTO pensumDTO)
        {
            var success = await _pensumService.UpdatePensumAsync(id, pensumDTO);
            if (!success)
                return BadRequest();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePensum(Guid id)
        {
            var success = await _pensumService.DeletePensumAsync(id);
            if (!success)
                return NotFound();
            return NoContent();
        }
    }
}
