using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.AspNetCore.Authorization;
using TaekwondoApp.Shared.DTO;
using TaekwondoOrchestration.ApiService.NotificationHubs;
using TaekwondoOrchestration.ApiService.ServiceInterfaces;
using TaekwondoOrchestration.ApiService.Helpers;

namespace TaekwondoOrchestration.ApiService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdbogController : ApiBaseController
    {
        private readonly IOrdbogService _ordbogService;
        private readonly IHubContext<OrdbogHub> _hubContext;

        public OrdbogController(IOrdbogService ordbogService, IHubContext<OrdbogHub> hubContext)
        {
            _ordbogService = ordbogService;
            _hubContext = hubContext;
        }

        [HttpGet]
        public async Task<IActionResult> GetOrdboger()
        {
            var result = await _ordbogService.GetAllOrdbogAsync();
            return result.ToApiResponse();
        }

        [HttpGet("including-deleted")]
        public async Task<IActionResult> GetOrdbogerIncludingDeleted()
        {
            var result = await _ordbogService.GetAllOrdbogIncludingDeletedAsync();
            return result.ToApiResponse();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetOrdbog(Guid id)
        {
            var result = await _ordbogService.GetOrdbogByIdAsync(id);
            return result.ToApiResponse();
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("restore/{id}")]
        public async Task<IActionResult> Restore(Guid id, [FromBody] OrdbogDTO dto)
        {
            var result = await _ordbogService.RestoreOrdbogAsync(id, dto);
            return result.ToApiResponse();
        }
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> PostOrdbog(OrdbogDTO dto)
        {
            var result = await _ordbogService.CreateOrdbogAsync(dto);
            if (result.Success)
                await _hubContext.Clients.All.SendAsync("OrdbogUpdated");

            return result.ToApiResponse();
        }
        [Authorize]
        [HttpPut("including-deleted/{id}")]
        public async Task<IActionResult> UpdateOrdbogIncludingDeleted(Guid id, OrdbogDTO dto)
        {
            var result = await _ordbogService.UpdateOrdbogIncludingDeletedByIdAsync(id, dto);
            if (result.Success)
                await _hubContext.Clients.All.SendAsync("OrdbogUpdated");

            return result.ToApiResponse();
        }
        [Authorize]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateOrdbog(Guid id, OrdbogDTO dto)
        {
            var result = await _ordbogService.UpdateOrdbogAsync(id, dto);
            if (result.Success)
                await _hubContext.Clients.All.SendAsync("OrdbogUpdated");

            return result.ToApiResponse();
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrdbog(Guid id)
        {
            var result = await _ordbogService.DeleteOrdbogAsync(id);
            if (result.Success)
                await _hubContext.Clients.All.SendAsync("OrdbogDeleted");

            return result.ToApiResponse();
        }

        [HttpGet("by-danskord/{danskOrd}")]
        public async Task<IActionResult> GetOrdbogByDanskOrd(string danskOrd)
        {
            var result = await _ordbogService.GetOrdbogByDanskOrdAsync(danskOrd);
            return result.ToApiResponse();
        }

        [HttpGet("by-koranord/{koranOrd}")]
        public async Task<IActionResult> GetOrdbogByKoranOrd(string koranOrd)
        {
            var result = await _ordbogService.GetOrdbogByKoranOrdAsync(koranOrd);
            return result.ToApiResponse();
        }
    }
}
