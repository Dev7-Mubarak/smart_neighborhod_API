using SmartNeighborhoodAPI.Interfaces;
using System.Net;

namespace SmartNeighborhoodAPI.Services
{
    public class FamilyTypeService : IFamilyTypeService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly ILogger<FamilyTypeService> _logger;

        public FamilyTypeService(ApplicationDbContext context, IMapper mapper, ILogger<FamilyTypeService> logger)
        {
            _context = context;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<ApiResponse<GetFamilyTypeDto>> GetByIdAsync(int id)
        {
            _logger.LogInformation("Fetching FamilyType by ID: {Id}", id);

            var familyType = await _context.FamilyTypes.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);

            if (familyType == null)
            {
                _logger.LogWarning("FamilyType with ID {Id} not found.", id);
                return ApiResponse<GetFamilyTypeDto>.Error(HttpStatusCode.NotFound, "نوع العائلة غير موجود");
            }

            var dto = new GetFamilyTypeDto
            {
                Id = id,
                Name = familyType.Name
            };

            _logger.LogInformation("FamilyType with ID {Id} retrieved successfully.", id);
            return ApiResponse<GetFamilyTypeDto>.Success(dto);
        }

        public async Task<ApiResponse<IEnumerable<GetFamilyTypeDto>>> GetAll()
        {
            _logger.LogInformation("Fetching all FamilyTypes...");

            var familyTypes = _context.FamilyTypes.AsNoTracking().ToList();

            if (familyTypes.Count > 0)
            {
                var dtos = familyTypes.Select(x => new GetFamilyTypeDto
                {
                    Id = x.Id,
                    Name = x.Name,
                });

                _logger.LogInformation("{Count} FamilyTypes retrieved successfully.", dtos.Count());
                return ApiResponse<IEnumerable<GetFamilyTypeDto>>.Success(dtos);
            }

            _logger.LogWarning("No FamilyTypes found.");
            return ApiResponse<IEnumerable<GetFamilyTypeDto>>.Error(HttpStatusCode.NotFound, "لم يتم العثور على أنواع العائلات");
        }

        public async Task<ApiResponse<string>> AddAsync(string nameFamilyType)
        {
            _logger.LogInformation("Attempting to add FamilyType: {Name}", nameFamilyType);

            var existFamilyType = await _context.FamilyTypes.FirstOrDefaultAsync(x => x.Name == nameFamilyType);
            if (existFamilyType != null)
            {
                _logger.LogWarning("Add failed: FamilyType '{Name}' already exists.", nameFamilyType);
                return ApiResponse<string>.Error(HttpStatusCode.Conflict, "نوع العائلة موجود مسبقاً");
            }

            var familyType = new FamilyType { Name = nameFamilyType };
            await _context.FamilyTypes.AddAsync(familyType);

            if (await _context.SaveChangesAsync() > 0)
            {
                _logger.LogInformation("FamilyType '{Name}' added successfully.", nameFamilyType);
                return ApiResponse<string>.Success(nameFamilyType, "تمت الإضافة بنجاح");
            }

            _logger.LogError("Failed to add FamilyType '{Name}'.", nameFamilyType);
            return ApiResponse<string>.Error(HttpStatusCode.BadRequest, "فشل في إضافة نوع العائلة");
        }

        public async Task<ApiResponse<string>> DeleteAsync(int id)
        {
            _logger.LogInformation("Attempting to delete FamilyType with ID: {Id}", id);

            var entity = await _context.FamilyTypes.FindAsync(id);
            if (entity == null)
            {
                _logger.LogWarning("Delete failed: FamilyType with ID {Id} not found.", id);
                return ApiResponse<string>.Error(HttpStatusCode.NotFound, "نوع العائلة غير موجود");
            }

            _context.FamilyTypes.Remove(entity);

            if (await _context.SaveChangesAsync() > 0)
            {
                _logger.LogInformation("FamilyType with ID {Id} deleted successfully.", id);
                return ApiResponse<string>.Success("تم حذف نوع العائلة بنجاح");
            }

            _logger.LogError("Failed to delete FamilyType with ID {Id}.", id);
            return ApiResponse<string>.Error(HttpStatusCode.NotModified, "فشل في حذف نوع العائلة");
        }

        public async Task<ApiResponse<string>> UpdateAsync(int id, string nameFamilyTypeDto)
        {
            _logger.LogInformation("Attempting to update FamilyType ID {Id} with new name: {Name}", id, nameFamilyTypeDto);

            var existing = await _context.FamilyTypes.FirstOrDefaultAsync(x => x.Id == id);
            if (existing == null)
            {
                _logger.LogWarning("Update failed: FamilyType with ID {Id} not found.", id);
                return ApiResponse<string>.Error(HttpStatusCode.NotFound, "نوع العائلة غير موجود");
            }

            if (existing.Name == nameFamilyTypeDto)
            {
                _logger.LogWarning("Update skipped: new name is same as current for ID {Id}", id);
                return ApiResponse<string>.Error(HttpStatusCode.Conflict, "الاسم الجديد مطابق للاسم الحالي");
            }

            var nameConflict = await _context.FamilyTypes
                .AnyAsync(x => x.Name == nameFamilyTypeDto && x.Id != id);

            if (nameConflict)
            {
                _logger.LogWarning("Update failed: Name '{Name}' already used by another FamilyType.", nameFamilyTypeDto);
                return ApiResponse<string>.Error(HttpStatusCode.Conflict, "اسم النوع مستخدم مسبقًا");
            }

            existing.Name = nameFamilyTypeDto;
            _context.FamilyTypes.Update(existing);

            if (await _context.SaveChangesAsync() > 0)
            {
                _logger.LogInformation("FamilyType with ID {Id} updated successfully.", id);
                return ApiResponse<string>.Success("تم تحديث نوع العائلة بنجاح");
            }

            _logger.LogError("Failed to update FamilyType with ID {Id}.", id);
            return ApiResponse<string>.Error(HttpStatusCode.NotModified, "فشل في تحديث نوع العائلة");
        }
    }
}
