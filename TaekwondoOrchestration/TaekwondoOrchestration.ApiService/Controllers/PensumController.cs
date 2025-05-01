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
    public class PensumController : ApiBaseController
    {
        private readonly IPensumService _pensumService;
        private readonly IHubContext<PensumHub> _hubContext; // Assuming you have a PensumHub for real-time notifications

        public PensumController(IPensumService pensumService, IHubContext<PensumHub> hubContext)
        {
            _pensumService = pensumService;
            _hubContext = hubContext;
        }

        // GET: api/Pensum
        [HttpGet]
        public async Task<IActionResult> GetPensum()
        {
            var result = await _pensumService.GetAllPensumAsync();
            return result.ToApiResponse(); // Assuming result is a Response object that has the ToApiResponse method
        }

        // GET: api/Pensum/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetPensum(Guid id)
        {
            var result = await _pensumService.GetPensumByIdAsync(id);
            return result.ToApiResponse(); // Assuming result is a Response object that has the ToApiResponse method
        }

        // GET: api/Pensum/grad/{grad}
        [HttpGet("grad/{grad}")]
        public async Task<IActionResult> GetPensumByGrad(string grad)
        {
            var result = await _pensumService.GetPensumByGradAsync(grad);
            return result.ToApiResponse();
        }

        // POST: api/Pensum
        [HttpPost]
        public async Task<IActionResult> PostPensum([FromBody] PensumDTO pensumDto)
        {
            var result = await _pensumService.CreatePensumAsync(pensumDto);

            // Optionally, trigger notifications if required
            if (result.Success)
                await _hubContext.Clients.All.SendAsync("PensumUpdated");

            return result.ToApiResponse();
        }

        // PUT: api/Pensum/5
        [Authorize]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPensum(Guid id, [FromBody] PensumDTO pensumDto)
        {
            var result = await _pensumService.UpdatePensumAsync(id, pensumDto);

            // Optionally, trigger notifications if required
            if (result.Success)
                await _hubContext.Clients.All.SendAsync("PensumUpdated");

            return result.ToApiResponse();
        }

        // DELETE: api/Pensum/5
        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePensum(Guid id)
        {
            var result = await _pensumService.DeletePensumAsync(id);

            // Optionally, trigger notifications if required
            if (result.Success)
                await _hubContext.Clients.All.SendAsync("PensumDeleted");

            return result.ToApiResponse();
        }
    }
}
