﻿using Invoice.Domains.Common.Models;
using Invoice.Mapper;

namespace Invoice.Api.Domains.Common.Mappers;

public sealed class InvoiceMapper : AbstractMapper<ClientAddressModel>
{
    public InvoiceMapper()
    {
        CreateMap(config =>
        {
            config.CreateMap<InvoiceModel, InvoiceEntity>().ReverseMap();

            config.CreateMap<InvoiceModel, InvoiceEntity>()
                .ForMember(dest => dest.Date, opts =>
                {
                    opts.MapFrom(src => src.Date);
                })
                .ReverseMap();
        });
    }
}
