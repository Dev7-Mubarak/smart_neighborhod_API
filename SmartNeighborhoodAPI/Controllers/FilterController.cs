using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace SmartNeighborhoodAPI.Controllers
{
    public class FilterController : Controller
    {
        private readonly ApplicationDbContext _context;
        public FilterController(ApplicationDbContext context)
        {
            _context = context;
        }
        [HttpGet("filter")]
        public IActionResult FilterFimlies([FromQuery] string? name)
        {
            var families = _context.FamilyCatgories.AsQueryable();

            if (!string.IsNullOrEmpty(name))
                families = families.Where(u => u.Name.Contains(name));

            
            return Ok(families.ToList());
        }
}
