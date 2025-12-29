using SmartNeighborhoodAPI.Entites;
using SmartNeighborhoodAPI.Helpers.DTOs.ResidentialNeighborhood;
using SmartNeighborhoodAPI.Interfaces;
using System.Net;
using Microsoft.AspNetCore.Identity;
using SmartNeighborhoodAPI.Helpers;
using Microsoft.AspNetCore.Identity.UI.Services;
using SmartNeighborhoodAPI.Helpers.DTOs.Auth;
using Microsoft.EntityFrameworkCore;

namespace SmartNeighborhoodAPI.Services
{
    public class ResidentialNeighborhoodService : IResidentialNeighborhoodService
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<AppUser> _userManager;
        private readonly IEmailSender _emailSender;
        private readonly ILogger<ResidentialNeighborhoodService> _logger;

        public ResidentialNeighborhoodService(ApplicationDbContext context, UserManager<AppUser> userManager, IEmailSender emailSender, ILogger<ResidentialNeighborhoodService> logger)
        {
            _context = context;
            _userManager = userManager;
            _emailSender = emailSender;
            _logger = logger;
        }

        public async Task<ApiResponse<ReturnResidentialNeighborhoodDto>> CreateAsync(CreateResidentialNeighborhoodDto dto)
        {
            _logger.LogInformation("Attempting to create a new residential neighborhood with name: {Name}", dto.Name);

            if (await _context.ResidentialNeighborhoods.AnyAsync(n => n.Name == dto.Name))
            {
                _logger.LogWarning("Neighborhood with name '{Name}' already exists", dto.Name);
                return ApiResponse<ReturnResidentialNeighborhoodDto>.Error(HttpStatusCode.BadRequest, "Neighborhood name already exists");
            }

            var person = await _context.People.FindAsync(dto.NeighborhoodManagerId);
            if (person == null)
            {
                _logger.LogWarning("Person with ID {PersonId} not found", dto.NeighborhoodManagerId);
                return ApiResponse<ReturnResidentialNeighborhoodDto>.Error(HttpStatusCode.NotFound, "Person not found");
            }

            // Create Manager Account Logic
            var existingUser = await _userManager.Users.FirstOrDefaultAsync(u => u.PersonId == dto.NeighborhoodManagerId);
            if (existingUser != null)
            {
                _logger.LogWarning("User with PersonId {PersonId} already exists", dto.NeighborhoodManagerId);
                return ApiResponse<ReturnResidentialNeighborhoodDto>.Error(HttpStatusCode.BadRequest, "User already exists for this person");
            }

            var strategy = _context.Database.CreateExecutionStrategy();

            return await strategy.ExecuteAsync(async () =>
            {
                using var transaction = await _context.Database.BeginTransactionAsync();
                try
                {
                    var user = new AppUser
                    {
                        UserName = dto.Email,
                        Email = dto.Email,
                        PersonId = dto.NeighborhoodManagerId,
                        IsActive = true,
                        EmailConfirmed = true
                    };

                    var result = await _userManager.CreateAsync(user, dto.Password);
                    if (!result.Succeeded)
                    {
                        var errors = result.Errors.Select(e => new ErrorDetails { Field = e.Code, ErrorMessage = e.Description }).ToList();
                        return ApiResponse<ReturnResidentialNeighborhoodDto>.Error(HttpStatusCode.BadRequest, "Failed to create user", errors);
                    }

                    if (!await _userManager.IsInRoleAsync(user, Role.ResidentialNeighborhoodManager))
                    {
                        await _userManager.AddToRoleAsync(user, Role.ResidentialNeighborhoodManager);
                    }

                    // Send Email Logic (Simplified)
                    var otp = new Random().Next(100000, 999999).ToString();
                    user.EmailConfirmationCode = otp;
                    user.EmailConfirmationCodeExpiresAt = DateTime.UtcNow.AddHours(1);
                    await _userManager.UpdateAsync(user);
                     _emailSender.SendEmailAsync(user.Email, "Account Created", $"Your account has been created. OTP: {otp}");
                    var entity = new ResidentialNeighborhood
                    {
                        Name = dto.Name,
                        NeighborhoodManagerId = user.Id // Use the newly created user's ID
                    };

                    _context.ResidentialNeighborhoods.Add(entity);
                    await _context.SaveChangesAsync();

                    await _context.Entry(entity).Reference(e => e.NeighborhoodManager).LoadAsync();

                    await transaction.CommitAsync();

                    _logger.LogInformation("Successfully created residential neighborhood '{Name}' with ID {Id}", entity.Name, entity.Id);

                    return ApiResponse<ReturnResidentialNeighborhoodDto>.Success(entity.ToDto());
                }
                catch (Exception ex)
                {
                    await transaction.RollbackAsync();
                    _logger.LogError(ex, "Transaction failed in CreateAsync");
                    return ApiResponse<ReturnResidentialNeighborhoodDto>.Error(HttpStatusCode.InternalServerError, "An error occurred while creating the neighborhood.");
                }
            });
        }
        public async Task<ApiResponse<List<ReturnResidentialNeighborhoodDto>>> GetAllAsync(
            CancellationToken ct = default)
        {
            var data = await _context.ResidentialNeighborhoods
                .AsNoTracking()
                .Include(n => n.NeighborhoodManager)
                .Include(n => n.ResidentialUnits)
                    .ThenInclude(u => u.Blocks)
                .OrderBy(n => n.Name) 
                .ToListAsync(ct);

            return ApiResponse<List<ReturnResidentialNeighborhoodDto>>
                .Success(data.Select(n => n.ToDto()).ToList());
        }

        public async Task<ApiResponse<ReturnResidentialNeighborhoodDto>> GetByIdAsync(int id)
        {
            var entity = await _context.ResidentialNeighborhoods
                .Include(n => n.NeighborhoodManager)
                .Include(n => n.ResidentialUnits)
                    .ThenInclude(u => u.Blocks)
                .FirstOrDefaultAsync(n => n.Id == id);

            if (entity == null)
                return ApiResponse<ReturnResidentialNeighborhoodDto>
                    .Error(HttpStatusCode.NotFound, "Neighborhood not found");

            return ApiResponse<ReturnResidentialNeighborhoodDto>.Success(entity.ToDto());
        }

        public async Task<ApiResponse<string>> UpdateAsync(int id, UpdateResidentialNeighborhoodDto dto)
        {
            var entity = await _context.ResidentialNeighborhoods.FindAsync(id);
            if (entity == null)
                return ApiResponse<string>.Error(HttpStatusCode.NotFound, "Neighborhood not found");

            if (await _context.ResidentialNeighborhoods.AnyAsync(n => n.Name == dto.Name && n.Id != id))
                return ApiResponse<string>.Error(HttpStatusCode.BadRequest, "Neighborhood name already exists");

            entity.Name = dto.Name;

            await _context.SaveChangesAsync();
            return ApiResponse<string>.Success("Neighborhood updated");
        }

        public async Task<ApiResponse<ReturnResidentialNeighborhoodDto>> ChangeManagerAsync( ChangeResidentialManagerDto dto)
        {
            _logger.LogInformation("Initiating change of residential neighborhood manager for NeighborhoodId: {NeighborhoodId}, PersonId: {PersonId}",
                dto.neighborhoodId, dto.PersonId);

            // Step 1: Validate neighborhood
            var neighborhood = await _context.ResidentialNeighborhoods.FindAsync(dto.neighborhoodId);
            if (neighborhood == null)
            {
                _logger.LogWarning("Neighborhood with ID '{NeighborhoodId}' not found.", dto.neighborhoodId);
                return ApiResponse<ReturnResidentialNeighborhoodDto>.Error(HttpStatusCode.NotFound, "Neighborhood not found");
            }

            // Step 2: Validate person
            var person = await _context.People.FindAsync(dto.PersonId);
            if (person == null)
            {
                _logger.LogWarning("Person with ID '{PersonId}' not found.", dto.PersonId);
                return ApiResponse<ReturnResidentialNeighborhoodDto>.Error(HttpStatusCode.NotFound, "Person not found");
            }

            // Check if user already exists for this person
            var existingUserByPerson = await _userManager.Users.FirstOrDefaultAsync(u => u.PersonId == dto.PersonId);
            if (existingUserByPerson != null)
            {
                _logger.LogWarning("User with PersonId {PersonId} already exists", dto.PersonId);
                return ApiResponse<ReturnResidentialNeighborhoodDto>.Error(HttpStatusCode.BadRequest, "Â–« «·„” Œœ„ ÂÊ „œÌ— »«·›⁄·.");
            }

            var existingUser = await _userManager.FindByEmailAsync(dto.Email);
            if (existingUser != null)
            {
                _logger.LogWarning("Email '{Email}' is already used.", dto.Email);
                return ApiResponse<ReturnResidentialNeighborhoodDto>.Error(HttpStatusCode.Conflict, "«·»—Ìœ «·≈·ﬂ —Ê‰Ì „” Œœ„ „”»ﬁ«.");
            }

            var strategy = _context.Database.CreateExecutionStrategy();

            return await strategy.ExecuteAsync(async () =>
            {
                using var transaction = await _context.Database.BeginTransactionAsync();
                try
                {
                    // Step 4: Create new manager account
                    var user = new AppUser
                    {
                        UserName = dto.Email,
                        Email = dto.Email,
                        PersonId = dto.PersonId,
                        IsActive = true,
                        EmailConfirmed = true
                    };

                    var createResult = await _userManager.CreateAsync(user, dto.Password);

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
                        _logger.LogError("Failed to create new residential neighborhood manager. Reason: {Reason}", string.Join(", ", errors.Select(e => e.ErrorMessage)));
                        return ApiResponse<ReturnResidentialNeighborhoodDto>.Error(HttpStatusCode.BadRequest, "ÕœÀ Œÿ√ √À‰«¡ ≈‰‘«¡ «·„” Œœ„.", errors);
                    }

                    if (!await _userManager.IsInRoleAsync(user, Role.ResidentialNeighborhoodManager))
                    {
                        await _userManager.AddToRoleAsync(user, Role.ResidentialNeighborhoodManager);
                    }

                    // Send Email Logic (Simplified)
                    var otp = new Random().Next(100000, 999999).ToString();
                    user.EmailConfirmationCode = otp;
                    user.EmailConfirmationCodeExpiresAt = DateTime.UtcNow.AddHours(1);
                    await _userManager.UpdateAsync(user);
                     _emailSender.SendEmailAsync(user.Email, "Account Created", $"Your account has been created. OTP: {otp}");


                    var oldManagerId = neighborhood.NeighborhoodManagerId;
                    var oldManagerUser = await _userManager.FindByIdAsync(oldManagerId);

                    // Step 5: Update neighborhood manager
                    neighborhood.NeighborhoodManagerId = user.Id;
                    await _context.SaveChangesAsync();

                    // Step 6: Delete old manager account (if any)
                    if (oldManagerUser != null)
                    {
                        var deleteResult = await _userManager.DeleteAsync(oldManagerUser);
                        if (!deleteResult.Succeeded)
                        {
                            var errors = deleteResult.Errors.Select(e => new ErrorDetails { Field = e.Code, ErrorMessage = e.Description }).ToList();
                            _logger.LogError("Failed to delete old residential neighborhood manager with ID: {OldManagerId}", oldManagerUser.Id);
                            // Rollback is handled by the catch block
                            throw new Exception("Failed to delete old manager");
                        }
                    }

                    await _context.Entry(neighborhood).Reference(e => e.NeighborhoodManager).LoadAsync();

                    await transaction.CommitAsync();

                    _logger.LogInformation("Neighborhood manager updated successfully for neighborhood '{Name}' (ID: {Id})",
                        neighborhood.Name, neighborhood.Id);

                    return ApiResponse<ReturnResidentialNeighborhoodDto>.Success(neighborhood.ToDto(),
                        "Neighborhood manager changed successfully. Login details sent via email.");
                }
                catch (Exception ex)
                {
                    await transaction.RollbackAsync();
                    _logger.LogError(ex, "Transaction failed in ChangeManagerAsync");
                    return ApiResponse<ReturnResidentialNeighborhoodDto>.Error(HttpStatusCode.InternalServerError, "An error occurred while processing the request.");
                }
            });
        }

        
       
        public async Task<ApiResponse<ResidentialDashboardDto>> GetDashboardAsync(
        CancellationToken ct = default)
        {
            var neighborhoods = await _context.ResidentialNeighborhoods
                .AsNoTracking()
                .Select(n => new
                {
                    n.Id,
                    n.Name,
                    UnitsCount = n.ResidentialUnits.Count,
                    BlocksCount = n.ResidentialUnits.Sum(u => u.Blocks.Count)
                })
                .ToListAsync(ct);

            var dashboard = new ResidentialDashboardDto
            {
                TotalNeighborhoods = neighborhoods.Count,
                TotalUnits = neighborhoods.Sum(n => n.UnitsCount),
                TotalBlocks = neighborhoods.Sum(n => n.BlocksCount),
                Neighborhoods = neighborhoods.Select(n => new NeighborhoodStatsDto
                {
                    NeighborhoodId = n.Id,
                    NeighborhoodName = n.Name,
                    UnitsCount = n.UnitsCount,
                    BlocksCount = n.BlocksCount
                }).ToList()
            };

            return ApiResponse<ResidentialDashboardDto>.Success(dashboard);
        }


    }

}
