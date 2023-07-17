using FluentValidation;

namespace Invoice.Domains.Common.Validators;

public class InvoiceValidator : AbstractValidator<InvoiceModel>
{
    public InvoiceValidator(ValidationMode mode)
    {
        When(e => mode == ValidationMode.Update || mode == ValidationMode.Delete, () =>
        {
            RuleFor(e => e.Id).NotNull().GreaterThan(0);
            RuleFor(e => e.Guid).NotNull().NotEqual(Guid.Empty);
            When(e => mode == ValidationMode.Add || mode == ValidationMode.Update, () =>
            {
                RuleFor(e => e.Number).NotEmpty().MaximumLength(8);
                RuleFor(e => e.Description).NotEmpty().MaximumLength(128);
                RuleFor(e => e.UtcDate).NotNull().GreaterThanOrEqualTo(DateTime.MinValue);
                RuleFor(e => e.UtcDueDate).NotNull().GreaterThanOrEqualTo(e => e.UtcDate);
            });
        });
    }
}
