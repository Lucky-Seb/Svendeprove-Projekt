using FluentValidation;
using TaekwondoApp.Shared.DTO;

namespace TaekwondoOrchestration.ApiService.Validators
{
    public class TræningDTOValidator : AbstractValidator<TræningDTO>
    {
        public TræningDTOValidator()
        {
            RuleFor(x => x.TræningID)
                .NotEmpty().WithMessage("TræningID is required.");

            RuleFor(x => x.TræningRækkefølge)
                .GreaterThan(0).WithMessage("TræningRækkefølge must be greater than 0.");

            RuleFor(x => x.Tid)
                .GreaterThan(0).WithMessage("Tid must be greater than 0.");

            RuleFor(x => x.ProgramID)
                .NotEmpty().WithMessage("ProgramID is required.");

            RuleFor(x => x.QuizID)
                .NotEmpty().WithMessage("QuizID is required.")
                .When(x => x.QuizID.HasValue);

            RuleFor(x => x.TeknikID)
                .NotEmpty().WithMessage("TeknikID is required.")
                .When(x => x.TeknikID.HasValue);

            RuleFor(x => x.TeoriID)
                .NotEmpty().WithMessage("TeoriID is required.")
                .When(x => x.TeoriID.HasValue);

            RuleFor(x => x.ØvelseID)
                .NotEmpty().WithMessage("ØvelseID is required.")
                .When(x => x.ØvelseID.HasValue);

            RuleFor(x => x.PensumID)
                .NotEmpty().WithMessage("PensumID is required.");
        }
    }
}
