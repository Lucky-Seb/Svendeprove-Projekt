using FluentValidation;
using TaekwondoApp.Shared.DTO;

namespace TaekwondoOrchestration.ApiService.Validators
{
    public class RegisterDTOValidator : AbstractValidator<RegisterDTO>
    {
        public RegisterDTOValidator()
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

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Password is required.")
                .MinimumLength(8).WithMessage("Password must be at least 8 characters.")
                .MaximumLength(256).WithMessage("Password must be less than 256 characters.")
                .Must(HasThreeOfFourCharacterTypes).WithMessage("Password must contain at least three of the following: uppercase letter, lowercase letter, number, and special character.");

            RuleFor(x => x.ConfirmPassword)
                .NotEmpty().WithMessage("Password is required.")
                .MinimumLength(8).WithMessage("Password must be at least 8 characters.")
                .MaximumLength(256).WithMessage("Password must be less than 256 characters.")
                .Must(HasThreeOfFourCharacterTypes).WithMessage("Password must contain at least three of the following: uppercase letter, lowercase letter, number, and special character.");

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

        private bool HasThreeOfFourCharacterTypes(string password)
        {
            if (string.IsNullOrEmpty(password))
                return false;

            int types = 0;

            if (password.Any(char.IsUpper))
                types++;
            if (password.Any(char.IsLower))
                types++;
            if (password.Any(char.IsDigit))
                types++;
            if (password.Any(c => !char.IsLetterOrDigit(c)))
                types++;

            return types >= 3;
        }
    }
}
