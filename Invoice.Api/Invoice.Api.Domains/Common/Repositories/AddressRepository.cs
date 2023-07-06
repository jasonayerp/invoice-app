using Invoice.Api.Data;
using Invoice.Api.Data.Entities;
using Invoice.Api.Data.SqlServer;
using Invoice.Api.Domains.Common.Mappers;
using Invoice.Domains.Common.Models;

namespace Invoice.Api.Domains.Common.Repositories;

public interface IAddressRepository
{
    Task<List<AddressModel>> ToListAsync(bool ignoreQueryFilters = false);
    Task<List<AddressModel>> ToListAsync(int page, int pageNumber, bool ignoreQueryFilters = false);
    Task<List<AddressModel>> ToListAsync(int count, bool ignoreQueryFilters = false);
    Task<bool> ExistsAsync(bool ignoreQueryFilters = false);
    Task<int> CountAsync(bool ignoreQueryFilters = false);
    Task<AddressModel?> GetByIdAsync(int id, bool ignoreQueryFilters = false);
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
    private readonly IMapper _mapper;

    private IQueryable<AddressEntity> SetQueryFilters(IQueryable<AddressEntity> source, bool ignoreQueryFilters)
    {
        return ignoreQueryFilters ? source.IgnoreQueryFilters() : source;
    }

    public AddressRepository(IDbContextFactory<SqlServerDbContext> factory, IMapper mapper)
    {
        _factory = factory;
        _mapper = mapper;
    }

    public async Task<AddressModel> AddAsync(AddressModel address)
    {
        var data = _mapper.Map<AddressEntity>(address);

        using (var context = _factory.CreateDbContext())
        {
            await context.Addresses.AddAsync(data);

            await context.SaveChangesAsync();

            return Map(data);
        }
    }

    public async Task<List<AddressModel>> AddRangeAsync(IEnumerable<AddressModel> addresses)
    {
        var data = addresses.Select(address => _mapper.Map<AddressEntity>(address));

        using (var context = _factory.CreateDbContext())
        {
            await context.Addresses.AddRangeAsync(data);

            await context.SaveChangesAsync();

            return data.Select(Map).ToList();
        }
    }

    public async Task<int> CountAsync(bool ignoreQueryFilters = false)
    {
        using (var context = _factory.CreateDbContext())
        {
            var query = SetQueryFilters(context.Addresses.AsQueryable(), ignoreQueryFilters);

            return await query.CountAsync();
        }
    }

    public async Task<bool> ExistsAsync(bool ignoreQueryFilters = false)
    {
        using (var context = _factory.CreateDbContext())
        {
            var query = SetQueryFilters(context.Addresses.AsQueryable(), ignoreQueryFilters);

            return await query.AnyAsync();
        }
    }

    public async Task<AddressModel?> GetByIdAsync(int id, bool ignoreQueryFilters = false)
    {
        using (var context = _factory.CreateDbContext())
        {
            var data = ignoreQueryFilters
                ? await context.Addresses.IgnoreQueryFilters().SingleOrDefaultAsync(e => e.AddressId == id)
                : await context.Addresses.SingleOrDefaultAsync(e => e.AddressId == id);

            return data != null ? Map(data) : null;
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

    public async Task<List<AddressModel>> ToListAsync(bool ignoreQueryFilters = false)
    {
        using (var context = _factory.CreateDbContext())
        {
            var query = SetQueryFilters(context.Addresses.AsQueryable(), ignoreQueryFilters);

            var data = await query.ToListAsync();

            return data.Select(Map).ToList();
        }
    }

    public async Task<List<AddressModel>> ToListAsync(int page, int pageNumber, bool ignoreQueryFilters = false)
    {
        using (var context = _factory.CreateDbContext())
        {
            var query = SetQueryFilters(context.Addresses.AsQueryable(), ignoreQueryFilters);

            var data = await query.Skip((page - 1) * pageNumber).Take(pageNumber).ToListAsync();

            return data.Select(Map).ToList();
        }
    }

    public async Task<List<AddressModel>> ToListAsync(int count, bool ignoreQueryFilters = false)
    {
        using (var context = _factory.CreateDbContext())
        {
            var query = SetQueryFilters(context.Addresses.AsQueryable(), ignoreQueryFilters);

            var data = await query.Take(count).ToListAsync();

            return data.Select(Map).ToList();
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

    private AddressModel Map(AddressEntity address)
    {
        var mapped = _mapper.Map<AddressModel>(address);
        mapped.Id = address.AddressId;
        return mapped;
    }

    private AddressEntity Map(AddressModel address)
    {
        var mapped = _mapper.Map<AddressEntity>(address);
        mapped.AddressId = address.Id;
        return mapped;
    }
}   
