using Invoice.Api.Domains.Common.Mappers;
using Invoice.Domains.Common.Models;

namespace Invoice.Api.Domains.Common.Repositories;

public interface IClientRepository
{
    void IgnoreQueryFilters();
    Task<List<ClientModel>> ToListAsync(Expression<Func<ClientModel, bool>>? predicate = null);
    Task<List<ClientModel>> ToListAsync(int page, int pageNumber);
    Task<List<ClientModel>> ToListAsync(int count);
    Task<bool> ExistsAsync();
    Task<int> CountAsync();
    Task<ClientModel?> GetByIdAsync(int id);
    Task<ClientModel> AddAsync(ClientModel address);
    Task<List<ClientModel>> AddRangeAsync(IEnumerable<ClientModel> clients);
    Task UpdateAsync(ClientModel address);
    Task UpdateRangeAsync(IEnumerable<ClientModel> clients);
    Task RemoveAsync(ClientModel model);
    Task RemoveRangeAsync(IEnumerable<ClientModel> clients);
}

internal sealed class SqlServerClientRepository : IClientRepository
{
    private readonly IDbContextFactory<SqlServerDbContext> _factory;
    private bool _ignoreQueryFilters = false;

    private IQueryable<ClientAddressEntity> SetQueryFilters(IQueryable<ClientAddressEntity> source, bool ignoreQueryFilters)
    {
        return ignoreQueryFilters ? source.IgnoreQueryFilters() : source;
    }

    public SqlServerClientRepository(IDbContextFactory<SqlServerDbContext> factory)
    {
        _factory = factory;
    }

    public void IgnoreQueryFilters()
    {
        _ignoreQueryFilters = true;
    }

    public async Task<ClientModel> AddAsync(ClientModel address)
    {
        var data = Map(address);

        using (var context = _factory.CreateDbContext())
        {
            await context.ClientAddresses.AddAsync(data);

            await context.SaveChangesAsync();

            return Map(data);
        }
    }

    public async Task<List<ClientModel>> AddRangeAsync(IEnumerable<ClientModel> clients)
    {
        var data = clients.Select(Map);

        using (var context = _factory.CreateDbContext())
        {
            await context.ClientAddresses.AddRangeAsync(data);

            await context.SaveChangesAsync();

            return data.Select(Map).ToList();
        }
    }

    public async Task<int> CountAsync()
    {
        using (var context = _factory.CreateDbContext())
        {
            var query = SetQueryFilters(context.ClientAddresses.AsQueryable(), _ignoreQueryFilters);

            return await query.CountAsync();
        }
    }

    public async Task<bool> ExistsAsync()
    {
        using (var context = _factory.CreateDbContext())
        {
            var query = SetQueryFilters(context.ClientAddresses.AsQueryable(), _ignoreQueryFilters);

            return await query.AnyAsync();
        }
    }

    public async Task<ClientModel?> GetByIdAsync(int id)
    {
        using (var context = _factory.CreateDbContext())
        {
            var data = _ignoreQueryFilters
                ? await context.ClientAddresses.IgnoreQueryFilters().SingleOrDefaultAsync(e => e.ClientAddressId == id)
                : await context.ClientAddresses.SingleOrDefaultAsync(e => e.ClientAddressId == id);

            return data != null ? Map(data) : null;
        }
    }

    public async Task RemoveAsync(ClientModel model)
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

    public async Task RemoveRangeAsync(IEnumerable<ClientModel> clients)
    {
        using (var context = _factory.CreateDbContext())
        {
            var data = await context.ClientAddresses.Where(e => clients.Select(address => address.Id).Contains(e.ClientAddressId)).ToListAsync();

            if (data.Count > 0)
            {
                context.ClientAddresses.RemoveRange(data);

                await context.SaveChangesAsync();
            }
        }
    }

    public async Task<List<ClientModel>> ToListAsync(Expression<Func<ClientModel, bool>>? predicate = null)
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

    public async Task<List<ClientModel>> ToListAsync(int page, int pageNumber)
    {
        using (var context = _factory.CreateDbContext())
        {
            var query = SetQueryFilters(context.ClientAddresses.AsQueryable(), _ignoreQueryFilters);

            var data = await query.Skip((page - 1) * pageNumber).Take(pageNumber).ToListAsync();

            return data.Select(Map).ToList();
        }
    }

    public async Task<List<ClientModel>> ToListAsync(int count)
    {
        using (var context = _factory.CreateDbContext())
        {
            var query = SetQueryFilters(context.ClientAddresses.AsQueryable(), _ignoreQueryFilters);

            var data = await query.Take(count).ToListAsync();

            return data.Select(Map).ToList();
        }
    }

    public async Task UpdateAsync(ClientModel address)
    {
        using (var context = _factory.CreateDbContext())
        {
            var entity = await context.ClientAddresses.SingleOrDefaultAsync(e => e.ClientAddressId == address.Id);

            if (entity != null)
            {
                entity.AddressLine1 = address.AddressLine1;
                entity.AddressLine2 = address.AddressLine2;
                entity.AddressLine3 = address.AddressLine3;
                entity.AddressLine4 = address.AddressLine4;
                entity.City = address.City;
                entity.Region = address.Region;
                entity.PostalCode = address.PostalCode;
                entity.CountryCode = Enum.GetName(address.CountryCode) ?? "";
                entity.UtcCreatedDate = address.UtcCreatedDate;
                entity.UtcUpdatedDate = address.UtcUpdatedDate;
                entity.UtcDeletedDate = address.UtcDeletedDate;

                await context.SaveChangesAsync();
            }
        }
    }

    public async Task UpdateRangeAsync(IEnumerable<ClientModel> clients)
    {
        var ids = clients.Select(address => address.Id).ToList();

        using (var context = _factory.CreateDbContext())
        {
            var data = await context.ClientAddresses.Where(e => ids.Contains(e.ClientAddressId)).ToListAsync();

            data.ForEach(entity =>
            {
                var address = clients.Single(e => e.Id == entity.ClientAddressId);

                entity.AddressLine1 = address.AddressLine1;
                entity.AddressLine2 = address.AddressLine2;
                entity.AddressLine3 = address.AddressLine3;
                entity.AddressLine4 = address.AddressLine4;
                entity.City = address.City;
                entity.Region = address.Region;
                entity.PostalCode = address.PostalCode;
                entity.CountryCode = Enum.GetName(address.CountryCode) ?? "";
                entity.UtcCreatedDate = address.UtcCreatedDate;
                entity.UtcUpdatedDate = address.UtcUpdatedDate;
                entity.UtcDeletedDate = address.UtcDeletedDate;
            });

            await context.SaveChangesAsync();
        }
    }

    private ClientModel Map(ClientAddressEntity address)
    {
        var mapper = new AddressMapper();

        return mapper.Map<ClientModel>(address);
    }

    private Expression<Func<ClientAddressEntity, bool>> MapExpression(Expression<Func<ClientModel, bool>> expression)
    {
        var mapper = new AddressMapper();

        return mapper.Map<Expression<Func<ClientAddressEntity, bool>>>(expression);
    }

    private ClientAddressEntity Map(ClientModel address)
    {
        var mapper = new AddressMapper();

        return mapper.Map<ClientAddressEntity>(address);
    }
}

internal sealed class MySqlClientRepository : IClientRepository
{
    private readonly IDbContextFactory<MySqlDbContext> _factory;
    private bool _ignoreQueryFilters = false;

    private IQueryable<ClientEntity> SetQueryFilters(IQueryable<ClientEntity> source, bool ignoreQueryFilters)
    {
        return ignoreQueryFilters ? source.IgnoreQueryFilters() : source;
    }

    public MySqlClientRepository(IDbContextFactory<MySqlDbContext> factory)
    {
        _factory = factory;
    }

    public void IgnoreQueryFilters()
    {
        _ignoreQueryFilters = true;
    }

    public async Task<ClientModel> AddAsync(ClientModel address)
    {
        var data = Map(address);

        using (var context = _factory.CreateDbContext())
        {
            await context.ClientAddresses.AddAsync(data);

            await context.SaveChangesAsync();

            return Map(data);
        }
    }

    public async Task<List<ClientModel>> AddRangeAsync(IEnumerable<ClientModel> clients)
    {
        var data = clients.Select(Map);

        using (var context = _factory.CreateDbContext())
        {
            await context.ClientAddresses.AddRangeAsync(data);

            await context.SaveChangesAsync();

            return data.Select(Map).ToList();
        }
    }

    public async Task<int> CountAsync()
    {
        using (var context = _factory.CreateDbContext())
        {
            var query = SetQueryFilters(context.ClientAddresses.AsQueryable(), _ignoreQueryFilters);

            return await query.CountAsync();
        }
    }

    public async Task<bool> ExistsAsync()
    {
        using (var context = _factory.CreateDbContext())
        {
            var query = SetQueryFilters(context.ClientAddresses.AsQueryable(), _ignoreQueryFilters);

            return await query.AnyAsync();
        }
    }

    public async Task<ClientModel?> GetByIdAsync(int id)
    {
        using (var context = _factory.CreateDbContext())
        {
            var data = _ignoreQueryFilters
                ? await context.ClientAddresses.IgnoreQueryFilters().SingleOrDefaultAsync(e => e.ClientAddressId == id)
                : await context.ClientAddresses.SingleOrDefaultAsync(e => e.ClientAddressId == id);

            return data != null ? Map(data) : null;
        }
    }

    public async Task RemoveAsync(ClientModel model)
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

    public async Task RemoveRangeAsync(IEnumerable<ClientModel> clients)
    {
        using (var context = _factory.CreateDbContext())
        {
            var ids = clients.Select(address => address.Id);

            var data = await context.ClientAddresses.Where(e => ids.Contains(e.ClientAddressId)).ToListAsync();

            if (data.Count > 0)
            {
                context.ClientAddresses.RemoveRange(data);

                await context.SaveChangesAsync();
            }
        }
    }

    public async Task<List<ClientModel>> ToListAsync(Expression<Func<ClientModel, bool>>? predicate = null)
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

    public async Task<List<ClientModel>> ToListAsync(int page, int pageNumber)
    {
        using (var context = _factory.CreateDbContext())
        {
            var query = SetQueryFilters(context.ClientAddresses.AsQueryable(), _ignoreQueryFilters);

            var data = await query.Skip((page - 1) * pageNumber).Take(pageNumber).ToListAsync();

            return data.Select(Map).ToList();
        }
    }

    public async Task<List<ClientModel>> ToListAsync(int count)
    {
        using (var context = _factory.CreateDbContext())
        {
            var query = SetQueryFilters(context.ClientAddresses.AsQueryable(), _ignoreQueryFilters);

            var data = await query.Take(count).ToListAsync();

            return data.Select(Map).ToList();
        }
    }

    public async Task UpdateAsync(ClientModel address)
    {
        using (var context = _factory.CreateDbContext())
        {
            var entity = await context.ClientAddresses.SingleOrDefaultAsync(e => e.ClientAddressId == address.Id);

            if (entity != null)
            {
                entity.AddressLine1 = address.AddressLine1;
                entity.AddressLine2 = address.AddressLine2;
                entity.AddressLine3 = address.AddressLine3;
                entity.AddressLine4 = address.AddressLine4;
                entity.City = address.City;
                entity.Region = address.Region;
                entity.PostalCode = address.PostalCode;
                entity.CountryCode = Enum.GetName(address.CountryCode) ?? "";
                entity.UtcCreatedDate = address.UtcCreatedDate;
                entity.UtcUpdatedDate = address.UtcUpdatedDate;
                entity.UtcDeletedDate = address.UtcDeletedDate;

                await context.SaveChangesAsync();
            }
        }
    }

    public async Task UpdateRangeAsync(IEnumerable<ClientModel> clients)
    {
        var ids = clients.Select(address => address.Id).ToList();

        using (var context = _factory.CreateDbContext())
        {
            var data = await context.ClientAddresses.Where(e => ids.Contains(e.ClientAddressId)).ToListAsync();

            data.ForEach(entity =>
            {
                var address = clients.Single(e => e.Id == entity.ClientAddressId);

                entity.AddressLine1 = address.AddressLine1;
                entity.AddressLine2 = address.AddressLine2;
                entity.AddressLine3 = address.AddressLine3;
                entity.AddressLine4 = address.AddressLine4;
                entity.City = address.City;
                entity.Region = address.Region;
                entity.PostalCode = address.PostalCode;
                entity.CountryCode = Enum.GetName(address.CountryCode) ?? "";
                entity.UtcCreatedDate = address.UtcCreatedDate;
                entity.UtcUpdatedDate = address.UtcUpdatedDate;
                entity.UtcDeletedDate = address.UtcDeletedDate;
            });

            await context.SaveChangesAsync();
        }
    }

    private ClientModel Map(ClientAddressEntity address)
    {
        var mapper = new AddressMapper();

        return mapper.Map<ClientModel>(address);
    }

    private Expression<Func<ClientAddressEntity, bool>> MapExpression(Expression<Func<ClientModel, bool>> expression)
    {
        var mapper = new AddressMapper();

        return mapper.Map<Expression<Func<ClientAddressEntity, bool>>>(expression);
    }

    private ClientAddressEntity Map(ClientModel address)
    {
        var mapper = new AddressMapper();

        return mapper.Map<ClientAddressEntity>(address);
    }
}