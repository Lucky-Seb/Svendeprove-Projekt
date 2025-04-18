using FluentValidation;
using TaekwondoApp.Shared.DTO;

namespace TaekwondoOrchestration.ApiService.Validators
{
    public class TeknikDTOValidator : AbstractValidator<TeknikDTO>
    {
        public TeknikDTOValidator()
        {
            RuleFor(x => x.TeknikNavn)
                .NotEmpty().WithMessage("TeknikNavn is required.")
                .MaximumLength(100).WithMessage("TeknikNavn must be less than 100 characters.");

            RuleFor(x => x.TeknikBeskrivelse)
                .MaximumLength(500).WithMessage("TeknikBeskrivelse must be less than 500 characters.");

            RuleFor(x => x.PensumID)
                .NotEmpty().WithMessage("PensumID is required.");
        }
    }
}
