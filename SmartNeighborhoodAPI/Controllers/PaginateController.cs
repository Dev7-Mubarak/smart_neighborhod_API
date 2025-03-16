using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace SmartNeighborhoodAPI.Controllers
{
    public class PaginateController : Controller
    {
        private readonly ApplicationDbContext _context;
        public PaginateController(ApplicationDbContext context)
        {
            _context = context;
        }
        [HttpGet("paginate")]
        public IActionResult GetUsersPaginated([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        {
            var users = _context.Users.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();
            return Ok(users);
        }
    }
}
