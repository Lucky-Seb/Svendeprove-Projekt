﻿using Microsoft.AspNetCore.Mvc;
using TaekwondoOrchestration.ApiService.Services;
using TaekwondoApp.Shared.DTO;
using Microsoft.AspNetCore.SignalR;
using TaekwondoOrchestration.ApiService.NotificationHubs;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

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
        public async Task<ActionResult<IEnumerable<OrdbogDTO>>> GetOrdboger()
        {
            return Ok(await _ordbogService.GetAllOrdbogAsync());
        }
        [HttpGet("including-deleted")]
        public async Task<ActionResult<IEnumerable<OrdbogDTO>>> GetOrdbogerIncludingDeleted()
        {
            var ordboger = await _ordbogService.GetAllOrdbogIncludingDeletedAsync();
            return Ok(ordboger);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<OrdbogDTO>> GetOrdbog(Guid id)
        {
            var ordbog = await _ordbogService.GetOrdbogByIdAsync(id);
            if (ordbog == null)
                return NotFound();
            return Ok(ordbog);
        }
        [Authorize(Roles = "Admin")]
        // This is your restore endpoint
        [HttpPut("restore/{id}")]
        public async Task<IActionResult> Restore(Guid id, [FromBody] OrdbogDTO ordbogDto)
        {
            var success = await _ordbogService.RestoreOrdbogAsync(id, ordbogDto);

            if (!success)
                return NotFound();

            return Ok();
        }

        [HttpPost]
        public async Task<ActionResult<OrdbogDTO>> PostOrdbog(OrdbogDTO ordbogDto)
        {
            var createdOrdbog = await _ordbogService.CreateOrdbogAsync(ordbogDto);

            // Notify all connected clients
            await _hubContext.Clients.All.SendAsync("OrdbogUpdated");

            return CreatedAtAction(nameof(GetOrdbog), new { id = createdOrdbog.OrdbogId }, createdOrdbog);
        }
        [HttpPut("including-deleted/{id}")]
        public async Task<ActionResult<OrdbogDTO>> UpdateOrdbogIncludingDeleted(Guid id, OrdbogDTO ordbogDto)
        {
            var updatedOrdbog = await _ordbogService.UpdateOrdbogIncludingDeletedByIdAsync(id, ordbogDto);

            if (updatedOrdbog == null)
                return NotFound();

            await _hubContext.Clients.All.SendAsync("OrdbogUpdated");

            return Ok(updatedOrdbog);
        }
        [HttpPut("{id}")]
        public async Task<ActionResult<OrdbogDTO>> UpdateOrdbog(Guid id, OrdbogDTO ordbogDto)
        {
            // Check if the Ordbog exists
            var existingOrdbog = await _ordbogService.GetOrdbogByIdAsync(id);
            if (existingOrdbog == null)
                return NotFound();

            // Update the Ordbog
            var updatedOrdbog = await _ordbogService.UpdateOrdbogAsync(id, ordbogDto);

            // Notify all connected clients about the update
            await _hubContext.Clients.All.SendAsync("OrdbogUpdated");

            return Ok(updatedOrdbog);
        }
        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrdbog(Guid id)
        {
            var success = await _ordbogService.DeleteOrdbogAsync(id);
            if (!success)
                return NotFound();

            // Notify all connected clients
            await _hubContext.Clients.All.SendAsync("OrdbogUpdated");

            return NoContent();
        }

        // GET: api/Ordbog/by-danskord/{danskOrd}
        [HttpGet("by-danskord/{danskOrd}")]
        public async Task<ActionResult<OrdbogDTO>> GetOrdbogByDanskOrd(string danskOrd)
        {
            var ordbog = await _ordbogService.GetOrdbogByDanskOrdAsync(danskOrd);
            if (ordbog == null)
                return NotFound();

            return Ok(ordbog);
        }

        // GET: api/Ordbog/by-koranord/{koranOrd}
        [HttpGet("by-koranord/{koranOrd}")]
        public async Task<ActionResult<OrdbogDTO>> GetOrdbogByKoranOrd(string koranOrd)
        {
            var ordbog = await _ordbogService.GetOrdbogByKoranOrdAsync(koranOrd);
            if (ordbog == null)
                return NotFound();

            return Ok(ordbog);
        } 
    }
}
