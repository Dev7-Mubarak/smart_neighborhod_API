using SmartNeighborhoodAPI.Helpers.DTOs;
using SmartNeighborhoodAPI.Helpers.DTOs.FamilyMember;
using SmartNeighborhoodAPI.Helpers.DTOs.Person;
using SmartNeighborhoodAPI.Interfaces;
using System.Net;

namespace SmartNeighborhoodAPI.Services
{
    public class FamilyMemberService : IFamilyMemberService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly ILogger<FamilyMember> _logger;

        public FamilyMemberService(ApplicationDbContext context, IMapper mapper, ILogger<FamilyMember> logger)
        {
            _context = context;
            _mapper = mapper;
            _logger = logger;
        }
        public async Task<ApiResponse<FamilyMember>> AddAsync(FamilyMemberDto familyMemberDto)
        {
            var isPersonExist = await _context.People.AnyAsync(x => x.Id == familyMemberDto.PersonId);

            if (!isPersonExist)
            {
                _logger.LogWarning("Person with ID {PersonId} does not exist.", familyMemberDto.PersonId);
                return ApiResponse<FamilyMember>.Error(HttpStatusCode.NotFound, "الشخص غير موجود");
            }

            var isFamilyExist = await _context.Families.AnyAsync(x => x.Id == familyMemberDto.FamilyId);
            if (!isFamilyExist)
            {
                _logger.LogWarning("Family with ID {FamilyId} does not exist.", familyMemberDto.FamilyId);
                return ApiResponse<FamilyMember>.Error(HttpStatusCode.NotFound, "العائلة غير موجودة");
            }

            var isRoleExist = await _context.MemberFamilyRoles.AnyAsync(x => x.Id == familyMemberDto.RoleId);
            if (!isRoleExist)
            {
                _logger.LogWarning("Role with ID {RoleId} does not exist.", familyMemberDto.RoleId);
                return ApiResponse<FamilyMember>.Error(HttpStatusCode.NotFound, "الدور غير موجود");
            }

            var isFamilyMemberRealtedWithCurrrentFamily = await _context.Families
                .Include(x => x.FamilyMembers)
                .AnyAsync(x => x.FamilyMembers.Any(x => x.PersonId == familyMemberDto.PersonId && x.FamilyId == familyMemberDto.FamilyId));

            if (isFamilyMemberRealtedWithCurrrentFamily)
            {
                _logger.LogWarning("FamilyMember with PersonId {PersonId} already exists in the family.", familyMemberDto.PersonId);
                return ApiResponse<FamilyMember>.Error(HttpStatusCode.BadRequest, "هذا الشخص مسجل بالفعل في العائلة");
            }

            var familyMember = new FamilyMember
            {
                PersonId = familyMemberDto.PersonId,
                FamilyId = familyMemberDto.FamilyId,
                MemberFamilyRoleId = familyMemberDto.RoleId,
            };

            _context.FamilyMembers.Add(familyMember);
            if (await _context.SaveChangesAsync() <= 0)
            {
                _logger.LogError("Failed to add FamilyMember with ID {FamilyMemberId}.", familyMember.Id);
                return ApiResponse<FamilyMember>.Error(HttpStatusCode.BadRequest, "فشل في إضافة الفرد إلى العائلة");
            }

            _logger.LogInformation("FamilyMember with ID {FamilyMemberId} added successfully.", familyMember.Id);
            return ApiResponse<FamilyMember>.Success(familyMember, "تمت إضافة الفرد بنجاح");
        }
        public async Task<ApiResponse<string>> DeleteAsync(int id, int familyId)
        {
            var isFamilyExists = await _context.Families.AnyAsync(f => f.Id == familyId);
            if (!isFamilyExists)
            {
                _logger.LogWarning("Family with ID {familyId} not found.", familyId);
                return ApiResponse<string>.Error(HttpStatusCode.NotFound, "لم يتم العثور على العائلة المطلوبة");
            }

            var familyMember = await _context.FamilyMembers.FirstOrDefaultAsync(x => x.Id == id && x.FamilyId == familyId);
            if (familyMember == null)
            {
                _logger.LogWarning("FamilyMember with ID {FamilyMemberId} not found.", id);
                return ApiResponse<string>.Error(HttpStatusCode.NotFound, "لم يتم العثور على الفرد في هذه العائلة");
            }

            _context.FamilyMembers.Remove(familyMember);
            if (await _context.SaveChangesAsync() <= 0)
            {
                _logger.LogError("Failed to delete FamilyMember with ID {FamilyMemberId}.", id);
                return ApiResponse<string>.Error(HttpStatusCode.NotModified, "فشل في حذف الفرد من العائلة");
            }

            return ApiResponse<string>.Success("تم حذف الفرد من العائلة بنجاح");
        }
        public async Task<ApiResponse<IEnumerable<ReturnFamilyMemberWithFullInfo>>> GetAllAsync()
        {
            _logger.LogInformation("جاري جلب جميع أفراد الأسرة من قاعدة البيانات.");

            var familyMembers = await _context.FamilyMembers
                .Include(x => x.Person)
                .Include(x => x.MemberFamilyRole)
                .AsNoTracking()
                .ToListAsync();

            if (!familyMembers.Any())
            {
                _logger.LogWarning("لم يتم العثور على أي فرد من أفراد الأسرة.");
                return ApiResponse<IEnumerable<ReturnFamilyMemberWithFullInfo>>.Error(
                    HttpStatusCode.NotFound,
                    "لم يتم العثور على أي فرد من أفراد الأسرة."
                );
            }

            var returnFamilyMember = familyMembers.Select(x => new ReturnFamilyMemberWithFullInfo
            {
                FamilyMemberId = x.Id,
                Role = x.MemberFamilyRole,
                Person = new PersonDto
                {
                    BloodType = x.Person.BloodType.ToString(),
                    DateOfBirth = x.Person.DateOfBirth,
                    Email = x.Person.Email,
                    FullName = x.Person.FullName,
                    Gender = x.Person.Gender.ToString(),
                    Id = x.Person.Id,
                    IdentityNumber = x.Person.IdentityNumber,
                    IdentityType = x.Person.IdentityType.ToString(),
                    Image = x.Person.Image,
                    IsCall = x.Person.IsContactNumber,
                    IsWhatsapp = x.Person.IsWhatsapp,
                    MaritalStatus = x.Person.MaritalStatus.ToString(),
                    OccupationStatus = x.Person.OccupationStatus.ToString(),
                    PhoneNumber = x.Person.PhoneNumber,
                    Job = x.Person.Job ?? "NAN",
                }
            });

            _logger.LogInformation($"تم جلب {familyMembers.Count} من أفراد الأسرة بنجاح.");
            return ApiResponse<IEnumerable<ReturnFamilyMemberWithFullInfo>>.Success(returnFamilyMember, "تم جلب أفراد الأسرة بنجاح.");
        }
        public async Task<ApiResponse<ReturnFamilyMemberWithFullInfo>> GetByIdAsync(int id)
        {
            _logger.LogInformation($"جاري جلب فرد الأسرة الذي يحمل المعرف {id} من قاعدة البيانات.");

            var familyMember = await _context.FamilyMembers
                .Include(x => x.Person)
                .Include(x => x.MemberFamilyRole)
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == id);

            if (familyMember == null)
            {
                _logger.LogWarning($"لم يتم العثور على فرد الأسرة بالمعرف {id}.");
                return ApiResponse<ReturnFamilyMemberWithFullInfo>.Error(
                    HttpStatusCode.NotFound,
                    "لم يتم العثور على فرد الأسرة."
                );
            }

            var familyMemberDto = new ReturnFamilyMemberWithFullInfo
            {
                FamilyMemberId = familyMember.Id,
                Role = familyMember.MemberFamilyRole,
                Person = new PersonDto
                {
                    BloodType = familyMember.Person.BloodType.ToString(),
                    DateOfBirth = familyMember.Person.DateOfBirth,
                    Email = familyMember.Person.Email,
                    FullName = familyMember.Person.FullName,
                    Gender = familyMember.Person.Gender.ToString(),
                    Id = familyMember.Person.Id,
                    IdentityNumber = familyMember.Person.IdentityNumber,
                    IdentityType = familyMember.Person.IdentityType.ToString(),
                    Image = familyMember.Person.Image,
                    IsCall = familyMember.Person.IsContactNumber,
                    IsWhatsapp = familyMember.Person.IsWhatsapp,
                    MaritalStatus = familyMember.Person.MaritalStatus.ToString(),
                    OccupationStatus = familyMember.Person.OccupationStatus.ToString(),
                    PhoneNumber = familyMember.Person.PhoneNumber,
                    Job = familyMember.Person.Job ?? "NAN",
                }
            };

            _logger.LogInformation($"تم جلب فرد الأسرة بالمعرف {id} بنجاح.");
            return ApiResponse<ReturnFamilyMemberWithFullInfo>.Success(familyMemberDto, "تم جلب فرد الأسرة بنجاح.");
        }

        public async Task<ApiResponse<string>> UpdateAsync(int id, FamilyMember FamilyMember)
        {
            var ExsitFamilyMember = await _context.FamilyMembers.FirstOrDefaultAsync(x => x.Id == id);

            if (ExsitFamilyMember is null)
                return ApiResponse<string>.Error(HttpStatusCode.NotFound, "FamilyMember Not Found");


            var UpdateFamilyMember = _mapper.Map(FamilyMember, ExsitFamilyMember);

            _context.FamilyMembers.Update(UpdateFamilyMember);
            if (await _context.SaveChangesAsync() > 0)
                return ApiResponse<string>.Success("FamilyMember Updated Successfully");

            return ApiResponse<string>.Error(HttpStatusCode.BadRequest, "Faild To Update FamilyMember");


        }
    }
}
