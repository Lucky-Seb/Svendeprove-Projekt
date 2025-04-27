using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using TaekwondoApp.Shared.DTO;
using TaekwondoOrchestration.ApiService.NotificationHubs;
using TaekwondoOrchestration.ApiService.ServiceInterfaces;
using TaekwondoOrchestration.ApiService.Helpers;

namespace TaekwondoOrchestration.ApiService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class KlubProgramController : ApiBaseController
    {
        private readonly IKlubProgramService _klubProgramService;
        private readonly IHubContext<KlubProgramHub> _hubContext;

        public KlubProgramController(IKlubProgramService klubProgramService, IHubContext<KlubProgramHub> hubContext)
        {
            _klubProgramService = klubProgramService;
            _hubContext = hubContext;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllKlubProgrammer()
        {
            var result = await _klubProgramService.GetAllKlubProgrammerAsync();
            return result.ToApiResponse(); // Converts result to the appropriate API response
        }

        [HttpGet("{klubId}/{programId}")]
        public async Task<IActionResult> GetKlubProgram(Guid klubId, Guid programId)
        {
            var result = await _klubProgramService.GetKlubProgramByIdAsync(klubId, programId);
            return result.ToApiResponse(); // Converts result to the appropriate API response
        }

        [HttpPost]
        public async Task<IActionResult> CreateKlubProgram(KlubProgramDTO klubProgramDto)
        {
            var result = await _klubProgramService.CreateKlubProgramAsync(klubProgramDto);
            if (result.Success)
            {
                // Notify clients about the new KlubProgram
                await _hubContext.Clients.All.SendAsync("KlubProgramCreated", result.Value);
                return CreatedAtAction(nameof(GetKlubProgram), new { klubId = result.Value.KlubID, programId = result.Value.ProgramID }, result.Value);
            }
            return result.ToApiResponse(); // Converts failure to BadRequest
        }

        [HttpDelete("{klubId}/{programId}")]
        public async Task<IActionResult> DeleteKlubProgram(Guid klubId, Guid programId)
        {
            var result = await _klubProgramService.DeleteKlubProgramAsync(klubId, programId);
            if (result.Success)
            {
                // Notify clients about the deletion of KlubProgram
                await _hubContext.Clients.All.SendAsync("KlubProgramDeleted", new { KlubID = klubId, ProgramID = programId });
                return NoContent();
            }
            return result.ToApiResponse(); // Converts failure to BadRequest
        }
    }
}
