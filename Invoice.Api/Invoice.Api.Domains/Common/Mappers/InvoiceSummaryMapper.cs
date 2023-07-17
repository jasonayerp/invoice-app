using Invoice.Api.Data.Entities;
using Invoice.Domains.Common.Models;
using Invoice.Domains.Common.Objects;
using Invoice.Mapper;

namespace Invoice.Api.Domains.Common.Mappers;

public sealed class InvoiceSummaryMapper : AbstractMapper<InvoiceSummaryModel>
{
    public InvoiceSummaryMapper()
    {
        CreateMap(config =>
        {
            config.AddExpressionMapping();

            config.CreateMap<InvoiceSummaryModel, VwInvoiceSummaryEntity>()
                .ReverseMap();

            config.CreateMap<InvoiceSummaryModel, VwInvoiceSummaryEntity>()
                .ForMember((dest) => dest.InvoiceId, (options) => options.MapFrom((src) => src.Id))
                .ReverseMap();

            config.CreateMap<InvoiceSummaryModel, InvoiceSummaryObject>()
                .ReverseMap();

            config.CreateMap<InvoiceSummaryModel, InvoiceSummaryObject>()
                .ForMember((dest) => dest.PublicId, (options) => options.MapFrom((src) => src.Guid))
                .ReverseMap();
        });
    }
}
