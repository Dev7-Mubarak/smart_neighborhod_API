using System.Net;
using SmartNeighborhoodAPI.Entites.Enums;
using SmartNeighborhoodAPI.Helpers.DTOs.FamilyMember;
using SmartNeighborhoodAPI.Helpers.DTOs.Person;

namespace SmartNeighborhoodAPI.Services
{
    public class FamilyService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly ILogger<Family> _logger;
        public FamilyService(ApplicationDbContext context, IMapper mapper, ILogger<Family> logger)
        {
            _context = context;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<ApiResponse<ReturnFamilyDto>> AddAsync(FamilyDto familyDto)
        {
            _logger. LogInformation("Adding a new family with details: {@FamilyDto}", familyDto);
            var isFamilyCategoryExists = await _context.FamilyCatgories.AnyAsync(x => x.Id == familyDto.FamilyCatgoryId);
            if (!isFamilyCategoryExists)
            {
                _logger.LogWarning("Family Category with ID {FamilyCategoryId} does not exist", familyDto.FamilyCatgoryId);
                return ApiResponse<ReturnFamilyDto>.Error(HttpStatusCode.NotFound, "Family Category Not Found");
            }

            var isFamilyTypeExists = await _context.FamilyTypes.AnyAsync(x => x.Id == familyDto.FamilyTypeId);
            if (!isFamilyTypeExists)
            {
                _logger.LogWarning("Family Type with ID {FamilyTypeId} does not exist", familyDto.FamilyTypeId);
                return ApiResponse<ReturnFamilyDto>.Error(HttpStatusCode.NotFound, "Family Type Not Found");
            }

            var isBlockExists = await _context.Blocks.AnyAsync(x => x.Id == familyDto.BlockId);
            if (!isBlockExists)
            {
                _logger.LogWarning("Block with ID {BlockId} does not exist", familyDto.BlockId);
                return ApiResponse<ReturnFamilyDto>.Error(HttpStatusCode.NotFound, "Block Not Found");
            }

            var isFamilyExists = await _context.Families.AnyAsync(x => x.Name == familyDto.Name && x.BlockId == familyDto.BlockId);
            if (isFamilyExists)
            {
                _logger.LogWarning("Family with Name {FamilyName} already exists in Block ID {BlockId}", familyDto.Name, familyDto.BlockId);
                return ApiResponse<ReturnFamilyDto>.Error(HttpStatusCode.Conflict, "Family already exists in Block ");
            }

            var isPersonExists = await _context.People.AnyAsync(x => x.Id == familyDto.FamilyHeadId);
            if (!isPersonExists)
            {
                _logger.LogWarning("Person with ID {PersonId} does not exist", familyDto.FamilyHeadId);
                return ApiResponse<ReturnFamilyDto>.Error(HttpStatusCode.NotFound, "Person Not Found");
            }

            var family = new Family
            {
                Name = familyDto.Name,
                FamilyCatgoryId = familyDto.FamilyCatgoryId,
                FamilyTypeId = familyDto.FamilyTypeId,
                BlockId = familyDto.BlockId,
                Location = familyDto.Location,
                FamilyNotes = familyDto.FamilyNotes,
                HousingType = HousingType.Rent,
            };
            _logger.LogInformation("Creating a new family with details: {@Family}", family);
            await _context.Families.AddAsync(family);

            _logger.LogInformation("Get Head Of Falimy Role");
            var headOfFamilyRole = await _context.MemberFamilyRoles
                .FirstOrDefaultAsync(x => x.RoleName == "أب");

            if (headOfFamilyRole == null)
            {
                _logger.LogError("Head of family role 'أب' not found");
                return ApiResponse<ReturnFamilyDto>.Error(HttpStatusCode.InternalServerError, "Head of family role not found");
            }

            var familyMember = new FamilyMember
            {
                Family = family,
                PersonId = familyDto.FamilyHeadId,
                MemberFamilyRoleId = headOfFamilyRole.Id
            };

            _logger.LogInformation("Creating a new family member with details: {@FamilyMember}", familyMember);
            await _context.FamilyMembers.AddAsync(familyMember);

            if (await _context.SaveChangesAsync() <= 0)
                return ApiResponse<ReturnFamilyDto>.Error(HttpStatusCode.BadRequest, "Failed to add family");
          
            _logger.LogInformation("Family with ID {FamilyId} added successfully", family.Id);

            _logger.LogInformation("Call GetFamilyByIdAsync with familId {FamilyId}", family.Id);
            var response = await GetById(family.Id);

            if (!response.IsSuccess)
            {
                _logger.LogError("Failed to retrieve family details after adding: {ErrorMessage}", response.Message);
                return ApiResponse<ReturnFamilyDto>.Error(response.StatusCode, response.Message, response.Errors);
            }

            return ApiResponse<ReturnFamilyDto>.Success(response.Data, response.Message);
        }
        public async Task<ApiResponse<List<ReturnFamilyDto>>> GetAllAsync()
        {
            _logger.LogInformation("Retrieving all families");

            var families = await _context.Families
                .AsNoTracking()
                .Include(x => x.FamilyCatgory)
                .Include(x => x.FamilyType)
                .Include(x => x.Block)
                .Include(x => x.FamilyMembers)
                    .ThenInclude(x => x.MemberFamilyRole)
                .Include(x => x.FamilyMembers)
                    .ThenInclude(x => x.Person)
                .ToListAsync();

            if (!families.Any())
            {
                _logger.LogWarning("No families found");
                return ApiResponse<List<ReturnFamilyDto>>.Error(HttpStatusCode.NotFound, "No families found");
            }

            var familyDtos = families.Select(family => new ReturnFamilyDto
            {
                Id = family.Id,
                Name = family.Name,
                Location = family.Location,
                FamilyCategoryId = family.FamilyCatgoryId,
                FamilyCategoryName = family.FamilyCatgory.Name,
                FamilyNotes = family.FamilyNotes,
                FamilyTypeId = family.FamilyTypeId,
                FamilyTypeName = family.FamilyType.Name,
                BlockId = family.BlockId,
                BlockName = family.Block.Name,
                FamilyMembers = family.FamilyMembers.Select(x => new ReturnFamilyMemberDto
                {
                    PersonFullName = x.Person.FullName,
                    PersonId = x.PersonId,
                    RoleId = x.MemberFamilyRoleId,
                    RoleName = x.MemberFamilyRole.RoleName,
                }).ToList(),
            }).ToList();

            _logger.LogInformation("Successfully retrieved all families");

            return ApiResponse<List<ReturnFamilyDto>>.Success(familyDtos);
        }
        public async Task<ApiResponse<ReturnFamilyDto>> GetById(int id)
        {
            _logger.LogInformation("Retrieving family details for Family ID: {FamilyId}", id);

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
            {
                _logger.LogWarning("Family with ID {FamilyId} not found", id);
                return ApiResponse<ReturnFamilyDto>.Error(HttpStatusCode.NotFound, "Family Not Found");
            }

            var returnFamilyDto = new ReturnFamilyDto
            {
                Id = family.Id,
                Name = family.Name,
                Location = family.Location,
                FamilyCategoryId = family.FamilyCatgoryId,
                FamilyCategoryName = family.FamilyCatgory.Name,    
                FamilyNotes = family.FamilyNotes,
                FamilyTypeId = family.FamilyTypeId,
                FamilyTypeName = family.FamilyType.Name,        
                BlockId = family.BlockId,
                BlockName = family.Block.Name,
                FamilyMembers = family.FamilyMembers.Select(x => new ReturnFamilyMemberDto
                {
                    PersonFullName = x.Person.FullName,
                    PersonId = x.PersonId,
                    RoleId = x.MemberFamilyRoleId,
                    RoleName = x.MemberFamilyRole.RoleName,
                }).ToList(),
            };

            _logger.LogInformation("Successfully retrieved family details for Family ID: {FamilyId}", id);

            return ApiResponse<ReturnFamilyDto>.Success(returnFamilyDto);
        }
        public async Task<ApiResponse<ReturnFamilyInfoDto>> GetDetailesAsync(int id)
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
                //FamilyMembers = family.FamilyMembers.Select(m => new FamilyMemberDto
                //{
                //    Person = new PersonDto
                //    {
                //        Id = m.Person.Id,
                //        FirstName = m.Person.FirstName,
                //        SecondName = m.Person.LastName,
                //        ThirdName = m.Person.ThirdName,
                //        LastName = m.Person.LastName,
                //        BloodType = GetDisplayName(m.Person.BloodType),
                //        DateOfBirth = m.Person.DateOfBirth,
                //        Email = m.Person.Email,
                //        Gender = GetDisplayName(m.Person.Gender),
                //        IdentityNumber = m.Person.IdentityNumber,
                //        Image = m.Person.Image,
                //        Job = m.Person.Job,
                //        PhoneNumber = m.Person.PhoneNumber,
                //        MaritalStatus = GetDisplayName(m.Person.MaritalStatus),
                //        IsCall = m.Person.IsContactNumber,
                //        IdentityType = GetDisplayName(m.Person.IdentityType),
                //        IsWhatsapp = m.Person.IsWhatsapp,
                //        OccupationStatus = GetDisplayName(m.Person.OccupationStatus)
                //    },
                //    RoleName = m.MemberFamilyRole.RoleName
                //}).ToList()
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

            //var person = await _context.People.FirstOrDefaultAsync(x => x.Id == familyDto.PersonId);
            //if (person == null)
            //    return ApiResponse<string>.Error(HttpStatusCode.NotFound, "Person Not Found");

            existingFamily.Name = familyDto.Name;
            existingFamily.FamilyCatgoryId = familyDto.FamilyCatgoryId;
            existingFamily.FamilyTypeId = familyDto.FamilyTypeId;
            existingFamily.BlockId = familyDto.BlockId;
            existingFamily.Location = familyDto.Location;
            existingFamily.FamilyNotes = familyDto.FamilyNotes;
            //existingFamily.HousingType = familyDto.HousingType;

            _context.Families.Update(existingFamily);
            if (await _context.SaveChangesAsync() > 0)
                return ApiResponse<string>.Success("Family Updated Successfully");

            return ApiResponse<string>.Error(HttpStatusCode.NotModified, "Failed To Update Family");
        }
        public async Task<ApiResponse<string>> DeleteAsync(int id)
        {
            _logger.LogInformation("Attempting to delete family with ID {FamilyId}", id);
            var entity = await _context.Families.Include(x => x.FamilyMembers).FirstOrDefaultAsync(x => x.Id == id);
            if (entity == null)
            {
                _logger.LogWarning("Family with ID {FamilyId} not found", id);
                return ApiResponse<string>.Error(HttpStatusCode.NotFound, "Family Not Found");
            }

            _context.Families.Remove(entity);
            if (await _context.SaveChangesAsync() <= 0)
            {
                _logger.LogError("Failed to delete family with ID {FamilyId}", id);
                return ApiResponse<string>.Error(HttpStatusCode.NotModified, "Failed To Delete the Family");
            }

            return ApiResponse<string>.Success("Family Deleted Successfully");
        }
    }
}
