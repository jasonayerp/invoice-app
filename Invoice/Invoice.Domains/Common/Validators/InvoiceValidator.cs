using FluentValidation;

namespace Invoice.Domains.Common.Validators;

public class InvoiceValidator : AbstractValidator<InvoiceModel>
{
    public InvoiceValidator(ValidationMode mode)
    {
        RuleFor(e => e.Number).NotNull().NotEmpty().MaximumLength(8);
        RuleFor(e => e.UtcDate).NotNull().GreaterThanOrEqualTo(DateTime.MinValue);
        RuleFor(e => e.Amount).NotNull().GreaterThan(0);

        // Conditional
        When(e => mode == ValidationMode.Update || mode == ValidationMode.Delete, () =>
        {
            RuleFor(e => e.Id).NotNull().GreaterThan(0);
            RuleFor(e => e.Guid).NotNull().NotEqual(Guid.Empty);
        });
        When(e => e.Description != null, () =>
        {
            RuleFor(e => e.Description).NotNull().NotEmpty().MaximumLength(128);
        });
    }
}
