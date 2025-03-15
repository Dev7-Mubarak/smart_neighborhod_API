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
            var family = new Family
            {
                Name = familyDto.Name,
                FamilyCatgoryId = familyDto.FamilyCatgoryId,
                Location = familyDto.Location,
                FamilyNotes = familyDto.FamilyNotes,
                FamilyTypeId = familyDto.FamilyTypeId,
                BlockId = familyDto.BlockId
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

        public async Task<ApiResponse<IEnumerable<FamilyDto>>> GetAll()
        {
            var families = await _context.Families.AsNoTracking().ToListAsync();
            if (families.Count > 0)
            {
                var familyDtos = families.Select(f => new FamilyDto
                {
                    Name = f.Name,
                    FamilyCatgoryId = f.FamilyCatgoryId,
                    Location = f.Location,
                    FamilyNotes = f.FamilyNotes,
                    FamilyTypeId = f.FamilyTypeId,

                }).ToList();

                return ApiResponse<IEnumerable<FamilyDto>>.Success(familyDtos);
            }

            return ApiResponse<IEnumerable<FamilyDto>>.Error(HttpStatusCode.NotFound, "No Families Found");
        }

        public async Task<ApiResponse<FamilyDto>> GetByIdAsync(int id)
        {
            var family = await _context.Families.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
            if (family == null)
                return ApiResponse<FamilyDto>.Error(HttpStatusCode.NotFound, "Family Not Found");

            var familyDto = new FamilyDto
            {
                Name = family.Name,
                FamilyCatgoryId = family.FamilyCatgoryId,
                Location = family.Location,
                FamilyNotes = family.FamilyNotes,
                FamilyTypeId = family.FamilyTypeId,

            };

            return ApiResponse<FamilyDto>.Success(familyDto);
        }

        public async Task<ApiResponse<string>> UpdateAsync(int id, FamilyDto familyDto)
        {
            var existingFamily = await _context.Families.FirstOrDefaultAsync(x => x.Id == id);

            if (existingFamily is null)
                return ApiResponse<string>.Error(HttpStatusCode.NotFound, "Family Not Found");

            existingFamily.Name = familyDto.Name;
            existingFamily.FamilyCatgoryId = familyDto.FamilyCatgoryId;
            existingFamily.Location = familyDto.Location;
            existingFamily.FamilyNotes = familyDto.FamilyNotes;
            existingFamily.FamilyTypeId = familyDto.FamilyTypeId;


            _context.Families.Update(existingFamily);
            if (await _context.SaveChangesAsync() > 0)
                return ApiResponse<string>.Success("Family Updated Successfully");

            return ApiResponse<string>.Error(HttpStatusCode.NotModified, "Failed To Update Family");
        }
    }
}
