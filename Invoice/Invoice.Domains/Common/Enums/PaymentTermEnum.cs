using System.ComponentModel;

namespace Invoice.Domains.Common.Enums;

public enum PaymentTermEnum
{
    [Description("Payment in Advance")]
    PIA,
    [Description("Net 7")]
    Net7,
    [Description("Net 10")]
    Net10,
    [Description("Net 15")]
    Net15,
    [Description("Net 30")]
    Net30,
    [Description("Net 60")]
    Net60,
    [Description("Net 90")]
    Net90,
    [Description("End of month")]
    EOM,
    [Description("21 MFI")]
    TwentyFirstMFI,
    [Description("Cash on delivery")]
    COD,
    [Description("Cash next delivery")]
    CND,
    [Description("Cash before shipment")]
    CBS,
    [Description("Cash in advance")]
    CIA,
    [Description("Cash with order")]
    CWO,
    [Description("1MD")]
    OneMD,
    [Description("2MD")]
    TwoMD,
    [Description("Stage payments")]
    StagePayments,
    [Description("Forward dating")]
    ForwardDating,
    [Description("Accumulation discounts")]
    AccumulationDiscounts,
    [Description("Partial payment discount")]
    PartialPaymentDiscount,
    [Description("Rebate")]
    Rebate,
    [Description("Contra")]
    Contra
}
