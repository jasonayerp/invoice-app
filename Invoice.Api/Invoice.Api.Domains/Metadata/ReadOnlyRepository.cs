namespace Invoice.Api.Domains.Metadata;

public interface IReadOnlyRepository<TModel>
    where TModel : class
{
    void IgnoreQueryFilters();
    Task<List<TModel>> ToListAsync(Expression<Func<TModel, bool>>? predicateExpression = null);
    Task<List<TModel>> ToListAsync(int page, int pageSize, Expression<Func<TModel, bool>>? predicateExpression = null);
    Task<List<TModel>> ToListAsync(int count, Expression<Func<TModel, bool>>? predicateExpression = null);
    Task<TModel?> FirstOrDefault(Expression<Func<TModel, bool>>? predicateExpression = null);
    Task<bool> ExistsAsync(Expression<Func<TModel, bool>>? predicateExpression = null);
    Task<int> CountAsync(Expression<Func<TModel, bool>>? predicateExpression = null);
}
