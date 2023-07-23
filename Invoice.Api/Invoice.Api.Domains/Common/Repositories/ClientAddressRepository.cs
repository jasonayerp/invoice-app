using Invoice.Api.Domains.Common.Mappers;
using Invoice.Domains.Common.Models;

namespace Invoice.Api.Domains.Common.Repositories;

public interface IClientAddressRepository
{
    void IgnoreQueryFilters();
    Task<List<ClientAddressModel>> ToListAsync(Expression<Func<ClientAddressModel, bool>>? predicate = null);
    Task<List<ClientAddressModel>> ToListAsync(int page, int pageNumber);
    Task<List<ClientAddressModel>> ToListAsync(int count);
    Task<bool> ExistsAsync(Expression<Func<ClientAddressModel, bool>>? predicate = null);
    Task<int> CountAsync(Expression<Func<ClientAddressModel, bool>>? predicate = null);
    Task<ClientAddressModel?> GetByIdAsync(int id);
    Task<ClientAddressModel> AddAsync(ClientAddressModel clientAddress);
    Task<List<ClientAddressModel>> AddRangeAsync(IEnumerable<ClientAddressModel> clientAddresses);
    Task UpdateAsync(ClientAddressModel clientAddress);
    Task UpdateRangeAsync(IEnumerable<ClientAddressModel> clientAddresses);
    Task RemoveAsync(ClientAddressModel model);
    Task RemoveRangeAsync(IEnumerable<ClientAddressModel> clientAddresses);
}

internal sealed class SqlServerClientAddressRepository : IClientAddressRepository
{
    private readonly IDbContextFactory<SqlServerDbContext> _factory;
    private bool _ignoreQueryFilters = false;

    private IQueryable<ClientAddressEntity> SetQueryFilters(IQueryable<ClientAddressEntity> source, bool ignoreQueryFilters)
    {
        return ignoreQueryFilters ? source.IgnoreQueryFilters() : source;
    }

    public SqlServerClientAddressRepository(IDbContextFactory<SqlServerDbContext> factory)
    {
        _factory = factory;
    }

    public void IgnoreQueryFilters()
    {
        _ignoreQueryFilters = true;
    }

    public async Task<ClientAddressModel> AddAsync(ClientAddressModel clientAddress)
    {
        var data = Map(clientAddress);

        using (var context = _factory.CreateDbContext())
        {
            await context.ClientAddresses.AddAsync(data);

            await context.SaveChangesAsync();

            return Map(data);
        }
    }

    public async Task<List<ClientAddressModel>> AddRangeAsync(IEnumerable<ClientAddressModel> clientAddresses)
    {
        var data = clientAddresses.Select(Map);

        using (var context = _factory.CreateDbContext())
        {
            await context.ClientAddresses.AddRangeAsync(data);

            await context.SaveChangesAsync();

            return data.Select(Map).ToList();
        }
    }

    public async Task<int> CountAsync(Expression<Func<ClientAddressModel, bool>>? predicate = null)
    {
        using (var context = _factory.CreateDbContext())
        {
            var query = SetQueryFilters(context.ClientAddresses.AsQueryable(), _ignoreQueryFilters);

            return await query.CountAsync();
        }
    }

    public async Task<bool> ExistsAsync(Expression<Func<ClientAddressModel, bool>>? predicate = null)
    {
        using (var context = _factory.CreateDbContext())
        {
            var query = SetQueryFilters(context.ClientAddresses.AsQueryable(), _ignoreQueryFilters);

            return await query.AnyAsync();
        }
    }

    public async Task<ClientAddressModel?> GetByIdAsync(int id)
    {
        using (var context = _factory.CreateDbContext())
        {
            var data = _ignoreQueryFilters
                ? await context.ClientAddresses.IgnoreQueryFilters().SingleOrDefaultAsync(e => e.ClientAddressId == id)
                : await context.ClientAddresses.SingleOrDefaultAsync(e => e.ClientAddressId == id);

            return data != null ? Map(data) : null;
        }
    }

    public async Task RemoveAsync(ClientAddressModel model)
    {
        using (var context = _factory.CreateDbContext())
        {
            var data = await context.ClientAddresses.SingleOrDefaultAsync(e => e.ClientAddressId == model.Id);

            if (data != null)
            {
                context.ClientAddresses.Remove(data);

                await context.SaveChangesAsync();
            }
        }
    }

    public async Task RemoveRangeAsync(IEnumerable<ClientAddressModel> clientAddresses)
    {
        using (var context = _factory.CreateDbContext())
        {
            var data = await context.ClientAddresses.Where(e => clientAddresses.Select(clientAddress => clientAddress.Id).Contains(e.ClientAddressId)).ToListAsync();

            if (data.Count > 0)
            {
                context.ClientAddresses.RemoveRange(data);

                await context.SaveChangesAsync();
            }
        }
    }

    public async Task<List<ClientAddressModel>> ToListAsync(Expression<Func<ClientAddressModel, bool>>? predicate = null)
    {
        using (var context = _factory.CreateDbContext())
        {
            var query = SetQueryFilters(context.ClientAddresses.AsQueryable(), _ignoreQueryFilters);

            if (predicate != null)
            {
                var entityPredicate = MapExpression(predicate);

                query = query.Where(entityPredicate);
            }

            var data = await query.ToListAsync();

            return data.Select(Map).ToList();
        }
    }

    public async Task<List<ClientAddressModel>> ToListAsync(int page, int pageNumber)
    {
        using (var context = _factory.CreateDbContext())
        {
            var query = SetQueryFilters(context.ClientAddresses.AsQueryable(), _ignoreQueryFilters);

            var data = await query.Skip((page - 1) * pageNumber).Take(pageNumber).ToListAsync();

            return data.Select(Map).ToList();
        }
    }

    public async Task<List<ClientAddressModel>> ToListAsync(int count)
    {
        using (var context = _factory.CreateDbContext())
        {
            var query = SetQueryFilters(context.ClientAddresses.AsQueryable(), _ignoreQueryFilters);

            var data = await query.Take(count).ToListAsync();

            return data.Select(Map).ToList();
        }
    }

    public async Task UpdateAsync(ClientAddressModel clientAddress)
    {
        using (var context = _factory.CreateDbContext())
        {
            var entity = await context.ClientAddresses.SingleOrDefaultAsync(e => e.ClientAddressId == clientAddress.Id);

            if (entity != null)
            {
                entity.AddressLine1 = clientAddress.AddressLine1;
                entity.AddressLine2 = clientAddress.AddressLine2;
                entity.AddressLine3 = clientAddress.AddressLine3;
                entity.AddressLine4 = clientAddress.AddressLine4;
                entity.City = clientAddress.City;
                entity.Region = clientAddress.Region;
                entity.PostalCode = clientAddress.PostalCode;
                entity.CountryCode = Enum.GetName(clientAddress.CountryCode) ?? "";
                entity.CreatedAt = clientAddress.CreatedAt;
                entity.UpdatedAt = clientAddress.UpdatedAt;
                entity.DeletedAt = clientAddress.DeletedAt;

                await context.SaveChangesAsync();
            }
        }
    }

    public async Task UpdateRangeAsync(IEnumerable<ClientAddressModel> clientAddresses)
    {
        var ids = clientAddresses.Select(clientAddress => clientAddress.Id).ToList();

        using (var context = _factory.CreateDbContext())
        {
            var data = await context.ClientAddresses.Where(e => ids.Contains(e.ClientAddressId)).ToListAsync();

            data.ForEach(entity =>
            {
                var clientAddress = clientAddresses.Single(e => e.Id == entity.ClientAddressId);

                entity.AddressLine1 = clientAddress.AddressLine1;
                entity.AddressLine2 = clientAddress.AddressLine2;
                entity.AddressLine3 = clientAddress.AddressLine3;
                entity.AddressLine4 = clientAddress.AddressLine4;
                entity.City = clientAddress.City;
                entity.Region = clientAddress.Region;
                entity.PostalCode = clientAddress.PostalCode;
                entity.CountryCode = Enum.GetName(clientAddress.CountryCode) ?? "";
                entity.CreatedAt = clientAddress.CreatedAt;
                entity.UpdatedAt = clientAddress.UpdatedAt;
                entity.DeletedAt = clientAddress.DeletedAt;
            });

            await context.SaveChangesAsync();
        }
    }

    private ClientAddressModel Map(ClientAddressEntity clientAddress)
    {
        var mapper = new AddressMapper();

        return mapper.Map<ClientAddressModel>(clientAddress);
    }

    private Expression<Func<ClientAddressEntity, bool>> MapExpression(Expression<Func<ClientAddressModel, bool>> expression)
    {
        var mapper = new AddressMapper();

        return mapper.Map<Expression<Func<ClientAddressEntity, bool>>>(expression);
    }

    private ClientAddressEntity Map(ClientAddressModel clientAddress)
    {
        var mapper = new AddressMapper();

        return mapper.Map<ClientAddressEntity>(clientAddress);
    }
}