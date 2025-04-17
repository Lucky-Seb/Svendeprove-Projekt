using FluentValidation;
using TaekwondoApp.Shared.DTO;

namespace TaekwondoOrchestration.ApiService.Validators
{
    public class BrugerDTOValidator : AbstractValidator<BrugerDTO>
    {
        public BrugerDTOValidator()
        {
            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email is required.")
                .EmailAddress().WithMessage("Invalid email format.")
                .MaximumLength(100).WithMessage("Email must be less than 100 characters.");

            RuleFor(x => x.Brugernavn)
                .NotEmpty().WithMessage("Brugernavn is required.")
                .MaximumLength(50).WithMessage("Brugernavn must be less than 50 characters.");

            RuleFor(x => x.Fornavn)
                .NotEmpty().WithMessage("Fornavn is required.")
                .MaximumLength(50).WithMessage("Fornavn must be less than 50 characters.");

            RuleFor(x => x.Efternavn)
                .NotEmpty().WithMessage("Efternavn is required.")
                .MaximumLength(50).WithMessage("Efternavn must be less than 50 characters.");

            RuleFor(x => x.Brugerkode)
                .NotEmpty().WithMessage("Brugerkode is required.")
                .MinimumLength(6).WithMessage("Brugerkode must be at least 6 characters long.")
                .MaximumLength(100).WithMessage("Brugerkode must be less than 100 characters.");

            RuleFor(x => x.Address)
                .MaximumLength(200).WithMessage("Address must be less than 200 characters.")
                .When(x => !string.IsNullOrWhiteSpace(x.Address));

            RuleFor(x => x.Bæltegrad)
                .MaximumLength(50).WithMessage("Bæltegrad must be less than 50 characters.")
                .When(x => !string.IsNullOrWhiteSpace(x.Bæltegrad));

            RuleFor(x => x.Role)
                .NotEmpty().WithMessage("Role is required.")
                .MaximumLength(50).WithMessage("Role must be less than 50 characters.");
        }
    }
}
