using FluentValidation;
using TaekwondoApp.Shared.DTO;

namespace TaekwondoOrchestration.ApiService.Validators
{
    public class TeoriDTOValidator : AbstractValidator<TeoriDTO>
    {
        public TeoriDTOValidator()
        {
            RuleFor(x => x.TeoriNavn)
                .NotEmpty().WithMessage("TeoriNavn is required.")
                .MaximumLength(100).WithMessage("TeoriNavn must be less than 100 characters.");

            RuleFor(x => x.TeoriBeskrivelse)
                .MaximumLength(500).WithMessage("TeoriBeskrivelse must be less than 500 characters.");

            RuleFor(x => x.PensumID)
                .NotEmpty().WithMessage("PensumID is required.");
        }
    }
}
