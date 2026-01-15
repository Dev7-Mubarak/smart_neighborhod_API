using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;
using SmartNeighborhoodAPI.Entites;
using SmartNeighborhoodAPI.Helpers;
using SmartNeighborhoodAPI.Interfaces;
using System.Net;
using System.Xml.Linq;

namespace SmartNeighborhoodAPI.Services
{
    public class ManagerAccountService : IManagerAccountService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly ApplicationDbContext _context;
        private readonly IEmailSender _emailSender;
        private readonly ILogger<ManagerAccountService> _logger;

        public ManagerAccountService(UserManager<AppUser> userManager, ApplicationDbContext context, IEmailSender emailSender, ILogger<ManagerAccountService> logger)
        {
            _userManager = userManager;
            _context = context;
            _emailSender = emailSender;
            _logger = logger;
        }

        public async Task<ApiResponse<AppUser>> CreateManagerAccountAsync(int personId, string email, string password, string role)
        {
            // 1. Check if user already exists for this person
            var existingUserByPerson = await _userManager.Users.FirstOrDefaultAsync(u => u.PersonId == personId);
            if (existingUserByPerson != null)
            {
                _logger.LogWarning("User with PersonId {PersonId} already exists", personId);
                return ApiResponse<AppUser>.Error(HttpStatusCode.BadRequest, "المستخدم لهذا الشخص موجود بالفعل.");
            }

            // 2. Check if email is already used
            var existingUserByEmail = await _userManager.FindByEmailAsync(email);
            if (existingUserByEmail != null)
            {
                _logger.LogWarning("Email '{Email}' is already used.", email);
                return ApiResponse<AppUser>.Error(HttpStatusCode.Conflict, "البريد الإلكتروني مستخدم بالفعل.");
            }

            // 3. Create new user
            var user = new AppUser
            {
                UserName = email, // Using Email as Username for managers usually, or separate logic if needed
                Email = email,
                PersonId = personId,
                IsActive = true,
                EmailConfirmed = true // Managers created by admin are auto-confirmed usually
            }; 

            var createResult = await _userManager.CreateAsync(user, password);
            if (!createResult.Succeeded)
            {
                List<ErrorDetails> errors = createResult.Errors.Select(e =>
                {
                    string arabicMessage = e.Code switch
                    {
                        "DuplicateUserName" => "المستخدم لهذا الشخص موجود بالفعل.",
                        "InvalidUserName" => "اسم المستخدم غير صالح.",
                        "PasswordTooShort" => "كلمة المرور قصيرة جداً.",
                        "PasswordRequiresNonAlphanumeric" => "كلمة المرور تحتاج إلى رمز غير أبجدي رقمي.",
                        "PasswordRequiresDigit" => "كلمة المرور تحتاج إلى رقم.",
                        "PasswordRequiresLower" => "كلمة المرور تحتاج إلى حرف صغير  .",
                        "PasswordRequiresUpper" => "كلمة المرور تحتاج إلى حرف كبير.",
                        "PasswordIsRequired" => "كلمة المرور مطلوبة.",
                        _ => e.Description
                    };

                    return new ErrorDetails
                    {
                        Field = e.Code,
                        ErrorMessage = arabicMessage
                    };
                }).ToList();
                _logger.LogError("Failed to create new manager. Reason: {Reason}", string.Join(", ", errors.Select(e => e.ErrorMessage)));
                return ApiResponse<AppUser>.Error(HttpStatusCode.BadRequest, "فشل إنشاء حساب المدير.", errors);
            }

            // 4. Assign Role
            if (!await _userManager.IsInRoleAsync(user, role))
            {
                await _userManager.AddToRoleAsync(user, role); 
            }

            // 5. Send Email
            var otp = new Random().Next(100000, 999999).ToString(); 
            user.EmailConfirmationCode = otp;
            user.EmailConfirmationCodeExpiresAt = DateTime.UtcNow.AddHours(1);
            await _userManager.UpdateAsync(user);
            await _emailSender.SendEmailAsync(user.Email, "تأكيد البريد الإلكتروني", $"تم تأكيد بريدك الإلكتروني. رمز التأكيد هو: {otp}"); 
            return ApiResponse<AppUser>.Success(user, "تم تأكيد البريد الإلكتروني بنجاح.");
        }

        public async Task<ApiResponse<string>> DeleteManagerAccountAsync(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return ApiResponse<string>.Error(HttpStatusCode.NotFound, "المستخدم غير موجود.");
            }

            var deleteResult = await _userManager.DeleteAsync(user);
            if (!deleteResult.Succeeded)
            {
                var errors = deleteResult.Errors.Select(e => new ErrorDetails { Field = e.Code, ErrorMessage = e.Description }).ToList(); 
                _logger.LogError("Failed to delete manager user {UserId}.", userId);
                return ApiResponse<string>.Error(HttpStatusCode.InternalServerError, "فشل حذف حساب المدير.");
            }

            return ApiResponse<string>.Success("تم حذف حساب المدير بنجاح.");
        }
    }
}