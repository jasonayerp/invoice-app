using Invoice.Api.Domains.Common.Mappers;
using Invoice.Api.Domains.Common.Services;
using Invoice.Api.Mvc;
using Invoice.Api.Mvc.Filters;
using Invoice.Domains.Common.Models;
using Invoice.Domains.Common.Objects;
using Invoice.Mvc;

namespace Invoice.Api.Controllers
{
    [Route("api/v1")]
    [ApiController]
    [ControllerExceptionFilter]
    public class AddressesController : ApiControllerBase
    {
        private readonly IAddressService _addressService;
        private readonly IMapper _mapper;

        public AddressesController(IAddressService addressService, IMapper mapper)
        {
            _addressService = addressService;
            _mapper = mapper;
        }

        [HttpPost]
        [Route("addresses")]
        public async Task<IHttpResult<AddressObject>> CreateAsync([FromBody] AddressObject address)
        {
            var data = await _addressService.CreateAsync(Map(address));

            return Success(Map(data));
        }

        [HttpGet]
        [Route("addresses")]
        public async Task<IHttpCollectionResult<AddressObject>> ReadAsync([FromQuery] int page = 0, [FromQuery] int pageSize = 0, [FromQuery] int count = 0)
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

            return SuccessList(data.Select(Map).ToList());
        }

        [HttpGet]
        [Route("addresses/{id}")]
        public async Task<IHttpResult<AddressObject>> ReadAsync([FromRoute] int id)
        {
            var data = await _addressService.GetByIdAsync(id);

            return Success(data != null ? Map(data) : null);
        }

        [HttpPatch]
        [Route("addresses")]
        public async Task<IHttpResult<AddressObject>> UpdateAsync([FromBody] AddressObject address)
        {
            var data = await _addressService.UpdateAsync(Map(address));

            return Success(Map(data));
        }

        [HttpDelete]
        [Route("addresses/{id}")]
        public async Task<IHttpResult<bool>> DeleteAsync([FromRoute] int id)
        {
            var address = await _addressService.GetByIdAsync(id);

            if (address != null)
            {
                await _addressService.DeleteAsync(address);
            }

            return Success(address != null);
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
