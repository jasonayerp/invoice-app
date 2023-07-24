using FluentValidation;

namespace Invoice.Domains.Common.Validators;

public class InvoiceItemValidator : AbstractValidator<InvoiceItemModel>
{
    public InvoiceItemValidator(ValidationMode validationMode)
    {
        When(e => validationMode == ValidationMode.Update || validationMode == ValidationMode.Delete, () =>
        {
            RuleFor(e => e.Id).GreaterThan(0);
            When(e => validationMode == ValidationMode.Add || validationMode == ValidationMode.Update, () =>
            {
                RuleFor(e => e.InvoiceId).GreaterThan(0);
                RuleFor(e => e.Description).NotEmpty().MaximumLength(50);
                RuleFor(e => e.Quantity).GreaterThan(0);
                RuleFor(e => e.UnitPrice).GreaterThan(0);
            });
        });
    }
}
