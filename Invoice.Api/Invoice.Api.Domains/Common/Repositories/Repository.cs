namespace Invoice.Api.Domains.Common.Repositories;

public interface IReadOnlyRepository<T>
{
    Task<List<T>> ToListAsync();
    Task<List<T>> ToListAsync(int page, int pageSize);
    Task<List<T>> ToListAsync(int count);
    Task<T> SingleOrDefaultAsync();
    Task<bool> ExistsAsync();
}