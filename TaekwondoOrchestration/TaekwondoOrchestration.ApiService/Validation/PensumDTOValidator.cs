namespace TaekwondoOrchestration.ApiService.Validation
{
    using FluentValidation;
    using TaekwondoApp.Shared.DTO;

    public class PensumDTOValidator : AbstractValidator<PensumDTO>
    {
        public PensumDTOValidator()
        {
            RuleFor(p => p.Name).NotEmpty().WithMessage("Name is required.");
            RuleFor(p => p.Description).MaximumLength(500);
        }
    }

}
