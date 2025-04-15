using FluentValidation;
using TaekwondoApp.Shared.DTO;

namespace TaekwondoOrchestration.ApiService.Validators
{
    public class OrdbogDTOValidator : AbstractValidator<OrdbogDTO>
    {
        public OrdbogDTOValidator()
        {
            RuleFor(x => x.DanskOrd)
                .NotEmpty().WithMessage("DanskOrd is required.")
                .MaximumLength(100).WithMessage("DanskOrd must be less than 100 characters.");

            RuleFor(x => x.KoranskOrd)
                .NotEmpty().WithMessage("KoranskOrd is required.")
                .MaximumLength(100).WithMessage("KoranskOrd must be less than 100 characters.");

            RuleFor(x => x.Beskrivelse)
                .MaximumLength(500).WithMessage("Beskrivelse must be less than 500 characters.");

            RuleFor(x => x.BilledeLink)
                .MaximumLength(200).WithMessage("BilledeLink must be less than 200 characters.")
                .When(x => !string.IsNullOrWhiteSpace(x.BilledeLink));

            RuleFor(x => x.LydLink)
                .MaximumLength(200).WithMessage("LydLink must be less than 200 characters.")
                .When(x => !string.IsNullOrWhiteSpace(x.LydLink));

            RuleFor(x => x.VideoLink)
                .MaximumLength(200).WithMessage("VideoLink must be less than 200 characters.")
                .When(x => !string.IsNullOrWhiteSpace(x.VideoLink));
        }
    }
}
