
using Invoice.Api.Domains.Common.Repositories;
using Invoice.Services;

namespace Invoice.Api.Domains.Common.Services;

public interface IInvoiceService
{
    Task<InvoiceModel> SaveAsync(InvoiceModel invoice);
}

public class InvoiceService : IInvoiceService
{
    private readonly IDateTimeOffsetService _dateTimeOfffsetService;
    private readonly IInvoiceValidatorService _invoiceValidatorService;
    private readonly IInvoiceRepository _invoiceRepository;
    private readonly IInvoiceItemRepository _invoiceItemRepository;

    public InvoiceService(IDateTimeOffsetService dateTimeOffsetService, IInvoiceValidatorService invoiceValidatorService, IInvoiceRepository invoiceRepository, IInvoiceItemRepository invoiceItemRepository)
    {
        _dateTimeOfffsetService = dateTimeOffsetService;
        _invoiceValidatorService = invoiceValidatorService;
        _invoiceRepository = invoiceRepository;
        _invoiceItemRepository = invoiceItemRepository;
    }

    public async Task<InvoiceModel> SaveAsync(InvoiceModel invoice)
    {
        await _invoiceValidatorService.ValidateAsync(invoice);

        if (invoice.Id > 0)
        {
            var invoiceToSave = new InvoiceModel
            {
                ClientId = invoice.ClientId,
                Number = invoice.Number,
                Description = invoice.Description,
                Date = invoice.Date,
                DueDate = invoice.DueDate,
                Total = invoice.InvoiceItems.Sum(e => e.TotalPrice),
                Status = invoice.Status,
                PaymentTermDays = invoice.PaymentTermDays,
                InvoiceItems = invoice.InvoiceItems.Select(invoiceItem => new InvoiceItemModel
                {
                    InvoiceId = invoiceItem.Id,
                    Description = invoiceItem.Description,
                    Quantity = invoiceItem.Quantity,
                    UnitPrice = invoiceItem.UnitPrice,
                    CreatedAt = _dateTimeOfffsetService.UtcNow
                }).ToList(),
                CreatedAt = _dateTimeOfffsetService.UtcNow
            };

            var data = await _invoiceRepository.AddAsync(invoiceToSave);

            return data;
        }
        else
        {
            var invoiceToSave = new InvoiceModel
            {
                ClientId = invoice.ClientId,
                Number = invoice.Number,
                Description = invoice.Description,
                Date = invoice.Date,
                DueDate = invoice.DueDate,
                Total = invoice.InvoiceItems.Sum(e => e.TotalPrice),
                Status = invoice.Status,
                PaymentTermDays = invoice.PaymentTermDays,
                InvoiceItems = invoice.InvoiceItems.Where(e => e.Id > 0).Select(invoiceItem => new InvoiceItemModel
                {
                    InvoiceId = invoiceItem.Id,
                    Description = invoiceItem.Description,
                    Quantity = invoiceItem.Quantity,
                    UnitPrice = invoiceItem.UnitPrice,
                    CreatedAt = _dateTimeOfffsetService.UtcNow
                }).ToList(),
                CreatedAt = invoice.CreatedAt,
                UpdatedAt = _dateTimeOfffsetService.UtcNow
            };

            var newInvoiceItems = invoice.InvoiceItems.Where(e => e.Id < 1).Select(invoiceItem => new InvoiceItemModel
            {
                InvoiceId = invoiceItem.Id,
                Description = invoiceItem.Description,
                Quantity = invoiceItem.Quantity,
                UnitPrice = invoiceItem.UnitPrice,
                CreatedAt = invoiceItem.CreatedAt,
                UpdatedAt = _dateTimeOfffsetService.UtcNow
            }).ToList();

            var data = await _invoiceItemRepository.AddRangeAsync(newInvoiceItems);

            invoice.InvoiceItems.AddRange(data);

            return invoice;
        }
    }
}
