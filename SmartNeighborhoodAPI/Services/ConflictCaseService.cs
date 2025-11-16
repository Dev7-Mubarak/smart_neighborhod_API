using Microsoft.AspNetCore.Hosting;
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
    public class ConflictCaseService
    {
        private readonly ApplicationDbContext _context;
        private readonly UserContextService _userContextService;
        private readonly IHierarchyService _hierarchyService;
        private readonly IMapper _mapper;
        private readonly ILogger<ConflictCaseService> _logger;
        private readonly UserManager<AppUser> _userManager;
        private readonly ImageService _imageService;
        private string _personImagePath;

        private readonly IWebHostEnvironment _webHostEnvironment;

        public ConflictCaseService(
            ApplicationDbContext context,
            UserContextService userContextService,
            IHierarchyService hierarchyService,
            IMapper mapper,
            ILogger<ConflictCaseService> logger,
            UserManager<AppUser> userManager,
            ImageService imageService,
            IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _userContextService = userContextService;
            _hierarchyService = hierarchyService;
            _mapper = mapper;
            _logger = logger;
            _webHostEnvironment = webHostEnvironment;
            _userManager = userManager;
            _imageService = imageService;
            _personImagePath = $"{_webHostEnvironment.WebRootPath}{FileHelper.PersonImagesPath}";
      
        }

        public async Task<ApiResponse<IEnumerable<GetConflictCaseDto>>> GetAll()
        {
            var currentUser = _userContextService.GetCurrentUser();
            _logger.LogInformation("Retrieving ConflictCases for user {UserId} with role {Role}", currentUser.Id, currentUser.Role);

            if (currentUser.Role == Role.User)
            {
                _logger.LogWarning("User {UserId} attempted to access ConflictCases without permission", currentUser.Id);
                return ApiResponse<IEnumerable<GetConflictCaseDto>>
                    .Error(HttpStatusCode.Unauthorized, "ليس لديك صلاحية الوصول إلى هذه البيانات.");
            }

            IQueryable<ConflictCase> query = _context.ConfilctCases
                .Include(c => c.Manager).ThenInclude(m => m.Person)
                .Include(c => c.ConflictType)
                .Include(c => c.FirstParty).ThenInclude(fp => fp.Person)
                .Include(c => c.SecondParty).ThenInclude(sp => sp.Person)
                .AsNoTracking();

            var allowedBlockIds = await _hierarchyService.GetAllowedBlockIdsAsync();

            if (!allowedBlockIds.Any())
            {
                _logger.LogInformation("User {UserId} has no blocks in hierarchy.", currentUser.Id);
                return ApiResponse<IEnumerable<GetConflictCaseDto>>
                    .Success(Enumerable.Empty<GetConflictCaseDto>(), "لا توجد قضايا نزاع.");
            }

            query = query.Where(c => allowedBlockIds.Contains(c.BlockId));

            var conflictCases = await query.ToListAsync();
            var conflictCaseDtos = _mapper.Map<IEnumerable<GetConflictCaseDto>>(conflictCases);

            if (conflictCaseDtos.Any())
            {
                _logger.LogInformation("Found {Count} ConflictCases", conflictCaseDtos.Count());
                return ApiResponse<IEnumerable<GetConflictCaseDto>>
                    .Success(conflictCaseDtos, "تم جلب جميع القضايا بنجاح.");
            }

            _logger.LogWarning("No ConflictCases found");
            return ApiResponse<IEnumerable<GetConflictCaseDto>>
                .Success(Enumerable.Empty<GetConflictCaseDto>(), "لا توجد قضايا نزاع.");
        }


        public async Task<ApiResponse<ReturnConflictCaseDto>> AddAsync(AddConflictCaseDto conflictCaseDto)
        {
            _logger.LogInformation("Start adding ConflictCase");

            // 1️ Check if Manager exists
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

            // 2️ Check if ConflictType exists
            var isConflictTypeExists = await _context.ConfilctTypes
                .AnyAsync(x => x.Id == conflictCaseDto.ConflictTypeId);

            if (!isConflictTypeExists)
            {
                _logger.LogWarning("Conflict type not found");
                return ApiResponse<ReturnConflictCaseDto>.Error(HttpStatusCode.NotFound, "نوع النزاع غير موجود.");
            }

            // 3️ Check if FirstParty exists
            var firstParty = await _context.FamilyMembers.FindAsync(conflictCaseDto.FirstPartyId);
            if (firstParty is null)
            {
                _logger.LogWarning("First party not found");
                return ApiResponse<ReturnConflictCaseDto>.Error(HttpStatusCode.NotFound, "الطرف الأول غير موجود.");
            }

            // 4️ Check if SecondParty exists
            var secondParty = await _context.FamilyMembers.FindAsync(conflictCaseDto.SecondPartyId);
            if (secondParty is null)
            {
                _logger.LogWarning("Second party not found");
                return ApiResponse<ReturnConflictCaseDto>.Error(HttpStatusCode.NotFound, "الطرف الثاني غير موجود.");
            }

            // 5️ Prevent same party on both sides
            if (conflictCaseDto.FirstPartyId == conflictCaseDto.SecondPartyId)
            {
                _logger.LogWarning("First and second party cannot be the same");
                return ApiResponse<ReturnConflictCaseDto>.Error(HttpStatusCode.BadRequest, "لا يمكن أن يكون الطرف الأول والطرف الثاني نفس الشخص.");
            }

            // 6️ Save image (if exists)
            string conflictCaseImage = string.Empty;
            if (conflictCaseDto.Image != null)
            {
                _logger.LogInformation("Saving image for ConflictCase");
                conflictCaseImage = await _imageService.SaveImageAsync(conflictCaseDto.Image, _personImagePath);
            }

            // 7️ Map DTO → Entity
            var conflictCase = _mapper.Map<ConflictCase>(conflictCaseDto);
            conflictCase.ManagerId = manager.Id;
            conflictCase.ImagePath = conflictCaseImage;

            // 8️ Add and Save
            _context.ConfilctCases.Add(conflictCase);
            await _context.SaveChangesAsync();

            // 9️ Map back to return DTO
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
