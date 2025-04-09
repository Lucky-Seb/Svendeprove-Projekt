using Microsoft.AspNetCore.Mvc;
using TaekwondoOrchestration.ApiService.Services;
using TaekwondoApp.Shared.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TaekwondoOrchestration.ApiService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuizController : ControllerBase
    {
        private readonly QuizService _quizService;

        public QuizController(QuizService quizService)
        {
            _quizService = quizService;
        }

        // GET: api/quiz
        [HttpGet]
        public async Task<ActionResult<IEnumerable<QuizDTO>>> GetQuizzes()
        {
            var quizList = await _quizService.GetAllQuizzesAsync();
            return Ok(quizList);
        }

        // GET: api/quiz/5
        [HttpGet("{id}")]
        public async Task<ActionResult<QuizDTO>> GetQuiz(int id)
        {
            var quiz = await _quizService.GetQuizByIdAsync(id);
            if (quiz == null)
                return NotFound();
            return Ok(quiz);
        }

        // POST: api/quiz (Create Quiz with Bruger or Klub)
        [HttpPost]
        public async Task<ActionResult<QuizDTO>> PostQuiz([FromBody] QuizDTO quizDto)
        {
            var createdQuiz = await _quizService.CreateQuizWithBrugerAndKlubAsync(quizDto);
            return CreatedAtAction(nameof(GetQuiz), new { id = createdQuiz.QuizID }, createdQuiz);
        }

        // PUT: api/quiz/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutQuiz(int id, QuizDTO quizDto)
        {
            var success = await _quizService.UpdateQuizAsync(id, quizDto);
            if (!success)
                return BadRequest();
            return NoContent();
        }

        // DELETE: api/quiz/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteQuiz(int id)
        {
            var success = await _quizService.DeleteQuizAsync(id);
            if (!success)
                return NotFound();
            return NoContent();
        }
        [HttpPut("spørgsmål/{id}")]
        public async Task<IActionResult> PutProgramPlanwithtræning(int id, QuizDTO quizDTO)
        {
            var updatedProgramPlan = await _quizService.UpdateQuizWithBrugerAndKlubAsync(id, quizDTO);
            if (updatedProgramPlan == null)
                return NotFound();

            // Return the updated ProgramPlanDTO with a 200 OK response
            return Ok(updatedProgramPlan);
        }

        // Get all quizzes by bruger (user)
        [HttpGet("by-bruger/{brugerId}")]
        public async Task<IActionResult> GetAllByBrugerAsync(int brugerId)
        {
            var quizzes = await _quizService.GetAllQuizzesByBrugerAsync(brugerId);
            if (quizzes == null || !quizzes.Any())
            {
                return NotFound("No quizzes found for the specified user.");
            }
            return Ok(quizzes);
        }

        // Get all quizzes by klub (club)
        [HttpGet("by-klub/{klubId}")]
        public async Task<IActionResult> GetAllByKlubAsync(int klubId)
        {
            var quizzes = await _quizService.GetAllQuizzesByKlubAsync(klubId);
            if (quizzes == null || !quizzes.Any())
            {
                return NotFound("No quizzes found for the specified club.");
            }
            return Ok(quizzes);
        }

        // Get all quizzes by pensum (curriculum)
        [HttpGet("by-pensum/{pensumId}")]
        public async Task<IActionResult> GetAllByPensumAsync(int pensumId)
        {
            var quizzes = await _quizService.GetAllQuizzesByPensumAsync(pensumId);
            if (quizzes == null || !quizzes.Any())
            {
                return NotFound("No quizzes found for the specified curriculum.");
            }
            return Ok(quizzes);
        }

    }
}
