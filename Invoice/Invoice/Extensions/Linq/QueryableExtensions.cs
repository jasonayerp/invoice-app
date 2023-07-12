namespace Invoice.Extensions.Linq;

public static class QueryableExtensions
{
    public static IQueryable<T> Paginate<T>(this IQueryable<T> source, int page, int pageNumber)
    {
        return source.Skip((page - 1) * pageNumber).Take(pageNumber);
    }
}
