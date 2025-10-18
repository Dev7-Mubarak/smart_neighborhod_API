using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Text;
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
        private readonly ApplicationDbContext _context;

        public AuthService(UserManager<AppUser> userManager, IOptions<JWT> jwt, SignInManager<AppUser> signInManager, IEmailSender emailSender, ILogger<AuthService> logger, ApplicationDbContext context)
        {
            _userManager = userManager;
            _jwt = jwt.Value;
            _signInManager = signInManager;
            _emailSender = emailSender;
            _logger = logger;
            _context = context;
        }
        public async Task<ApiResponse<UserResponse>> LoginAsync(LoginDto loginDto)
        {
            var user = await _userManager.FindByEmailAsync(loginDto.Email);

            if (user == null)
                return ApiResponse<UserResponse>.Error(HttpStatusCode.NotFound, "بيانات تسجيل الدخول غير صحيحة.");

            var result = await _signInManager.CheckPasswordSignInAsync(user, loginDto.Password, false);

            if (!result.Succeeded)
                return ApiResponse<UserResponse>.Error(HttpStatusCode.BadRequest, "بيانات تسجيل الدخول غير صحيحة.");

            if (!user.EmailConfirmed)
            {
                return ApiResponse<UserResponse>.Error(HttpStatusCode.Forbidden, "لم يتم تأكيد البريد الإلكتروني. الرجاء إدخال رمز التحقق المرسل إلى بريدك الإلكتروني.");
            }

            var jwtSecurityToken = await CreateJwtToken(user);

            UserResponse userResponse = new UserResponse
            {
                Id = user.Id,
                Email = loginDto.Email,
                Role = (await _userManager.GetRolesAsync(user)).FirstOrDefault(),
                Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken),
            };

            return ApiResponse<UserResponse>.Success(userResponse, "تم تسجيل الدخول بنجاح.");
        }

        public async Task<ApiResponse<UserResponse>> CreateBlockManagerAccountAsync(CreateBlockManagerDto dto)
        {
            _logger.LogInformation("Attempting to create a Block Manager for email: {Email}", dto.Email);

            // Step 1: Check if user already exists
            var existingUser = await _context.Users.FirstOrDefaultAsync(x => x.PersonId == dto.PersonId);
            if (existingUser != null)
            {
                    _logger.LogWarning("User {Email} is already assigned as a Block Manager.", dto.Email);
                    return ApiResponse<UserResponse>.Error(HttpStatusCode.BadRequest, "هذا المستخدم هو مدير بالفعل لإحدى المربعات.");
            }

            // Step 2: Create new user if not exists
            AppUser user = existingUser ?? new AppUser
            {
                Email = dto.Email,
                UserName = dto.Email,
                PersonId = dto.PersonId,
                IsActive = false,
            };

            if (existingUser == null)
            {
                var result = await _userManager.CreateAsync(user, dto.Password);
                if (!result.Succeeded)
                {
                    List<ErrorDetails> errors = result.Errors.Select(e =>
                    {
                        string arabicMessage = e.Code switch
                        {
                            "DuplicateUserName" => "البريد الإلكتروني مستخدم مسبقاً.",
                            "InvalidUserName" => "اسم المستخدم غير صالح.",
                            "PasswordTooShort" => "كلمة المرور قصيرة جداً.",
                            "PasswordRequiresNonAlphanumeric" => "كلمة المرور يجب أن تحتوي على رمز خاص.",
                            "PasswordRequiresDigit" => "كلمة المرور يجب أن تحتوي على رقم.",
                            "PasswordRequiresLower" => "كلمة المرور يجب أن تحتوي على حرف صغير.",
                            "PasswordRequiresUpper" => "كلمة المرور يجب أن تحتوي على حرف كبير.",
                            _ => e.Description
                        };

                        return new ErrorDetails
                        {
                            Field = e.Code,
                            ErrorMessage = arabicMessage
                        };
                    }).ToList();

                    _logger.LogError("User creation failed for email {Email}. Errors: {@Errors}", dto.Email, errors);
                    return ApiResponse<UserResponse>.Error(HttpStatusCode.BadRequest, "حدث خطأ أثناء إنشاء المستخدم.", errors);
                }
            }

            // Step 3: Assign BlockManager role
            var roleResult = await _userManager.AddToRoleAsync(user, Role.BlockManager);
            if (!roleResult.Succeeded)
            {
                _logger.LogError("Failed to assign role BlockManager to user {Email}. Errors: {@Errors}", dto.Email, roleResult.Errors);
                return ApiResponse<UserResponse>.Error(HttpStatusCode.BadRequest, "تم إنشاء المستخدم ولكن فشل إسناد الدور BlockManager.");
            }

            // Step 4: Generate OTP and send email
            var otp = new Random().Next(100000, 999999).ToString();
            user.EmailConfirmationCode = otp;
            user.EmailConfirmationCodeExpiresAt = DateTime.UtcNow.AddHours(1);

            await _userManager.UpdateAsync(user);

            await _emailSender.SendEmailAsync(
                user.Email,
                "رمز تأكيد البريد الإلكتروني",
                $"مرحباً,<br/><br/>رمز تأكيد البريد الإلكتروني الخاص بك هو: <strong>{otp}</strong><br/>هذا الرمز سينتهي خلال ساعة واحدة."
            );

            _logger.LogInformation("User created successfully: {Email}. OTP sent to email.", user.Email);

            UserResponse userResponse = new UserResponse
            {
                Id = user.Id,
                Email = user.Email,
                Role = Role.BlockManager
            };

            return ApiResponse<UserResponse>.Success(userResponse, "تم تسجيل المستخدم بنجاح. تم إرسال رمز التأكيد إلى البريد الإلكتروني.");
        }

        public async Task<ApiResponse<UserResponse>> DeleteBlockManagerAccountByIdAsync(string managerId)
        {
            _logger.LogInformation("Deleting Block Manager with ID: {ManagerId}", managerId);

            var user = await _userManager.FindByIdAsync(managerId);
            if (user == null)
            {
                _logger.LogWarning("Block Manager with ID '{ManagerId}' not found.", managerId);
                return ApiResponse<UserResponse>.Error(HttpStatusCode.NotFound, "المستخدم غير موجود.");
            }

            var deletionResult = await _userManager.DeleteAsync(user);
            if (!deletionResult.Succeeded)
            {
                _logger.LogError("Failed to delete Block Manager with ID: {ManagerId}. Errors: {Errors}",
                                 managerId, string.Join(", ", deletionResult.Errors.Select(e => e.Description)));

                return ApiResponse<UserResponse>.Error(HttpStatusCode.InternalServerError, "فشل في حذف حساب المستخدم.");
            }

            var userResponse = new UserResponse
            {
                Id = user.Id,
                Email = user.Email,
                Role = "BlockManager"
            };

            _logger.LogInformation("Successfully deleted Block Manager with ID: {ManagerId}", managerId);

            return ApiResponse<UserResponse>.Success(userResponse, "تم حذف حساب مدير الحي بنجاح.");
        }


        public async Task<string?> GetUserRole(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user != null)
            {
                var roles = await _userManager.GetRolesAsync(user);
                if (roles.Any())
                {
                    return roles.First(); 
                }
            }
            return null;
        }
        private async Task<JwtSecurityToken> CreateJwtToken(AppUser user)
        {
            var userClaims = await _userManager.GetClaimsAsync(user);
            var roles = await _userManager.GetRolesAsync(user);
            var roleClaims = new List<Claim>();

            foreach (var role in roles)
                roleClaims.Add(new Claim(ClaimTypes.Role, role));

            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
            }
            .Union(userClaims)
            .Union(roleClaims);

            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwt.SigningKey));
            var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

            var jwtSecurityToken = new JwtSecurityToken(
                issuer: _jwt.Issuer,
                audience: _jwt.Audience,
                claims: claims,
                expires: DateTime.UtcNow.AddDays(30),
                signingCredentials: signingCredentials);

            return jwtSecurityToken;
        }
        public async Task<ApiResponse<string>> VerifyResetCodeAsync(VerifyResetCodeDto model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null)
                return ApiResponse<string>.Error(HttpStatusCode.NotFound, "المستخدم غير موجود.");

            if (string.IsNullOrEmpty(user.EmailConfirmationCode) || user.EmailConfirmationCodeExpiresAt == null)
                return ApiResponse<string>.Error(HttpStatusCode.BadRequest, "لم يتم إرسال رمز إعادة تعيين كلمة المرور.");

            if (DateTime.UtcNow > user.EmailConfirmationCodeExpiresAt)
                return ApiResponse<string>.Error(HttpStatusCode.BadRequest, "انتهت صلاحية رمز إعادة تعيين كلمة المرور.");

            if (user.EmailConfirmationCode != model.Code)
                return ApiResponse<string>.Error(HttpStatusCode.BadRequest, "رمز إعادة التعيين غير صحيح.");

            user.EmailConfirmed = true;
            user.EmailConfirmationCodeExpiresAt = null;

            var updateResult = await _userManager.UpdateAsync(user);
            if (!updateResult.Succeeded)
                return ApiResponse<string>.Error(HttpStatusCode.InternalServerError, "حدث خطأ أثناء تحديث بيانات المستخدم.");

            return ApiResponse<string>.Success(null, "تم التحقق من رمز إعادة تعيين كلمة المرور بنجاح.");
        }


        public async Task<ApiResponse<string>> ResetPasswordAsync(ResetPasswordDto model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null)
            {
                return ApiResponse<string>.Error(HttpStatusCode.NotFound, "المستخدم غير موجود.");
            }

            if (model.NewPassword != model.ConfirmPassword)
            {
                return ApiResponse<string>.Error(HttpStatusCode.BadRequest, "كلمتا المرور غير متطابقتين.");
            }

            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            var result = await _userManager.ResetPasswordAsync(user, token, model.NewPassword);

            if (!result.Succeeded)
            {
                var errors = string.Join(", ", result.Errors.Select(e => e.Description));
                return ApiResponse<string>.Error(HttpStatusCode.BadRequest, $"فشل في إعادة تعيين كلمة المرور: {errors}");
            }

            return ApiResponse<string>.Success("تمت إعادة تعيين كلمة المرور بنجاح.");
        }

        public async Task<ApiResponse<string>> RegisterAsync(RegisterDto model)
        {
            var existingUser = await _userManager.FindByEmailAsync(model.Email);
            if (existingUser != null)
            {
                return ApiResponse<string>.Error(HttpStatusCode.BadRequest, "Email is already in use.");
            }

     
            var user = new AppUser
            {
                UserName = model.Email,
                Email = model.Email,
                PersonId=1
          
            };

            var result = await _userManager.CreateAsync(user, model.Password);

            if (!result.Succeeded)
            {
                var errors = string.Join(", ", result.Errors.Select(e => e.Description));
                return ApiResponse<string>.Error(HttpStatusCode.BadRequest, $"User creation failed: {errors}");
            }

            await _userManager.AddToRoleAsync(user, "User"); 

            return ApiResponse<string>.Success("User registered successfully.");
        
        }
        public async Task<ApiResponse<string>> SendResetCodeAsync(ForgotPasswordDto model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null)
            {
                return ApiResponse<string>.Error(HttpStatusCode.NotFound, "المستخدم غير موجود.");
            }

            var code = new Random().Next(100000, 999999).ToString();

            user.EmailConfirmationCode = code;
            user.EmailConfirmationCodeExpiresAt = DateTime.UtcNow.AddMinutes(10);

            var result = await _userManager.UpdateAsync(user);
            if (!result.Succeeded)
            {
                return ApiResponse<string>.Error(HttpStatusCode.InternalServerError, "حدث خطأ أثناء حفظ رمز التحقق.");
            }

            await _emailSender.SendEmailAsync(
                model.Email,
                "رمز إعادة تعيين كلمة المرور",
                $"رمز إعادة تعيين كلمة المرور الخاص بك هو: <b>{code}</b><br/>" +
                "سينتهي صلاحيته خلال 10 دقائق.");

            return ApiResponse<string>.Success("تم إرسال رمز التحقق إلى بريدك الإلكتروني.");
        }



    }
}
