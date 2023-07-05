using FluentValidation;

namespace Invoice.Domains.Common.Validators;

public class ClientValidator : AbstractValidator<ClientModel>
{
    public ClientValidator(ValidationMode mode = ValidationMode.Add)
    {
        RuleFor(e => e.Name).NotNull().NotEmpty();

        // Conditional
        When(e => mode == ValidationMode.Update || mode == ValidationMode.Delete, () =>
        {
            RuleFor(e => e.Id).NotNull().GreaterThan(0);
            RuleFor(e => e.Guid).NotNull().NotEqual(Guid.Empty);
        });
        When(e => e.Email != null, () =>
        {
            RuleFor(e => e.Email).NotEmpty().MaximumLength(128);
        });
    }
}
