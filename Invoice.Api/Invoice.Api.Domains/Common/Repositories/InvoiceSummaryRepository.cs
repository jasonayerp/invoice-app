using Invoice.Api.Data.Entities;
using Invoice.Api.Data.SqlServer;
using Invoice.Api.Domains.Common.Mappers;
using Invoice.Domains.Common.Models;
using Invoice.Extensions.Linq;

namespace Invoice.Api.Domains.Common.Repositories;

public interface IInvoiceSummaryRepository
{
    void IgnoreQueryFilters();
    Task<List<InvoiceSummaryModel>> ToListAsync();
    Task<List<InvoiceSummaryModel>> ToListAsync(int page, int pageNumber);
    Task<List<InvoiceSummaryModel>> ToListAsync(int count);
    Task<InvoiceSummaryModel?> GetByIdAsync(int id);
    Task<bool> ExistsAsync();
    Task<int> CountAsync();
}

public class InvoiceSummaryRepository : IInvoiceSummaryRepository
{
    private readonly IDbContextFactory<SqlServerDbContext> _factory;
    private readonly IMapper _mapper;
    private bool _ignoreQueryFilters = false;

    public InvoiceSummaryRepository(IDbContextFactory<SqlServerDbContext> factory, IMapper maper)
    {
        _factory = factory;
        _mapper = maper;
    }

    private IQueryable<VwInvoiceSummaryEntity> Filter(IQueryable<VwInvoiceSummaryEntity> source)
    {
        return _ignoreQueryFilters ? source.IgnoreQueryFilters() : source;
    }

    public async Task<int> CountAsync()
    {
        using (var context = _factory.CreateDbContext())
        {
            return await context.InvoiceSummaries.CountAsync();
        }
    }

    public async Task<bool> ExistsAsync()
    {
        using (var context = _factory.CreateDbContext())
        {
            return await context.InvoiceSummaries.AnyAsync();
        }
    }

    public async Task<InvoiceSummaryModel?> GetByIdAsync(int id)
    {
        using (var context = _factory.CreateDbContext())
        {
            var result = _ignoreQueryFilters
                ? await context.InvoiceSummaries.IgnoreQueryFilters().SingleOrDefaultAsync(invoiceSummary => invoiceSummary.InvoiceId == id)
                : await context.InvoiceSummaries.SingleOrDefaultAsync(invoiceSummary => invoiceSummary.InvoiceId == id);

            return result != null ? Map(result) : null;
        }
    }

    public void IgnoreQueryFilters()
    {
        _ignoreQueryFilters = true;
    }

    public async Task<List<InvoiceSummaryModel>> ToListAsync()
    {
        using (var context = _factory.CreateDbContext())
        {
            var query = Filter(context.InvoiceSummaries.AsQueryable());

            var result = await query.ToListAsync();

            return result.Select(Map).ToList();
        }
    }

    public async Task<List<InvoiceSummaryModel>> ToListAsync(int page, int pageNumber)
    {
        using (var context = _factory.CreateDbContext())
        {
            var query = Filter(context.InvoiceSummaries.AsQueryable());

            var result = await query.Paginate(page, pageNumber).ToListAsync();

            return result.Select(Map).ToList();
        }
    }

    public async Task<List<InvoiceSummaryModel>> ToListAsync(int count)
    {
        using (var context = _factory.CreateDbContext())
        {
            var query = Filter(context.InvoiceSummaries.AsQueryable());

            var result = await query.Take(count).ToListAsync();

            return result.Select(Map).ToList();
        }
    }

    private InvoiceSummaryModel Map(VwInvoiceSummaryEntity entity)
    {
        var model = _mapper.Map<InvoiceSummaryModel>(entity);
        model.Id = entity.InvoiceId;
        return model;
    }
}
