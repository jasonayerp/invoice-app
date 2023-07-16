using Invoice.Api.Data.Entities;
using Invoice.Domains.Common.Models;
using Invoice.Domains.Common.Objects;

namespace Invoice.Api.Domains.Common.Mappers;

public interface IMapper<TSource>
{
    TTarget Map<TTarget>(object source);
}

public abstract class AbstractMapper<TSource> : IMapper<TSource>
{
    private MapperConfiguration _mapperConfiguration;

    protected void CreateMap(Action<IMapperConfigurationExpression> mapperConfigurationExpression)
    {
        _mapperConfiguration = new MapperConfiguration(mapperConfigurationExpression);
    }

    public TTarget Map<TTarget>(object source)
    {
        Mapper mapper = new Mapper(_mapperConfiguration);

        return mapper.Map<TTarget>(source);
    }
}

public sealed class AddressMapper : AbstractMapper<AddressModel>
{
    public AddressMapper()
    {
        CreateMap(config =>
        {
            config.CreateMap<AddressModel, AddressEntity>()
                .ReverseMap();

            config.CreateMap<AddressModel, AddressEntity>()
                .ForMember((dest) => dest.AddressId, (options) => options.MapFrom((src) => src.Id))
                .ReverseMap();

            config.CreateMap<AddressModel, AddressObject>()
                .ReverseMap();

            config.CreateMap<AddressModel, AddressObject>()
                .ForMember((dest) => dest.PublicId, (options) => options.MapFrom((src) => src.Guid))
                .ReverseMap();
        });
    }
}
