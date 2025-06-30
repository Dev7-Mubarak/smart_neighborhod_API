namespace SmartNeighborhoodAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConfilctPartyController : AppControllerBase
    {
        private readonly ConfilctPartyService _ConfilctPartyService;


        public ConfilctPartyController(ConfilctPartyService ConfilctPartyService)
        {
            _ConfilctPartyService = ConfilctPartyService;

        }

        //[HttpPost("AddFirstParty")]
        //public async Task<IActionResult> AddFirstParty([FromForm] AddConfilctPartyDto dto)
        //{
        //    var result = await _ConfilctPartyService.AddFirstPartyAsync(dto);
        //    return Response(result);
        //}

        //[HttpPost("AddSecondParty")]
        //public async Task<IActionResult> AddSecondParty([FromForm] AddConfilctPartyDto dto)
        //{
        //    var result = await _ConfilctPartyService.AddSecondPartyAsync(dto);
        //    return Response(result);
        //}


        [HttpGet("[action]")]
        public async Task<IActionResult> GetAllAsync()
        {
            var result = await _ConfilctPartyService.GetAll();

            return Response(result);
        }
        [HttpGet("[action]/{id:int}")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            var result = await _ConfilctPartyService.GetByIdAsync(id);

            return Response(result);



        }
        [HttpPut("[action]/{id:int}")]
        public async Task<IActionResult> UpdateAsync(int id, AddConfilctPartyDto ConfilctPartyDto)
        {

            var result = await _ConfilctPartyService.UpdateAsync(id, ConfilctPartyDto);

            return Response(result);
        }
        [HttpDelete("[action]/{id:int}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var result = await _ConfilctPartyService.DeleteAsync(id);

            return Response(result);
        }


    }
}
