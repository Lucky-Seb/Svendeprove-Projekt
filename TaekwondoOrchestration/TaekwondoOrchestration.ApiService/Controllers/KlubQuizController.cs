using Microsoft.AspNetCore.Mvc;
using TaekwondoOrchestration.ApiService.Services;
using TaekwondoApp.Shared.DTO;


namespace TaekwondoOrchestration.ApiService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class KlubQuizController : ControllerBase
    {
        private readonly KlubQuizService _klubQuizService;

        public KlubQuizController(KlubQuizService klubQuizService)
        {
            _klubQuizService = klubQuizService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<KlubQuizDTO>>> GetKlubQuizzer()
        {
            return Ok(await _klubQuizService.GetAllKlubQuizzerAsync());
        }

        [HttpGet("{klubId}/{quizId}")]
        public async Task<ActionResult<KlubQuizDTO>> GetKlubQuiz(Guid klubId, Guid quizId)
        {
            var klubQuiz = await _klubQuizService.GetKlubQuizByIdAsync(klubId, quizId);
            if (klubQuiz == null)
                return NotFound();
            return Ok(klubQuiz);
        }

        [HttpPost]
        public async Task<ActionResult<KlubQuizDTO>> PostKlubQuiz(KlubQuizDTO klubQuizDto)
        {
            var createdKlubQuiz = await _klubQuizService.CreateKlubQuizAsync(klubQuizDto);
            return CreatedAtAction(nameof(GetKlubQuiz), new { klubId = createdKlubQuiz.KlubID, quizId = createdKlubQuiz.QuizID }, createdKlubQuiz);
        }

        [HttpDelete("{klubId}/{quizId}")]
        public async Task<IActionResult> DeleteKlubQuiz(Guid klubId, Guid quizId)
        {
            var success = await _klubQuizService.DeleteKlubQuizAsync(klubId, quizId);
            if (!success)
                return NotFound();
            return NoContent();
        }
    }
}
