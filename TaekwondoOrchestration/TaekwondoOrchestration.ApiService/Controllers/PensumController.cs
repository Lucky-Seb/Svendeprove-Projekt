using Microsoft.AspNetCore.Mvc;
using TaekwondoOrchestration.ApiService.Services;
using TaekwondoApp.Shared.DTO;
using TaekwondoApp.Shared.Helper;

namespace TaekwondoOrchestration.ApiService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PensumController : ApiBaseController
    {
        private readonly PensumService _pensumService;

        public PensumController(PensumService pensumService)
        {
            _pensumService = pensumService;
        }

        [HttpGet]
        public async Task<ActionResult<ApiResponse<IEnumerable<PensumDTO>>>> GetPensum()
        {
            var pensumList = await _pensumService.GetAllPensumAsync();
            return Ok(ApiResponse<IEnumerable<PensumDTO>>.Ok(pensumList.AsEnumerable()));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ApiResponse<PensumDTO>>> GetPensum(Guid id)
        {
            var pensum = await _pensumService.GetPensumByIdAsync(id);
            if (pensum == null)
                return NotFoundResponse<PensumDTO>("Pensum not found.");

            return OkResponse(pensum);
        }

        [HttpPost]
        public async Task<ActionResult<ApiResponse<PensumDTO>>> PostPensum([FromBody] PensumDTO pensumDTO)
        {
            var createdPensum = await _pensumService.CreatePensumAsync(pensumDTO);
            return CreatedResponse(nameof(GetPensum), new { id = createdPensum.PensumID }, createdPensum);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ApiResponse<string>>> PutPensum(Guid id, [FromBody] PensumDTO pensumDTO)
        {
            var success = await _pensumService.UpdatePensumAsync(id, pensumDTO);
            if (!success)
                return NotFoundResponse<string>("Failed to update Pensum. It may not exist.");

            return OkResponse("Pensum updated successfully.");
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<ApiResponse<string>>> DeletePensum(Guid id)
        {
            var success = await _pensumService.DeletePensumAsync(id);
            if (!success)
                return NotFoundResponse<string>("Pensum not found.");

            return OkResponse("Pensum deleted successfully.");
        }
    }
}
