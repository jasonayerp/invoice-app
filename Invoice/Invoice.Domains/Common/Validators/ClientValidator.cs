using FluentValidation;

namespace Invoice.Domains.Common.Validators;

public class ClientValidator : AbstractValidator<ClientModel>
{
    public ClientValidator(ValidationMode validationMode)
    {
        When(e => validationMode == ValidationMode.Update || validationMode == ValidationMode.Delete, () =>
        {
            RuleFor(e => e.Id).GreaterThan(0);
            RuleFor(e => e.Guid).NotEmpty();
            When(e => validationMode == ValidationMode.Add || validationMode == ValidationMode.Update, () =>
            {
                RuleFor(e => e.Name).NotEmpty().MaximumLength(128);
                RuleFor(e => e.Addresses).NotEmpty();
                When(e => e.Email != null, () =>
                {
                    RuleFor(e => e.Email).NotEmpty().MaximumLength(128);
                });
                RuleForEach(e => e.Addresses).SetValidator(new ClientAddressValidator(validationMode));
            });
        });
    }
}
