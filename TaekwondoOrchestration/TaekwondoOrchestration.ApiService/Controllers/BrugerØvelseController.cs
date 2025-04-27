using Microsoft.AspNetCore.Mvc;
using TaekwondoOrchestration.ApiService.ServiceInterfaces;
using TaekwondoApp.Shared.DTO;
using TaekwondoOrchestration.ApiService.Helpers;
using Microsoft.AspNetCore.SignalR;
using TaekwondoOrchestration.ApiService.NotificationHubs;
using TaekwondoApp.Shared.Helper;

namespace TaekwondoOrchestration.ApiService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BrugerØvelseController : ApiBaseController
    {
        private readonly IBrugerØvelseService _brugerØvelseService;
        private readonly IHubContext<BrugerØvelseHub> _hubContext;

        // Constructor injection
        public BrugerØvelseController(IBrugerØvelseService brugerØvelseService, IHubContext<BrugerØvelseHub> hubContext)
        {
            _brugerØvelseService = brugerØvelseService;
            _hubContext = hubContext;
        }

        // Get all "BrugerØvelser"
        [HttpGet]
        public async Task<IActionResult> GetAllBrugerØvelser()
        {
            var result = await _brugerØvelseService.GetAllBrugerØvelserAsync();
            return result.ToApiResponse(); // Use your helper for consistent API response formatting
        }

        // Get "BrugerØvelse" by Id
        [HttpGet("{brugerId}/{øvelseId}")]
        public async Task<IActionResult> GetBrugerØvelseById(Guid brugerId, Guid øvelseId)
        {
            var result = await _brugerØvelseService.GetBrugerØvelseByIdAsync(brugerId, øvelseId);
            return result.ToApiResponse(); // Using the helper for API responses
        }

        // Create a new "BrugerØvelse"
        [HttpPost]
        public async Task<IActionResult> CreateBrugerØvelse([FromBody] BrugerØvelseDTO brugerØvelseDto)
        {
            // Call the service method to create a new BrugerØvelse
            var result = await _brugerØvelseService.CreateBrugerØvelseAsync(brugerØvelseDto);

            if (result.Success)
            {
                // Notify clients about the new "BrugerØvelse" via SignalR
                await _hubContext.Clients.All.SendAsync("BrugerØvelseCreated", result.Value);

                // Return a response with the newly created "BrugerØvelse"
                return CreatedAtAction(nameof(GetBrugerØvelseById), new { brugerId = result.Value.BrugerID, øvelseId = result.Value.ØvelseID }, result.Value);
            }

            // Return failure response if creation was not successful
            return result.ToApiResponse(); // Consistent failure handling
        }


        // Delete a "BrugerØvelse"
        [HttpDelete("{brugerId}/{øvelseId}")]
        public async Task<IActionResult> DeleteBrugerØvelse(Guid brugerId, Guid øvelseId)
        {
            var result = await _brugerØvelseService.DeleteBrugerØvelseAsync(brugerId, øvelseId);
            if (result.Success)
            {
                // Notify clients about the deleted "BrugerØvelse" via SignalR
                await _hubContext.Clients.All.SendAsync("BrugerØvelseDeleted", new { brugerId, øvelseId });
                return NoContent();
            }

            return result.ToApiResponse(); // Consistent failure handling
        }
    }
}
