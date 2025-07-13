using OurProjectSmartNeiborhood.Entites;
using SmartNeighborhoodAPI.Helpers.DTOs.ConfilctCase;
using SmartNeighborhoodAPI.Helpers.DTOs.ConflictType;
using System.Net;

namespace SmartNeighborhoodAPI.Services
{
    public class ConflictTypeService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly ILogger<ConflictTypeService> _logger;

        public ConflictTypeService(ApplicationDbContext context, IMapper mapper, ILogger<ConflictTypeService> logger)
        {
            _context = context;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<ApiResponse<AddConflictTypeDto>> AddAsync(AddConflictTypeDto conflictTypeDto)
        {
            _logger.LogInformation("Starting to add a new ConflictType with name: {Name}", conflictTypeDto.Name);

            var existingConflictType = await _context.ConfilctTypes
                .FirstOrDefaultAsync(x => x.Name == conflictTypeDto.Name);

            if (existingConflictType != null)
            {
                _logger.LogWarning("ConflictType with name '{Name}' already exists", conflictTypeDto.Name);
                return ApiResponse<AddConflictTypeDto>.Error(HttpStatusCode.Conflict, "Conflict type already exists.");
            }

            _logger.LogInformation("Mapping and saving new ConflictType");
            var conflictType = new ConfilctType
            {
                Name = conflictTypeDto.Name
             
            };
            await _context.ConfilctTypes.AddAsync(conflictType);

            if (await _context.SaveChangesAsync() > 0)
            {
                _logger.LogInformation("ConflictType added successfully");
                return ApiResponse<AddConflictTypeDto>.Success(conflictTypeDto, "Added successfully.");
            }

            _logger.LogWarning("Failed to save ConflictType");
            return ApiResponse<AddConflictTypeDto>.Error(HttpStatusCode.BadRequest, "Failed to add conflict type.");
        }

        public async Task<ApiResponse<string>> DeleteAsync(int id)
        {
            _logger.LogInformation("Attempting to delete ConflictType with ID: {Id}", id);

            var entity = await _context.ConfilctTypes.FirstOrDefaultAsync(x => x.Id == id);
            if (entity == null)
            {
                _logger.LogWarning("ConflictType with ID {Id} not found", id);
                return ApiResponse<string>.Error(HttpStatusCode.NotFound, "ComplainType Not Found");
            }

            _context.ConfilctTypes.Remove(entity);

            if (await _context.SaveChangesAsync() > 0)
            {
                _logger.LogInformation("ConflictType deleted successfully");
                return ApiResponse<string>.Success("ComplainType Deleted Successfully");
            }

            _logger.LogWarning("Failed to delete ConflictType");
            return ApiResponse<string>.Error(HttpStatusCode.NotModified, "Faild To Delete the ComplainType");
        }

        public async Task<ApiResponse<IEnumerable<GetConflictTypeDto>>> GetAll()
        {
            _logger.LogInformation("Retrieving all ConflictTypes");

            var confilctTypes = await _context.ConfilctTypes.AsNoTracking().ToListAsync();
            if (confilctTypes.Count > 0)
            {
                _logger.LogInformation("Found {Count} ConflictTypes", confilctTypes.Count);
                var confilctTypesDtos = _mapper.Map<IEnumerable<GetConflictTypeDto>>(confilctTypes);
                return ApiResponse<IEnumerable<GetConflictTypeDto>>.Success(confilctTypesDtos);
            }

            _logger.LogWarning("No ConflictTypes found");
            return ApiResponse<IEnumerable<GetConflictTypeDto>>.Error(HttpStatusCode.NotFound, "No ConfilctTypes Found");
        }

        public async Task<ApiResponse<GetConflictTypeDto>> GetByIdAsync(int id)
        {
            _logger.LogInformation("Retrieving ConflictType by ID: {Id}", id);

            var conflictType = await _context.ConfilctTypes.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
            if (conflictType == null)
            {
                _logger.LogWarning("ConflictType with ID {Id} not found", id);
                return ApiResponse<GetConflictTypeDto>.Error(HttpStatusCode.NotFound, "ConfilctType Not Found");
            }

            var conflictTypeDto = _mapper.Map<GetConflictTypeDto>(conflictType);
            _logger.LogInformation("ConflictType retrieved successfully");
            return ApiResponse<GetConflictTypeDto>.Success(conflictTypeDto);
        }

        public async Task<ApiResponse<string>> UpdateAsync(int id, UpdateConflictTypeDto ComplainTypeDto)
        {
            _logger.LogInformation("Updating ConflictType with ID: {Id}", id);

            var ExsitComplainType = await _context.ConfilctTypes.FirstOrDefaultAsync(x => x.Id == id);
            if (ExsitComplainType is null)
            {
                _logger.LogWarning("ConflictType with ID {Id} not found", id);
                return ApiResponse<string>.Error(HttpStatusCode.NotFound, "ConfilctType Not Found");
            }

            _logger.LogInformation("Mapping updates to ConflictType");
            var UpdateComplainType = _mapper.Map(ComplainTypeDto, ExsitComplainType);

            _context.ConfilctTypes.Update(UpdateComplainType);

            if (await _context.SaveChangesAsync() > 0)
            {
                _logger.LogInformation("ConflictType updated successfully");
                return ApiResponse<string>.Success("ConfilctType Updated Successfully");
            }

            _logger.LogWarning("Failed to update ConflictType");
            return ApiResponse<string>.Error(HttpStatusCode.NotModified, "Faild To Update ComplainType");
        }
    }

}
