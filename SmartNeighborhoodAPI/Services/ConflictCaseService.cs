using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using OurProjectSmartNeiborhood.Entites;
using SmartNeighborhoodAPI.Entites;
using SmartNeighborhoodAPI.Helpers.DTOs.ConfilctCase;
using SmartNeighborhoodAPI.Helpers.DTOs.Families;
using SmartNeighborhoodAPI.Helpers.DTOs.FamilyMember;
using SmartNeighborhoodAPI.Interfaces;
using System.Net;

namespace SmartNeighborhoodAPI.Services
{
    public class ConflictCaseService : IConflictCaseService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly ImageService _imageService;
        private readonly ILogger<ConflictCaseService> _logger;
        private string _personImagePath;
        private readonly UserManager<AppUser> _userManager;

        public ConflictCaseService(UserManager<AppUser> userManager, ApplicationDbContext context, IMapper mapper, IWebHostEnvironment webHostEnvironment, ImageService imageService, ILogger<ConflictCaseService> logger)
        {
            _context = context;
            _mapper = mapper;
            _webHostEnvironment = webHostEnvironment;
            _imageService = imageService;
            _logger = logger;
            _personImagePath = $"{_webHostEnvironment.WebRootPath}{FileHelper.PersonImagesPath}";
            _userManager = userManager;
        }
        public async Task<ApiResponse<ReturnConflictCaseDto>> AddAsync(AddConflictCaseDto conflictCaseDto)
        {
            _logger.LogInformation("Start adding ConflictCase");

            string conflictCaseImage = string.Empty;

            if (conflictCaseDto.Image != null)
            {
                _logger.LogInformation("Saving image for ConflictCase");
                conflictCaseImage = await _imageService.SaveImageAsync(conflictCaseDto.Image, _personImagePath);
            }

            AppUser manager = null;

            if (!string.IsNullOrWhiteSpace(conflictCaseDto.ManagerId))
            {
                manager = await _userManager.FindByIdAsync(conflictCaseDto.ManagerId);
            }

            if (manager is null)
            {
                _logger.LogWarning("Manager not found");
                return ApiResponse<ReturnConflictCaseDto>.Error(HttpStatusCode.NotFound, "لم يتم العثور على المدير.");
            }

            var isConflictTypeExists = await _context.ConfilctTypes.AnyAsync(x => x.Id == conflictCaseDto.ConflictTypeId);
            if (!isConflictTypeExists)
            {
                _logger.LogWarning("Conflict type not found");
                return ApiResponse<ReturnConflictCaseDto>.Error(HttpStatusCode.NotFound, "نوع النزاع غير موجود.");
            }

            var firstParty = await _context.FamilyMembers.FindAsync(conflictCaseDto.FirstPartyId);
            if (firstParty is null)
            {
                _logger.LogWarning("First party not found");
                return ApiResponse<ReturnConflictCaseDto>.Error(HttpStatusCode.NotFound, "الطرف الأول غير موجود.");
            }

            var secondParty = await _context.FamilyMembers.FindAsync(conflictCaseDto.SecondPartyId);
            if (secondParty is null)
            {
                _logger.LogWarning("Second party not found");
                return ApiResponse<ReturnConflictCaseDto>.Error(HttpStatusCode.NotFound, "الطرف الثاني غير موجود.");
            }

            var conflictCase = _mapper.Map<ConflictCase>(conflictCaseDto);
            conflictCase.ManagerId = manager.Id;
            conflictCase.ImagePath = conflictCaseImage;

            _context.ConfilctCases.Add(conflictCase);
            await _context.SaveChangesAsync();

            var returnDto = _mapper.Map<ReturnConflictCaseDto>(conflictCase);
            returnDto.ManagerName = manager.UserName;

            _logger.LogInformation("ConflictCase added successfully");
            return ApiResponse<ReturnConflictCaseDto>.Success(returnDto, "تمت إضافة القضية بنجاح.");
        }

        public async Task<ApiResponse<string>> DeleteAsync(int id)
        {
            _logger.LogInformation("Attempting to delete ConflictCase with ID {Id}", id);

            var entity = await _context.ConfilctCases.FirstOrDefaultAsync(x => x.Id == id);
            if (entity == null)
            {
                _logger.LogWarning("ConflictCase not found");
                return ApiResponse<string>.Error(HttpStatusCode.NotFound, "القضية غير موجودة.");
            }

            _context.ConfilctCases.Remove(entity);

            if (!string.IsNullOrEmpty(entity.ImagePath))
            {
                _logger.LogInformation("Deleting image for ConflictCase");
                await _imageService.DeleteImageAsync(entity.ImagePath, _personImagePath);
            }

            if (await _context.SaveChangesAsync() > 0)
            {
                _logger.LogInformation("ConflictCase deleted successfully");
                return ApiResponse<string>.Success("تم حذف القضية بنجاح.");
            }

            _logger.LogWarning("Failed to delete ConflictCase");
            return ApiResponse<string>.Error(HttpStatusCode.NotModified, "فشل في حذف القضية.");
        }

        public async Task<ApiResponse<IEnumerable<GetConflictCaseDto>>> GetAll()
        {
            _logger.LogInformation("Retrieving all ConflictCases");

            var conflictCases = await _context.ConfilctCases
                .Include(c => c.Manager).ThenInclude(m => m.Person)
                .Include(c => c.ConflictType)
                .Include(c => c.FirstParty).ThenInclude(fp => fp.Person)
                .Include(c => c.SecondParty).ThenInclude(sp => sp.Person)
                .AsNoTracking()
                .ToListAsync();

            var conflictCaseDtos = _mapper.Map<IEnumerable<GetConflictCaseDto>>(conflictCases);

            if (conflictCaseDtos.Any())
            {
                _logger.LogInformation("Found {Count} ConflictCases", conflictCaseDtos.Count());
                return ApiResponse<IEnumerable<GetConflictCaseDto>>.Success(conflictCaseDtos, "تم جلب جميع القضايا بنجاح.");
            }

            _logger.LogWarning("No ConflictCases found");
            return ApiResponse<IEnumerable<GetConflictCaseDto>>.Success(Enumerable.Empty<GetConflictCaseDto>(), "لا توجد قضايا نزاع.");
        }


        public async Task<ApiResponse<GetConflictCaseDto>> GetByIdAsync(int id)
        {
            _logger.LogInformation("Retrieving ConflictCase by ID {Id}", id);

            var conflictCase = await _context.ConfilctCases
                .Include(x => x.Manager)
                .Include(x => x.FirstParty)
                .Include(x => x.SecondParty)
                .Include(x => x.ConflictType)
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == id);

            if (conflictCase == null)
            {
                _logger.LogWarning("ConflictCase not found");
                return ApiResponse<GetConflictCaseDto>.Error(HttpStatusCode.NotFound, "القضية غير موجودة.");
            }

            var conflictCaseDto = _mapper.Map<GetConflictCaseDto>(conflictCase);
            _logger.LogInformation("ConflictCase retrieved successfully");
            return ApiResponse<GetConflictCaseDto>.Success(conflictCaseDto, "تم جلب القضية بنجاح.");
        }

        public async Task<ApiResponse<string>> UpdateAsync(int id, UpdateConflictCaseDto conflictCaseDto)
        {
            _logger.LogInformation("Updating ConflictCase with ID {Id}", id);

            var existingConflictCase = await _context.ConfilctCases.FirstOrDefaultAsync(x => x.Id == id);
            if (existingConflictCase is null)
            {
                _logger.LogWarning("ConflictCase not found");
                return ApiResponse<string>.Error(HttpStatusCode.NotFound, "القضية غير موجودة.");
            }

            if (!await _context.ConfilctTypes.AnyAsync(x => x.Id == conflictCaseDto.ConflictTypeId))
            {
                _logger.LogWarning("Conflict type not found");
                return ApiResponse<string>.Error(HttpStatusCode.NotFound, "نوع النزاع غير موجود.");
            }

            if (!await _context.FamilyMembers.AnyAsync(x => x.Id == conflictCaseDto.FirstPartyId))
            {
                _logger.LogWarning("First party not found");
                return ApiResponse<string>.Error(HttpStatusCode.NotFound, "الطرف الأول غير موجود.");
            }

            if (!await _context.FamilyMembers.AnyAsync(x => x.Id == conflictCaseDto.SecondPartyId))
            {
                _logger.LogWarning("Second party not found");
                return ApiResponse<string>.Error(HttpStatusCode.NotFound, "الطرف الثاني غير موجود.");
            }

            AppUser manager = null;
            if (!string.IsNullOrWhiteSpace(conflictCaseDto.ManagerId))
            {
                manager = await _userManager.FindByIdAsync(conflictCaseDto.ManagerId);
            }

            if (manager == null)
            {
                _logger.LogWarning("Manager not found during update");
                return ApiResponse<string>.Error(HttpStatusCode.NotFound, "لم يتم العثور على المدير.");
            }

            _mapper.Map(conflictCaseDto, existingConflictCase);
            existingConflictCase.ManagerId = manager.Id;

            if (conflictCaseDto.Image != null)
            {
                if (!string.IsNullOrEmpty(existingConflictCase.ImagePath))
                {
                    _logger.LogInformation("Deleting old image");
                    await _imageService.DeleteImageAsync(existingConflictCase.ImagePath, _personImagePath);
                }

                _logger.LogInformation("Saving new image");
                existingConflictCase.ImagePath = await _imageService.SaveImageAsync(conflictCaseDto.Image, _personImagePath);
            }

            if (await _context.SaveChangesAsync() > 0)
            {
                _logger.LogInformation("ConflictCase updated successfully");
                return ApiResponse<string>.Success("تم تحديث القضية بنجاح.");
            }

            _logger.LogWarning("Failed to update ConflictCase");
            return ApiResponse<string>.Error(HttpStatusCode.NotModified, "فشل في تحديث القضية.");
        }

        public async Task<ApiResponse<IEnumerable<GetConflictCaseDto>>> GetByFamilyMemberIdAsync(int familyMemberId)
        {
            _logger.LogInformation("Checking existence of FamilyMemberId {FamilyMemberId}", familyMemberId);

            var familyMemberExists = await _context.FamilyMembers
                .AsNoTracking()
                .AnyAsync(f => f.Id == familyMemberId);

            if (!familyMemberExists)
            {
                _logger.LogWarning("FamilyMemberId {FamilyMemberId} does not exist", familyMemberId);
                return ApiResponse<IEnumerable<GetConflictCaseDto>>.Error(HttpStatusCode.NotFound, "لم يتم العثور على هذا الفرد.");
            }

            _logger.LogInformation("Retrieving ConflictCases for FamilyMemberId {FamilyMemberId}", familyMemberId);

            var conflictCases = await _context.ConfilctCases
                .Include(c => c.Manager)
                .Include(c => c.ConflictType)
                .Include(c => c.FirstParty)
                .Include(c => c.SecondParty)
                .Where(c => c.FirstPartyId == familyMemberId || c.SecondPartyId == familyMemberId)
                .AsNoTracking()
                .ToListAsync();

            var conflictCaseDtos = _mapper.Map<IEnumerable<GetConflictCaseDto>>(conflictCases);

            if (!conflictCaseDtos.Any())
            {
                _logger.LogInformation("No ConflictCases found for FamilyMemberId {FamilyMemberId}", familyMemberId);
                return ApiResponse<IEnumerable<GetConflictCaseDto>>.Success(Array.Empty<GetConflictCaseDto>(), "لا توجد أي نزاعات لهذا الفرد.");
            }

            _logger.LogInformation("Found {Count} ConflictCases for FamilyMemberId {FamilyMemberId}", conflictCases.Count, familyMemberId);
            return ApiResponse<IEnumerable<GetConflictCaseDto>>.Success(conflictCaseDtos, "تم جلب النزاعات بنجاح.");
        }


    }
}
