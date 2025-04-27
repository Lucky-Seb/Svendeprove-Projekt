using FluentValidation;
using TaekwondoApp.Shared.DTO;

namespace TaekwondoOrchestration.ApiService.Validators
{
    public class ØvelseDTOValidator : AbstractValidator<ØvelseDTO>
    {
        public ØvelseDTOValidator()
        {
            RuleFor(x => x.ØvelseNavn)
                .NotEmpty().WithMessage("ØvelseNavn is required.")
                .MaximumLength(100).WithMessage("ØvelseNavn must be less than 100 characters.");

            RuleFor(x => x.ØvelseBeskrivelse)
                .MaximumLength(500).WithMessage("ØvelseBeskrivelse must be less than 500 characters.");

            RuleFor(x => x.ØvelseSværhed)
                .NotEmpty().WithMessage("ØvelseSværhed is required.")
                .Must(s => s == "Let" || s == "Middel" || s == "Svær")
                .WithMessage("ØvelseSværhed must be one of the following values: Let, Middel, or Svær.");

            RuleFor(x => x.PensumID)
                .NotEmpty().WithMessage("PensumID is required.")
                .Must(id => id != Guid.Empty).WithMessage("PensumID must be a valid GUID.");

            RuleFor(x => x.BrugerID)
                .Must(id => !id.HasValue || id.Value != Guid.Empty).WithMessage("BrugerID must be a valid GUID if provided.");

            RuleFor(x => x.KlubID)
                .Must(id => !id.HasValue || id.Value != Guid.Empty).WithMessage("KlubID must be a valid GUID if provided.");

            RuleFor(x => x.ØvelseBillede)
                .MaximumLength(200).WithMessage("ØvelseBillede must be less than 200 characters.")
                .When(x => !string.IsNullOrWhiteSpace(x.ØvelseBillede));

            RuleFor(x => x.ØvelseVideo)
                .MaximumLength(200).WithMessage("ØvelseVideo must be less than 200 characters.")
                .When(x => !string.IsNullOrWhiteSpace(x.ØvelseVideo));

            RuleFor(x => x.ØvelseTid)
                .GreaterThan(0).WithMessage("ØvelseTid must be greater than 0.");
        }
    }
}
