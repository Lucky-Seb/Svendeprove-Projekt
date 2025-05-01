using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using TaekwondoApp.Shared.DTO;
using TaekwondoOrchestration.ApiService.NotificationHubs;
using TaekwondoOrchestration.ApiService.ServiceInterfaces;
using TaekwondoOrchestration.ApiService.Helpers;
using Microsoft.AspNetCore.Authorization;

namespace TaekwondoOrchestration.ApiService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class KlubQuizController : ApiBaseController
    {
        private readonly IKlubQuizService _klubQuizService;
        private readonly IHubContext<KlubQuizHub> _hubContext;

        public KlubQuizController(IKlubQuizService klubQuizService, IHubContext<KlubQuizHub> hubContext)
        {
            _klubQuizService = klubQuizService;
            _hubContext = hubContext;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllKlubQuizzer()
        {
            var result = await _klubQuizService.GetAllKlubQuizzerAsync();
            return result.ToApiResponse(); // Converts result to the appropriate API response
        }

        [HttpGet("{klubId}/{quizId}")]
        public async Task<IActionResult> GetKlubQuiz(Guid klubId, Guid quizId)
        {
            var result = await _klubQuizService.GetKlubQuizByIdAsync(klubId, quizId);
            return result.ToApiResponse(); // Converts result to the appropriate API response
        }

        [HttpPost]
        public async Task<IActionResult> CreateKlubQuiz(KlubQuizDTO klubQuizDto)
        {
            var result = await _klubQuizService.CreateKlubQuizAsync(klubQuizDto);
            if (result.Success)
            {
                // Notify clients about the new KlubQuiz
                await _hubContext.Clients.All.SendAsync("KlubQuizCreated", result.Value);
                return CreatedAtAction(nameof(GetKlubQuiz), new { klubId = result.Value.KlubID, quizId = result.Value.QuizID }, result.Value);
            }
            return result.ToApiResponse(); // Converts failure to BadRequest
        }

        [HttpDelete("{klubId}/{quizId}")]
        [Authorize]
        public async Task<IActionResult> DeleteKlubQuiz(Guid klubId, Guid quizId)
        {
            var result = await _klubQuizService.DeleteKlubQuizAsync(klubId, quizId);
            if (result.Success)
            {
                // Notify clients about the deletion of KlubQuiz
                await _hubContext.Clients.All.SendAsync("KlubQuizDeleted", new { KlubID = klubId, QuizID = quizId });
                return NoContent();
            }
            return result.ToApiResponse(); // Converts failure to BadRequest
        }
    }
}
