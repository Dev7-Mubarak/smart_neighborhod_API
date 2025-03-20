using SmartNeighborhoodAPI.Interfaces;

namespace SmartNeighborhoodAPI.Controllers
{
    public class AuthController : AppControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> Login(LoginDto loginDto)
        {
            return Response(await _authService.LoginAsync(loginDto));
        }

    }
}
