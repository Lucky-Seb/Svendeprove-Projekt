using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using TaekwondoApp.Shared.DTO;
using TaekwondoOrchestration.ApiService.NotificationHubs;
using TaekwondoOrchestration.ApiService.ServiceInterfaces;
using TaekwondoOrchestration.ApiService.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaekwondoOrchestration.ApiService.Services;

namespace TaekwondoOrchestration.ApiService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuizController : ApiBaseController
    {
        private readonly IQuizService _quizService;
        private readonly IHubContext<QuizHub> _hubContext; // Assuming you have a QuizHub for real-time notifications

        public QuizController(IQuizService quizService, IHubContext<QuizHub> hubContext)
        {
            _quizService = quizService;
            _hubContext = hubContext;
        }

        // GET: api/quiz
        [HttpGet]
        public async Task<IActionResult> GetQuizzes()
        {
            var result = await _quizService.GetAllQuizzesAsync();
            return result.ToApiResponse(); // Assuming result is a Response object that has the ToApiResponse method
        }

        [HttpGet("own")]
        public async Task<IActionResult> GetQuizzes([FromQuery] Guid? brugerId = null, [FromQuery] string klubIds = "")
        {
            // Parse the klubIds query parameter into a list of GUIDs
            var klubIdList = klubIds?
                .Split(',', StringSplitOptions.RemoveEmptyEntries)
                .Select(Guid.Parse)
                .ToList() ?? new List<Guid>();

            // Call the service to get filtered quizzes
            var result = await _quizService.GetFilteredQuizzesAsync(brugerId, klubIdList);

            // Return the result as an API response
            return result.ToApiResponse();
        }


        // GET: api/quiz/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetQuiz(Guid id)
        {
            var result = await _quizService.GetQuizByIdAsync(id);
            return result.ToApiResponse(); // Assuming result is a Response object that has the ToApiResponse method
        }

        // GET: api/quiz/details/5
        [HttpGet("details/{id}")]
        public async Task<IActionResult> GetQuizWithDetails(Guid id)
        {
            var result = await _quizService.GetQuizWithDetailsAsync(id);
            return result.ToApiResponse(); // Assumes Result<T> has a ToApiResponse() extension method
        }

        // POST: api/quiz (Create Quiz with Bruger or Klub)
        [HttpPost]
        public async Task<IActionResult> PostQuiz([FromBody] QuizDTO quizDto)
        {
            if (quizDto == null)
            {
                return BadRequest("Invalid data");
            }

            var result = await _quizService.CreateQuizAsync(quizDto);

            // Optionally, trigger notifications if required
            if (result.Success)
                await _hubContext.Clients.All.SendAsync("QuizUpdated");

            return result.ToApiResponse();
        }

        // PUT: api/quiz/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutQuiz(Guid id, [FromBody] QuizDTO quizDto)
        {
            var result = await _quizService.UpdateQuizAsync(id, quizDto);

            // Optionally, trigger notifications if required
            if (result.Success)
                await _hubContext.Clients.All.SendAsync("QuizUpdated");

            return result.ToApiResponse();
        }

        // DELETE: api/quiz/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteQuiz(Guid id)
        {
            var result = await _quizService.DeleteQuizAsync(id);

            // Optionally, trigger notifications if required
            if (result.Success)
                await _hubContext.Clients.All.SendAsync("QuizDeleted");

            return result.ToApiResponse();
        }

        //// PUT: api/quiz/spørgsmål/5
        //[HttpPut("spørgsmål/{id}")]
        //public async Task<IActionResult> PutQuizWithSpørgsmål(Guid id, [FromBody] QuizDTO quizDto)
        //{
        //    var result = await _quizService.UpdateQuizAsync(id, quizDto);

        //    // Optionally, trigger notifications if required
        //    if (result.Success)
        //        await _hubContext.Clients.All.SendAsync("QuizUpdated");

        //    return result.ToApiResponse();
        //}

        // GET: api/quiz/by-bruger/{brugerId}
        [HttpGet("by-bruger/{brugerId}")]
        public async Task<IActionResult> GetAllByBrugerAsync(Guid brugerId)
        {
            var result = await _quizService.GetAllQuizzesByBrugerIdAsync(brugerId);
            return result.ToApiResponse();
        }

        // GET: api/quiz/by-klub/{klubId}
        [HttpGet("by-klub/{klubId}")]
        public async Task<IActionResult> GetAllByKlubAsync(Guid klubId)
        {
            var result = await _quizService.GetAllQuizzesByKlubIdAsync(klubId);
            return result.ToApiResponse();
        }

        // GET: api/quiz/by-pensum/{pensumId}
        [HttpGet("by-pensum/{pensumId}")]
        public async Task<IActionResult> GetAllByPensumAsync(Guid pensumId)
        {
            var result = await _quizService.GetAllQuizzesByPensumIdAsync(pensumId);
            return result.ToApiResponse();
        }

        // GET: api/quiz/all
        [HttpGet("all")]
        public async Task<IActionResult> GetAllQuizzes()
        {
            var result = await _quizService.GetAllQuizzesIncludingDeletedAsync();
            return result.ToApiResponse();
        }

        // GET: api/quiz/{id}
        [HttpGet("including-deleted/{id}")]
        public async Task<IActionResult> GetQuizByIdIncludingDeleted(Guid id)
        {
            var result = await _quizService.GetQuizByIdIncludingDeletedAsync(id);
            return result.ToApiResponse();
        }
    }
}
