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

        public CurrentUserDto GetCurrentUser()
        {
            var httpContext = _httpContextAccessor.HttpContext
                ?? throw new InvalidOperationException("HTTP context is not available.");

            var user = httpContext.User;

            if (user?.Identity == null || !user.Identity.IsAuthenticated)
                throw new InvalidOperationException("User is not authenticated.");

            var id = user.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var email = user.FindFirst(ClaimTypes.Email)?.Value;
            var role = user.FindFirst(ClaimTypes.Role)?.Value;

            if (string.IsNullOrWhiteSpace(id))
                throw new InvalidOperationException("User identifier claim is missing.");

            if (string.IsNullOrWhiteSpace(role))
                throw new InvalidOperationException("User role claim is missing.");

            return new CurrentUserDto
            {
                Id = id,
                Email = email ?? string.Empty,
                Role = role
            };
        }
    }
}
