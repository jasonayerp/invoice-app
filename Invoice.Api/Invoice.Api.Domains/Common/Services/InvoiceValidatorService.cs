using Invoice.Api.Domains.Common.Repositories;
using Invoice.Domains.Common.Validators;
using Invoice.Validation;

namespace Invoice.Api.Domains.Common.Services;

public interface IInvoiceValidatorService
{
    Task ValidateAsync(InvoiceModel invoice);
}

public class InvoiceValidatorService : IInvoiceValidatorService
{
    private readonly IInvoiceRepository _invoiceRepository;
    private readonly IInvoiceItemRepository _invoiceItemRepository;

    public InvoiceValidatorService(IInvoiceRepository invoiceRepository, IInvoiceItemRepository invoiceItemRepository)
    {
        _invoiceRepository = invoiceRepository;
        _invoiceItemRepository = invoiceItemRepository;
    }

    public async Task ValidateAsync(InvoiceModel invoice)
    {
        var validator = new InvoiceValidator(invoice.Id > 0 ? ValidationMode.Update : ValidationMode.Add);

        await validator.ValidateAndThrowAsync(invoice);

        if (await _invoiceRepository.ExistsAsync(e => e.ClientId == invoice.ClientId && e.Number == invoice.Number))
            throw new Exception($"Cannot create duplicate invoice with for ClientId '{invoice.ClientId}' with Number '{invoice.Number}'");

        if (invoice.InvoiceItems.Any(e => e.InvoiceId != invoice.Id))
        {
            throw new Exception($"InvoiceItem must belong to Invoice with InvoiceId '{invoice.Id}'");
        }

        if (await _invoiceItemRepository.ExistsAsync(e => e.InvoiceId == invoice.Id && invoice.InvoiceItems.Select(x => x.Description).Contains(e.Description)))
        {
            var invoiceItems = await _invoiceItemRepository.GetAllByInvoiceIdAsync(invoice.Id);

            var invoiceItem = invoiceItems.Where(e => e.Id == invoice.Id && e.Description == invoice.Description).First();

            throw new Exception($"Cannot create new InvoiceItem for InvoiceId '{invoice.Id}' with Description '{invoiceItem.Description}'");
        }
    }
}
