using Microsoft.AspNetCore.Authorization;
using SmartNeighborhoodAPI.AppMetaData;
using Microsoft.AspNetCore.Mvc;
using SmartNeighborhoodAPI.Helpers.DTOs.Auth;
using SmartNeighborhoodAPI.Interfaces;
using Swashbuckle.AspNetCore.Annotations;
using System.Net;
namespace SmartNeighborhoodAPI.Controllers.V1
{
    [AllowAnonymous]

    [SwaggerTag("Authentication endpoints for login, password reset, and email confirmation")]
    [Route(Router.Auth.Prefix)]

    public class AuthController : AppControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost(Router.Auth.Login)]
        [SwaggerOperation(Summary = "User login", Description = "Authenticates a user and returns a JWT token.")]
        [ProducesResponseType(typeof(UserResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Login([FromBody, SwaggerParameter("User login credentials", Required = true)] LoginDto loginDto)
        {
            return Response(await _authService.LoginAsync(loginDto));
        }

        [HttpPost(Router.Auth.PasswordReset.SendCode)]
        [SwaggerOperation(Summary = "Send password reset code", Description = "Sends a password reset verification code to the user's email.")]
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> ForgotPassword([FromBody, SwaggerParameter("Email for which to send reset code", Required = true)] ForgotPasswordDto dto)
        {
            return Response(await _authService.SendResetCodeAsync(dto));
        }

        [HttpPost(Router.Auth.PasswordReset.VerifyCode)]
        [SwaggerOperation(Summary = "Verify password reset code", Description = "Verifies the password reset code sent to the user's email.")]
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> VerifyResetCode([FromBody, SwaggerParameter("Reset code verification data", Required = true)] VerifyResetCodeDto dto)
        {
            return Response(await _authService.VerifyResetCodeAsync(dto));
        }

        [HttpPost(Router.Auth.PasswordReset.Confirm)]
        [SwaggerOperation(Summary = "Reset password", Description = "Resets the password for the user after verifying the reset code.")]
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> ResetPassword([FromBody, SwaggerParameter("Password reset data", Required = true)] ResetPasswordDto dto)
        {
            return Response(await _authService.ResetPasswordAsync(dto));
        }

        [HttpPost(Router.Auth.Register)]
        [SwaggerOperation(Summary = "Register new user", Description = "Registers a new user account.")]
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Register([FromBody, SwaggerParameter("User registration data", Required = true)] RegisterDto dto)
        {
            return Response(await _authService.RegisterAsync(dto));
        }

        //[HttpPost(Router.Auth.ConfirmEmailOtp)]
        //[SwaggerOperation(Summary = "Confirm email OTP", Description = "Confirms a user's email address using the OTP sent to their email.")]
        //[ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        //[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        //[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        //public async Task<IActionResult> ConfirmEmailOtp([FromBody, SwaggerParameter("Email confirmation OTP data", Required = true)] ConfirmEmailOtpDto dto)
        //{
        //    return Response(await _authService.ConfirmEmailOtp(dto));
        //}

    }
}
