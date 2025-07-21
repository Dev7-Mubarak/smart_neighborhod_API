using SmartNeighborhoodAPI.Helpers.DTOs.Auth;
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
        public async Task<IActionResult> Register(RegisterDto register)
        {
            return Response(await _authService.RegisterAsync(register));
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> Login(LoginDto loginDto)
        {
            return Response(await _authService.LoginAsync(loginDto));
        }


        [HttpPost("[action]")]
        public async Task<IActionResult> ConfirmEmailOtp([FromBody] ConfirmEmailOtpDto emailOtpDto)
        {
            return Response(await _authService.ConfirmEmailOtp(emailOtpDto));
        }
        [HttpPost("forgot-password")]
        public async Task<IActionResult> ForgotPassword([FromBody] ForgotPasswordDto dto)
        {
            var result = await _authService.ForgotPasswordAsync(dto);
            return Response(result);
        }

        [HttpPost("verify-reset-code")]
        public async Task<IActionResult> VerifyResetCodeAndResetPassword([FromBody] ResetPasswordWithCodeDto model)
        {
            var result = await _authService.VerifyResetCodeAndResetPasswordAsync(model);
            return Response(result); 
        }

    }
}
