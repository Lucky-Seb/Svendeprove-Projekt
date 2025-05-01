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
    public class BrugerQuizController : ApiBaseController
    {
        private readonly IBrugerQuizService _brugerQuizService;
        private readonly IHubContext<BrugerQuizHub> _hubContext; // Assuming you have a Hub for BrugerQuiz

        public BrugerQuizController(IBrugerQuizService brugerQuizService, IHubContext<BrugerQuizHub> hubContext)
        {
            _brugerQuizService = brugerQuizService;
            _hubContext = hubContext;
        }

        // Get all BrugerQuizzes
        [HttpGet]
        public async Task<IActionResult> GetBrugerQuizzes()
        {
            var result = await _brugerQuizService.GetAllBrugerQuizzesAsync();
            return result.ToApiResponse(); // Assuming this is an extension method to return consistent API responses
        }

        // Get a BrugerQuiz by ID
        [HttpGet("{brugerId}/{quizId}")]
        public async Task<IActionResult> GetBrugerQuiz(Guid brugerId, Guid quizId)
        {
            var result = await _brugerQuizService.GetBrugerQuizByIdAsync(brugerId, quizId);
            return result.ToApiResponse();
        }

        // Create a new BrugerQuiz
        [HttpPost]
        public async Task<IActionResult> PostBrugerQuiz([FromBody] BrugerQuizDTO brugerQuizDto)
        {
            var result = await _brugerQuizService.CreateBrugerQuizAsync(brugerQuizDto);
            if (result.Success)
                await _hubContext.Clients.All.SendAsync("BrugerQuizUpdated"); // Broadcasting an update via SignalR

            return result.ToApiResponse();
        }

        // Delete a BrugerQuiz by ID
        [HttpDelete("{brugerId}/{quizId}")]
        [Authorize]
        public async Task<IActionResult> DeleteBrugerQuiz(Guid brugerId, Guid quizId)
        {
            var result = await _brugerQuizService.DeleteBrugerQuizAsync(brugerId, quizId);
            if (result.Success)
                await _hubContext.Clients.All.SendAsync("BrugerQuizDeleted");

            return result.ToApiResponse();
        }
    }
}
