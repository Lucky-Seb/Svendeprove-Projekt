using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using TaekwondoApp.Shared.DTO;
using TaekwondoOrchestration.ApiService.NotificationHubs;
using TaekwondoOrchestration.ApiService.ServiceInterfaces;
using TaekwondoOrchestration.ApiService.Helpers;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace TaekwondoOrchestration.ApiService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class KlubØvelseController : ApiBaseController
    {
        private readonly IKlubØvelseService _klubØvelseService;
        private readonly IHubContext<KlubØvelseHub> _hubContext;

        // Constructor for Dependency Injection
        public KlubØvelseController(IKlubØvelseService klubØvelseService, IHubContext<KlubØvelseHub> hubContext)
        {
            _klubØvelseService = klubØvelseService;
            _hubContext = hubContext;
        }

        // Get all KlubØvelser
        [HttpGet]
        public async Task<IActionResult> GetKlubØvelser()
        {
            var result = await _klubØvelseService.GetAllKlubØvelserAsync();
            return result.ToApiResponse();  // This uses your ToApiResponse extension
        }

        // Get a specific KlubØvelse by its composite keys (KlubID, ØvelseID)
        [HttpGet("{klubId}/{øvelseId}")]
        public async Task<IActionResult> GetKlubØvelse(Guid klubId, Guid øvelseId)
        {
            var result = await _klubØvelseService.GetKlubØvelseByIdAsync(klubId, øvelseId);
            return result.ToApiResponse();
        }

        // Create a new KlubØvelse
        [HttpPost]
        public async Task<IActionResult> CreateKlubØvelse(KlubØvelseDTO klubØvelseDto)
        {
            var result = await _klubØvelseService.CreateKlubØvelseAsync(klubØvelseDto);
            if (result.Success)
            {
                await _hubContext.Clients.All.SendAsync("KlubØvelseCreated", result.Value); // Notify clients
                return CreatedAtAction(nameof(GetKlubØvelse), new { klubId = result.Value.KlubID, øvelseId = result.Value.ØvelseID }, result.Value);
            }
            return result.ToApiResponse();  // Converts failure to BadRequest
        }

        // Delete a specific KlubØvelse by its composite keys (KlubID, ØvelseID)
        [HttpDelete("{klubId}/{øvelseId}")]
        [Authorize]
        public async Task<IActionResult> DeleteKlubØvelse(Guid klubId, Guid øvelseId)
        {
            var result = await _klubØvelseService.DeleteKlubØvelseAsync(klubId, øvelseId);
            if (result.Success)
            {
                await _hubContext.Clients.All.SendAsync("KlubØvelseDeleted", new { klubId, øvelseId }); // Notify clients
                return NoContent();
            }
            return result.ToApiResponse();  // Converts failure to BadRequest
        }
    }
}
