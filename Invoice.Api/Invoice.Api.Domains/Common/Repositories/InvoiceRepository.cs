using Invoice.Api.Domains.Common.Mappers;
using Invoice.Api.Domains.Metadata;
using Invoice.Extensions.Linq;

namespace Invoice.Api.Domains.Common.Repositories;

public interface IInvoiceRepository : IRepository<InvoiceModel>
{
    Task<List<InvoiceModel>> GetAllByStatusAsync(InvoiceStatus[] invoiceStatuses);
    Task<InvoiceModel?> GetByIdAsync(int id);
}

public class InvoiceRepository : IInvoiceRepository
{
    private bool _ignoreQueryFilters = false;
    private readonly IDbContextFactory<SqlServerDbContext> _factory;

    public InvoiceRepository(IDbContextFactory<SqlServerDbContext> factory)
    {
        _factory = factory;
    }

    public async Task<InvoiceModel> AddAsync(InvoiceModel model)
    {
        using (var context = _factory.CreateDbContext())
        {
            var data = Map(model);

            await context.Invoices.AddAsync(data);

            await context.SaveChangesAsync();

            return Map(data);
        }
    }

    public async Task<List<InvoiceModel>> AddRangeAsync(IEnumerable<InvoiceModel> models)
    {
        using(var context = _factory.CreateDbContext())
        {
            var data = models.Select(Map);

            await context.Invoices.AddRangeAsync(data);

            await context.SaveChangesAsync();

            return data.Select(Map).ToList();
        }
    }

    public async Task<int> CountAsync(Expression<Func<InvoiceModel, bool>>? predicateExpression = null)
    {
        using (var context = _factory.CreateDbContext())
        {
            return await context.Invoices.CountAsync();
        }
    }

    public async Task DeleteAsync(InvoiceModel model)
    {
        using (var context = _factory.CreateDbContext())
        {
            var data = await context.Invoices.FirstOrDefaultAsync(e => e.Id == model.Id);

            if (data != null)
            {
                context.Invoices.Remove(data);

                await context.SaveChangesAsync();
            }
        }
    }

    public async Task DeleteRangeAsync(IEnumerable<InvoiceModel> models)
    {
        using (var context = _factory.CreateDbContext())
        {
            context.Invoices.RemoveRange(await context.Invoices.Where(e => models.Select(x => x.Id).Contains(e.Id)).ToListAsync());

            await context.SaveChangesAsync();
        }
    }

    public async Task<bool> ExistsAsync(Expression<Func<InvoiceModel, bool>>? predicateExpression = null)
    {
        using (var context = _factory.CreateDbContext())
        {
            return await context.Invoices.AnyAsync();
        }
    }

    public async Task<InvoiceModel?> FirstOrDefault(Expression<Func<InvoiceModel, bool>>? predicateExpression = null)
    {
        using (var context = _factory.CreateDbContext())
        {
            var result = await context.Invoices.FirstOrDefaultAsync();

            return result != null ? Map(result) : null;
        }
    }

    public async Task<List<InvoiceModel>> GetAllByStatusAsync(InvoiceStatus[] invoiceStatuses)
    {
        using (var context = _factory.CreateDbContext())
        {
            var result = await context.Invoices.Where(e => invoiceStatuses.Select(x => (int)x).Contains(e.Status)).ToListAsync();

            return result.Select(Map).ToList();
        }
    }

    public async Task<InvoiceModel?> GetByIdAsync(int id)
    {
        using (var context = _factory.CreateDbContext())
        {
            var result = await context.Invoices.SingleOrDefaultAsync(e => e.Id == id);

            return result != null ? Map(result) : null;
        }
    }

    public void IgnoreQueryFilters()
    {
        _ignoreQueryFilters = true;
    }

    public async Task<List<InvoiceModel>> ToListAsync(Expression<Func<InvoiceModel, bool>>? predicateExpression = null)
    {
        using (var context = _factory.CreateDbContext())
        {
            var result = await context.Invoices.ToListAsync();

            return result.Select(Map).ToList();
        }
    }

    public async Task<List<InvoiceModel>> ToListAsync(int page, int pageSize, Expression<Func<InvoiceModel, bool>>? predicateExpression = null)
    {
        using (var context = _factory.CreateDbContext())
        {
            var result = await context.Invoices.Paginate(page, pageSize).ToListAsync();

            return result.Select(Map).ToList();
        }
    }

    public async Task<List<InvoiceModel>> ToListAsync(int count, Expression<Func<InvoiceModel, bool>>? predicateExpression = null)
    {
        using (var context = _factory.CreateDbContext())
        {
            var result = await context.Invoices.Take(count).ToListAsync();

            return result.Select(Map).ToList();
        }
    }

    public async Task UpdateAsync(InvoiceModel model)
    {
        using (var context = _factory.CreateDbContext())
        {
            var entity = await context.Invoices.SingleOrDefaultAsync(e => e.Id == model.Id);

            if (entity != null)
            {
                entity.ClientId = model.ClientId;
                entity.Number = model.Number;
                entity.Description = model.Description;
                entity.Date = model.Date;
                entity.DueDate = model.DueDate;
                entity.Status = (int)model.Status;
                entity.PaymentTermDays = model.PaymentTermDays;
                entity.Total = model.Total;
                entity.CreatedAt = model.CreatedAt;
                entity.UpdatedAt = model.UpdatedAt;
                entity.DeletedAt = model.DeletedAt;

                await context.SaveChangesAsync();
            }
        }
    }

    public async Task UpdateRangeAsync(IEnumerable<InvoiceModel> models)
    {
        using (var context = _factory.CreateDbContext())
        {
            var entities = await context.Invoices.Where(e => models.Select(x => x.Id).Contains(e.Id)).ToListAsync();

            entities.ForEach(entity =>
            {
                var model = models.Single(e => e.Id == entity.Id);

                entity.ClientId = model.ClientId;
                entity.Number = model.Number;
                entity.Description = model.Description;
                entity.Date = model.Date;
                entity.DueDate = model.DueDate;
                entity.Status = (int)model.Status;
                entity.PaymentTermDays = model.PaymentTermDays;
                entity.Total = model.Total;
                entity.CreatedAt = model.CreatedAt;
                entity.UpdatedAt = model.UpdatedAt;
                entity.DeletedAt = model.DeletedAt;
            });

            await context.SaveChangesAsync();
        }
    }

    private InvoiceModel Map(InvoiceEntity entity)
    {
        var mapper = new InvoiceMapper();

        return mapper.Map<InvoiceModel>(entity);
    }

    private InvoiceEntity Map(InvoiceModel model)
    {
        var mapper = new InvoiceMapper();

        return mapper.Map<InvoiceEntity>(model);
    }
}
