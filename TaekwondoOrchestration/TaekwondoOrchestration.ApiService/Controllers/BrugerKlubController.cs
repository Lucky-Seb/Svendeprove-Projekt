using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using TaekwondoApp.Shared.DTO;
using TaekwondoApp.Shared.Helper;
using TaekwondoOrchestration.ApiService.Helpers;
using TaekwondoOrchestration.ApiService.NotificationHubs;
using TaekwondoOrchestration.ApiService.ServiceInterfaces;

namespace TaekwondoOrchestration.ApiService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BrugerKlubController : ApiBaseController
    {
        private readonly IBrugerKlubService _brugerKlubService;
        private readonly IHubContext<BrugerKlubHub> _hubContext;

        public BrugerKlubController(IBrugerKlubService brugerKlubService, IHubContext<BrugerKlubHub> hubContext)
        {
            _brugerKlubService = brugerKlubService;
            _hubContext = hubContext;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllBrugerKlubs()
        {
            var result = await _brugerKlubService.GetAllBrugerKlubsAsync();
            return result.ToApiResponse();
        }

        [HttpGet("{brugerId}/{klubId}")]
        public async Task<IActionResult> GetBrugerKlubById(Guid brugerId, Guid klubId)
        {
            var result = await _brugerKlubService.GetBrugerKlubByIdAsync(brugerId, klubId);
            return result.ToApiResponse();
        }

        [HttpPost]
        public async Task<IActionResult> CreateBrugerKlub([FromBody] BrugerKlubDTO brugerKlubDto)
        {
            var result = await _brugerKlubService.CreateBrugerKlubAsync(brugerKlubDto);
            if (result.Success)
            {
                await _hubContext.Clients.All.SendAsync("BrugerKlubCreated", result.Value);
                return CreatedAtAction(nameof(GetBrugerKlubById), new { brugerId = result.Value.BrugerID, klubId = result.Value.KlubID }, result.Value);
            }
            return result.ToApiResponse();
        }

        [HttpDelete("{brugerId}/{klubId}")]
        public async Task<IActionResult> DeleteBrugerKlub(Guid brugerId, Guid klubId)
        {
            var result = await _brugerKlubService.DeleteBrugerKlubAsync(brugerId, klubId);
            if (result.Success)
            {
                await _hubContext.Clients.All.SendAsync("BrugerKlubDeleted", new { brugerId, klubId });
                return NoContent();
            }
            return result.ToApiResponse();
        }

        // 🔐 Check if current user is Admin in a specific club
        [HttpGet("admin/{klubId}")]
        [Authorize]
        public async Task<IActionResult> CheckIfUserIsAdmin(Guid klubId)
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userIdClaim == null)
            {
                return Unauthorized(ApiResponse<bool>.Fail("User not logged in", 401));
            }

            if (!Guid.TryParse(userIdClaim, out Guid brugerId))
            {
                return BadRequest(ApiResponse<bool>.Fail("Invalid user ID"));
            }

            var result = await _brugerKlubService.CheckIfUserIsAdminAsync(brugerId, klubId);

            if (result.Success)
            {
                return Ok(ApiResponse<bool>.Ok(result.Value));
            }

            return BadRequest(ApiResponse<bool>.Fail(result.Errors));
        }
    }
}
