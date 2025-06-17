using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SmartNeighborhoodAPI.Services.V1;

namespace SmartNeighborhoodAPI.Controllers.V1
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]

    public class AdController : AppControllerBase
    {
        private readonly AdsV1Service _AdsService;

        public AdController(AdsV1Service adsService)
        {
            _AdsService = adsService;
        }
        [HttpPost("[action]")]
        public async Task<IActionResult> AddAsync(AdDto AdDto)
        {
            var result = await _AdsService.AddAsync(AdDto);

            return Response(result);
        }
        [HttpGet("[action]")]
        public async Task<IActionResult> GetAllAsync()
        {
            var result = await _AdsService.GetAll();

            return Response(result);
        }
        [HttpGet("[action]/{id:int}")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            var result = await _AdsService.GetByIdAsync(id);

            return Response(result);



        }
        [HttpPut("[action]/{id:int}")]
        public async Task<IActionResult> UpdateAsync(int id, AdDto AdDto)
        {

            var result = await _AdsService.UpdateAsync(id, AdDto);

            return Response(result);
        }
        [HttpDelete("[action]/{id:int}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var result = await _AdsService.DeleteAsync(id);

            return Response(result);
        }

    }
}
