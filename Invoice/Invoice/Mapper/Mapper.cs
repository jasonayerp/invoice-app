namespace Invoice.Mapper;

public interface IMapper<TSource>
{
    TTarget Map<TTarget>(object source);
}
