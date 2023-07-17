using Invoice.Api.Data.Entities;
using Invoice.Domains.Common.Models;
using Invoice.Domains.Common.Objects;
using Invoice.Mapper;

namespace Invoice.Api.Domains.Common.Mappers;

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
