﻿using Microsoft.AspNetCore.Mvc;
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
    public class BrugerController : ApiBaseController
    {
        private readonly IBrugerService _brugerService;
        private readonly IHubContext<BrugerHub> _hubContext;

        public BrugerController(IBrugerService brugerService, IHubContext<BrugerHub> hubContext)
        {
            _brugerService = brugerService;
            _hubContext = hubContext;
        }

        [HttpGet]
        public async Task<IActionResult> GetBrugere()
        {
            var result = await _brugerService.GetAllBrugereAsync();
            return result.ToApiResponse();
        }

        [HttpGet("{id}")]
        [Authorize]
        public async Task<IActionResult> GetBruger(Guid id)
        {
            var result = await _brugerService.GetBrugerByIdAsync(id);
            return result.ToApiResponse();
        }
        // New endpoint for getting a user with detailed information
        [HttpGet("details/{id}")]
        [Authorize]
        public async Task<IActionResult> GetBrugerWithDetails(Guid id)
        {
            var result = await _brugerService.GetBrugerWithDetailsAsync(id);
            return result.ToApiResponse();
        }
        [HttpGet("role/{role}")]
        [Authorize]
        public async Task<IActionResult> GetBrugerByRole(string role)
        {
            var result = await _brugerService.GetBrugerByRoleAsync(role);
            return result.ToApiResponse();
        }

        [HttpGet("bælte/{bæltegrad}")]
        [Authorize]
        public async Task<IActionResult> GetBrugerByBælte(string bæltegrad)
        {
            var result = await _brugerService.GetBrugerByBælteAsync(bæltegrad);
            return result.ToApiResponse();
        }

        [HttpGet("klub/{klubId}")]
        [Authorize]
        public async Task<IActionResult> GetBrugereByKlubAsync(Guid klubId)
        {
            var result = await _brugerService.GetBrugereByKlubAsync(klubId);
            return result.ToApiResponse();
        }

        [HttpGet("klub/{klubId}/bæltegrad/{bæltegrad}")]
        [Authorize]
        public async Task<IActionResult> GetBrugereByKlubAndBæltegrad(Guid klubId, string bæltegrad)
        {
            var result = await _brugerService.GetBrugereByKlubAndBæltegradAsync(klubId, bæltegrad);
            return result.ToApiResponse();
        }

        [HttpGet("brugernavn/{brugernavn}")]
        [Authorize]
        public async Task<IActionResult> GetBrugerByBrugernavn(string brugernavn)
        {
            var result = await _brugerService.GetBrugerByBrugernavnAsync(brugernavn);
            return result.ToApiResponse();
        }

        [HttpGet("navn/{fornavn}/{efternavn}")]
        [Authorize]
        public async Task<IActionResult> GetBrugerByFornavnEfternavn(string fornavn, string efternavn)
        {
            var result = await _brugerService.GetBrugerByFornavnEfternavnAsync(fornavn, efternavn);
            return result.ToApiResponse();
        }

        [HttpPost]
        public async Task<IActionResult> PostBruger([FromBody] BrugerDTO brugerDTO)
        {
            var result = await _brugerService.CreateBrugerAsync(brugerDTO);
            if (result.Success)
                await _hubContext.Clients.All.SendAsync("BrugerCreated");

            return result.ToApiResponse();
        }

        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> PutBruger(Guid id, [FromBody] BrugerUpdateDTO brugerUpdateDTO)
        {
            var result = await _brugerService.UpdateBrugerAsync(id, brugerUpdateDTO);
            if (result.Success)
                await _hubContext.Clients.All.SendAsync("BrugerUpdated");

            return result.ToApiResponse();
        }

        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> DeleteBruger(Guid id)
        {
            var result = await _brugerService.DeleteBrugerAsync(id);
            if (result.Success)
                await _hubContext.Clients.All.SendAsync("BrugerDeleted");

            return result.ToApiResponse();
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDTO loginDto)
        {
            var result = await _brugerService.AuthenticateBrugerAsync(loginDto);
            return result.ToApiResponse();
        }
    }
}
