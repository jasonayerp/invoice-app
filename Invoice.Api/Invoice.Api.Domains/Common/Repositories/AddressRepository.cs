using Invoice.Api.Data.Entities;
using Invoice.Api.Data.SqlServer;
using Invoice.Api.Domains.Common.Mappers;
using Invoice.Domains.Common.Models;

namespace Invoice.Api.Domains.Common.Repositories;

public interface IAddressRepository
{
    void IgnoreQueryFilters();
    Task<List<AddressModel>> ToListAsync();
    Task<List<AddressModel>> ToListAsync(int page, int pageNumber);
    Task<List<AddressModel>> ToListAsync(int count);
    Task<bool> ExistsAsync();
    Task<int> CountAsync();
    Task<AddressModel?> GetByIdAsync(int id);
    Task<AddressModel> AddAsync(AddressModel address);
    Task<List<AddressModel>> AddRangeAsync(IEnumerable<AddressModel> addresses);
    Task UpdateAsync(AddressModel address);
    Task UpdateRangeAsync(IEnumerable<AddressModel> addresses);
    Task RemoveAsync(AddressModel model);
    Task RemoveRangeAsync(IEnumerable<AddressModel> addresses);
}

public class AddressRepository : IAddressRepository
{
    private readonly IDbContextFactory<SqlServerDbContext> _factory;
    private bool _ignoreQueryFilters = false;

    private IQueryable<AddressEntity> SetQueryFilters(IQueryable<AddressEntity> source, bool ignoreQueryFilters)
    {
        return ignoreQueryFilters ? source.IgnoreQueryFilters() : source;
    }

    public AddressRepository(IDbContextFactory<SqlServerDbContext> factory)
    {
        _factory = factory;
    }

    public void IgnoreQueryFilters()
    {
        _ignoreQueryFilters = true;
    }

    public async Task<AddressModel> AddAsync(AddressModel address)
    {
        var data = Map<AddressEntity>(address);

        using (var context = _factory.CreateDbContext())
        {
            await context.Addresses.AddAsync(data);

            await context.SaveChangesAsync();

            return Map<AddressModel>(data);
        }
    }

    public async Task<List<AddressModel>> AddRangeAsync(IEnumerable<AddressModel> addresses)
    {
        var data = addresses.Select(e => Map<AddressEntity>(e));

        using (var context = _factory.CreateDbContext())
        {
            await context.Addresses.AddRangeAsync(data);

            await context.SaveChangesAsync();

            return data.Select(e => Map<AddressModel>(e)).ToList();
        }
    }

    public async Task<int> CountAsync()
    {
        using (var context = _factory.CreateDbContext())
        {
            var query = SetQueryFilters(context.Addresses.AsQueryable(), _ignoreQueryFilters);

            return await query.CountAsync();
        }
    }

    public async Task<bool> ExistsAsync()
    {
        using (var context = _factory.CreateDbContext())
        {
            var query = SetQueryFilters(context.Addresses.AsQueryable(), _ignoreQueryFilters);

            return await query.AnyAsync();
        }
    }

    public async Task<AddressModel?> GetByIdAsync(int id)
    {
        using (var context = _factory.CreateDbContext())
        {
            var data = _ignoreQueryFilters
                ? await context.Addresses.IgnoreQueryFilters().SingleOrDefaultAsync(e => e.AddressId == id)
                : await context.Addresses.SingleOrDefaultAsync(e => e.AddressId == id);

            return data != null ? Map<AddressModel>(data) : null;
        }
    }

    public async Task RemoveAsync(AddressModel model)
    {
        using (var context = _factory.CreateDbContext())
        {
            var data = await context.Addresses.SingleOrDefaultAsync(e => e.AddressId == model.Id);

            if (data != null)
            {
                context.Addresses.Remove(data);

                await context.SaveChangesAsync();
            }
        }
    }

    public async Task RemoveRangeAsync(IEnumerable<AddressModel> addresses)
    {
        using (var context = _factory.CreateDbContext())
        {
            var ids = addresses.Select(address => address.Id);

            var data = await context.Addresses.Where(e => ids.Contains(e.AddressId)).ToListAsync();

            if (data.Count > 0)
            {
                context.Addresses.RemoveRange(data);

                await context.SaveChangesAsync();
            }
        }
    }

    public async Task<List<AddressModel>> ToListAsync()
    {
        using (var context = _factory.CreateDbContext())
        {
            var query = SetQueryFilters(context.Addresses.AsQueryable(), _ignoreQueryFilters);

            var data = await query.ToListAsync();

            return data.Select(e => Map<AddressModel>(e)).ToList();
        }
    }

    public async Task<List<AddressModel>> ToListAsync(int page, int pageNumber)
    {
        using (var context = _factory.CreateDbContext())
        {
            var query = SetQueryFilters(context.Addresses.AsQueryable(), _ignoreQueryFilters);

            var data = await query.Skip((page - 1) * pageNumber).Take(pageNumber).ToListAsync();

            return data.Select(e => Map<AddressModel>(e)).ToList();
        }
    }

    public async Task<List<AddressModel>> ToListAsync(int count)
    {
        using (var context = _factory.CreateDbContext())
        {
            var query = SetQueryFilters(context.Addresses.AsQueryable(), _ignoreQueryFilters);

            var data = await query.Take(count).ToListAsync();

            return data.Select(e => Map<AddressModel>(e)).ToList();
        }
    }

    public async Task UpdateAsync(AddressModel address)
    {
        using (var context = _factory.CreateDbContext())
        {
            var entity = await context.Addresses.SingleOrDefaultAsync(e => e.AddressId == address.Id);

            if (entity != null)
            {
                entity.AddressLine1 = address.AddressLine1;
                entity.AddressLine2 = address.AddressLine2;
                entity.AddressLine3 = address.AddressLine3;
                entity.AddressLine4 = address.AddressLine4;
                entity.City = address.City;
                entity.Region = address.Region;
                entity.PostalCode = address.PostalCode;
                entity.CountryCode = address.CountryCode;
                entity.UtcCreatedDate = address.UtcCreatedDate;
                entity.UtcUpdatedDate = address.UtcUpdatedDate;
                entity.UtcDeletedDate = address.UtcDeletedDate;

                await context.SaveChangesAsync();
            }
        }
    }

    public async Task UpdateRangeAsync(IEnumerable<AddressModel> addresses)
    {
        var ids = addresses.Select(address => address.Id).ToList();

        using (var context = _factory.CreateDbContext())
        {
            var data = await context.Addresses.Where(e => ids.Contains(e.AddressId)).ToListAsync();

            data.ForEach(entity =>
            {
                var address = addresses.Single(e => e.Id == entity.AddressId);

                entity.AddressLine1 = address.AddressLine1;
                entity.AddressLine2 = address.AddressLine2;
                entity.AddressLine3 = address.AddressLine3;
                entity.AddressLine4 = address.AddressLine4;
                entity.City = address.City;
                entity.Region = address.Region;
                entity.PostalCode = address.PostalCode;
                entity.CountryCode = address.CountryCode;
                entity.UtcCreatedDate = address.UtcCreatedDate;
                entity.UtcUpdatedDate = address.UtcUpdatedDate;
                entity.UtcDeletedDate = address.UtcDeletedDate;
            });

            await context.SaveChangesAsync();
        }
    }

    private T Map<T>(object address)
    {
        var mapper = new AddressMapper();

        return mapper.Map<T>(address);
    }
}   
