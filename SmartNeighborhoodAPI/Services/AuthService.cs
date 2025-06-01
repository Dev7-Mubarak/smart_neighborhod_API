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
        private readonly SignInManager<AppUser> _signInManager;
        private readonly JWT _jwt;

        public AuthService(UserManager<AppUser> userManager, IOptions<JWT> jwt, SignInManager<AppUser> signInManager)
        {
            _userManager = userManager;
            _jwt = jwt.Value;
            _signInManager = signInManager;
        }

        public async Task<ApiResponse<UserResponse>> LoginAsync(LoginDto loginDto)
        {
            var user = await _userManager.FindByNameAsync(loginDto.UserNameOrEmail);

            if (user == null)
            {
                user = await _userManager.FindByEmailAsync(loginDto.UserNameOrEmail);
            }

            if (user == null)
            {
                return ApiResponse<UserResponse>.Error(HttpStatusCode.Unauthorized, "Invalid username or email.");
            }

            var result = await _signInManager.CheckPasswordSignInAsync(user, loginDto.Password, false);


            if (!result.Succeeded)
            {
                return ApiResponse<UserResponse>.Error(HttpStatusCode.BadRequest, "Invalid password.");

            }

            //var jwtSecurityToken = await CreateJwtToken(user);

            UserResponse userResponse = new UserResponse
            {
                Id = user.Id,
                UserNameOrEmail = loginDto.UserNameOrEmail,
                //Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken),
            };

            return ApiResponse<UserResponse>.Success(userResponse, "User Login successfully.");
        }

        public async Task<ApiResponse<UserResponse>> RegisterAsync(RegisterDto registerDto)
        {
            if (await _userManager.FindByNameAsync(registerDto.UserName) is not null)
                return ApiResponse<UserResponse>.Error(HttpStatusCode.Conflict, "UserName already exists.");

            AppUser user = new()
            {
                UserName = registerDto.UserName,
                PersonId = registerDto.PersonId,
                IsActive = true
            };

            var result = await _userManager.CreateAsync(user, registerDto.Password);

            if (!result.Succeeded)
            {
                return ApiResponse<UserResponse>.Error(HttpStatusCode.BadRequest, "Error acourse will create the user");
            }

            //var jwtSecurityToken = await CreateJwtToken(user);

            UserResponse userResponse = new UserResponse
            {
                Id = user.Id,
                UserNameOrEmail = user.UserName,
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
