using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using TaekwondoApp.Shared.DTO;
using TaekwondoOrchestration.ApiService.NotificationHubs;
using TaekwondoOrchestration.ApiService.ServiceInterfaces;
using TaekwondoOrchestration.ApiService.Helpers;
using Microsoft.AspNetCore.Authorization;

namespace TaekwondoOrchestration.ApiService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BrugerProgramController : ApiBaseController
    {
        private readonly IBrugerProgramService _brugerProgramService;
        private readonly IHubContext<BrugerProgramHub> _hubContext;

        public BrugerProgramController(IBrugerProgramService brugerProgramService, IHubContext<BrugerProgramHub> hubContext)
        {
            _brugerProgramService = brugerProgramService;
            _hubContext = hubContext;
        }

        // Get all BrugerPrograms
        [HttpGet]
        public async Task<IActionResult> GetAllBrugerPrograms()
        {
            var result = await _brugerProgramService.GetAllBrugerProgramsAsync();
            return result.ToApiResponse();  // Uses the ToApiResponse extension for consistent response formatting
        }

        // Get BrugerProgram by ID
        [HttpGet("{brugerId}/{programId}")]
        public async Task<IActionResult> GetBrugerProgram(Guid brugerId, Guid programId)
        {
            var result = await _brugerProgramService.GetBrugerProgramByIdAsync(brugerId, programId);
            return result.ToApiResponse();  // Uses the ToApiResponse extension for consistent response formatting
        }

        // Create a new BrugerProgram
        [HttpPost]
        public async Task<IActionResult> CreateBrugerProgram([FromBody] BrugerProgramDTO brugerProgramDto)
        {
            var result = await _brugerProgramService.CreateBrugerProgramAsync(brugerProgramDto);
            if (result.Success)
            {
                await _hubContext.Clients.All.SendAsync("BrugerProgramCreated", result.Value); // Notify clients via SignalR
                return CreatedAtAction(nameof(GetBrugerProgram), new { brugerId = result.Value.BrugerID, programId = result.Value.ProgramID }, result.Value);
            }
            return result.ToApiResponse();  // Converts failure to BadRequest
        }

        // Delete a BrugerProgram
        [HttpDelete("{brugerId}/{programId}")]
        [Authorize]
        public async Task<IActionResult> DeleteBrugerProgram(Guid brugerId, Guid programId)
        {
            var result = await _brugerProgramService.DeleteBrugerProgramAsync(brugerId, programId);
            if (result.Success)
            {
                await _hubContext.Clients.All.SendAsync("BrugerProgramDeleted", new { brugerId, programId }); // Notify clients via SignalR
                return NoContent();
            }
            return result.ToApiResponse();  // Converts failure to BadRequest
        }
    }
}
