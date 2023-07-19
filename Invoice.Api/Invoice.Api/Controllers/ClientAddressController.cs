using Invoice.Api.Domains.Common.Mappers;
using Invoice.Api.Domains.Common.Services;
using Invoice.Domains.Common.Models;
using Invoice.Domains.Common.Objects;

namespace Invoice.Api.Controllers;

[ApiController]
[Route("api/v1/addresses")]
[ControllerExceptionFilter]
[ProducesResponseType(typeof(Error), StatusCodes.Status500InternalServerError)]
[ProducesResponseType(typeof(Error), StatusCodes.Status401Unauthorized)]
public class ClientAddressController : ApiControllerBase
{
    private readonly IClientAddressService _addressService;

    public ClientAddressController(IClientAddressService addressService)
    {
        _addressService = addressService;
    }

    [HttpPost("create")]
    [Authorize("create:addresses")]
    [ProducesResponseType(typeof(ClientAddressObject), StatusCodes.Status201Created)]
    public async Task<ClientAddressObject> CreateAsync([FromBody] ClientAddressObject address)
    {
        var data = await _addressService.CreateAsync(Map(address));

        return Map(data);
    }

    [HttpGet("read")]
    //Authorize("read:addresses")]
    [ProducesResponseType(typeof(IEnumerable<ClientAddressObject>), StatusCodes.Status200OK)]
    public async Task<IEnumerable<ClientAddressObject>> ReadAsync([FromQuery] int page = 0, [FromQuery] int pageSize = 0, [FromQuery] int count = 0)
    {
        var data = new List<ClientAddressModel>();

        if (page > 0 && pageSize > 0)
        {
            data = await _addressService.GetPaginatedAsync(page, pageSize);
        }

        else if (count > 0)
        {
            data = await _addressService.GetTopAsync(count);
        }

        else
        {
            data = await _addressService.GetAllAsync(e => e.Id > 0);
        }

        return data.Select(Map);
    }

    [HttpGet("read/{id}")]
    [Authorize("read:addresses")]
    [ProducesResponseType(typeof(ClientAddressObject), StatusCodes.Status200OK)]
    public async Task<ClientAddressObject?> ReadAsync([FromRoute] int id)
    {
        var data = await _addressService.GetByIdAsync(id);

        return data != null ? Map(data) : null;
    }

    [HttpPut("update")]
    [Authorize("update:addresses")]
    [ProducesResponseType(typeof(ClientAddressObject), StatusCodes.Status200OK)]
    public async Task<ClientAddressObject> UpdateAsync([FromBody] ClientAddressObject address)
    {
        var data = await _addressService.UpdateAsync(Map(address));

        return Map(data);
    }

    [HttpDelete("delete/{id}")]
    [Authorize("delete:addresses")]
    [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
    public async Task<bool> DeleteAsync([FromRoute] int id)
    {
        var address = await _addressService.GetByIdAsync(id);

        if (address != null)
        {
            await _addressService.DeleteAsync(address);
        }

        return address != null;
    }

    private ClientAddressModel Map(ClientAddressObject address)
    {
        var mapper = new AddressMapper();

        return mapper.Map<ClientAddressModel>(address);
    }

    private ClientAddressObject Map(ClientAddressModel address)
    {
        var mapper = new AddressMapper();

        return mapper.Map<ClientAddressObject>(address);
    }
}
