using Invoice.Api.Domains.Common.Mappers;
using Invoice.Domains.Common.Models;

namespace Invoice.Api.Domains.Common.Repositories;

public interface IClientRepository
{
    void IgnoreQueryFilters();
    Task<List<ClientModel>> ToListAsync(Expression<Func<ClientModel, bool>>? predicate = null);
    Task<List<ClientModel>> ToListAsync(int page, int pageNumber);
    Task<List<ClientModel>> ToListAsync(int count);
    Task<bool> ExistsAsync(ClientModel client);
    Task<bool> ExistsAsync(Expression<Func<ClientModel, bool>>? predicate = null);
    Task<int> CountAsync(Expression<Func<ClientModel, bool>>? predicate = null);
    Task<ClientModel?> GetByClientIdAsync(int clientId);
    Task<ClientModel?> GetByGuidAsync(Guid guid);
    Task<ClientModel> AddAsync(ClientModel client);
    Task<List<ClientModel>> AddRangeAsync(IEnumerable<ClientModel> clients);
    Task UpdateAsync(ClientModel client);
    Task UpdateRangeAsync(IEnumerable<ClientModel> clients);
    Task RemoveAsync(ClientModel model);
    Task RemoveRangeAsync(IEnumerable<ClientModel> clients);
}

internal sealed class SqlServerClientRepository : IClientRepository
{
    private readonly IDbContextFactory<SqlServerDbContext> _factory;
    private bool _ignoreQueryFilters = false;

    private IQueryable<ClientEntity> AsQueryable(IQueryable<ClientEntity> source)
    {
        return _ignoreQueryFilters ? source.IgnoreQueryFilters() : source;
    }

    public SqlServerClientRepository(IDbContextFactory<SqlServerDbContext> factory)
    {
        _factory = factory;
    }

    public void IgnoreQueryFilters()
    {
        _ignoreQueryFilters = true;
    }

    public async Task<ClientModel> AddAsync(ClientModel client)
    {
        var data = Map(client);

        using (var context = _factory.CreateDbContext())
        {
            await context.Clients.AddAsync(data);

            await context.SaveChangesAsync();

            return Map(data);
        }
    }

    public async Task<List<ClientModel>> AddRangeAsync(IEnumerable<ClientModel> clients)
    {
        var data = clients.Select(Map);

        using (var context = _factory.CreateDbContext())
        {
            await context.Clients.AddRangeAsync(data);

            await context.SaveChangesAsync();

            return data.Select(Map).ToList();
        }
    }

    public async Task<int> CountAsync(Expression<Func<ClientModel, bool>>? predicate = null)
    {
        using (var context = _factory.CreateDbContext())
        {
            var query = AsQueryable(context.Clients.AsQueryable());

            return await query.CountAsync();
        }
    }

    public async Task<bool> ExistsAsync(ClientModel client)
    {
        using (var context = _factory.CreateDbContext())
        {
            return await context.Clients.AnyAsync(e => e.Name == client.Name);
        }
    }

    public async Task<bool> ExistsAsync(Expression<Func<ClientModel, bool>>? predicate = null)
    {
        using (var context = _factory.CreateDbContext())
        {
            var query = AsQueryable(context.Clients.AsQueryable());

            return await query.AnyAsync();
        }
    }

    public async Task<ClientModel?> GetByClientIdAsync(int clientId)
    {
        using (var context = _factory.CreateDbContext())
        {
            var query = AsQueryable(context.Clients.AsQueryable());

            var data = await query.SingleOrDefaultAsync(e => e.Id == clientId);

            return data != null ? Map(data) : null;
        }
    }

    public async Task<ClientModel?> GetByGuidAsync(Guid guid)
    {
        using (var context = _factory.CreateDbContext())
        {
            var query = AsQueryable(context.Clients.AsQueryable());

            var data = await query.SingleOrDefaultAsync(e => e.Guid == guid);

            return data != null ? Map(data) : null;
        }
    }

    public async Task RemoveAsync(ClientModel model)
    {
        using (var context = _factory.CreateDbContext())
        {
            var data = await context.Clients.SingleOrDefaultAsync(e => e.Id == model.Id);

            if (data != null)
            {
                context.Clients.Remove(data);

                await context.SaveChangesAsync();
            }
        }
    }

    public async Task RemoveRangeAsync(IEnumerable<ClientModel> clients)
    {
        using (var context = _factory.CreateDbContext())
        {
            var data = await context.Clients.Where(e => clients.Select(address => address.Id).Contains(e.Id)).ToListAsync();

            if (data.Count > 0)
            {
                context.Clients.RemoveRange(data);

                await context.SaveChangesAsync();
            }
        }
    }

    public async Task<List<ClientModel>> ToListAsync(Expression<Func<ClientModel, bool>>? predicate = null)
    {
        using (var context = _factory.CreateDbContext())
        {
            var query = AsQueryable(context.Clients.AsQueryable());

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
            var query = AsQueryable(context.Clients.AsQueryable());

            var data = await query.Skip((page - 1) * pageNumber).Take(pageNumber).ToListAsync();

            return data.Select(Map).ToList();
        }
    }

    public async Task<List<ClientModel>> ToListAsync(int count)
    {
        using (var context = _factory.CreateDbContext())
        {
            var query = AsQueryable(context.Clients.AsQueryable());

            var data = await query.Take(count).ToListAsync();

            return data.Select(Map).ToList();
        }
    }

    public async Task UpdateAsync(ClientModel client)
    {
        using (var context = _factory.CreateDbContext())
        {
            var entity = await context.Clients.SingleOrDefaultAsync(e => e.Id == client.Id);

            if (entity != null)
            {
                entity.Name = client.Name;
                entity.Email = client.Email;
                entity.CreatedAt = client.CreatedAt;
                entity.UpdatedAt = client.UpdatedAt;
                entity.DeletedAt = client.DeletedAt;

                await context.SaveChangesAsync();
            }
        }
    }

    public async Task UpdateRangeAsync(IEnumerable<ClientModel> clients)
    {
        var ids = clients.Select(address => address.Id).ToList();

        using (var context = _factory.CreateDbContext())
        {
            var data = await context.Clients.Where(e => ids.Contains(e.Id)).ToListAsync();

            data.ForEach(entity =>
            {
                var client = clients.Single(e => e.Id == entity.Id);

                entity.Name = client.Name;
                entity.Email = client.Email;
                entity.CreatedAt = client.CreatedAt;
                entity.UpdatedAt = client.UpdatedAt;
                entity.DeletedAt = client.DeletedAt;
            });

            await context.SaveChangesAsync();
        }
    }

    private ClientModel Map(ClientEntity client)
    {
        var mapper = new AddressMapper();

        return mapper.Map<ClientModel>(client);
    }

    private Expression<Func<ClientEntity, bool>> MapExpression(Expression<Func<ClientModel, bool>> expression)
    {
        var mapper = new AddressMapper();

        return mapper.Map<Expression<Func<ClientEntity, bool>>>(expression);
    }

    private ClientEntity Map(ClientModel client)
    {
        var mapper = new AddressMapper();

        return mapper.Map<ClientEntity>(client);
    }
}