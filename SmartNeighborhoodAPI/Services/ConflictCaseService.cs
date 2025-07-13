using Microsoft.Extensions.Logging;
using OurProjectSmartNeiborhood.Entites;
using SmartNeighborhoodAPI.Helpers.DTOs.ConfilctCase;
using SmartNeighborhoodAPI.Helpers.DTOs.Families;
using SmartNeighborhoodAPI.Helpers.DTOs.FamilyMember;
using System.Net;

namespace SmartNeighborhoodAPI.Services
{
    public class ConflictCaseService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly ImageService _imageService;
        private readonly ILogger<ConflictCaseService> _logger;
        private string _personImagePath;

        public ConflictCaseService(ApplicationDbContext context, IMapper mapper, IWebHostEnvironment webHostEnvironment, ImageService imageService, ILogger<ConflictCaseService> logger)
        {
            _context = context;
            _mapper = mapper;
            _webHostEnvironment = webHostEnvironment;
            _imageService = imageService;
            _logger = logger;
            _personImagePath = $"{_webHostEnvironment.WebRootPath}{FileHelper.PersonImagesPath}";
        }

        public async Task<ApiResponse<ReturnConflictCaseDto>> AddAsync(AddConflictCaseDto ConfilctCaseDto)
        {
            _logger.LogInformation("Start adding ConflictCase");

            string conflictCaseImage = string.Empty;

            if (ConfilctCaseDto.Image != null)
            {
                _logger.LogInformation("Saving image for ConflictCase");
                conflictCaseImage = await _imageService.SaveImageAsync(ConfilctCaseDto.Image, _personImagePath);
            }

            _logger.LogInformation("Checking if manager exists");
            var isManagerExists = await _context.People.AnyAsync(x => x.Id == ConfilctCaseDto.ManagerId);
            if (!isManagerExists)
            {
                _logger.LogWarning("Manager not found");
                return ApiResponse<ReturnConflictCaseDto>.Error(HttpStatusCode.NotFound, "Manager not found.");
            }

            _logger.LogInformation("Checking if conflict type exists");
            var isConflictTypeExists = await _context.ConfilctTypes.AnyAsync(x => x.Id == ConfilctCaseDto.ConflictTypeId);
            if (!isConflictTypeExists)
            {
                _logger.LogWarning("Conflict type not found");
                return ApiResponse<ReturnConflictCaseDto>.Error(HttpStatusCode.NotFound, "Conflict type not found.");
            }

            _logger.LogInformation("Checking if first party exists");
            var isFirstPartyExists = await _context.People.AnyAsync(x => x.Id == ConfilctCaseDto.FirstPartyId);
            if (!isFirstPartyExists)
            {
                _logger.LogWarning("First party not found");
                return ApiResponse<ReturnConflictCaseDto>.Error(HttpStatusCode.NotFound, "First party not found.");
            }

            _logger.LogInformation("Checking if second party exists");
            var isSecondPartyExists = await _context.People.AnyAsync(x => x.Id == ConfilctCaseDto.SecondPartyId);
            if (!isSecondPartyExists)
            {
                _logger.LogWarning("Second party not found");
                return ApiResponse<ReturnConflictCaseDto>.Error(HttpStatusCode.NotFound, "Second party not found.");
            }

            _logger.LogInformation("Mapping and saving ConflictCase");
            var conflictCase = _mapper.Map<ConflictCase>(ConfilctCaseDto);
            conflictCase.ImagePath = conflictCaseImage;

            _context.ConfilctCases.Add(conflictCase);
            await _context.SaveChangesAsync();

            var returnDto = _mapper.Map<ReturnConflictCaseDto>(conflictCase);

            _logger.LogInformation("ConflictCase added successfully");
            return ApiResponse<ReturnConflictCaseDto>.Success(returnDto, "تمت الاضافة بنجاح");
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

        public async Task<ApiResponse<IEnumerable<GetConflictCaseDto>>> GetAll()
        {
            _logger.LogInformation("Retrieving all ConflictCases");

            var conflictCases = await _context.ConfilctCases
                .Include(c => c.Manager)
                .Include(c => c.ConflictType)
                .Include(c => c.FirstParty)
                .Include(c => c.SecondParty)
                .AsNoTracking()
                .ToListAsync();

            if (conflictCases.Count > 0)
            {
                _logger.LogInformation("Found {Count} ConflictCases", conflictCases.Count);
                var ConflictCaseDTos = _mapper.Map<IEnumerable<GetConflictCaseDto>>(conflictCases);
                return ApiResponse<IEnumerable<GetConflictCaseDto>>.Success(ConflictCaseDTos);
            }

            _logger.LogWarning("No ConflictCases found");
            return ApiResponse<IEnumerable<GetConflictCaseDto>>.Error(HttpStatusCode.NotFound, "No ConflictCase Found");
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

            var existingConflictCase = await _context.ConfilctCases
                .Include(x => x.Manager)
                .Include(x => x.FirstParty)
                .Include(x => x.SecondParty)
                .Include(x => x.ConflictType)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (existingConflictCase is null)
            {
                _logger.LogWarning("ConflictCase not found");
                return ApiResponse<string>.Error(HttpStatusCode.NotFound, "Conflict case not found.");
            }

            _logger.LogInformation("Mapping updates to existing ConflictCase");
            _mapper.Map(conflictCaseDto, existingConflictCase);

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
                return ApiResponse<string>.Success("Conflict case updated successfully.");
            }

            _logger.LogWarning("Failed to update ConflictCase");
            return ApiResponse<string>.Error(HttpStatusCode.NotModified, "Failed to update conflict case.");
        }
    }

}
