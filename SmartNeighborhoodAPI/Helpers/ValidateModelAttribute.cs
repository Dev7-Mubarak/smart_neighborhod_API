using Microsoft.AspNetCore.Mvc.Filters;
using System.Net;

namespace SmartNeighborhoodAPI.Helpers
{
    public class ValidateActionFilterAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.ModelState.IsValid)
            {
                var errors = new List<ErrorDetails>();

                foreach (var state in context.ModelState)
                {
                    foreach (var error in state.Value.Errors)
                    {
                        errors.Add(new ErrorDetails(state.Key, error.ErrorMessage));
                    }
                }

                ApiResponse<List<ErrorDetails>> errorResponse = new ApiResponse<List<ErrorDetails>>
                {
                    StatusCode = HttpStatusCode.BadRequest,
                    Message = "Validation errors occurred.",
                    Errors = errors
                };

                context.Result = new BadRequestObjectResult(errorResponse);
            }
        }
    }
}
