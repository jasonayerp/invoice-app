using Invoice.Api.Domains.Common.Mappers;
using Invoice.Api.Domains.Common.Services;
using Invoice.Api.Mvc;
using Invoice.Api.Mvc.Filters;
using Invoice.Api.Objects;
using Invoice.Domains.Common.Models;
using Invoice.Mvc;

namespace Invoice.Api.Controllers
{
    [Route("api/[controller]")]
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

        [HttpGet]
        public async Task<IHttpListResult<AddressObject>> GetAllAsync([FromQuery] int page = 0, [FromQuery] int pageSize = 0, [FromQuery] int count = 0)
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

        private AddressObject Map(AddressModel address)
        {
            var addressObject = _mapper.Map<AddressObject>(address);
            addressObject.PublicId = Guid.NewGuid();
            addressObject.Metadata = new Metadata
            {
                Id = address.Id,
                UtcCreatedDate = address.UtcCreatedDate,
                UtcUpdatedDate = address.UtcUpdatedDate,
                UtcDeletedDate = address.UtcDeletedDate
            };
            return addressObject;
        }
    }
}
