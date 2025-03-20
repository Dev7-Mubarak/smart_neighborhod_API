using System.Net;

namespace SmartNeighborhoodAPI.Services
{
    public class FamilyService
    {
        private readonly ApplicationDbContext _context;

        public FamilyService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<ApiResponse<Family>> AddAsync(FamilyDto familyDto)
        {
            var familyCategory = await _context.FamilyCatgories.FirstOrDefaultAsync(x => x.Id == familyDto.FamilyCatgoryId);
            if (familyCategory == null)
                return ApiResponse<Family>.Error(HttpStatusCode.NotFound, "Family Category Not Found");

            var block = await _context.Blocks.FirstOrDefaultAsync(x => x.Id == familyDto.BlockId);
            if (block == null)
                return ApiResponse<Family>.Error(HttpStatusCode.NotFound, "Block Not Found");


            var family = new Family
            {
                Name = familyDto.Name,
                FamilyCatgoryId = familyDto.FamilyCatgoryId,
                BlockId = familyDto.BlockId,
                FamilyTypeId = familyDto.FamilyTypeId,
                Location = "test",
                FamilyNotes = "test",
            };

            await _context.Families.AddAsync(family);
            if (await _context.SaveChangesAsync() > 0)
                return ApiResponse<Family>.Success(family, "Added Successfully");

            return ApiResponse<Family>.Error(HttpStatusCode.BadRequest, "Failed to add family");
        }

        public async Task<ApiResponse<string>> DeleteAsync(int id)
        {
            var entity = await _context.Families.FindAsync(id);
            if (entity == null)
                return ApiResponse<string>.Error(HttpStatusCode.NotFound, "Family Not Found");

            _context.Families.Remove(entity);
            if (await _context.SaveChangesAsync() > 0)
                return ApiResponse<string>.Success("Family Deleted Successfully");

            return ApiResponse<string>.Error(HttpStatusCode.NotModified, "Failed To Delete the Family");
        }

        public async Task<ApiResponse<IEnumerable<Family>>> GetAll()
        {
            var families = await _context.Families.AsNoTracking().ToListAsync();
            if (families.Count > 0)
            {
                return ApiResponse<IEnumerable<Family>>.Success(families);
            }

            return ApiResponse<IEnumerable<Family>>.Error(HttpStatusCode.NotFound, "No Families Found");
        }

        public async Task<ApiResponse<Family>> GetByIdAsync(int id)
        {
            var family = await _context.Families.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
            if (family == null)
                return ApiResponse<Family>.Error(HttpStatusCode.NotFound, "Family Not Found");

            return ApiResponse<Family>.Success(family);
        }

        public async Task<ApiResponse<string>> UpdateAsync(int id, FamilyDto familyDto)
        {
            var existingFamily = await _context.Families.FirstOrDefaultAsync(x => x.Id == id);

            if (existingFamily is null)
                return ApiResponse<string>.Error(HttpStatusCode.NotFound, "Family Not Found");

            existingFamily.Name = familyDto.Name;
            existingFamily.FamilyCatgoryId = familyDto.FamilyCatgoryId;
            existingFamily.FamilyTypeId = familyDto.FamilyTypeId;
            existingFamily.BlockId = familyDto.BlockId;


            _context.Families.Update(existingFamily);
            if (await _context.SaveChangesAsync() > 0)
                return ApiResponse<string>.Success("Family Updated Successfully");

            return ApiResponse<string>.Error(HttpStatusCode.NotModified, "Failed To Update Family");
        }
    }
}
