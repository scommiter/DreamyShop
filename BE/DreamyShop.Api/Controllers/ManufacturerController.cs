using DreamyShop.Api.Authorization;
using DreamyShop.Common.Constants;
using DreamyShop.Common.Results;
using DreamyShop.Domain.Shared.Dtos;
using DreamyShop.Domain.Shared.Dtos.Manufacturer;
using DreamyShop.Logic.Manufacturer;
using DreamyShop.Repository.Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using AuthorizeAttribute = DreamyShop.Api.Authorization.AuthorizeAttribute;

namespace DreamyShop.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ManufacturerController : Controller
    {
        private readonly IManufacturerLogic _manufacturerService;
        private readonly ILogger<ManufacturerController> _logger;
        private IMemoryCache _cache;
        public ManufacturerController(
            IManufacturerLogic manufacturerService,
            ILogger<ManufacturerController> logger,
            IMemoryCache cache)
        {
            _logger = logger;
            _manufacturerService = manufacturerService;
            _cache = cache;
        }

        [HttpGet("getAll")]
        public async Task<IActionResult> GetAllManufacturer([FromQuery] PagingRequest pagingRequest)
        {
            //if (!_cache.TryGetValue(ConstantCaches.MANUFACTURERCACHES, out List<ManufacturerDto> manufacturers))
            //{
            //    var result = await _manufacturerService.GetAllManufacturer(pagingRequest);
            //    manufacturers = result.Result.Items;
            //    var cacheExpiryOptions = new MemoryCacheEntryOptions
            //    {
            //        AbsoluteExpiration = DateTime.Now.AddSeconds(50),
            //        Priority = CacheItemPriority.High,
            //        SlidingExpiration = TimeSpan.FromSeconds(20)
            //    };
            //    _cache.Set(ConstantCaches.MANUFACTURERCACHES, manufacturers, cacheExpiryOptions);
            //}
            var _manufacturerCache = new CacheHelper<ApiResult<PageResult<ManufacturerDto>>>(_cache);
            var result = await _manufacturerCache.GetOrCreate(ConstantCaches.MANUFACTURERCACHES, async () => await _manufacturerService.GetAllManufacturer(pagingRequest));
            //var result = await _manufacturerService.GetAllManufacturer(pagingRequest, _cache);
            return Ok(result);
        }

        [HttpPost("create")]
        [Authorize]
        [Member]
        public async Task<IActionResult> CreateManufacturer([FromForm] ManufacturerCreateUpdateDto manufacturerCreateUpdateDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _manufacturerService.CreateManufacturer(manufacturerCreateUpdateDto);
            return Ok(result);
        }

        [HttpPut("updateManufacturer")]
        [Authorize]
        [Member]
        public async Task<IActionResult> UpdateManufacturer(int id, [FromForm] ManufacturerCreateUpdateDto manufacturerCreateUpdateDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _manufacturerService.UpdateManufacturer(id, manufacturerCreateUpdateDto);
            return Ok(result);
        }

        [HttpDelete("removeManufacturer")]
        [Authorize]
        [Member]
        public async Task<IActionResult> RemoveManufacturer(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _manufacturerService.RemoveManufacturer(id);
            return Ok(result);
        }
    }
}
