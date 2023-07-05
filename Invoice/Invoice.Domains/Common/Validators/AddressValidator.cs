using FluentValidation;

namespace Invoice.Domains.Common.Validators;

public class AddressValidator : AbstractValidator<AddressModel>
{
    public AddressValidator(ValidationMode mode)
    {
        RuleFor(e => e.AddressLine1).NotNull().NotEmpty().MaximumLength(64);
        RuleFor(e => e.City).NotNull().NotEmpty().MaximumLength(64);
        RuleFor(e => e.Region).NotNull().NotEmpty().MaximumLength(64);
        RuleFor(e => e.PostalCode).NotNull().NotEmpty().MaximumLength(64);
        RuleFor(e => e.CountryCode).NotNull().NotEmpty().Length(2);

        // Conditional
        When(e => mode == ValidationMode.Update || mode == ValidationMode.Delete, () =>
        {
            RuleFor(e => e.Id).NotNull().GreaterThan(0);
            RuleFor(e => e.Guid).NotNull().NotEqual(Guid.Empty);
        });
        When(e => e.AddressLine2 != null, () =>
        {
            RuleFor(e => e.AddressLine2).NotEmpty().MaximumLength(64);
        });
        When(e => e.AddressLine3 != null, () =>
        {
            RuleFor(e => e.AddressLine3).NotEmpty().MaximumLength(64);
        });
        When(e => e.AddressLine4 != null, () =>
        {
            RuleFor(e => e.AddressLine4).NotEmpty().MaximumLength(64);
        });
    }
}
