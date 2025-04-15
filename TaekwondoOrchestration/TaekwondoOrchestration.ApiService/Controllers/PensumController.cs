using MediatR;
using Microsoft.AspNetCore.Mvc;
using TaekwondoApp.Shared.DTO;
using TaekwondoOrchestration.ApiService.MediatR.Pensum;
using TaekwondoOrchestration.ApiService.MediatR.Pensum.Commands;
using TaekwondoOrchestration.ApiService.MediatR.Pensum.Queries;

namespace TaekwondoOrchestration.ApiService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PensumController : ControllerBase
    {
        private readonly IMediator _mediator;

        public PensumController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // GET: api/pensum
        [HttpGet]
        public async Task<ActionResult<ApiResponse<IEnumerable<PensumDTO>>>> GetPensum()
        {
            var result = await _mediator.Send(new GetAllPensumQuery());
            return Ok(new ApiResponse<IEnumerable<PensumDTO>>(result, "Pensum retrieved successfully"));
        }

        // GET: api/pensum/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<ApiResponse<PensumDTO>>> GetPensum(Guid id)
        {
            var result = await _mediator.Send(new GetPensumByIdQuery(id));
            if (result == null)
            {
                return NotFound(new ApiResponse<PensumDTO>("Pensum not found", 404));
            }
            return Ok(new ApiResponse<PensumDTO>(result, "Pensum retrieved successfully"));
        }

        // POST: api/pensum
        [HttpPost]
        public async Task<ActionResult<ApiResponse<PensumDTO>>> CreatePensum(PensumDTO dto)
        {
            var result = await _mediator.Send(new CreatePensumCommand(dto));
            if (result == null)
            {
                return BadRequest(new ApiResponse<PensumDTO>("Error creating pensum", 400));
            }
            return CreatedAtAction(nameof(GetPensum), new { id = result.PensumID }, new ApiResponse<PensumDTO>(result, "Pensum created successfully"));
        }

        // PUT: api/pensum/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePensum(Guid id, PensumDTO dto)
        {
            var success = await _mediator.Send(new UpdatePensumCommand(id, dto));
            if (!success)
            {
                return NotFound(new ApiResponse<PensumDTO>("Pensum not found", 404));
            }
            return NoContent();
        }

        // DELETE: api/pensum/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePensum(Guid id)
        {
            var success = await _mediator.Send(new DeletePensumCommand(id));
            if (!success)
            {
                return NotFound(new ApiResponse<PensumDTO>("Pensum not found", 404));
            }
            return NoContent();
        }
    }
}
