using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OurProjectSmartNeiborhood.Entites;
using SmartNeighborhoodAPI.Entites;

namespace SmartNeighborhoodAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodoController : AppControllerBase
    {
        private readonly ApplicationDbContext _context;

        public TodoController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet("[action]")]
        public async Task<ActionResult<IEnumerable<TodoItem>>> GetTodos()
        {
            var todos = await _context.TodoItems
                .Include(t => t.Category)
                .ToListAsync();

            return Ok(ApiResponse<IEnumerable<TodoItem>>.Success(todos));
        }

        [HttpGet("[action]/{id:int}")]
        public async Task<ActionResult<TodoItem>> GetTodo(int id)
        {
            var todo = await _context.TodoItems
                .Include(t => t.Category)
                .FirstOrDefaultAsync(t => t.Id == id);

            if (todo == null)
                return NotFound(new { message = $"Todo item with ID {id} not found." });

            return Ok(todo);
        }

        [HttpPost("[action]")]
        public async Task<ActionResult<TodoItem>> CreateTodo(TodoItem item)
        {
            var entity = await _context.Categories.FirstOrDefaultAsync(p => p.Id == item.CategoryId);
            if (entity == null)
                return NotFound(ApiResponse<string>.Error(HttpStatusCode.NotFound, "CategoryIds Not Found"));

            await _context.TodoItems.AddAsync(item);
            if (await _context.SaveChangesAsync() > 0)
            {

                return Ok(ApiResponse<TodoItem>.Success(item));
            }

            return BadRequest(ApiResponse<string>.Error(HttpStatusCode.BadRequest, "Failed to add todo"));
        }

        [HttpPut("[action]/{id:int}")]
        public async Task<IActionResult> UpdateTodo(int id, TodoItem updatedItem)
        {
            if (id != updatedItem.Id)
                return NotFound(ApiResponse<string>.Error(HttpStatusCode.BadRequest, "ID in route does not match ID in body."));


            var todo = await _context.TodoItems.FindAsync(id);
            if (todo == null)
                return NotFound(ApiResponse<string>.Error(HttpStatusCode.NotFound, "Todo Not Found"));

            todo.Title = updatedItem.Title;
            todo.Description = updatedItem.Description;
            todo.CategoryId = updatedItem.CategoryId;
            todo.IsCompleted = updatedItem.IsCompleted;

            await _context.SaveChangesAsync();
            return Ok(ApiResponse<string>.Success("Todo item updated successfully."));

        }

        [HttpDelete("[action]/{id:int}")]
        public async Task<IActionResult> DeleteTodo(int id)
        { 
            var entity = await _context.TodoItems.FirstOrDefaultAsync(p => p.Id == id);
            if (entity == null)
                return NotFound(ApiResponse<string>.Error(HttpStatusCode.NotFound, "Person Not Found"));

            _context.TodoItems.Remove(entity);
            await _context.SaveChangesAsync();

            return Ok(ApiResponse<string>.Success());
        }
    }
}
