namespace SmartNeighborhoodAPI.Controllers.V1
{
    [Route("api/[controller]")]
    [ApiController]
    [ApiVersion("1.0")]
    //[EnableRateLimiting("fixed-window")]
    public class GroupsController : AppControllerBase
    {
        private readonly GroupService _GroupService;



        public GroupsController(GroupService GroupService)
        {
            _GroupService = GroupService;


        }
        [HttpPost("[action]")]
        public async Task<IActionResult> AddAsync(GroupDto GroupDto)
        {
            var result = await _GroupService.AddAsync(GroupDto);
            return Response(result);
        }
        [HttpGet("get-all")]
        public async Task<IActionResult> GetAllAsync()
        {
            var result = await _GroupService.GetAll();

            return Response(result);
        }
        [HttpGet("get-by-id/{id:int}")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            var result = await _GroupService.GetByIdAsync(id);

            return Response(result);



        }
        [HttpPut("[action]/{id:int}")]
        public async Task<IActionResult> UpdateAsync(int id, GroupDto GroupDto)
        {
            var result = await _GroupService.UpdateAsync(id, GroupDto);

            return Response(result);
        }
        [HttpDelete("[action]/{id:int}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var result = await _GroupService.DeleteAsync(id);

            return Response(result);
        }
    }
}
