using FluentValidation;
using TaekwondoApp.Shared.DTO;

namespace TaekwondoOrchestration.ApiService.Validators
{
    public class LoginDTOValidator : AbstractValidator<LoginDTO>
    {
        public LoginDTOValidator()
        {
            RuleFor(x => x.EmailOrBrugernavn)
                .NotEmpty().WithMessage("Email or Brugernavn is required.");

            RuleFor(x => x.Brugerkode)
                .NotEmpty().WithMessage("Brugerkode is required.");
        }
    }
}
