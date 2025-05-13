using System.Net;
using OurProjectSmartNeiborhood.Services;

namespace SmartNeighborhoodAPI.Services
{
    public class FamilyService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly FamilyMemberService _familyMemberService;
        public FamilyService(ApplicationDbContext context, IMapper mapper, FamilyMemberService familyMemberService)
        {
            _context = context;
            _mapper = mapper;
            _familyMemberService = familyMemberService;
        }
        // Edit
        public async Task<ApiResponse<ReturnFamilyDto>> AddAsync(FamilyDto familyDto)
        {
            var familyCategory = await _context.FamilyCatgories.FirstOrDefaultAsync(x => x.Id == familyDto.FamilyCatgoryId);
            if (familyCategory == null)
                return ApiResponse<ReturnFamilyDto>.Error(HttpStatusCode.NotFound, "Family Category Not Found");

            var block = await _context.Blocks.FirstOrDefaultAsync(x => x.Id == familyDto.BlockId);
            if (block == null)
                return ApiResponse<ReturnFamilyDto>.Error(HttpStatusCode.NotFound, "Block Not Found");

            var person = await _context.People.FirstOrDefaultAsync(x => x.Id == familyDto.PersonId);
            if (person == null)
                return ApiResponse<ReturnFamilyDto>.Error(HttpStatusCode.NotFound, "Person Not Found");
            var family = _mapper.Map<Family>(familyDto);

            await _context.Families.AddAsync(family);
            await _context.SaveChangesAsync();

            var fimalyMember = new FamilyMember
            {
                FamilyId = family.Id,
                PersonId = person.Id,
                MemberTypeId = 1
            };
            await _context.FamilyMembers.AddAsync(fimalyMember);

            if (await _context.SaveChangesAsync() > 0) {
                var returnFamilyDto = new ReturnFamilyDto
                {
                    Id = family.Id,
                    Name = family.Name,
                    BlockId = family.BlockId,
                    FamilyCatgoryId = family.FamilyCatgoryId,
                    FamilyNotes = family.FamilyNotes,
                    FamilyTypeId = family.FamilyTypeId,
                    Location = family.Location,
                    FamilyMemberId = fimalyMember.Id,
                };
                return ApiResponse<ReturnFamilyDto>.Success(returnFamilyDto, "Added Successfully");
            }

            return ApiResponse<ReturnFamilyDto>.Error(HttpStatusCode.BadRequest, "Failed to add family");
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
