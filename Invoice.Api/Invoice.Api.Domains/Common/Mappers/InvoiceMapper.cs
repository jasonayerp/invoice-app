using Invoice.Mapper;

namespace Invoice.Api.Domains.Common.Mappers;

public sealed class InvoiceMapper : AbstractMapper<ClientAddressModel>
{
    public InvoiceMapper()
    {
        CreateMap(config =>
        {
            config.CreateMap<InvoiceModel, InvoiceEntity>().ReverseMap();
        });
    }
}
