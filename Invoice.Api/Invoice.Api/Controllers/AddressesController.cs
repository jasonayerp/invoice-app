using Invoice.Api.Domains.Common.Mappers;
using Invoice.Api.Domains.Common.Services;
using Invoice.Api.Mvc;
using Invoice.Api.Mvc.Filters;
using Invoice.Domains.Common.Models;
using Invoice.Domains.Common.Objects;
using Invoice.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace Invoice.Api.Controllers
{
    [Route("api/v1/addresses")]
    [ApiController]
    [ControllerExceptionFilter]
    [ProducesResponseType(typeof(Error), StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(typeof(Error), StatusCodes.Status401Unauthorized)]
    public class AddressesController : ApiControllerBase
    {
        private readonly IAddressService _addressService;
        private readonly IMapper _mapper;

        public AddressesController(IAddressService addressService, IMapper mapper)
        {
            _addressService = addressService;
            _mapper = mapper;
        }

        [HttpPost("create")]
        [Authorize("create:addresses")]
        [ProducesResponseType(typeof(AddressObject), StatusCodes.Status201Created)]
        public async Task<AddressObject> CreateAsync([FromBody] AddressObject address)
        {
            var data = await _addressService.CreateAsync(Map(address));

            return Map(data);
        }

        [HttpGet("read")]
        [Authorize("read:addresses")]
        [ProducesResponseType(typeof(IEnumerable<AddressObject>), StatusCodes.Status200OK)]
        public async Task<IEnumerable<AddressObject>> ReadAsync([FromQuery] int page = 0, [FromQuery] int pageSize = 0, [FromQuery] int count = 0)
        {
            var data = new List<AddressModel>();

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
                data = await _addressService.GetAllAsync();
            }

            return data.Select(Map);
        }

        [HttpGet("read/{id}")]
        [Authorize("read:addresses")]
        [ProducesResponseType(typeof(AddressObject), StatusCodes.Status200OK)]
        public async Task<AddressObject?> ReadAsync([FromRoute] int id)
        {
            var data = await _addressService.GetByIdAsync(id);

            return data != null ? Map(data) : null;
        }

        [HttpPut("update")]
        [Authorize("update:addresses")]
        [ProducesResponseType(typeof(AddressObject), StatusCodes.Status200OK)]
        public async Task<AddressObject> UpdateAsync([FromBody] AddressObject address)
        {
            var data = await _addressService.UpdateAsync(Map(address));

            return Map(data);
        }

        [HttpDelete("delete/{id}")]
        [Authorize("delete:addresses")]
        public async Task<bool> DeleteAsync([FromRoute] int id)
        {
            var address = await _addressService.GetByIdAsync(id);

            if (address != null)
            {
                await _addressService.DeleteAsync(address);
            }

            return address != null;
        }

        private AddressObject Map(AddressModel address)
        {
            var addressObject = _mapper.Map<AddressObject>(address);
            addressObject.PublicId = address.Guid;
            return addressObject;
        }

        private AddressModel Map(AddressObject address)
        {
            var addressObject = _mapper.Map<AddressModel>(address);
            addressObject.Guid = address.PublicId;
            return addressObject;
        }
    }
}
