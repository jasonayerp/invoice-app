using FluentValidation;

namespace Invoice.Domains.Common.Validators;

public class InvoiceValidator : AbstractValidator<InvoiceModel>
{
    public InvoiceValidator(ValidationMode validationMode)
    {
        When(e => validationMode == ValidationMode.Update || validationMode == ValidationMode.Delete, () =>
        {
            RuleFor(e => e.Id).NotNull().GreaterThan(0);
            
            When(e => validationMode == ValidationMode.Add || validationMode == ValidationMode.Update, () =>
            {
                RuleFor(e => e.Guid).NotEmpty ();
                RuleFor(e => e.Number).NotEmpty().MaximumLength(8);
                RuleFor(e => e.Description).NotEmpty().MaximumLength(128);
                RuleFor(e => e.Date).NotNull().GreaterThanOrEqualTo(DateTime.MinValue).LessThanOrEqualTo(DateTime.MaxValue);
                RuleFor(e => e.DueDate).NotNull().GreaterThan(e => e.Date);
                RuleForEach(e => e.InvoiceItems).Configure(e => new InvoiceItemValidator(validationMode));
            });
        });
    }
}
