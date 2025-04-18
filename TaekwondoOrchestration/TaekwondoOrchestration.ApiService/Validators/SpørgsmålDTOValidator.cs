using FluentValidation;
using TaekwondoApp.Shared.DTO;

namespace TaekwondoOrchestration.ApiService.Validators
{
    public class SpørgsmålDTOValidator : AbstractValidator<SpørgsmålDTO>
    {
        public SpørgsmålDTOValidator()
        {
            RuleFor(x => x.SpørgsmålID)
                .NotEmpty().WithMessage("SpørgsmålID is required.");

            RuleFor(x => x.SpørgsmålRækkefølge)
                .GreaterThan(0).WithMessage("SpørgsmålRækkefølge must be greater than 0.");

            RuleFor(x => x.SpørgsmålTid)
                .GreaterThan(0).WithMessage("SpørgsmålTid must be greater than 0.");

            RuleFor(x => x.QuizID)
                .NotEmpty().WithMessage("QuizID is required.");

            RuleFor(x => x.TeoriID)
                .NotNull().WithMessage("TeoriID is required.")
                .Must(g => g != Guid.Empty).WithMessage("TeoriID cannot be an empty GUID.");

            RuleFor(x => x.TeknikID)
                .NotNull().WithMessage("TeknikID is required.")
                .Must(g => g != Guid.Empty).WithMessage("TeknikID cannot be an empty GUID.");

            RuleFor(x => x.ØvelseID)
                .NotNull().WithMessage("ØvelseID is required.")
                .Must(g => g != Guid.Empty).WithMessage("ØvelseID cannot be an empty GUID.");

            RuleFor(x => x.Teknik)
                .NotNull().WithMessage("Teknik is required.")
                .When(x => x.TeknikID.HasValue);

            RuleFor(x => x.Teori)
                .NotNull().WithMessage("Teori is required.")
                .When(x => x.TeoriID.HasValue);

            RuleFor(x => x.Øvelse)
                .NotNull().WithMessage("Øvelse is required.")
                .When(x => x.ØvelseID.HasValue);
        }
    }
}
