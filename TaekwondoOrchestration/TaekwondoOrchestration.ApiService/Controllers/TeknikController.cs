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
    public class TeknikController : ApiBaseController
    {
        private readonly ITeknikService _teknikService;
        private readonly IHubContext<TeknikHub> _hubContext; // Assuming you have a TeknikHub for real-time notifications

        public TeknikController(ITeknikService teknikService, IHubContext<TeknikHub> hubContext)
        {
            _teknikService = teknikService;
            _hubContext = hubContext;
        }

        // GET: api/teknik
        [HttpGet]
        public async Task<IActionResult> GetTekniks()
        {
            var result = await _teknikService.GetAllTekniksAsync();
            return result.ToApiResponse(); // Assuming Result has ToApiResponse extension
        }

        // GET: api/teknik/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetTeknik(Guid id)
        {
            var result = await _teknikService.GetTeknikByIdAsync(id);
            return result.ToApiResponse(); // Assuming Result has ToApiResponse extension
        }

        // GET: api/teknik/pensum/{pensumId}
        [HttpGet("pensum/{pensumId}")]
        public async Task<IActionResult> GetTekniksByPensum(Guid pensumId)
        {
            var result = await _teknikService.GetAllTeknikByPensumAsync(pensumId);
            return result.ToApiResponse(); // Assuming Result has ToApiResponse extension
        }

        // GET: api/teknik/tekniknavn/{teknikNavn}
        [HttpGet("tekniknavn/{teknikNavn}")]
        public async Task<IActionResult> GetTeknikByTeknikNavn(string teknikNavn)
        {
            var result = await _teknikService.GetTeknikByTeknikNavnAsync(teknikNavn);
            return result.ToApiResponse(); // Assuming Result has ToApiResponse extension
        }

        // POST: api/teknik
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> PostTeknik([FromBody] TeknikDTO teknikDto)
        {
            if (teknikDto == null)
            {
                return BadRequest("Invalid data");
            }

            var result = await _teknikService.CreateTeknikAsync(teknikDto);

            // Optionally, trigger notifications if required
            if (result.Success)
                await _hubContext.Clients.All.SendAsync("TeknikUpdated");

            return result.ToApiResponse(); // Assuming Result has ToApiResponse extension
        }

        // DELETE: api/teknik/{id}
        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> DeleteTeknik(Guid id)
        {
            var result = await _teknikService.DeleteTeknikAsync(id);

            // Optionally, trigger notifications if required
            if (result.Success)
                await _hubContext.Clients.All.SendAsync("TeknikDeleted");

            return result.ToApiResponse(); // Assuming Result has ToApiResponse extension
        }

        // PUT: api/teknik/restore/{id}
        [HttpPut("restore/{id}")]
        [Authorize]
        public async Task<IActionResult> RestoreTeknik(Guid id, [FromBody] TeknikDTO teknikDto)
        {
            var result = await _teknikService.RestoreTeknikAsync(id, teknikDto);

            // Optionally, trigger notifications if required
            if (result.Success)
                await _hubContext.Clients.All.SendAsync("TeknikRestored");

            return result.ToApiResponse(); // Assuming Result has ToApiResponse extension
        }

        // GET: api/teknik/all
        [HttpGet("all")]
        public async Task<IActionResult> GetAllTeknikIncludingDeleted()
        {
            var result = await _teknikService.GetAllTeknikIncludingDeletedAsync();
            return result.ToApiResponse(); // Assuming Result has ToApiResponse extension
        }

        // GET: api/teknik/{id}/including-deleted
        [HttpGet("{id}/including-deleted")]
        public async Task<IActionResult> GetTeknikByIdIncludingDeleted(Guid id)
        {
            var result = await _teknikService.GetTeknikByIdIncludingDeletedAsync(id);
            return result.ToApiResponse(); // Assuming Result has ToApiResponse extension
        }
    }
}
