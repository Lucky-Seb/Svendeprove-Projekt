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
    public class TræningController : ApiBaseController
    {
        private readonly ITræningService _træningService;
        private readonly IHubContext<TræningHub> _hubContext; // Assuming you have a TræningHub for real-time notifications

        public TræningController(ITræningService træningService, IHubContext<TræningHub> hubContext)
        {
            _træningService = træningService;
            _hubContext = hubContext;
        }

        // GET: api/træning
        [HttpGet]
        public async Task<IActionResult> GetTræning()
        {
            var result = await _træningService.GetAllTræningAsync();
            return result.ToApiResponse(); // Assuming Result has ToApiResponse extension
        }

        // GET: api/træning/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetTræning(Guid id)
        {
            var result = await _træningService.GetTræningByIdAsync(id);
            return result.ToApiResponse(); // Assuming Result has ToApiResponse extension
        }

        // POST: api/træning
        [HttpPost]
        public async Task<IActionResult> PostTræning([FromBody] TræningDTO træningDto)
        {
            if (træningDto == null)
            {
                return BadRequest("Invalid data");
            }

            var result = await _træningService.CreateTræningAsync(træningDto);

            // Optionally, trigger notifications if required
            if (result.Success)
                await _hubContext.Clients.All.SendAsync("TræningCreated");

            return result.ToApiResponse(); // Assuming Result has ToApiResponse extension
        }

        // PUT: api/træning/5
        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> PutTræning(Guid id, [FromBody] TræningDTO træningDto)
        {
            if (id != træningDto.TræningID)
            {
                return BadRequest("ID i forespørgslen matcher ikke ID i kroppen.");
            }

            var result = await _træningService.UpdateTræningAsync(id, træningDto);

            // Optionally, trigger notifications if required
            if (result.Success)
                await _hubContext.Clients.All.SendAsync("TræningUpdated");

            return result.ToApiResponse(); // Assuming Result has ToApiResponse extension
        }

        // DELETE: api/træning/5
        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> DeleteTræning(Guid id)
        {
            var result = await _træningService.DeleteTræningAsync(id);

            // Optionally, trigger notifications if required
            if (result.Success)
                await _hubContext.Clients.All.SendAsync("TræningDeleted");

            return result.ToApiResponse(); // Assuming Result has ToApiResponse extension
        }

        // PUT: api/træning/restore/5
        [HttpPut("restore/{id}")]
        [Authorize]
        public async Task<IActionResult> RestoreTræning(Guid id, [FromBody] TræningDTO træningDto)
        {
            var result = await _træningService.RestoreTræningAsync(id, træningDto);

            // Optionally, trigger notifications if required
            if (result.Success)
                await _hubContext.Clients.All.SendAsync("TræningRestored");

            return result.ToApiResponse(); // Assuming Result has ToApiResponse extension
        }

        // GET: api/træning/all
        [HttpGet("all")]
        public async Task<IActionResult> GetAllTræningIncludingDeleted()
        {
            var result = await _træningService.GetAllTræningIncludingDeletedAsync();
            return result.ToApiResponse(); // Assuming Result has ToApiResponse extension
        }

        // GET: api/træning/{id}/including-deleted
        [HttpGet("{id}/including-deleted")]
        public async Task<IActionResult> GetTræningByIdIncludingDeleted(Guid id)
        {
            var result = await _træningService.GetTræningByIdIncludingDeletedAsync(id);
            return result.ToApiResponse(); // Assuming Result has ToApiResponse extension
        }
    }
}
