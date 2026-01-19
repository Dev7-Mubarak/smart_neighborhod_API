using System.Net;
using SmartNeighborhoodAPI.Entites.Enums;
using SmartNeighborhoodAPI.Helpers.DTOs.Families;
using SmartNeighborhoodAPI.Helpers.DTOs.FamilyMember;
using SmartNeighborhoodAPI.Helpers.DTOs.FamilyMembers;
using SmartNeighborhoodAPI.Helpers.DTOs.Person;
using SmartNeighborhoodAPI.Helpers.DTOs.ProjectFamily;
using SmartNeighborhoodAPI.Helpers.DTOs.Teams;
using SmartNeighborhoodAPI.Interfaces;

namespace SmartNeighborhoodAPI.Services
{
    public class FamilyService : IFamilyService
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
            _logger.LogInformation("Adding a new family with details: {@FamilyDto}", familyDto);
            var isFamilyCategoryExists = await _context.FamilyCatgories.AnyAsync(x => x.Id == familyDto.FamilyCatgoryId);
            if (!isFamilyCategoryExists)
            {
                _logger.LogWarning("Family Category with ID {FamilyCategoryId} does not exist", familyDto.FamilyCatgoryId);
                return ApiResponse<ReturnFamilyDto>.Error(HttpStatusCode.NotFound, "Family Category Not Found");
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
            _logger.LogInformation("Starting to retrieve family details for Family ID: {FamilyId}", id);

            var family = await _context.Families
                .AsNoTracking()
                .Select(x => new ReturnFamilyInfoDto
                {
                    Id = x.Id,
                    Name = x.Name,
                    Location = x.Location,
                    FamilyCategoryId = x.FamilyCatgoryId,
                    FamilyCategoryName = x.FamilyCatgory.Name,
                    FamilyNotes = x.FamilyNotes,
                    BlockId = x.BlockId,
                    BlockName = x.Block.Name,
                    FamilyMembers = x.FamilyMembers.Select(fm => new ReturnFamilyMemberWithFullInfo
                    {
                        FamilyMemberId = fm.Id,
                        Role = new MemberFamilyRole
                        {
                            Id = fm.MemberFamilyRole.Id,
                            RoleName = fm.MemberFamilyRole.RoleName
                        },
                        Person = new PersonDto
                        {
                            Id = fm.Person.Id,
                            FullName = $"{fm.Person.FirstName} {fm.Person.SecondName} {fm.Person.ThirdName} {fm.Person.LastName}",
                            FirstName = fm.Person.FirstName,
                            SecondName = fm.Person.SecondName,
                            ThirdName = fm.Person.ThirdName,
                            LastName = fm.Person.LastName,
                            PhoneNumber = fm.Person.PhoneNumber,
                            DateOfBirth = fm.Person.DateOfBirth,
                            Image = string.IsNullOrEmpty(fm.Person.Image) ? null : fm.Person.Image,
                            Gender = GetDisplayName(fm.Person.Gender),
                            BloodType = GetDisplayName(fm.Person.BloodType),
                            OccupationStatus = GetDisplayName(fm.Person.OccupationStatus),
                            MaritalStatus = GetDisplayName(fm.Person.MaritalStatus),
                            Job = fm.Person.Job ?? "NAN"
                        }
                    }).ToList(),
                    Assistances = x.ProjectFamilies.Select(pfm => new Assistances
                    {
                        Id = pfm.Project.Id,
                        Name = pfm.Project.Name,
                        dateReceived = pfm.dateReceived.ToShortDateString(),
                    }).ToList()
                })
                .FirstOrDefaultAsync(x => x.Id == id);

            if (family == null)
            {
                _logger.LogWarning("Family with ID {FamilyId} was not found.", id);
                return ApiResponse<ReturnFamilyInfoDto>.Error(HttpStatusCode.NotFound, "الأسرة غير موجودة.");
            }

            _logger.LogInformation("Family found. Retrieving head of the family...");

            var headOfTheFamily = await _context.FamilyMembers
                .AsNoTracking()
                .Where(x => x.FamilyId == id && x.MemberFamilyRole.Id == 1)
                .Select(x => new HeadOfFamilyDto
                {
                    FullName = x.Person.FullName,
                    PhoneNumber = x.Person.PhoneNumber?? string.Empty,
                    FirstName = x.Person.FirstName,
                    LastName = x.Person.LastName,
                })
                .FirstOrDefaultAsync();

            if (headOfTheFamily == null)
            {
                _logger.LogWarning("Family ID {FamilyId} does not have a head of family (role ID 1).", id);
                // Return empty list for FamilyMembers
                family.FamilyMembers = new List<ReturnFamilyMemberWithFullInfo>();
                family.HeadOfFamily = null;
            }
            else
            {
                family.HeadOfFamily = headOfTheFamily;
            }

            _logger.LogInformation("Returning family details.");

            return ApiResponse<ReturnFamilyInfoDto>.Success(family, "تم جلب تفاصيل الأسرة بنجاح.");
        }
        public async Task<ApiResponse<string>> UpdateAsync(int id, FamilyDto familyDto)
        {
            _logger.LogInformation("Attempting to update Family with ID: {Id}", id);

            // Find the existing family
            var existingFamily = await _context.Families
                .Include(f => f.FamilyMembers) // include related members
                .FirstOrDefaultAsync(x => x.Id == id);

            if (existingFamily is null)
            {
                _logger.LogWarning("Family with ID {Id} not found.", id);
                return ApiResponse<string>.Error(HttpStatusCode.NotFound, "العائلة غير موجودة");
            }

            // Validate family category
            var familyCategoryExists = await _context.FamilyCatgories.AnyAsync(x => x.Id == familyDto.FamilyCatgoryId);
            if (!familyCategoryExists)
            {
                _logger.LogWarning("Family Category with ID {Id} not found.", familyDto.FamilyCatgoryId);
                return ApiResponse<string>.Error(HttpStatusCode.NotFound, "فئة العائلة غير موجودة");
            }

            // Validate block
            var blockExists = await _context.Blocks.AnyAsync(x => x.Id == familyDto.BlockId);
            if (!blockExists)
            {
                _logger.LogWarning("Block with ID {Id} not found.", familyDto.BlockId);
                return ApiResponse<string>.Error(HttpStatusCode.NotFound, "البلوك غير موجود");
            }

            // Validate person exists
            var personExists = await _context.People.AnyAsync(x => x.Id == familyDto.FamilyHeadId);
            if (!personExists)
            {
                _logger.LogWarning("Person with ID {Id} not found.", familyDto.FamilyHeadId);
                return ApiResponse<string>.Error(HttpStatusCode.NotFound, "الشخص غير موجود");
            }

            // Get the role for family head
            var headOfFamilyRole = await _context.MemberFamilyRoles
                .FirstOrDefaultAsync(r => r.RoleName == "أب");

            if (headOfFamilyRole == null)
            {
                _logger.LogError("Head of family role 'أب' not found.");
                return ApiResponse<string>.Error(HttpStatusCode.InternalServerError, "دور رب الأسرة غير موجود");
            }

            // Update basic family information
            existingFamily.Name = familyDto.Name;
            existingFamily.FamilyCatgoryId = familyDto.FamilyCatgoryId;
            existingFamily.BlockId = familyDto.BlockId;
            existingFamily.Location = familyDto.Location;
            existingFamily.FamilyNotes = familyDto.FamilyNotes;

            // --- Update Family Head ---
            var currentHead = existingFamily.FamilyMembers
                .FirstOrDefault(m => m.MemberFamilyRoleId == headOfFamilyRole.Id);

            if (currentHead != null && currentHead.PersonId != familyDto.FamilyHeadId)
            {
                // Update the family head to the new person
                _logger.LogInformation("Updating family head from Person ID {OldHead} to {NewHead}",
                    currentHead.PersonId, familyDto.FamilyHeadId);

                currentHead.PersonId = familyDto.FamilyHeadId;
                _context.FamilyMembers.Update(currentHead);
            }
            else if (currentHead == null)
            {
                // No head found, create a new record
                _logger.LogInformation("Assigning a new family head with Person ID {NewHead}", familyDto.FamilyHeadId);

                var newHead = new FamilyMember
                {
                    FamilyId = existingFamily.Id,
                    PersonId = familyDto.FamilyHeadId,
                    MemberFamilyRoleId = headOfFamilyRole.Id
                };

                await _context.FamilyMembers.AddAsync(newHead);
            }

            // Save changes
            if (await _context.SaveChangesAsync() > 0)
            {
                _logger.LogInformation("Family with ID {Id} updated successfully.", id);
                return ApiResponse<string>.Success("تم تحديث العائلة بنجاح");
            }

            _logger.LogError("Failed to update Family with ID {Id}.", id);
            return ApiResponse<string>.Error(HttpStatusCode.NotModified, "فشل في تحديث العائلة");
        }

        public async Task<ApiResponse<string>> DeleteAsync(int id)
        {
            _logger.LogInformation("Attempting to delete family with ID {FamilyId}", id);

            var entity = await _context.Families
                .Include(f => f.FamilyMembers)
                .FirstOrDefaultAsync(f => f.Id == id);

            if (entity == null)
            {
                _logger.LogWarning("Family with ID {FamilyId} not found", id);
                return ApiResponse<string>.Error(HttpStatusCode.NotFound, "العائلة غير موجودة");
            }

            // 🧹 Step 1: Get all family member IDs
            var memberIds = entity.FamilyMembers.Select(m => m.Id).ToList();

            // 🧹 Step 2: Find all related conflict cases (either as FirstParty or SecondParty)
            var relatedConflicts = await _context.ConfilctCases
                .Where(c => memberIds.Contains(c.FirstPartyId) || memberIds.Contains(c.SecondPartyId))
                .ToListAsync();

            // 🧹 Step 3: Delete all related conflict cases
            if (relatedConflicts.Any())
            {
                _context.ConfilctCases.RemoveRange(relatedConflicts);
            }

            // 🧹 Step 4: Remove the family (EF will also delete members if cascade delete is enabled)
            _context.Families.Remove(entity);

            // 🧹 Step 5: Save all changes
            if (await _context.SaveChangesAsync() <= 0)
            {
                _logger.LogError("Failed to delete family with ID {FamilyId}", id);
                return ApiResponse<string>.Error(HttpStatusCode.NotModified, "فشل في حذف العائلة");
            }

            return ApiResponse<string>.Success("تم حذف العائلة بنجاح");
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
