using System.Net;

namespace SmartNeighborhoodAPI.Services
{
    public class FamilyMemberService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly ILogger<FamilyMemberService> _logger;

        public FamilyMemberService(ApplicationDbContext context, IMapper mapper, ILogger<FamilyMemberService> logger)
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

        public async Task<ApiResponse<IEnumerable<FamilyMember>>> GetAllAsync()
        {
            var FamilyMembers = _context.FamilyMembers.AsNoTracking().ToList();
            if (FamilyMembers.Count > 0)
            {
                var FamilyMembersDtos = _mapper.Map<IQueryable<FamilyMember>>(FamilyMembers);
                return ApiResponse<IEnumerable<FamilyMember>>.Success(FamilyMembersDtos);
            }

            return ApiResponse<IEnumerable<FamilyMember>>.Error(HttpStatusCode.NotFound, "No FamilyMember Found");
        }
        public async Task<ApiResponse<FamilyMemberDto>> GetByIdAsync(int id)
        {
            var FamilyMember = await _context.FamilyMembers.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
            if (FamilyMember == null)
                return ApiResponse<FamilyMemberDto>.Error(HttpStatusCode.NotFound, "FamilyMember Not Found");


            var FamilyMemberDto = _mapper.Map<FamilyMemberDto>(FamilyMember);
            return ApiResponse<FamilyMemberDto>.Success(FamilyMemberDto);
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
