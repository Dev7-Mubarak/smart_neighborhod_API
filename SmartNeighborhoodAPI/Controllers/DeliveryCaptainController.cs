using Microsoft.AspNetCore.Mvc;
using SmartNeighborhoodAPI.Entites;

namespace SmartNeighborhoodAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DeliveryCaptainController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public DeliveryCaptainController(ApplicationDbContext context)
        {
            _context = context;
        }

                private IActionResult Response(
            object? data,
            bool success,
            int statusCode,
            string message = "",
            string messageAr = "",
            string? supabaseMessage = null)
        {
            return StatusCode(statusCode, new
            {
                success,
                status_code = statusCode,
                message,
                message_ar = messageAr,
                supabase_message = supabaseMessage,
                data
            });
        }

        [HttpGet("[action]")]
        public IActionResult GetAll()
        {
            var captains = _context.DeliveryCaptains.ToList();
            return Response(
                captains,
                success: true,
                statusCode: 200,
                message: captains.Count > 0 ? "DeliveryCaptains found." : "No DeliveryCaptains found.",
                messageAr: captains.Count > 0 ? "تم العثور على الكباتن." : "لم يتم العثور على كباتن.",
                supabaseMessage: null
            );
        }

        [HttpGet("{id}")]
        public IActionResult GetById(string id)
        {
            var deliveryCaptain = _context.DeliveryCaptains.FirstOrDefault(dc => dc.Id == id);
            if (deliveryCaptain == null)
            {
                return Response(
                    null,
                    false,
                    404,
                    $"DeliveryCaptain with id {id} not found.",
                    $"لم يتم العثور على الكابتن بالمعرف {id}.",
                    null
                );
            }
            return Response(
                deliveryCaptain,
                true,
                200,
                "DeliveryCaptain found.",
                "تم العثور على الكابتن.",
                null
            );
        }

        [HttpPost("[action]")]
        public IActionResult Create([FromBody] DeliveryCaptain deliveryCaptain)
        {
            if (deliveryCaptain == null)
            {
                return Response(
                    null,
                    false,
                    400,
                    "DeliveryCaptain is null.",
                    "الكابتن غير موجود.",
                    null
                );
            }

            deliveryCaptain.Id = Guid.NewGuid().ToString();
            _context.DeliveryCaptains.Add(deliveryCaptain);
            _context.SaveChanges();

            return Response(
                deliveryCaptain,
                true,
                201,
                "DeliveryCaptain created successfully.",
                "تم إنشاء الكابتن بنجاح.",
                null
            );
        }

        [HttpPut("{id}")]
        public IActionResult Update(string id, [FromBody] DeliveryCaptain updatedCaptain)
        {
            var existingCaptain = _context.DeliveryCaptains.FirstOrDefault(dc => dc.Id == id);
            if (existingCaptain == null)
            {
                return Response(
                    null,
                    false,
                    404,
                    $"DeliveryCaptain with id {id} not found.",
                    $"لم يتم العثور على الكابتن بالمعرف {id}.",
                    null
                );
            }

            existingCaptain.FullName = updatedCaptain.FullName;
            existingCaptain.PhoneNumber = updatedCaptain.PhoneNumber;
            existingCaptain.FinancialAccountName = updatedCaptain.FinancialAccountName;
            existingCaptain.FinancialAccountNumber = updatedCaptain.FinancialAccountNumber;

            _context.DeliveryCaptains.Update(existingCaptain);
            _context.SaveChanges();

            return Response(
                existingCaptain,
                true,
                200,
                "DeliveryCaptain updated successfully.",
                "تم تحديث الكابتن بنجاح.",
                null
            );
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(string id)
        {
            var existingCaptain = _context.DeliveryCaptains.FirstOrDefault(dc => dc.Id == id);
            if (existingCaptain == null)
            {
                return Response(
                    null,
                    false,
                    404,
                    $"DeliveryCaptain with id {id} not found.",
                    $"لم يتم العثور على الكابتن بالمعرف {id}.",
                    null
                );
            }

            _context.DeliveryCaptains.Remove(existingCaptain);
            _context.SaveChanges();

            return Response(
                null,
                true,
                200,
                "DeliveryCaptain deleted successfully.",
                "تم حذف الكابتن بنجاح.",
                null
            );
        }
    }
}
