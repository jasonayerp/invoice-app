using FluentValidation;

namespace Invoice.Domains.Common.Validators;

public class InvoiceItemValidator : AbstractValidator<InvoiceItemModel>
{
    public InvoiceItemValidator(ValidationMode mode)
    {
        RuleFor(e => e.InvoiceId).NotNull().GreaterThan(0);
        RuleFor(e => e.Description).NotNull().NotEmpty().MaximumLength(50);
        RuleFor(e => e.Quantity).NotNull().GreaterThan(0);
        RuleFor(e => e.Amount).NotNull().GreaterThan(0);

        // Conditional
        When(e => mode == ValidationMode.Update || mode == ValidationMode.Delete, () =>
        {
            RuleFor(e => e.Id).NotNull().GreaterThan(0);
        });
    }
}
