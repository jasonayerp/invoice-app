﻿using FluentValidation;

namespace Invoice.Domains.Common.Validators;

public class ClientAddressValidator : AbstractValidator<ClientAddressModel>
{
    public ClientAddressValidator(ValidationMode validationMode)
    {
        When(e => validationMode == ValidationMode.Update || validationMode == ValidationMode.Delete, () =>
        {
            RuleFor(e => e.Id).GreaterThan(0);
            When(e => validationMode == ValidationMode.Add || validationMode == ValidationMode.Update, () =>
            {
                RuleFor(e => e.ClientId).GreaterThan(0);
                RuleFor(e => e.Line1).NotEmpty().MaximumLength(64);
                RuleFor(e => e.City).NotEmpty().MaximumLength(64);
                RuleFor(e => e.PostalCode).NotEmpty().MaximumLength(64);
                RuleFor(e => e.CountryCode).IsInEnum();
                When(e => e.Line2 != null, () =>
                {
                    RuleFor(e => e.Line2).NotEmpty().MaximumLength(64);
                });
                When(e => e.Line3 != null, () =>
                {
                    RuleFor(e => e.Line3).NotEmpty().MaximumLength(64);
                });
                When(e => e.Line4 != null, () =>
                {
                    RuleFor(e => e.Line4).NotEmpty().MaximumLength(64);
                });
                When(e => e.Region != null, () =>
                {
                    RuleFor(e => e.Region).NotEmpty().MaximumLength(64);
                });
            });
        });
    }
}
