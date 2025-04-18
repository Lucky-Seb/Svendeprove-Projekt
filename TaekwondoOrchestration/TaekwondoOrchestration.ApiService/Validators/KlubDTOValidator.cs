using FluentValidation;
using TaekwondoApp.Shared.DTO;

namespace TaekwondoOrchestration.ApiService.Validators
{
    public class KlubDTOValidator : AbstractValidator<KlubDTO>
    {
        public KlubDTOValidator()
        {
            RuleFor(x => x.KlubID)
                .NotEmpty().WithMessage("KlubID is required.");

            RuleFor(x => x.KlubNavn)
                .NotEmpty().WithMessage("KlubNavn is required.")
                .MaximumLength(100).WithMessage("KlubNavn must be less than 100 characters.");
        }
    }
}
