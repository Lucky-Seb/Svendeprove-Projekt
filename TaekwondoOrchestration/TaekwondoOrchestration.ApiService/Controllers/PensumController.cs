using MediatR;
using Microsoft.AspNetCore.Mvc;
using TaekwondoApp.Shared.DTO;
using TaekwondoOrchestration.ApiService.MediatR.Pensum.Commands;
using TaekwondoOrchestration.ApiService.MediatR.Pensum.Queries;

namespace TaekwondoOrchestration.ApiService.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PensumController : ControllerBase
{
    private readonly IMediator _mediator;

    public PensumController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<PensumDTO>>> GetPensum()
    {
        var result = await _mediator.Send(new GetAllPensumQuery());
        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<PensumDTO>> GetPensum(Guid id)
    {
        var result = await _mediator.Send(new GetPensumByIdQuery(id));
        return Ok(result);
    }

    [HttpPost]
    public async Task<ActionResult<PensumDTO>> PostPensum(PensumDTO dto)
    {
        var result = await _mediator.Send(new CreatePensumCommand(dto));
        return CreatedAtAction(nameof(GetPensum), new { id = result.PensumID }, result);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> PutPensum(Guid id, PensumDTO dto)
    {
        await _mediator.Send(new UpdatePensumCommand(id, dto));
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeletePensum(Guid id)
    {
        await _mediator.Send(new DeletePensumCommand(id));
        return NoContent();
    }
}
