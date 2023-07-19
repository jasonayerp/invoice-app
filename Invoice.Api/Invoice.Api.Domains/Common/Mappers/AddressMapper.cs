using Invoice.Api.Data.Entities;
using Invoice.Domains.Common.Models;
using Invoice.Domains.Common.Objects;
using Invoice.Mapper;

namespace Invoice.Api.Domains.Common.Mappers;

public sealed class AddressMapper : AbstractMapper<ClientAddressModel>
{
    public AddressMapper()
    {
        CreateMap(config =>
        {
            config.CreateMap<ClientAddressModel, ClientAddressEntity>()
                .ReverseMap();

            config.CreateMap<ClientAddressModel, ClientAddressEntity>()
                .ForMember((dest) => dest.ClientAddressId, (options) => options.MapFrom((src) => src.Id))
                .ReverseMap();

            config.CreateMap<ClientAddressModel, ClientAddressObject>()
                .ReverseMap();
        });
    }
}
