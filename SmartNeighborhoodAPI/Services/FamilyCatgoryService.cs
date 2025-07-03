using System.Net;

namespace SmartNeighborhoodAPI.Services
{
    public class FamilyCatgoryService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly ILogger<FamilyCatgoryService> _logger;

        public FamilyCatgoryService(ApplicationDbContext context, IMapper mapper, ILogger<FamilyCatgoryService> logger)
        {
            _context = context;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<ApiResponse<string>> AddAsync(string name)
        {
            _logger.LogInformation("Attempting to add new FamilyCategory with name: {Name}", name);

            var existFamilyCategory = await _context.FamilyCatgories.FirstOrDefaultAsync(x => x.Name == name);

            if (existFamilyCategory != null)
            {
                _logger.LogWarning("Add failed: FamilyCategory with name '{Name}' already exists.", name);
                return ApiResponse<string>.Error(HttpStatusCode.Conflict, "الفئة العائلية موجودة بالفعل");
            }

            var familyCatgory = new FamilyCatgory { Name = name };
            await _context.FamilyCatgories.AddAsync(familyCatgory);

            if (await _context.SaveChangesAsync() > 0)
            {
                _logger.LogInformation("FamilyCategory '{Name}' added successfully.", name);
                return ApiResponse<string>.Success(name, "تمت الإضافة بنجاح");
            }

            _logger.LogError("Failed to add FamilyCategory '{Name}'.", name);
            return ApiResponse<string>.Error(HttpStatusCode.BadRequest, "فشل في إضافة الفئة العائلية");
        }

        public async Task<ApiResponse<IEnumerable<FamilyCatgoryDto>>> GetAll()
        {
            _logger.LogInformation("Fetching all FamilyCategories.");

            var familyCategories = await _context.FamilyCatgories.AsNoTracking().ToListAsync();

            if (familyCategories.Count > 0)
            {
                var dtos = familyCategories.Select(x => new FamilyCatgoryDto
                {
                    Id = x.Id,
                    Name = x.Name,
                });

                _logger.LogInformation("Retrieved {Count} FamilyCategories.", dtos.Count());
                return ApiResponse<IEnumerable<FamilyCatgoryDto>>.Success(dtos);
            }

            _logger.LogWarning("No FamilyCategories found.");
            return ApiResponse<IEnumerable<FamilyCatgoryDto>>.Error(HttpStatusCode.NotFound, "لم يتم العثور على أي فئة عائلية");
        }

        public async Task<ApiResponse<FamilyCatgoryDto>> GetByIdAsync(int id)
        {
            _logger.LogInformation("Fetching FamilyCategory by ID: {Id}", id);

            var familyCategory = await _context.FamilyCatgories.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);

            if (familyCategory == null)
            {
                _logger.LogWarning("FamilyCategory with ID {Id} not found.", id);
                return ApiResponse<FamilyCatgoryDto>.Error(HttpStatusCode.NotFound, "الفئة العائلية غير موجودة");
            }

            var dto = new FamilyCatgoryDto
            {
                Id = familyCategory.Id,
                Name = familyCategory.Name,
            };

            _logger.LogInformation("FamilyCategory with ID {Id} retrieved successfully.", id);
            return ApiResponse<FamilyCatgoryDto>.Success(dto);
        }

        public async Task<ApiResponse<string>> DeleteAsync(int id)
        {
            _logger.LogInformation("Attempting to delete FamilyCategory with ID: {Id}", id);

            var familyCategory = await _context.FamilyCatgories.FirstOrDefaultAsync(x => x.Id == id);

            if (familyCategory == null)
            {
                _logger.LogWarning("Delete failed: FamilyCategory with ID {Id} not found.", id);
                return ApiResponse<string>.Error(HttpStatusCode.NotFound, "الفئة العائلية غير موجودة");
            }

            _context.FamilyCatgories.Remove(familyCategory);

            if (await _context.SaveChangesAsync() > 0)
            {
                _logger.LogInformation("FamilyCategory with ID {Id} deleted successfully.", id);
                return ApiResponse<string>.Success("تم حذف الفئة العائلية بنجاح");
            }

            _logger.LogError("Failed to delete FamilyCategory with ID {Id}.", id);
            return ApiResponse<string>.Error(HttpStatusCode.NotModified, "فشل في حذف الفئة العائلية");
        }

        public async Task<ApiResponse<string>> UpdateAsync(int id, string nameFamilyCatgory)
        {
            _logger.LogInformation("Attempting to update FamilyCategory ID {Id} with new name: {Name}", id, nameFamilyCatgory);

            var existing = await _context.FamilyCatgories.FirstOrDefaultAsync(x => x.Id == id);

            if (existing == null)
            {
                _logger.LogWarning("Update failed: FamilyCategory with ID {Id} not found.", id);
                return ApiResponse<string>.Error(HttpStatusCode.NotFound, "الفئة العائلية غير موجودة");
            }

            if (existing.Name == nameFamilyCatgory)
            {
                _logger.LogWarning("Update skipped: New name is the same as the current name for FamilyCategory ID {Id}.", id);
                return ApiResponse<string>.Error(HttpStatusCode.Conflict, "الاسم الجديد موجود مسبقاً");
            }

            existing.Name = nameFamilyCatgory;
            _context.FamilyCatgories.Update(existing);

            if (await _context.SaveChangesAsync() > 0)
            {
                _logger.LogInformation("FamilyCategory with ID {Id} updated successfully.", id);
                return ApiResponse<string>.Success("تم التحديث بنجاح");
            }

            _logger.LogError("Failed to update FamilyCategory with ID {Id}.", id);
            return ApiResponse<string>.Error(HttpStatusCode.NotModified, "فشل في تحديث الفئة العائلية");
        }
    }
}
