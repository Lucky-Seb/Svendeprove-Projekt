using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.AspNetCore.Authorization;
using TaekwondoApp.Shared.DTO;
using TaekwondoOrchestration.ApiService.NotificationHubs;
using TaekwondoOrchestration.ApiService.ServiceInterfaces;
using TaekwondoOrchestration.ApiService.Helpers;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TaekwondoOrchestration.ApiService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ØvelseController : ApiBaseController
    {
        private readonly IØvelseService _øvelseService;
        private readonly IHubContext<ØvelseHub> _hubContext;  // If you want real-time notifications

        public ØvelseController(IØvelseService øvelseService, IHubContext<ØvelseHub> hubContext)
        {
            _øvelseService = øvelseService;
            _hubContext = hubContext;
        }

        // GET: api/Øvelse
        [HttpGet]
        public async Task<IActionResult> GetØvelser()
        {
            var result = await _øvelseService.GetAllØvelserAsync();
            return result.ToApiResponse();
        }

        // GET: api/Øvelse/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetØvelse(Guid id)
        {
            var result = await _øvelseService.GetØvelseByIdAsync(id);
            return result.ToApiResponse();
        }

        // GET: api/Øvelse/sværhed/{sværhed}
        [HttpGet("sværhed/{sværhed}")]
        public async Task<IActionResult> GetØvelserBySværhed(string sværhed)
        {
            var result = await _øvelseService.GetØvelserBySværhedAsync(sværhed);
            return result.ToApiResponse();
        }

        // GET: api/Øvelse/bruger/{brugerId}
        [HttpGet("bruger/{brugerId}")]
        public async Task<IActionResult> GetØvelserByBruger(Guid brugerId)
        {
            var result = await _øvelseService.GetØvelserByBrugerAsync(brugerId);
            return result.ToApiResponse();
        }

        // GET: api/Øvelse/klub/{klubId}
        [HttpGet("klub/{klubId}")]
        public async Task<IActionResult> GetØvelserByKlub(Guid klubId)
        {
            var result = await _øvelseService.GetØvelserByKlubAsync(klubId);
            return result.ToApiResponse();
        }

        // GET: api/Øvelse/navn/{navn}
        [HttpGet("navn/{navn}")]
        public async Task<IActionResult> GetØvelserByNavn(string navn)
        {
            var result = await _øvelseService.GetØvelserByNavnAsync(navn);
            return result.ToApiResponse();
        }

        // POST: api/Øvelse
        [HttpPost]
        public async Task<IActionResult> PostØvelse(ØvelseDTO øvelseDto)
        {
            var result = await _øvelseService.CreateØvelseAsync(øvelseDto);

            // Optionally, trigger notifications if required
            if (result.Success)
                await _hubContext.Clients.All.SendAsync("ØvelseUpdated");

            return result.ToApiResponse();
        }

        // DELETE: api/Øvelse/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteØvelse(Guid id)
        {
            var result = await _øvelseService.DeleteØvelseAsync(id);

            // Optionally, trigger notifications if required
            if (result.Success)
                await _hubContext.Clients.All.SendAsync("ØvelseDeleted");

            return result.ToApiResponse();
        }
    }
}
