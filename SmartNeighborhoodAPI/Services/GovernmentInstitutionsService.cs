using SmartNeighborhoodAPI.Entites;
using SmartNeighborhoodAPI.Helpers.DTOs.Auth;
using SmartNeighborhoodAPI.Helpers.DTOs.Contact;
using SmartNeighborhoodAPI.Interfaces;
using System.Net;

namespace SmartNeighborhoodAPI.Services
{
    public class GovernmentInstitutionsService : IGovernmentInstitutionsService
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<GovernmentInstitutionsService> _logger;

        public GovernmentInstitutionsService(ApplicationDbContext context, ILogger<GovernmentInstitutionsService> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<ApiResponse<IEnumerable<GetGovernmentInstitutionsDto>>> GetAllAuthoritiesAsync(CancellationToken ct = default)
        {
            _logger.LogInformation("جلب جميع الجهات");

            var institutions = await _context.GovernmentInstitutions
                .AsNoTracking()
                .Select(a => new GetGovernmentInstitutionsDto
                {
                    Id = a.Id,
                    Name = a.Name,
                    Contacts = a.Contacts.Select(p => new GetContactDto
                    {
                        Id = p.Id,
                        AuthorityId = a.Id,
                        Name = p.Name,
                        Job = p.Job,
                        Phone = p.Phone
                    }).ToList()
                })
                .ToListAsync(ct);

            if (institutions == null || !institutions.Any())
                return ApiResponse<IEnumerable<GetGovernmentInstitutionsDto>>
                    .Error(HttpStatusCode.NotFound, "لم يتم العثور على جهات");

            return ApiResponse<IEnumerable<GetGovernmentInstitutionsDto>>
                .Success(institutions, "تم جلب الجهات بنجاح.");
        }

        public async Task<ApiResponse<GetGovernmentInstitutionsDto>> GetAuthorityByIdAsync(int id, CancellationToken ct = default)
        {
            _logger.LogInformation("جلب جهة حسب المعرف");
            var authority = await _context.GovernmentInstitutions
                .AsNoTracking()
                .Where(a => a.Id == id)
                .Select(a => new GetGovernmentInstitutionsDto
                {
                    Id = a.Id,
                    Name = a.Name,
                    Contacts = a.Contacts.Select(p => new GetContactDto
                    {
                        Id = p.Id,
                        AuthorityId = a.Id,
                        Name = p.Name,
                        Job = p.Job,
                        Phone = p.Phone
                    })
                })
                .FirstOrDefaultAsync(ct);

            if (authority == null)
                return ApiResponse<GetGovernmentInstitutionsDto>.Error(HttpStatusCode.NotFound, "لم يتم العثور على الجهة");

            return ApiResponse<GetGovernmentInstitutionsDto>.Success(authority, "تم جلب الجهة بنجاح");
        }

        public async Task<ApiResponse<GetGovernmentInstitutionsDto>> AddAuthorityAsync(AddGovernmentInstitutionsDto dto, CancellationToken ct = default)
        {
            _logger.LogInformation("إضافة جهة جديدة");
            if (await _context.GovernmentInstitutions.AnyAsync(a => a.Name == dto.Name, ct))
                return ApiResponse<GetGovernmentInstitutionsDto>.Error(HttpStatusCode.BadRequest, "يوجد بالفعل جهة بنفس الاسم");


            var authority = new GovernmentInstitution
            {

                Name = dto.Name
            };

            _context.GovernmentInstitutions.Add(authority);
            await _context.SaveChangesAsync(ct);

            var result = new GetGovernmentInstitutionsDto { Id = authority.Id, Name = authority.Name, Contacts = Array.Empty<GetContactDto>() };
            return ApiResponse<GetGovernmentInstitutionsDto>.Success(result, "تمت إضافة الجهة بنجاح");
        }

        public async Task<ApiResponse<string>> UpdateAuthorityAsync(int id, UpdateAuthorityDto dto, CancellationToken ct = default)
        {
            _logger.LogInformation("تحديث جهة");
            var authority = await _context.GovernmentInstitutions.FirstOrDefaultAsync(a => a.Id == id, ct);
            if (authority == null)
                return ApiResponse<string>.Error(HttpStatusCode.NotFound, "لم يتم العثور على الجهة");

            if (await _context.GovernmentInstitutions.AnyAsync(a => a.Name == dto.Name && a.Id != id, ct))
                return ApiResponse<string>.Error(HttpStatusCode.BadRequest, "يوجد بالفعل جهة بنفس الاسم");

            authority.Name = dto.Name;
            _context.GovernmentInstitutions.Update(authority);
            await _context.SaveChangesAsync(ct);

            return ApiResponse<string>.Success("تم تحديث الجهة بنجاح");
        }

        public async Task<ApiResponse<string>> DeleteAuthorityAsync(int id, CancellationToken ct = default)
        {
            _logger.LogInformation("حذف جهة");
            var authority = await _context.GovernmentInstitutions.Include(a => a.Contacts).FirstOrDefaultAsync(a => a.Id == id, ct);
            if (authority == null)
                return ApiResponse<string>.Error(HttpStatusCode.NotFound, "لم يتم العثور على الجهة");

            if (authority.Contacts.Any())
                return ApiResponse<string>.Error(HttpStatusCode.BadRequest, "لا يمكن حذف الجهة لأنها تحتوي على أشخاص. قم بحذف الأشخاص أولاً.");

            _context.GovernmentInstitutions.Remove(authority);
            await _context.SaveChangesAsync(ct);
            return ApiResponse<string>.Success("تم حذف الجهة بنجاح");
        }
    }
}
