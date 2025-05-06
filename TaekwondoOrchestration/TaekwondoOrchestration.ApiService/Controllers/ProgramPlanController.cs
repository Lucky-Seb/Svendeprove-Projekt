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
    public class ProgramPlanController : ApiBaseController
    {
        private readonly IProgramPlanService _programPlanService;
        private readonly IHubContext<ProgramPlanHub> _hubContext; // Assuming you have a ProgramPlanHub for real-time notifications

        public ProgramPlanController(IProgramPlanService programPlanService, IHubContext<ProgramPlanHub> hubContext)
        {
            _programPlanService = programPlanService;
            _hubContext = hubContext;
        }

        // GET: api/programplan
        [HttpGet]
        public async Task<IActionResult> GetProgramPlans()
        {
            var result = await _programPlanService.GetAllProgramPlansAsync();
            return result.ToApiResponse(); // Assuming result is a Response object that has the ToApiResponse method
        }

        [HttpGet("own")]
        public async Task<IActionResult> GetProgramPlans([FromQuery] Guid? brugerId = null, [FromQuery] string klubIds = "")
        {
            // Parse the klubIds query parameter into a list of GUIDs
            var klubIdList = klubIds?
                .Split(',', StringSplitOptions.RemoveEmptyEntries)
                .Select(Guid.Parse)
                .ToList() ?? new List<Guid>();

            // Call the service to get filtered program plans
            var result = await _programPlanService.GetFilteredProgramPlansAsync(brugerId, klubIdList);

            // Return the result as an API response
            return result.ToApiResponse();
        }
        // GET: api/programplan/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetProgramPlan(Guid id)
        {
            var result = await _programPlanService.GetProgramPlanByIdAsync(id);
            return result.ToApiResponse(); // Assuming result is a Response object that has the ToApiResponse method
        }
        // GET: api/programplan/details/5
        [HttpGet("details/{id}")]
        public async Task<IActionResult> GetProgramPlanWithDetails(Guid id)
        {
            var result = await _programPlanService.GetProgramPlanWithDetailsAsync(id);
            return result.ToApiResponse(); // Assumes Result<T> has a ToApiResponse() extension method
        }

        // POST: api/programplan
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> PostProgramPlan([FromBody] ProgramPlanDTO programPlanDTO)
        {
            if (programPlanDTO == null)
            {
                return BadRequest("Invalid data");
            }

            var result = await _programPlanService.CreateProgramPlanWithBrugerOrKlubAsync(programPlanDTO);

            // Optionally, trigger notifications if required
            if (result.Success)
                await _hubContext.Clients.All.SendAsync("ProgramPlanUpdated");

            return result.ToApiResponse();
        }

        // PUT: api/programplan/5
        [Authorize]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProgramPlan(Guid id, [FromBody] ProgramPlanDTO programPlanDto)
        {
            var result = await _programPlanService.UpdateProgramPlanAsync(id, programPlanDto);

            // Optionally, trigger notifications if required
            if (result.Success)
                await _hubContext.Clients.All.SendAsync("ProgramPlanUpdated");

            return result.ToApiResponse();
        }

        // DELETE: api/programplan/5
        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProgramPlan(Guid id)
        {
            var result = await _programPlanService.DeleteProgramPlanAsync(id);

            // Optionally, trigger notifications if required
            if (result.Success)
                await _hubContext.Clients.All.SendAsync("ProgramPlanDeleted");

            return result.ToApiResponse();
        }

        // PUT: api/programplan/træning/5
        [Authorize]
        [HttpPut("træning/{id}")]
        public async Task<IActionResult> PutProgramPlanWithTræning(Guid id, [FromBody] ProgramPlanDTO programPlanDto)
        {
            var result = await _programPlanService.UpdateProgramPlanWithBrugerOrKlubAsync(id, programPlanDto);

            // Optionally, trigger notifications if required
            if (result.Success)
                await _hubContext.Clients.All.SendAsync("ProgramPlanUpdated");

            return result.ToApiResponse();
        }

        // GET: api/programplan/by-bruger/{brugerId}
        [HttpGet("by-bruger/{brugerId}")]
        public async Task<IActionResult> GetAllProgramsByBruger(Guid brugerId)
        {
            var result = await _programPlanService.GetAllProgramPlansByBrugerIdAsync(brugerId);
            return result.ToApiResponse();
        }

        // GET: api/programplan/by-klub/{klubId}
        [HttpGet("by-klub/{klubId}")]
        public async Task<IActionResult> GetAllProgramsByKlub(Guid klubId)
        {
            var result = await _programPlanService.GetAllProgramPlansByBrugerIdAsync(klubId);
            return result.ToApiResponse();
        }
    }
}
