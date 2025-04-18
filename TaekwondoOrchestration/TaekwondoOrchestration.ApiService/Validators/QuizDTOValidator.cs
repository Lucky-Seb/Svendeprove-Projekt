using FluentValidation;
using TaekwondoApp.Shared.DTO;

namespace TaekwondoOrchestration.ApiService.Validators
{
    public class QuizDTOValidator : AbstractValidator<QuizDTO>
    {
        public QuizDTOValidator()
        {
            RuleFor(x => x.QuizNavn)
                .NotEmpty().WithMessage("QuizNavn is required.");

            RuleFor(x => x.QuizBeskrivelse)
                .NotEmpty().WithMessage("QuizBeskrivelse is required.");

            RuleFor(x => x.PensumID)
                .NotEmpty().WithMessage("PensumID is required.");
        }
    }
}
