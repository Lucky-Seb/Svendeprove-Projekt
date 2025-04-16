using Microsoft.AspNetCore.Mvc;
using TaekwondoOrchestration.ApiService.Services;
using TaekwondoApp.Shared.DTO;
using Microsoft.AspNetCore.SignalR;
using TaekwondoOrchestration.ApiService.NotificationHubs;
using Microsoft.AspNetCore.Authorization;
using TaekwondoApp.Shared.Models;
using static SQLite.SQLite3;

namespace TaekwondoOrchestration.ApiService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdbogController : ApiBaseController
    {
        private readonly OrdbogService _ordbogService;
        private readonly IHubContext<OrdbogHub> _hubContext;

        public OrdbogController(OrdbogService ordbogService, IHubContext<OrdbogHub> hubContext)
        {
            _ordbogService = ordbogService;
            _hubContext = hubContext;
        }

        [HttpGet]
        public async Task<ActionResult<ApiResponse<IEnumerable<OrdbogDTO>>>> GetOrdboger()
        {
            var result = await _ordbogService.GetAllOrdbogAsync();
            return Ok(ApiResponse<IEnumerable<OrdbogDTO>>.Ok(result.AsEnumerable()));
        }

        [HttpGet("including-deleted")]
        public async Task<ActionResult<ApiResponse<IEnumerable<OrdbogDTO>>>> GetOrdbogerIncludingDeleted()
        {
            var result = await _ordbogService.GetAllOrdbogIncludingDeletedAsync();
            return OkResponse(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ApiResponse<OrdbogDTO>>> GetOrdbog(Guid id)
        {
            var result = await _ordbogService.GetOrdbogByIdAsync(id);
            if (result == null)
                return NotFoundResponse<OrdbogDTO>("Ordbog not found.");

            return OkResponse(result);
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("restore/{id}")]
        public async Task<ActionResult<ApiResponse<string>>> Restore(Guid id, [FromBody] OrdbogDTO ordbogDto)
        {
            var success = await _ordbogService.RestoreOrdbogAsync(id, ordbogDto);
            if (!success)
                return NotFoundResponse<string>("Restore failed, item not found.");

            return OkResponse("Ordbog restored successfully.");
        }

        [HttpPost]
        public async Task<ActionResult<ApiResponse<OrdbogDTO>>> PostOrdbog(OrdbogDTO ordbogDto)
        {
            var created = await _ordbogService.CreateOrdbogAsync(ordbogDto);
            await _hubContext.Clients.All.SendAsync("OrdbogUpdated");

            return CreatedResponse(nameof(GetOrdbog), new { id = created.OrdbogId }, created);
        }

        [HttpPut("including-deleted/{id}")]
        public async Task<ActionResult<ApiResponse<OrdbogDTO>>> UpdateOrdbogIncludingDeleted(Guid id, OrdbogDTO ordbogDto)
        {
            var updated = await _ordbogService.UpdateOrdbogIncludingDeletedByIdAsync(id, ordbogDto);
            if (updated == null)
                return NotFoundResponse<OrdbogDTO>("Ordbog not found.");

            await _hubContext.Clients.All.SendAsync("OrdbogUpdated");
            return OkResponse(updated);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ApiResponse<OrdbogDTO>>> UpdateOrdbog(Guid id, OrdbogDTO ordbogDto)
        {
            var existing = await _ordbogService.GetOrdbogByIdAsync(id);
            if (existing == null)
                return NotFoundResponse<OrdbogDTO>("Ordbog not found.");

            var updated = await _ordbogService.UpdateOrdbogAsync(id, ordbogDto);

            if (!updated)
                return BadRequest(ApiResponse<OrdbogDTO>.Fail("Update failed"));

            await _hubContext.Clients.All.SendAsync("OrdbogUpdated");

            // Return the updated OrdbogDTO as part of the ApiResponse
            return Ok(ApiResponse<OrdbogDTO>.Ok(ordbogDto)); // Return the ordbogDto as a result
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<ActionResult<ApiResponse<string>>> DeleteOrdbog(Guid id)
        {
            var success = await _ordbogService.DeleteOrdbogAsync(id);
            if (!success)
                return NotFoundResponse<string>("Delete failed. Ordbog not found.");

            await _hubContext.Clients.All.SendAsync("Ordbog Deleted");
            return OkResponse("Deleted successfully.");
        }

        [HttpGet("by-danskord/{danskOrd}")]
        public async Task<ActionResult<ApiResponse<OrdbogDTO>>> GetOrdbogByDanskOrd(string danskOrd)
        {
            var result = await _ordbogService.GetOrdbogByDanskOrdAsync(danskOrd);
            if (result == null)
                return NotFoundResponse<OrdbogDTO>("Ordbog not found.");

            return OkResponse(result);
        }

        [HttpGet("by-koranord/{koranOrd}")]
        public async Task<ActionResult<ApiResponse<OrdbogDTO>>> GetOrdbogByKoranOrd(string koranOrd)
        {
            var result = await _ordbogService.GetOrdbogByKoranOrdAsync(koranOrd);
            if (result == null)
                return NotFoundResponse<OrdbogDTO>("Ordbog not found.");

            return OkResponse(result);
        }
    }
}
