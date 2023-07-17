using AutoMapper;

namespace Invoice.Mapper;

public abstract class AbstractMapper<TSource> : IMapper<TSource>
{
    private MapperConfiguration _mapperConfiguration;

    protected void CreateMap(Action<IMapperConfigurationExpression> mapperConfigurationExpression)
    {
        _mapperConfiguration = new MapperConfiguration(mapperConfigurationExpression);
    }

    public TTarget Map<TTarget>(object source)
    {
        var mapper = new AutoMapper.Mapper(_mapperConfiguration);

        return mapper.Map<TTarget>(source);
    }
}
