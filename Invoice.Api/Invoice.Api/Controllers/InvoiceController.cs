using Invoice.Api.Domains.Common.Services;
using Invoice.Domains.Common.Models;
using Invoice.Domains.Common.Objects;

namespace Invoice.Api.Controllers;

[ApiController]
[Route("api/v1/invoicesummaries")]
[ControllerExceptionFilter]
[ProducesResponseType(typeof(Error), StatusCodes.Status500InternalServerError)]
[ProducesResponseType(typeof(Error), StatusCodes.Status401Unauthorized)]
public class InvoiceController : ApiControllerBase
{

    [HttpGet("read")]
    public async Task<List<InvoiceSummaryObject>> ReadAsync()
    {
        return new List<InvoiceSummaryObject>();
    }
}
