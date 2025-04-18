using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
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
    public class TeoriController : ApiBaseController
    {
        private readonly ITeoriService _teoriService;
        private readonly IHubContext<TeoriHub> _hubContext; // Assuming you have a TeoriHub for real-time notifications

        public TeoriController(ITeoriService teoriService, IHubContext<TeoriHub> hubContext)
        {
            _teoriService = teoriService;
            _hubContext = hubContext;
        }

        // GET: api/teori
        [HttpGet]
        public async Task<IActionResult> GetTeorier()
        {
            var result = await _teoriService.GetAllTeoriAsync();
            return result.ToApiResponse(); // Assuming Result has ToApiResponse extension
        }

        // GET: api/teori/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetTeori(Guid id)
        {
            var result = await _teoriService.GetTeoriByIdAsync(id);
            return result.ToApiResponse(); // Assuming Result has ToApiResponse extension
        }

        // GET: api/teori/pensum/{pensumId}
        [HttpGet("pensum/{pensumId}")]
        public async Task<IActionResult> GetTeorierByPensum(Guid pensumId)
        {
            var result = await _teoriService.GetTeoriByPensumAsync(pensumId);
            return result.ToApiResponse(); // Assuming Result has ToApiResponse extension
        }

        // GET: api/teori/navn/{teoriNavn}
        [HttpGet("navn/{teoriNavn}")]
        public async Task<IActionResult> GetTeoriByNavn(string teoriNavn)
        {
            var result = await _teoriService.GetTeoriByTeoriNavnAsync(teoriNavn);
            return result.ToApiResponse(); // Assuming Result has ToApiResponse extension
        }

        // POST: api/teori
        [HttpPost]
        public async Task<IActionResult> PostTeori([FromBody] TeoriDTO teoriDTO)
        {
            if (teoriDTO == null)
            {
                return BadRequest("Invalid data");
            }

            var result = await _teoriService.CreateTeoriAsync(teoriDTO);

            // Optionally, trigger notifications if required
            if (result.Success)
                await _hubContext.Clients.All.SendAsync("TeoriUpdated");

            return result.ToApiResponse(); // Assuming Result has ToApiResponse extension
        }

        // DELETE: api/teori/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTeori(Guid id)
        {
            var result = await _teoriService.DeleteTeoriAsync(id);

            // Optionally, trigger notifications if required
            if (result.Success)
                await _hubContext.Clients.All.SendAsync("TeoriDeleted");

            return result.ToApiResponse(); // Assuming Result has ToApiResponse extension
        }

        // PUT: api/teori/restore/{id}
        [HttpPut("restore/{id}")]
        public async Task<IActionResult> RestoreTeori(Guid id, [FromBody] TeoriDTO teoriDTO)
        {
            var result = await _teoriService.RestoreTeoriAsync(id, teoriDTO);

            // Optionally, trigger notifications if required
            if (result.Success)
                await _hubContext.Clients.All.SendAsync("TeoriRestored");

            return result.ToApiResponse(); // Assuming Result has ToApiResponse extension
        }

        // GET: api/teori/all
        [HttpGet("all")]
        public async Task<IActionResult> GetAllTeoriIncludingDeleted()
        {
            var result = await _teoriService.GetAllTeoriIncludingDeletedAsync();
            return result.ToApiResponse(); // Assuming Result has ToApiResponse extension
        }

        // GET: api/teori/{id}/including-deleted
        [HttpGet("{id}/including-deleted")]
        public async Task<IActionResult> GetTeoriByIdIncludingDeleted(Guid id)
        {
            var result = await _teoriService.GetTeoriByIdIncludingDeletedAsync(id);
            return result.ToApiResponse(); // Assuming Result has ToApiResponse extension
        }
    }
}
