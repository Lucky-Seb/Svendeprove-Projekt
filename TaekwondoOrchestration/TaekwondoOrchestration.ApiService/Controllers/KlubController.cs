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
    public class KlubController : ApiBaseController
    {
        private readonly IKlubService _klubService;
        private readonly IHubContext<KlubHub> _hubContext;

        public KlubController(IKlubService klubService, IHubContext<KlubHub> hubContext)
        {
            _klubService = klubService;
            _hubContext = hubContext;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllKlubber()
        {
            var result = await _klubService.GetAllKlubberAsync();
            return result.ToApiResponse();  // This uses your ToApiResponse extension
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetKlubById(Guid id)
        {
            var result = await _klubService.GetKlubByIdAsync(id);
            return result.ToApiResponse();
        }

        [HttpGet("by-name/{klubNavn}")]
        public async Task<IActionResult> GetKlubByNavn(string klubNavn)
        {
            var result = await _klubService.GetKlubByNavnAsync(klubNavn);
            return result.ToApiResponse();
        }

        [HttpPost]
        public async Task<IActionResult> CreateKlub(KlubDTO klubDto)
        {
            var result = await _klubService.CreateKlubAsync(klubDto);
            if (result.Success)
            {
                await _hubContext.Clients.All.SendAsync("KlubCreated", result.Value); // Optional: Notify clients
                return CreatedAtAction(nameof(GetKlubById), new { id = result.Value.KlubID }, result.Value);
            }
            return result.ToApiResponse();  // Converts failure to BadRequest
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateKlub(Guid id, KlubDTO klubDto)
        {
            var result = await _klubService.UpdateKlubAsync(id, klubDto);
            if (result.Success)
            {
                await _hubContext.Clients.All.SendAsync("KlubUpdated", result.Value); // Optional: Notify clients
                return NoContent();
            }
            return result.ToApiResponse();  // Converts failure to BadRequest
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteKlub(Guid id)
        {
            var result = await _klubService.DeleteKlubAsync(id);
            if (result.Success)
            {
                await _hubContext.Clients.All.SendAsync("KlubDeleted", id); // Optional: Notify clients
                return NoContent();
            }
            return result.ToApiResponse();  // Converts failure to BadRequest
        }
    }
}
