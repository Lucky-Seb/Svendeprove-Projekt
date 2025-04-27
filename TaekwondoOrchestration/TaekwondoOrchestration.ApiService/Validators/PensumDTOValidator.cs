using FluentValidation;
using TaekwondoApp.Shared.DTO;

namespace TaekwondoOrchestration.ApiService.Validators
{
    public class PensumDTOValidator : AbstractValidator<PensumDTO>
    {
        public PensumDTOValidator()
        {
            RuleFor(p => p.PensumGrad).NotEmpty().WithMessage("Navn is required.");
            // Add other rules as needed
        }
    }

}
