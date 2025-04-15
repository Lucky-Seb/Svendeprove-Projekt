using Microsoft.AspNetCore.Mvc;
using TaekwondoOrchestration.ApiService.Services;
using TaekwondoApp.Shared.DTO;
using Microsoft.AspNetCore.SignalR;
using TaekwondoOrchestration.ApiService.NotificationHubs;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using TaekwondoApp.Shared.Models;

namespace TaekwondoOrchestration.ApiService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdbogController : ControllerBase
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
            return Ok(ApiResponse<IEnumerable<OrdbogDTO>>.Ok(result));
        }

        [HttpGet("including-deleted")]
        public async Task<ActionResult<ApiResponse<IEnumerable<OrdbogDTO>>>> GetOrdbogerIncludingDeleted()
        {
            var result = await _ordbogService.GetAllOrdbogIncludingDeletedAsync();
            return Ok(ApiResponse<IEnumerable<OrdbogDTO>>.Ok(result));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ApiResponse<OrdbogDTO>>> GetOrdbog(Guid id)
        {
            var result = await _ordbogService.GetOrdbogByIdAsync(id);
            if (result == null)
                return NotFound(ApiResponse<OrdbogDTO>.Fail("Ordbog not found", 404));

            return Ok(ApiResponse<OrdbogDTO>.Ok(result));
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("restore/{id}")]
        public async Task<ActionResult<ApiResponse<string>>> Restore(Guid id, [FromBody] OrdbogDTO ordbogDto)
        {
            var success = await _ordbogService.RestoreOrdbogAsync(id, ordbogDto);
            if (!success)
                return NotFound(ApiResponse<string>.Fail("Restore failed, item not found.", 404));

            return Ok(ApiResponse<string>.Ok("Ordbog restored successfully"));
        }

        [HttpPost]
        public async Task<ActionResult<ApiResponse<OrdbogDTO>>> PostOrdbog(OrdbogDTO ordbogDto)
        {
            var created = await _ordbogService.CreateOrdbogAsync(ordbogDto);
            await _hubContext.Clients.All.SendAsync("OrdbogUpdated");

            return CreatedAtAction(nameof(GetOrdbog), new { id = created.OrdbogId },
                ApiResponse<OrdbogDTO>.Ok(created, 201));
        }

        [HttpPut("including-deleted/{id}")]
        public async Task<ActionResult<ApiResponse<OrdbogDTO>>> UpdateOrdbogIncludingDeleted(Guid id, OrdbogDTO ordbogDto)
        {
            var updated = await _ordbogService.UpdateOrdbogIncludingDeletedByIdAsync(id, ordbogDto);
            if (updated == null)
                return NotFound(ApiResponse<OrdbogDTO>.Fail("Ordbog not found", 404));

            await _hubContext.Clients.All.SendAsync("OrdbogUpdated");
            return Ok(ApiResponse<OrdbogDTO>.Ok(updated));
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ApiResponse<OrdbogDTO>>> UpdateOrdbog(Guid id, OrdbogDTO ordbogDto)
        {
            var existing = await _ordbogService.GetOrdbogByIdAsync(id);
            if (existing == null)
                return NotFound(ApiResponse<OrdbogDTO>.Fail("Ordbog not found", 404));

            var updated = await _ordbogService.UpdateOrdbogAsync(id, ordbogDto);
            await _hubContext.Clients.All.SendAsync("OrdbogUpdated");
            return Ok(ApiResponse<OrdbogDTO>.Ok(updated));
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<ActionResult<ApiResponse<string>>> DeleteOrdbog(Guid id)
        {
            var success = await _ordbogService.DeleteOrdbogAsync(id);
            if (!success)
                return NotFound(ApiResponse<string>.Fail("Delete failed. Ordbog not found.", 404));

            await _hubContext.Clients.All.SendAsync("OrdbogUpdated");
            return Ok(ApiResponse<string>.Ok("Deleted successfully"));
        }

        [HttpGet("by-danskord/{danskOrd}")]
        public async Task<ActionResult<ApiResponse<OrdbogDTO>>> GetOrdbogByDanskOrd(string danskOrd)
        {
            var result = await _ordbogService.GetOrdbogByDanskOrdAsync(danskOrd);
            if (result == null)
                return NotFound(ApiResponse<OrdbogDTO>.Fail("Ordbog not found", 404));

            return Ok(ApiResponse<OrdbogDTO>.Ok(result));
        }

        [HttpGet("by-koranord/{koranOrd}")]
        public async Task<ActionResult<ApiResponse<OrdbogDTO>>> GetOrdbogByKoranOrd(string koranOrd)
        {
            var result = await _ordbogService.GetOrdbogByKoranOrdAsync(koranOrd);
            if (result == null)
                return NotFound(ApiResponse<OrdbogDTO>.Fail("Ordbog not found", 404));

            return Ok(ApiResponse<OrdbogDTO>.Ok(result));
        }
    }
}
