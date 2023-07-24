using Invoice.Api.Domains.Metadata;

namespace Invoice.Api.Domains.Common.Repositories;

public interface IInvoiceItemRepository : IRepository<InvoiceItemModel>
{
    Task<List<InvoiceItemModel>> GetAllByInvoiceIdAsync(int invoiceId);
    Task<InvoiceItemModel?> GetByIdAsync(int id);
}

public class InvoiceItemRepository : IInvoiceItemRepository
{
    private bool _ignoreQueryFilters = false;
    private readonly IDbContextFactory<SqlServerDbContext> _factory;

    public InvoiceItemRepository(IDbContextFactory<SqlServerDbContext> factory)
    {
        _factory = factory;
    }

    public async Task<InvoiceItemModel> AddAsync(InvoiceItemModel model)
    {
        throw new NotImplementedException();
    }

    public async Task<List<InvoiceItemModel>> AddRangeAsync(IEnumerable<InvoiceItemModel> models)
    {
        throw new NotImplementedException();
    }

    public async Task<int> CountAsync(Expression<Func<InvoiceItemModel, bool>>? predicateExpression = null)
    {
        throw new NotImplementedException();
    }

    public async Task DeleteAsync(InvoiceItemModel model)
    {
        throw new NotImplementedException();
    }

    public async Task DeleteRangeAsync(IEnumerable<InvoiceItemModel> models)
    {
        throw new NotImplementedException();
    }

    public async Task<bool> ExistsAsync(Expression<Func<InvoiceItemModel, bool>>? predicateExpression = null)
    {
        throw new NotImplementedException();
    }

    public async Task<InvoiceItemModel?> FirstOrDefault(Expression<Func<InvoiceItemModel, bool>>? predicateExpression = null)
    {
        throw new NotImplementedException();
    }

    public async Task<List<InvoiceItemModel>> GetAllByInvoiceIdAsync(int invoiceId)
    {
        throw new NotImplementedException();
    }

    public async Task<InvoiceItemModel?> GetByIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    public void IgnoreQueryFilters()
    {
        _ignoreQueryFilters = true;
    }

    public async Task<List<InvoiceItemModel>> ToListAsync(Expression<Func<InvoiceItemModel, bool>>? predicateExpression = null)
    {
        throw new NotImplementedException();
    }

    public async Task<List<InvoiceItemModel>> ToListAsync(int page, int pageSize, Expression<Func<InvoiceItemModel, bool>>? predicateExpression = null)
    {
        throw new NotImplementedException();
    }

    public async Task<List<InvoiceItemModel>> ToListAsync(int count, Expression<Func<InvoiceItemModel, bool>>? predicateExpression = null)
    {
        throw new NotImplementedException();
    }

    public async Task UpdateAsync(InvoiceItemModel model)
    {
        throw new NotImplementedException();
    }

    public async Task UpdateRangeAsync(IEnumerable<InvoiceItemModel> models)
    {
        throw new NotImplementedException();
    }
}
