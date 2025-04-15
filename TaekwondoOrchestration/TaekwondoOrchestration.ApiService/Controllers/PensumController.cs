using MediatR;
using Microsoft.AspNetCore.Mvc;
using TaekwondoApp.Shared.DTO;
using TaekwondoOrchestration.ApiService.MediatR.Pensum;
using TaekwondoOrchestration.ApiService.Response;
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
            try
            {
                var result = await _mediator.Send(new GetAllPensumQuery());
                return Ok(new ApiResponse<IEnumerable<PensumDTO>>(result, "Pensum retrieved successfully"));
            }
            catch (Exception ex)
            {
                // Log exception and return a standardized error response
                return StatusCode(500, new ApiResponse<IEnumerable<PensumDTO>>("An error occurred while retrieving pensum", 500));
            }
        }

        // GET: api/pensum/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<ApiResponse<PensumDTO>>> GetPensum(Guid id)
        {
            try
            {
                var result = await _mediator.Send(new GetPensumByIdQuery(id));
                if (result == null)
                {
                    return NotFound(new ApiResponse<PensumDTO>("Pensum not found", 404));
                }
                return Ok(new ApiResponse<PensumDTO>(result, "Pensum retrieved successfully"));
            }
            catch (Exception ex)
            {
                // Log exception and return a standardized error response
                return StatusCode(500, new ApiResponse<PensumDTO>("An error occurred while retrieving the pensum", 500));
            }
        }

        // POST: api/pensum
        [HttpPost]
        public async Task<ActionResult<ApiResponse<PensumDTO>>> CreatePensum(PensumDTO dto)
        {
            try
            {
                var result = await _mediator.Send(new CreatePensumCommand(dto));
                if (result == null)
                {
                    return BadRequest(new ApiResponse<PensumDTO>("Error creating pensum", 400));
                }
                return CreatedAtAction(nameof(GetPensum), new { id = result.PensumID }, new ApiResponse<PensumDTO>(result, "Pensum created successfully"));
            }
            catch (Exception ex)
            {
                // Log exception and return a standardized error response
                return StatusCode(500, new ApiResponse<PensumDTO>("An error occurred while creating the pensum", 500));
            }
        }

        // PUT: api/pensum/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePensum(Guid id, PensumDTO dto)
        {
            try
            {
                var success = await _mediator.Send(new UpdatePensumCommand(id, dto));
                if (!success)
                {
                    return NotFound(new ApiResponse<PensumDTO>("Pensum not found", 404));
                }
                return NoContent();
            }
            catch (Exception ex)
            {
                // Log exception and return a standardized error response
                return StatusCode(500, new ApiResponse<PensumDTO>("An error occurred while updating the pensum", 500));
            }
        }

        // DELETE: api/pensum/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePensum(Guid id)
        {
            try
            {
                var success = await _mediator.Send(new DeletePensumCommand(id));
                if (!success)
                {
                    return NotFound(new ApiResponse<PensumDTO>("Pensum not found", 404));
                }
                return NoContent();
            }
            catch (Exception ex)
            {
                // Log exception and return a standardized error response
                return StatusCode(500, new ApiResponse<PensumDTO>("An error occurred while deleting the pensum", 500));
            }
        }
    }
}
