using System.Security.Claims;

namespace SmartNeighborhoodAPI.Services
{
    public class UserContextService 
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserContextService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public string? GetUserId()
        {
            return _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        }

        public string? GetUserEmail()
        {
            return _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.Email)?.Value;
        }

        public string? GetUserRoles()
        {
            return _httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.Role);
        }
    }
}
