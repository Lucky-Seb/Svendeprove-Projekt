using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using TaekwondoApp.Shared.DTO;
using TaekwondoOrchestration.ApiService.NotificationHubs;
using TaekwondoOrchestration.ApiService.ServiceInterfaces;
using TaekwondoOrchestration.ApiService.Helpers;
using TaekwondoOrchestration.ApiService.Services;

namespace TaekwondoOrchestration.ApiService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BrugerKlubController : ApiBaseController
    {
        private readonly IBrugerKlubService _brugerKlubService;
        private readonly IHubContext<BrugerKlubHub> _hubContext;

        public BrugerKlubController(IBrugerKlubService brugerKlubService, IHubContext<BrugerKlubHub> hubContext)
        {
            _brugerKlubService = brugerKlubService;
            _hubContext = hubContext;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllBrugerKlubs()
        {
            var result = await _brugerKlubService.GetAllBrugerKlubsAsync();
            return result.ToApiResponse();
        }

        [HttpGet("{brugerId}/{klubId}")]
        public async Task<IActionResult> GetBrugerKlubById(Guid brugerId, Guid klubId)
        {
            var result = await _brugerKlubService.GetBrugerKlubByIdAsync(brugerId, klubId);
            return result.ToApiResponse();
        }

        [HttpPost]
        public async Task<IActionResult> CreateBrugerKlub(BrugerKlubDTO brugerKlubDto)
        {
            var result = await _brugerKlubService.CreateBrugerKlubAsync(brugerKlubDto);
            if (result.Success)
            {
                await _hubContext.Clients.All.SendAsync("BrugerKlubCreated", result.Value); // Optional: Notify clients
                return CreatedAtAction(nameof(GetBrugerKlubById), new { brugerId = result.Value.BrugerID, klubId = result.Value.KlubID }, result.Value);
            }
            return result.ToApiResponse(); // Converts failure to BadRequest
        }

        [HttpDelete("{brugerId}/{klubId}")]
        public async Task<IActionResult> DeleteBrugerKlub(Guid brugerId, Guid klubId)
        {
            var result = await _brugerKlubService.DeleteBrugerKlubAsync(brugerId, klubId);
            if (result.Success)
            {
                await _hubContext.Clients.All.SendAsync("BrugerKlubDeleted", new { brugerId, klubId }); // Optional: Notify clients
                return NoContent();
            }
            return result.ToApiResponse(); // Converts failure to BadRequest
        }
    }
}
