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

            config.CreateMap<ClientAddressModel, ClientAddressObject>()
                .ReverseMap();
        });
    }
}
