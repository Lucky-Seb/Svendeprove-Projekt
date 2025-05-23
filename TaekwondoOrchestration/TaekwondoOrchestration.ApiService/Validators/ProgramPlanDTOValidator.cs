﻿using FluentValidation;
using TaekwondoApp.Shared.DTO;
using System;

namespace TaekwondoOrchestration.ApiService.Validators
{
    public class ProgramPlanDTOValidator : AbstractValidator<ProgramPlanDTO>
    {
        public ProgramPlanDTOValidator()
        {
            RuleFor(x => x.ProgramID)
                .NotEmpty().WithMessage("ProgramID is required.");

            RuleFor(x => x.ProgramNavn)
                .NotEmpty().WithMessage("ProgramNavn is required.")
                .MaximumLength(100).WithMessage("ProgramNavn must be less than 100 characters.");

            RuleFor(x => x.OprettelseDato)
                .NotEmpty().WithMessage("OprettelseDato is required.")
                .LessThan(DateTime.Now).WithMessage("OprettelseDato cannot be in the future.");

            RuleFor(x => x.Længde)
                .GreaterThan(0).WithMessage("Længde must be greater than 0.")
                .LessThan(1000).WithMessage("Længde must be less than 1000.");

            RuleFor(x => x.Beskrivelse)
                .MaximumLength(500).WithMessage("Beskrivelse must be less than 500 characters.");

            RuleFor(x => x.BrugerID)
                .NotEqual(Guid.Empty).WithMessage("BrugerID is required.")
                .When(x => x.BrugerID != Guid.Empty);  // Validate only if BrugerID is not Guid.Empty

            RuleFor(x => x.KlubID)
                .NotEqual(Guid.Empty).WithMessage("KlubID is required.")
                .When(x => x.KlubID != Guid.Empty);  // Validate only if KlubID is not Guid.Empty

            RuleFor(x => x.Træninger)
                .NotNull().WithMessage("Træninger cannot be null.")
                .Must(x => x.Count > 0).WithMessage("Træninger must contain at least one item.");
        }
    }
}
