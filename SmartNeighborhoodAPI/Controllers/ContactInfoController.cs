//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Mvc;

//namespace SmartNeighborhoodAPI.Controllers
//{
//    [Route("api/[controller]")]
//    [ApiController]
//    public class ContactInfoController : ControllerBase
//    {
//        private readonly ContactService _ContactService;
        


//        public ContactInfoController(ContactService ContactService)
//        {
//            _ContactService = ContactService;
//        }
//        [HttpPost("[action]")]
//        public async Task<IActionResult> AddAsync(ContactInfoDto ContactInfoDto)
//        {

//            if (!ModelState.IsValid)
//                return BadRequest(ValidationHelper.CreateErrorResponse(ModelState));
//            var result = await _ContactService.AddAsync(ContactInfoDto);
//            if (result.IsSuccess)
//                return Ok(result);
//            return BadRequest(result);
//        }
//        [HttpGet("[action]")]
//        public async Task<IActionResult> GetAllAsync()
//        {
//            var result = await _ContactService.GetAll();
//            if (result.IsSuccess)
//                return Ok(result);
//            return BadRequest(result);
//        }
//        [HttpGet("[action]/{id:int}")]
//        public async Task<IActionResult> GetByIdAsync(int id, ContactInfoDto ContactInfoDto)
//        {
//            var result = await _ContactService.GetByIdAsync(id);
//            if (result.IsSuccess)
//                return Ok(result);
//            return BadRequest(result);



//        }
//        [HttpPut("[action]/{id:int}")]
//        public async Task<IActionResult> UpdateAsync(int id, ContactInfoDto ContactInfoDto)
//        {
//            if (!ModelState.IsValid)
//                return BadRequest(ValidationHelper.CreateErrorResponse(ModelState));

//            var result = await _ContactService.UpdateAsync(id, ContactInfoDto);
//            if (result.IsSuccess)
//                return Ok(result);
//            return BadRequest(result);
//        }
//        [HttpDelete("[action]/{id:int}")]
//        public async Task<IActionResult> DeleteAsync(int id)
//        {
//            var result = await _ContactService.DeleteAsync(id);
//            if (result.IsSuccess)
//                return Ok(result);
//            return BadRequest(result);
//        }
//    }
//}
