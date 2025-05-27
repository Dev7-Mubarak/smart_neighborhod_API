using System.Net;
using OurProjectSmartNeiborhood.Entites;
using OurProjectSmartNeiborhood.Services;
using SmartNeighborhoodAPI.Helpers.DTOs.Person;

namespace SmartNeighborhoodAPI.Services
{
    public class FamilyService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        public FamilyService(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<ApiResponse<IEnumerable<ReturnFamilyDto>>> GetAllAsync()
        {
            var families = await _context.Families.AsNoTracking().ToListAsync();
            if (families.Count > 0)
            {
                var returnFamilyDto = families.Select(x => new ReturnFamilyDto
                {
                    Id = x.Id,
                    Name = x.Name,
                    BlockId = x.BlockId,
                    HousingType = GetDisplayName(x.HousingType),
                    FamilyCatgoryId = x.FamilyCatgoryId,
                    FamilyNotes = x.FamilyNotes,
                    FamilyTypeId = x.FamilyTypeId,
                    Location = x.Location,
                    FamilyMemberId = x.Id,
                });
                return ApiResponse<IEnumerable<ReturnFamilyDto>>.Success(returnFamilyDto);
            }

            return ApiResponse<IEnumerable<ReturnFamilyDto>>.Error(HttpStatusCode.NotFound, "No Families Found");
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

            //var s = _context.FamilyMembers.FirstOrDefaultAsync(x => x.PersonId == familyDto.PersonId && x.MemberFamilyRole.Id == 1);
            //if (person == null)
            //    return ApiResponse<ReturnFamilyDto>.Error(HttpStatusCode.NotFound, "This Preson is Already a head of an another family");

            var family = _mapper.Map<Family>(familyDto);

            await _context.Families.AddAsync(family);
            await _context.SaveChangesAsync();

            var fimalyMember = new FamilyMember
            {
                FamilyId = family.Id,
                PersonId = person.Id,
                MemberFamilyRoleId = 1
            };
            await _context.FamilyMembers.AddAsync(fimalyMember);

            if (await _context.SaveChangesAsync() > 0) {
                var returnFamilyDto = new ReturnFamilyDto
                {
                    Id = family.Id,
                    Name = family.Name,
                    BlockId = family.BlockId,
                    HousingType = GetDisplayName(family.HousingType),
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

        public async Task<ApiResponse<ReturnFamilyInfoDto>> GetFamilyDetilesByIdAsync(int id)
        {
            var family = await _context.Families
                .AsNoTracking()
                .Include(x => x.FamilyCatgory)
                .Include(x => x.FamilyType)
                .Include(x => x.Block)
                .Include(x => x.FamilyMembers)
                .ThenInclude(x => x.MemberFamilyRole)
                .Include(x => x.FamilyMembers)
                .ThenInclude(x => x.Person)
                .FirstOrDefaultAsync(x => x.Id == id);
            if (family == null)
                return ApiResponse<ReturnFamilyInfoDto>.Error(HttpStatusCode.NotFound, "Family Not Found");

            var headOfTheFamily = _context.FamilyMembers
                .AsNoTracking()
                .Include(x => x.MemberFamilyRole)
                .Include(x => x.Person)
                .FirstOrDefault(x => x.FamilyId == id && x.MemberFamilyRole.Id == 1);

            if (headOfTheFamily == null)
                return ApiResponse<ReturnFamilyInfoDto>.Error(HttpStatusCode.NotFound, "This Family Does Not Have A Father");

            var dto = new ReturnFamilyInfoDto
            {
                Id = family.Id,
                Name = family.Name,
                Location = family.Location,
                FamilyNotes = family.FamilyNotes,
                FamilyCatgoryId = family.FamilyCatgoryId,
                FamilyCatgoryName = family.FamilyCatgory.Name,
                FamilyTypeId = family.FamilyTypeId,
                FamilyTypeName = family.FamilyType.Name,
                BlockId = family.BlockId,
                BlockName = family.Block.Name,
                HeadOfTheFamilyId = headOfTheFamily.Id,
                HeadOfTheFamilyName = headOfTheFamily.Person.FirstName,
                FamilyMembers = family.FamilyMembers.Select(m => new FamilyMemberDto
                {
                    Person = new PersonDto
                    {
                        Id = m.Person.Id,
                        FirstName = m.Person.FirstName,
                        SecondName = m.Person.LastName,
                        ThirdName = m.Person.ThirdName,
                        LastName = m.Person.LastName,
                        BloodType = GetDisplayName(m.Person.BloodType),
                        DateOfBirth = m.Person.DateOfBirth,
                        Email = m.Person.Email,
                        Gender = GetDisplayName(m.Person.Gender),
                        IdentityNumber = m.Person.IdentityNumber,
                        Image = m.Person.Image,
                        Job = m.Person.Job,
                        PhoneNumber = m.Person.PhoneNumber,
                        MaritalStatus = GetDisplayName(m.Person.MaritalStatus),
                        IsCall = m.Person.IsContactNumber,
                        IdentityType = GetDisplayName(m.Person.IdentityType),
                        IsWhatsapp = m.Person.IsWhatsapp,
                        OccupationStatus = GetDisplayName(m.Person.OccupationStatus)
                    },
                    RoleName = m.MemberFamilyRole.RoleName
                }).ToList()
            };

            return ApiResponse<ReturnFamilyInfoDto>.Success(dto);
        }

        public async Task<ApiResponse<string>> UpdateAsync(int id, FamilyDto familyDto)
        {
            var existingFamily = await _context.Families.FirstOrDefaultAsync(x => x.Id == id);

            if (existingFamily is null)
                return ApiResponse<string>.Error(HttpStatusCode.NotFound, "Family Not Found");

            var familyCategory = await _context.FamilyCatgories.FirstOrDefaultAsync(x => x.Id == familyDto.FamilyCatgoryId);
            if (familyCategory == null)
                return ApiResponse<string>.Error(HttpStatusCode.NotFound, "Family Category Not Found");

            var block = await _context.Blocks.FirstOrDefaultAsync(x => x.Id == familyDto.BlockId);
            if (block == null)
                return ApiResponse<string>.Error(HttpStatusCode.NotFound, "Block Not Found");

            var person = await _context.People.FirstOrDefaultAsync(x => x.Id == familyDto.PersonId);
            if (person == null)
                return ApiResponse<string>.Error(HttpStatusCode.NotFound, "Person Not Found");

            existingFamily.Name = familyDto.Name;
            existingFamily.FamilyCatgoryId = familyDto.FamilyCatgoryId;
            existingFamily.FamilyTypeId = familyDto.FamilyTypeId;
            existingFamily.BlockId = familyDto.BlockId;
            existingFamily.Location = familyDto.Location;
            existingFamily.FamilyNotes = familyDto.FamilyNotes;
            existingFamily.HousingType = familyDto.HousingType;

            _context.Families.Update(existingFamily);
            if (await _context.SaveChangesAsync() > 0)
                return ApiResponse<string>.Success("Family Updated Successfully");

            return ApiResponse<string>.Error(HttpStatusCode.NotModified, "Failed To Update Family");
        }
        public async Task<ApiResponse<string>> DeleteAsync(int id)
        {
            var entity = await _context.Families.Include(x => x.FamilyMembers).FirstOrDefaultAsync(x => x.Id == id);
            if (entity == null)
                return ApiResponse<string>.Error(HttpStatusCode.NotFound, "Family Not Found");

            _context.Families.Remove(entity);
            if (await _context.SaveChangesAsync() > 0)
                return ApiResponse<string>.Success("Family Deleted Successfully");

            return ApiResponse<string>.Error(HttpStatusCode.NotModified, "Failed To Delete the Family");
        }
        private static string GetDisplayName<T>(T enumValue)
        {
            var memberInfo = typeof(T).GetMember(enumValue.ToString()).FirstOrDefault();
            var displayAttr = memberInfo?.GetCustomAttributes(typeof(DisplayAttribute), false)
                                        .FirstOrDefault() as DisplayAttribute;

            return displayAttr?.Name ?? enumValue.ToString();
        }
    }
}
