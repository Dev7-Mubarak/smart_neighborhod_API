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
                return ApiResponse<ReturnConflictCaseDto>.Error(HttpStatusCode.NotFound, "Manager not found.");
            }


            var isConflictTypeExists = await _context.ConfilctTypes.AnyAsync(x => x.Id == conflictCaseDto.ConflictTypeId);
            if (!isConflictTypeExists)
            {
                _logger.LogWarning("Conflict type not found");
                return ApiResponse<ReturnConflictCaseDto>.Error(HttpStatusCode.NotFound, "Conflict type not found.");
            }


            var firstParty = await _context.FamilyMembers.FindAsync(conflictCaseDto.FirstPartyId);
            if (firstParty is null)
            {
                _logger.LogWarning("First party not found");
                return ApiResponse<ReturnConflictCaseDto>.Error(HttpStatusCode.NotFound, "First party not found.");
            }


            var secondParty = await _context.FamilyMembers.FindAsync(conflictCaseDto.SecondPartyId);
            if (secondParty is null)
            {
                _logger.LogWarning("Second party not found");
                return ApiResponse<ReturnConflictCaseDto>.Error(HttpStatusCode.NotFound, "Second party not found.");
            }
            var blockExists = await _context.Blocks.AnyAsync(b => b.Id == conflictCaseDto.BlockId);
            if (!blockExists)
            {
                _logger.LogWarning("Block not found");
                return ApiResponse<ReturnConflictCaseDto>.Error(HttpStatusCode.NotFound, "Block not found.");
            }


            var conflictCase = _mapper.Map<ConflictCase>(conflictCaseDto);
            conflictCase.ManagerId = manager.Id;
            conflictCase.ImagePath = conflictCaseImage;


            _context.ConfilctCases.Add(conflictCase);
            await _context.SaveChangesAsync();


            var returnDto = _mapper.Map<ReturnConflictCaseDto>(conflictCase);
            returnDto.ManagerName = manager.UserName;

            _logger.LogInformation("ConflictCase added successfully");
            return ApiResponse<ReturnConflictCaseDto>.Success(returnDto, "تمت اضافة القضية بنجاح");
        }
        public async Task<ApiResponse<string>> DeleteAsync(int id)
        {
            _logger.LogInformation("Attempting to delete ConflictCase with ID {Id}", id);

            var entity = await _context.ConfilctCases.FirstOrDefaultAsync(x => x.Id == id);
            if (entity == null)
            {
                _logger.LogWarning("ConflictCase not found");
                return ApiResponse<string>.Error(HttpStatusCode.NotFound, "Confilct Case Not Found");
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
                return ApiResponse<string>.Success("ConfilctCases Deleted Successfully");
            }

            _logger.LogWarning("Failed to delete ConflictCase");
            return ApiResponse<string>.Error(HttpStatusCode.NotModified, "Faild To Delete the ConfilctCases");
        }
        public async Task<ApiResponse<IEnumerable<GetConflictCaseDto>>> GetAll(int? blockId = null)
        {
            _logger.LogInformation("Retrieving all ConflictCases {BlockFilter}", blockId.HasValue ? $"for Block {blockId}" : "");

            var query = _context.ConfilctCases
                .Include(c => c.Manager)
                    .ThenInclude(m => m.Person)
                .Include(c => c.ConflictType)
                .Include(c => c.FirstParty)
                    .ThenInclude(fp => fp.Person)
                .Include(c => c.SecondParty)
                    .ThenInclude(sp => sp.Person)
                .AsNoTracking();

            if (blockId.HasValue)
                query = query.Where(c => c.BlockId == blockId.Value);

            var conflictCases = await query.ToListAsync();

            if (conflictCases.Count == 0)
            {
                _logger.LogWarning("No ConflictCases found");
                return ApiResponse<IEnumerable<GetConflictCaseDto>>.Error(HttpStatusCode.NotFound, "No ConflictCase Found");
            }

            var conflictCaseDtos = _mapper.Map<IEnumerable<GetConflictCaseDto>>(conflictCases);
            return ApiResponse<IEnumerable<GetConflictCaseDto>>.Success(conflictCaseDtos);
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
                return ApiResponse<GetConflictCaseDto>.Error(HttpStatusCode.NotFound, "Conflict case not found.");
            }

            var conflictCaseDto = _mapper.Map<GetConflictCaseDto>(conflictCase);
            _logger.LogInformation("ConflictCase retrieved successfully");
            return ApiResponse<GetConflictCaseDto>.Success(conflictCaseDto);
        }
        public async Task<ApiResponse<string>> UpdateAsync(int id, UpdateConflictCaseDto conflictCaseDto)
        {
            _logger.LogInformation("Updating ConflictCase with ID {Id}", id);



            var existingConflictCase = await _context.ConfilctCases.FirstOrDefaultAsync(x => x.Id == id);

            if (existingConflictCase is null)
            {
                _logger.LogWarning("ConflictCase not found");
                return ApiResponse<string>.Error(HttpStatusCode.NotFound, "Conflict case not found.");
            }

            if (!await _context.ConfilctTypes.AnyAsync(x => x.Id == conflictCaseDto.ConflictTypeId))
            {
                _logger.LogWarning("Conflict type not found");
                return ApiResponse<string>.Error(HttpStatusCode.NotFound, "Conflict type not found.");
            }

            if (!await _context.FamilyMembers.AnyAsync(x => x.Id == conflictCaseDto.FirstPartyId))
            {
                _logger.LogWarning("First party not found");
                return ApiResponse<string>.Error(HttpStatusCode.NotFound, "First party not found.");
            }

            if (!await _context.FamilyMembers.AnyAsync(x => x.Id == conflictCaseDto.SecondPartyId))
            {
                _logger.LogWarning("Second party not found");
                return ApiResponse<string>.Error(HttpStatusCode.NotFound, "Second party not found.");
            }

            AppUser manager = null;

            if (!string.IsNullOrWhiteSpace(conflictCaseDto.ManagerId))
            {
                manager = await _userManager.FindByIdAsync(conflictCaseDto.ManagerId);
            }


            if (manager == null)
            {
                _logger.LogWarning("Manager not found during update");
                return ApiResponse<string>.Error(HttpStatusCode.NotFound, "Manager not found.");
            }

            _logger.LogInformation("Mapping updates to existing ConflictCase");
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

            _logger.LogDebug("Title after mapping: {Title}", existingConflictCase.Title);

            if (await _context.SaveChangesAsync() > 0)
            {
                _logger.LogInformation("ConflictCase updated successfully");
                return ApiResponse<string>.Success("Conflict case updated successfully.");
            }

            _logger.LogWarning("Failed to update ConflictCase");
            _logger.LogWarning("Entity state: {@Entity}", existingConflictCase);
            return ApiResponse<string>.Error(HttpStatusCode.NotModified, "Failed to update conflict case.");
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
