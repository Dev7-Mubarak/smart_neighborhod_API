using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using SmartNeighborhoodAPI.Entites;
using SmartNeighborhoodAPI.Helpers;
using SmartNeighborhoodAPI.Interfaces;
using System.Net;
using Microsoft.EntityFrameworkCore;

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
                return ApiResponse<AppUser>.Error(HttpStatusCode.BadRequest, "Â–« «·„” Œœ„ ÂÊ „œÌ— »«·›⁄·.");
            }

            // 2. Check if email is already used
            var existingUserByEmail = await _userManager.FindByEmailAsync(email);
            if (existingUserByEmail != null)
            {
                _logger.LogWarning("Email '{Email}' is already used.", email);
                return ApiResponse<AppUser>.Error(HttpStatusCode.Conflict, "«·»—Ìœ «·≈·ﬂ —Ê‰Ì „” Œœ„ „”»ﬁ«.");
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
                        "DuplicateUserName" => "«·»—Ìœ «·≈·ﬂ —Ê‰Ì „” Œœ„ „”»ﬁ«.",
                        "InvalidUserName" => "«”„ «·„” Œœ„ €Ì— ’«·Õ.",
                        "PasswordTooShort" => "ﬂ·„… «·„—Ê— ﬁ’Ì—… Ãœ«.",
                        "PasswordRequiresNonAlphanumeric" => "ﬂ·„… «·„—Ê— ÌÃ» √‰  Õ ÊÌ ⁄·Ï —„“ Œ«’.",
                        "PasswordRequiresDigit" => "ﬂ·„… «·„—Ê— ÌÃ» √‰  Õ ÊÌ ⁄·Ï —ﬁ„.",
                        "PasswordRequiresLower" => "ﬂ·„… «·„—Ê— ÌÃ» √‰  Õ ÊÌ ⁄·Ï Õ—› ’€Ì—.",
                        "PasswordRequiresUpper" => "ﬂ·„… «·„—Ê— ÌÃ» √‰  Õ ÊÌ ⁄·Ï Õ—› ﬂ»Ì—.",
                        "PasswordIsRequired" => "ﬂ·„… «·„—Ê— „ÿ·Ê»….",
                        _ => e.Description
                    };

                    return new ErrorDetails
                    {
                        Field = e.Code,
                        ErrorMessage = arabicMessage
                    };
                }).ToList();
                _logger.LogError("Failed to create new manager. Reason: {Reason}", string.Join(", ", errors.Select(e => e.ErrorMessage)));
                return ApiResponse<AppUser>.Error(HttpStatusCode.BadRequest, "ÕœÀ Œÿ√ √À‰«¡ ≈‰‘«¡ «·„” Œœ„.", errors);
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
            await _emailSender.SendEmailAsync(user.Email, " „ ≈‰‘«¡ «·Õ”«»", $" „ ≈‰‘«¡ Õ”«»ﬂ »‰Ã«Õ. —„“ «· Õﬁﬁ ÂÊ: {otp}");

            return ApiResponse<AppUser>.Success(user, " „ ≈‰‘«¡ «·Õ”«» Ê≈—”«· «·»—Ìœ «·≈·ﬂ —Ê‰Ì.");
        }

        public async Task<ApiResponse<string>> DeleteManagerAccountAsync(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return ApiResponse<string>.Error(HttpStatusCode.NotFound, "«·„” Œœ„ €Ì— „ÊÃÊœ.");
            }

            var deleteResult = await _userManager.DeleteAsync(user);
            if (!deleteResult.Succeeded)
            {
                var errors = deleteResult.Errors.Select(e => new ErrorDetails { Field = e.Code, ErrorMessage = e.Description }).ToList();
                _logger.LogError("Failed to delete manager user {UserId}.", userId);
                return ApiResponse<string>.Error(HttpStatusCode.InternalServerError, "›‘· Õ–› Õ”«» «·„œÌ—.");
            }

            return ApiResponse<string>.Success(" „ Õ–› Õ”«» «·„œÌ— »‰Ã«Õ.");
        }
    }
}
