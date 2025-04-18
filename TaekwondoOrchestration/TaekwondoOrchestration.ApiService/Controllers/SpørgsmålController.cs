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
    public class SpørgsmålController : ApiBaseController
    {
        private readonly ISpørgsmålService _spørgsmålService;
        private readonly IHubContext<SpørgsmålHub> _hubContext; // Assuming you have a SpørgsmålHub for real-time notifications

        public SpørgsmålController(ISpørgsmålService spørgsmålService, IHubContext<SpørgsmålHub> hubContext)
        {
            _spørgsmålService = spørgsmålService;
            _hubContext = hubContext;
        }

        // GET: api/spørgsmål
        [HttpGet]
        public async Task<IActionResult> GetSpørgsmål()
        {
            var result = await _spørgsmålService.GetAllSpørgsmålAsync();
            return result.ToApiResponse(); // Assuming Result has ToApiResponse extension
        }

        // GET: api/spørgsmål/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetSpørgsmål(Guid id)
        {
            var result = await _spørgsmålService.GetSpørgsmålByIdAsync(id);
            return result.ToApiResponse(); // Assuming Result has ToApiResponse extension
        }

        // POST: api/spørgsmål
        [HttpPost]
        public async Task<IActionResult> PostSpørgsmål([FromBody] SpørgsmålDTO spørgsmålDto)
        {
            if (spørgsmålDto == null)
            {
                return BadRequest("Invalid data");
            }

            var result = await _spørgsmålService.CreateSpørgsmålAsync(spørgsmålDto);

            // Optionally, trigger notifications if required
            if (result.Success)
                await _hubContext.Clients.All.SendAsync("SpørgsmålUpdated");

            return result.ToApiResponse();
        }

        // PUT: api/spørgsmål/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSpørgsmål(Guid id, [FromBody] SpørgsmålDTO spørgsmålDto)
        {
            var result = await _spørgsmålService.UpdateSpørgsmålAsync(id, spørgsmålDto);

            // Optionally, trigger notifications if required
            if (result.Success)
                await _hubContext.Clients.All.SendAsync("SpørgsmålUpdated");

            return result.ToApiResponse();
        }

        // DELETE: api/spørgsmål/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSpørgsmål(Guid id)
        {
            var result = await _spørgsmålService.DeleteSpørgsmålAsync(id);

            // Optionally, trigger notifications if required
            if (result.Success)
                await _hubContext.Clients.All.SendAsync("SpørgsmålDeleted");

            return result.ToApiResponse();
        }

        // PUT: api/spørgsmål/restore/5
        [HttpPut("restore/{id}")]
        public async Task<IActionResult> RestoreSpørgsmål(Guid id, [FromBody] SpørgsmålDTO spørgsmålDto)
        {
            var result = await _spørgsmålService.RestoreSpørgsmålAsync(id, spørgsmålDto);

            // Optionally, trigger notifications if required
            if (result.Success)
                await _hubContext.Clients.All.SendAsync("SpørgsmålRestored");

            return result.ToApiResponse();
        }

        // GET: api/spørgsmål/by-quiz/{quizId}
        [HttpGet("by-quiz/{quizId}")]
        public async Task<IActionResult> GetSpørgsmålByQuizId(Guid quizId)
        {
            var result = await _spørgsmålService.GetSpørgsmålByQuizIdAsync(quizId);
            return result.ToApiResponse();
        }

        // GET: api/spørgsmål/all
        [HttpGet("all")]
        public async Task<IActionResult> GetAllSpørgsmålIncludingDeleted()
        {
            var result = await _spørgsmålService.GetAllSpørgsmålIncludingDeletedAsync();
            return result.ToApiResponse();
        }

        // GET: api/spørgsmål/{id}/including-deleted
        [HttpGet("{id}/including-deleted")]
        public async Task<IActionResult> GetSpørgsmålByIdIncludingDeleted(Guid id)
        {
            var result = await _spørgsmålService.GetSpørgsmålByIdIncludingDeletedAsync(id);
            return result.ToApiResponse();
        }
    }
}
