using Invoice.Domains.Common.Models;

namespace Invoice.Web.Domains.Common.Repositories;

public interface IInvoiceRepository
{
    Task<List<InvoiceModel>> GetAllAsync();
    Task<List<InvoiceModel>> GetPaginatedAsync(int page, int pageSize);
    Task<List<InvoiceModel>> GetTopAsync(int count);
    Task<InvoiceModel?> GetByIdAsync(int id);
    Task<bool> ExistsAsync();
    Task<int> CountAsync();
    Task<InvoiceModel> CreateAsync(InvoiceModel model);
    Task<InvoiceModel> UpdateAsync(InvoiceModel model);
    Task DeleteAsync(int id);
}

internal class InvoiceRepository
{
}
