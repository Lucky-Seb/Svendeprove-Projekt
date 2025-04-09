using Microsoft.AspNetCore.Mvc;
using TaekwondoOrchestration.ApiService.Services;
using TaekwondoApp.Shared.DTO;

namespace TaekwondoOrchestration.ApiService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BrugerQuizController : ControllerBase
    {
        private readonly BrugerQuizService _brugerQuizService;

        public BrugerQuizController(BrugerQuizService brugerQuizService)
        {
            _brugerQuizService = brugerQuizService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<BrugerQuizDTO>>> GetBrugerQuizzes()
        {
            return Ok(await _brugerQuizService.GetAllBrugerQuizzesAsync());
        }

        [HttpGet("{brugerId}/{quizId}")]
        public async Task<ActionResult<BrugerQuizDTO>> GetBrugerQuiz(int brugerId, int quizId)
        {
            var brugerQuiz = await _brugerQuizService.GetBrugerQuizByIdAsync(brugerId, quizId);
            if (brugerQuiz == null)
                return NotFound();
            return Ok(brugerQuiz);
        }

        [HttpPost]
        public async Task<ActionResult<BrugerQuizDTO>> PostBrugerQuiz(BrugerQuizDTO brugerQuizDto)
        {
            var createdBrugerQuiz = await _brugerQuizService.CreateBrugerQuizAsync(brugerQuizDto);
            return CreatedAtAction(nameof(GetBrugerQuiz), new { brugerId = createdBrugerQuiz.BrugerID, quizId = createdBrugerQuiz.QuizID }, createdBrugerQuiz);
        }

        [HttpDelete("{brugerId}/{quizId}")]
        public async Task<IActionResult> DeleteBrugerQuiz(int brugerId, int quizId)
        {
            var success = await _brugerQuizService.DeleteBrugerQuizAsync(brugerId, quizId);
            if (!success)
                return NotFound();
            return NoContent();
        }
    }
}
