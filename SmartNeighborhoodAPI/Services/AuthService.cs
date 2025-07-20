using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Text;
using System.Web;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using SmartNeighborhoodAPI.Entites;
using SmartNeighborhoodAPI.Helpers.DTOs.Auth;
using SmartNeighborhoodAPI.Helpers.DTOs.block;
using SmartNeighborhoodAPI.Interfaces;

namespace SmartNeighborhoodAPI.Services
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly IEmailSender _emailSender;
        private readonly JWT _jwt;
        private readonly ILogger<AuthService> _logger;


        public AuthService(UserManager<AppUser> userManager, IOptions<JWT> jwt, SignInManager<AppUser> signInManager, IEmailSender emailSender, ILogger<AuthService> logger)
        {
            _userManager = userManager;
            _jwt = jwt.Value;
            _signInManager = signInManager;
            _emailSender = emailSender;
            _logger = logger;
        }


        public async Task<ApiResponse<UserResponse>> LoginAsync(LoginDto loginDto)
        {
            var user = await _userManager.FindByEmailAsync(loginDto.Email);

            if (user == null)
                return ApiResponse<UserResponse>.Error(HttpStatusCode.NotFound, "User not found.");

            var result = await _signInManager.CheckPasswordSignInAsync(user, loginDto.Password, false);

            if (!result.Succeeded)
                return ApiResponse<UserResponse>.Error(HttpStatusCode.BadRequest, "Invalid password.");

            if (!user.EmailConfirmed)
            {
                return ApiResponse<UserResponse>.Error(HttpStatusCode.Forbidden, "Email not confirmed. Please enter the OTP sent to your email.");
            }

            var jwtSecurityToken = await CreateJwtToken(user);

            UserResponse userResponse = new UserResponse
            {
                Id = user.Id,
                Email = loginDto.Email,
                Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken),
            };

            return ApiResponse<UserResponse>.Success(userResponse, "User login successful.");
        }


        public async Task<ApiResponse<UserResponse>> CreateBlockManagerAccountAsync(CreateBlockManagerDto dto)
        {
            _logger.LogInformation("Attempting to create a Block Manager for email: {Email}", dto.Email);

            if (await _userManager.FindByNameAsync(dto.Email) is not null)
            {
                _logger.LogWarning("User creation failed: Email {Email} already exists.", dto.Email);
                return ApiResponse<UserResponse>.Error(HttpStatusCode.Conflict, "Email already exists.");
            }

            AppUser user = new()
            {
                Email = dto.Email,
                UserName = dto.Email,
                PersonId = dto.PersonId,
                IsActive = false,
            };

            var result = await _userManager.CreateAsync(user, dto.Password);

            if (!result.Succeeded)
            {
                List<ErrorDetails> errors = result.Errors.Select(e => new ErrorDetails
                {
                    Field = e.Code,
                    ErrorMessage = e.Description
                }).ToList();

                _logger.LogError("User creation failed for email {Email}. Errors: {@Errors}", dto.Email, errors);
                return ApiResponse<UserResponse>.Error(HttpStatusCode.BadRequest, "An error occurred while creating the user.", errors);
            }

            var otp = new Random().Next(100000, 999999).ToString();
            user.EmailConfirmationCode = otp;
            user.EmailConfirmationCodeExpiresAt = DateTime.UtcNow.AddHours(1);

            await _userManager.UpdateAsync(user);

            await _emailSender.SendEmailAsync(user.Email, "Email Confirmation Code",
                $"Hello,<br/><br/>Your email confirmation code is: <strong>{otp}</strong><br/>This code will expire in 1 Hour.");

            _logger.LogInformation("User created successfully: {Email}. OTP sent to email.", user.Email);

            UserResponse userResponse = new UserResponse
            {
                Id = user.Id,
                Email = user.Email,
            };

            return ApiResponse<UserResponse>.Success(userResponse, "User registered successfully. OTP sent to email.");
        }

        public async Task<ApiResponse<UserResponse>> DeleteBlockManagerAccountByIdAsync(string managerId)
        {
            _logger.LogInformation("Deleting Block Manager with ID: {ManagerId}", managerId);

            var user = await _userManager.FindByIdAsync(managerId);
            if (user == null)
            {
                _logger.LogWarning("Block Manager with ID '{ManagerId}' not found.", managerId);
                return ApiResponse<UserResponse>.Error(HttpStatusCode.NotFound, "User not found.");
            }

            var deletionResult = await _userManager.DeleteAsync(user);
            if (!deletionResult.Succeeded)
            {
                _logger.LogError("Failed to delete Block Manager with ID: {ManagerId}. Errors: {Errors}",
                                 managerId, string.Join(", ", deletionResult.Errors.Select(e => e.Description)));
                return ApiResponse<UserResponse>.Error(HttpStatusCode.InternalServerError, "Failed to delete user.");
            }

            var userResponse = new UserResponse
            {
                Id = user.Id,
                Email = user.Email,
            };

            _logger.LogInformation("Successfully deleted Block Manager with ID: {ManagerId}", managerId);

            return ApiResponse<UserResponse>.Success(userResponse);
        }


        public async Task<ApiResponse<UserResponse>> ConfirmEmailOtp(ConfirmEmailOtpDto emailOtpDto)
        {
            var user = await _userManager.FindByEmailAsync(emailOtpDto.Email);
            if (user == null)
                return ApiResponse<UserResponse>.Error(HttpStatusCode.NotFound, "User not found.");

            if (user.EmailConfirmed)
                return ApiResponse<UserResponse>.Error(HttpStatusCode.BadRequest, "Email is already confirmed.");

            if (user.EmailConfirmationCode == emailOtpDto.Code && user.EmailConfirmationCodeExpiresAt > DateTime.UtcNow)
            {
                user.EmailConfirmed = true;
                user.EmailConfirmationCode = null;
                user.EmailConfirmationCodeExpiresAt = null;

                var updateResult = await _userManager.UpdateAsync(user);
                if (!updateResult.Succeeded)
                    return ApiResponse<UserResponse>.Error(HttpStatusCode.InternalServerError);

                return ApiResponse<UserResponse>.Success(null, "Email confirmed successfully.");
            }

            return ApiResponse<UserResponse>.Error(HttpStatusCode.BadRequest, "Invalid or expired confirmation code.");
        }
        private async Task<JwtSecurityToken> CreateJwtToken(AppUser user)
        {
            var userClaims = await _userManager.GetClaimsAsync(user);
            var roles = await _userManager.GetRolesAsync(user);
            var roleClaims = new List<Claim>();

            foreach (var role in roles)
                roleClaims.Add(new Claim("roles", role));

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim("uid", user.Id)
            }
            .Union(userClaims)
            .Union(roleClaims);

            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwt.signingKey));
            var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

            var jwtSecurityToken = new JwtSecurityToken(
                issuer: _jwt.Issuer,
                audience: _jwt.Audience,
                claims: claims,
                expires: DateTime.UtcNow.AddDays(_jwt.DurationInDays),
                signingCredentials: signingCredentials);

            return jwtSecurityToken;
        }

        public async Task<ApiResponse<string>> ForgotPasswordAsync(ForgotPasswordDto model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);

            if (user == null)
            {
                return ApiResponse<string>.Error(HttpStatusCode.NotFound, "User not found.");
            }

            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            var resetLink = $"https://SmartNebourhood.com/reset-password?userId={user.Id}&token={HttpUtility.UrlEncode(token)}";

            await _emailSender.SendEmailAsync(
                model.Email,
                "Reset Your Password",
                $"Click <a href='{resetLink}'>here</a> to reset your password.");

            return ApiResponse<string>.Success("Password reset link sent to your email.");
        }

        public Task<ApiResponse<string>> ResetPasswordAsync(ResetPasswordDto model)
        {
            var user = _userManager.FindByIdAsync(model.UserId);
            if (user == null)
            {
                return Task.FromResult(ApiResponse<string>.Error(HttpStatusCode.NotFound, "User not found."));
            }
            var resetResult = _userManager.ResetPasswordAsync(user.Result, model.Token, model.NewPassword);
            if (!resetResult.Result.Succeeded)
            {
                return Task.FromResult(ApiResponse<string>.Error(HttpStatusCode.BadRequest, "Failed to reset password."));
            }
            return Task.FromResult(ApiResponse<string>.Success("Password reset successfully."));

        }
    }
}
