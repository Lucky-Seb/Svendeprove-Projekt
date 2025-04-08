using Microsoft.AspNetCore.Mvc;
using TaekwondoOrchestration.ApiService.Services;
using TaekwondoOrchestration.ApiService.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TaekwondoOrchestration.ApiService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TræningController : ControllerBase
    {
        private readonly TræningService _træningService;

        public TræningController(TræningService træningService)
        {
            _træningService = træningService;
        }

        // GET: api/træning
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TræningDTO>>> GetTræning()
        {
            var træningList = await _træningService.GetAllTræningAsync();
            return Ok(træningList);
        }

        // GET: api/træning/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TræningDTO>> GetTræning(int id)
        {
            var træning = await _træningService.GetTræningByIdAsync(id);
            if (træning == null)
                return NotFound();
            return Ok(træning);
        }

        // POST: api/træning
        [HttpPost]
        public async Task<ActionResult<TræningDTO>> PostTræning(TræningDTO træningDto)
        {
            var createdTræning = await _træningService.CreateTræningAsync(træningDto);
            return CreatedAtAction(nameof(GetTræning), new { id = createdTræning.TræningID }, createdTræning);
        }

        // PUT: api/træning/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTræning(int id, TræningDTO træningDto)
        {
            if (id != træningDto.TræningID)
            {
                return BadRequest("ID i forespørgslen matcher ikke ID i kroppen.");
            }

            var success = await _træningService.UpdateTræningAsync(id, træningDto);
            if (!success)
            {
                return NotFound();
            }

            return NoContent();
        }

        // DELETE: api/træning/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTræning(int id)
        {
            var success = await _træningService.DeleteTræningAsync(id);
            if (!success)
                return NotFound();
            return NoContent();
        }
    }
}
