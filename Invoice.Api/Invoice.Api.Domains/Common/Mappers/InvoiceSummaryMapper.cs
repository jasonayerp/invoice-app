using Invoice.Api.Data.Entities;
using Invoice.Domains.Common.Models;

namespace Invoice.Api.Domains.Common.Mappers;

public sealed class InvoiceSummaryMapper : AbstractMapper<InvoiceSummaryModel>
{
    public InvoiceSummaryMapper()
    {
        CreateMap(config =>
        {
            config.CreateMap<InvoiceSummaryModel, VwInvoiceSummaryEntity>()
                .ReverseMap();
        });
    }
}
