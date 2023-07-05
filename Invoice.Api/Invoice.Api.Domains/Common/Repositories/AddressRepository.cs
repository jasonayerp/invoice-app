using Invoice.Api.Data;
using Invoice.Api.Data.Entities;
using Invoice.Api.Domains.Common.Mappers;
using Microsoft.EntityFrameworkCore;

namespace Invoice.Api.Domains.Common.Repositories;

public interface IAddressRepository
{
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
    private readonly IDbContextFactory<InvoiceDbContext> _factory;
    private readonly IMapper _mapper;

    public AddressRepository(IDbContextFactory<InvoiceDbContext> factory, IMapper mapper)
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

            return _mapper.Map<AddressModel>(data);
        }
    }

    public async Task<List<AddressModel>> AddRangeAsync(IEnumerable<AddressModel> addresses)
    {
        var data = addresses.Select(address => _mapper.Map<AddressEntity>(address));

        using (var context = _factory.CreateDbContext())
        {
            await context.Addresses.AddRangeAsync(data);

            await context.SaveChangesAsync();

            return data.Select(address => _mapper.Map<AddressModel>(data)).ToList();
        }
    }

    public async Task<int> CountAsync()
    {
        using (var context = _factory.CreateDbContext())
        {
            return await context.Addresses.CountAsync();
        }
    }

    public async Task<bool> ExistsAsync()
    {
        using (var context = _factory.CreateDbContext())
        {
            return await context.Addresses.AnyAsync();
        }
    }

    public async Task<AddressModel?> GetByIdAsync(int id)
    {
        using (var context = _factory.CreateDbContext())
        {
            var data = await context.Addresses.SingleOrDefaultAsync(e => e.AddressId == id);

            return data != null ? _mapper.Map<AddressModel>(data) : null;
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
            var data = await context.Addresses.ToListAsync();

            return data.Select(address => _mapper.Map<AddressModel>(address)).ToList();
        }
    }

    public async Task<List<AddressModel>> ToListAsync(int page, int pageNumber)
    {
        using (var context = _factory.CreateDbContext())
        {
            var data = await context.Addresses.Skip((page - 1) * pageNumber).Take(pageNumber).ToListAsync();

            return data.Select(address => _mapper.Map<AddressModel>(address)).ToList();
        }
    }

    public async Task<List<AddressModel>> ToListAsync(int count)
    {
        using (var context = _factory.CreateDbContext())
        {
            var data = await context.Addresses.Take(count).ToListAsync();

            return data.Select(address => _mapper.Map<AddressModel>(address)).ToList();
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
                entity.CountryCode = address.Country;
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
                entity.CountryCode = address.Country;
                entity.UtcCreatedDate = address.UtcCreatedDate;
                entity.UtcUpdatedDate = address.UtcUpdatedDate;
                entity.UtcDeletedDate = address.UtcDeletedDate;
            });

            await context.SaveChangesAsync();
        }
    }
}
