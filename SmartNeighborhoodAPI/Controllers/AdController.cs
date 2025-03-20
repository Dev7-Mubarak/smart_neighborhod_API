using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SmartNeighborhoodAPI.Controllers
{
    public class AdController : AppControllerBase
    {
        private readonly AdsService _AdsService;

        public AdController(AdsService adsService)
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
