namespace Invoice.Api.Domains.Metadata;

public interface IRepository<TModel> : IReadOnlyRepository<TModel>
    where TModel : class
{
    Task<TModel> AddAsync(TModel model);
    Task<List<TModel>> AddRangeAsync(IEnumerable<TModel> models);
    Task UpdateAsync(TModel model);
    Task UpdateRangeAsync(IEnumerable<TModel> models);
    Task DeleteAsync(TModel model);
    Task DeleteRangeAsync(IEnumerable<TModel> models);
}
