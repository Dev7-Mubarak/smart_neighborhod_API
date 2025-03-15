using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using SmartNeighborhoodAPI.Entites;
using SmartNeighborhoodAPI.Interfaces;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Text;

namespace SmartNeighborhoodAPI.Services
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly JWT _jwt;

        public AuthService(UserManager<AppUser> userManager, IOptions<JWT> jwt)
        {
            _userManager = userManager;
            _jwt = jwt.Value;
        }

        public async Task<ApiResponse<UserResponse>> LoginAsync(LoginDto loginDto)
        {
            var user = await _userManager.FindByEmailAsync(loginDto.Email);

            if (user is null || !await _userManager.CheckPasswordAsync(user, loginDto.Password))
            {
                return ApiResponse<UserResponse>.Error(HttpStatusCode.BadRequest, "Email or Password is incorrect!");
            }

            var jwtSecurityToken = await CreateJwtToken(user);

            UserResponse userResponse = new UserResponse
            {
                Email = user.Email,
                Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken),
            };

            return ApiResponse<UserResponse>.Success(userResponse, "User Login successfully.");
        }

        public async Task<ApiResponse<UserResponse>> RegisterAsync(RegisterDto registerDto)
        {
            if (await _userManager.FindByEmailAsync(registerDto.Email) is not null)
                return ApiResponse<UserResponse>.Error(HttpStatusCode.Conflict, "User with this email already exists.");

            AppUser user = new()
            {
                Email = registerDto.Email,
                UserName = registerDto.Email,
            };

            IdentityResult? result = await _userManager.CreateAsync(user, registerDto.Password);

            if (!result.Succeeded)
            {
                return ApiResponse<UserResponse>.Error(HttpStatusCode.BadRequest, "Error acourse will create the user");
            }

            var jwtSecurityToken = await CreateJwtToken(user);

            UserResponse userResponse = new UserResponse
            {
                Email = user.Email,
                ExpireOn = jwtSecurityToken.ValidTo,
                Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken)
            };
            return ApiResponse<UserResponse>.Success(userResponse, "User registered successfully.");
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
    }
}
