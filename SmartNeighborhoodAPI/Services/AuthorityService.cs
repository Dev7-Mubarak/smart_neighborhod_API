using Microsoft.AspNetCore.Mvc.RazorPages;
using SmartNeighborhoodAPI.Entites;
using SmartNeighborhoodAPI.Helpers.DTOs.Auth;
using SmartNeighborhoodAPI.Helpers.DTOs.Contact;
using SmartNeighborhoodAPI.Interfaces;
using System.Net;

namespace SmartNeighborhoodAPI.Services
{
    public class AuthorityService : IAuthorityService
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<AuthorityService> _logger;

        public AuthorityService(ApplicationDbContext context, ILogger<AuthorityService> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<ApiResponse<PaginatedResult<GetAuthorityDto>>> GetAllAuthoritiesAsync(int page = 1, int pageSize = 10, CancellationToken ct = default)
        {
            _logger.LogInformation("جلب جميع الجهات");

            var query = _context.Authorities
                .AsNoTracking()
                .Select(a => new GetAuthorityDto
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
                });

            var totalCount = await query.CountAsync(ct);
            var items = await query.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync(ct);

            if (!items.Any())
                return ApiResponse<PaginatedResult<GetAuthorityDto>>.Error(HttpStatusCode.NotFound, "لم يتم العثور على جهات");

            var response = PaginatedResult<GetAuthorityDto>.Success(items, totalCount, page, pageSize);
            return ApiResponse<PaginatedResult<GetAuthorityDto>>.Success(response, $"تم جلب الجهات بنجاح. الصفحة {page} من {response.TotalPages}.");
        }

        public async Task<ApiResponse<GetAuthorityDto>> GetAuthorityByIdAsync(int id, CancellationToken ct = default)
        {
            _logger.LogInformation("جلب جهة حسب المعرف");
            var authority = await _context.Authorities
                .AsNoTracking()
                .Where(a => a.Id == id)
                .Select(a => new GetAuthorityDto
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
                return ApiResponse<GetAuthorityDto>.Error(HttpStatusCode.NotFound, "لم يتم العثور على الجهة");

            return ApiResponse<GetAuthorityDto>.Success(authority, "تم جلب الجهة بنجاح");
        }

        public async Task<ApiResponse<GetAuthorityDto>> AddAuthorityAsync(AddAuthorityDto dto, CancellationToken ct = default)
        {
            _logger.LogInformation("إضافة جهة جديدة");
            if (await _context.Authorities.AnyAsync(a => a.Name == dto.Name, ct))
                return ApiResponse<GetAuthorityDto>.Error(HttpStatusCode.BadRequest, "يوجد بالفعل جهة بنفس الاسم");


            var authority = new Authority
            {

                Name = dto.Name
            };

            _context.Authorities.Add(authority);
            await _context.SaveChangesAsync(ct);

            var result = new GetAuthorityDto { Id = authority.Id, Name = authority.Name, Contacts = Array.Empty<GetContactDto>() };
            return ApiResponse<GetAuthorityDto>.Success(result, "تمت إضافة الجهة بنجاح");
        }

        public async Task<ApiResponse<string>> UpdateAuthorityAsync(int id, UpdateAuthorityDto dto, CancellationToken ct = default)
        {
            _logger.LogInformation("تحديث جهة");
            var authority = await _context.Authorities.FirstOrDefaultAsync(a => a.Id == id, ct);
            if (authority == null)
                return ApiResponse<string>.Error(HttpStatusCode.NotFound, "لم يتم العثور على الجهة");

            if (await _context.Authorities.AnyAsync(a => a.Name == dto.Name && a.Id != id, ct))
                return ApiResponse<string>.Error(HttpStatusCode.BadRequest, "يوجد بالفعل جهة بنفس الاسم");

            authority.Name = dto.Name;
            _context.Authorities.Update(authority);
            await _context.SaveChangesAsync(ct);

            return ApiResponse<string>.Success("تم تحديث الجهة بنجاح");
        }

        public async Task<ApiResponse<string>> DeleteAuthorityAsync(int id, CancellationToken ct = default)
        {
            _logger.LogInformation("حذف جهة");
            var authority = await _context.Authorities.Include(a => a.Contacts).FirstOrDefaultAsync(a => a.Id == id, ct);
            if (authority == null)
                return ApiResponse<string>.Error(HttpStatusCode.NotFound, "لم يتم العثور على الجهة");

            if (authority.Contacts.Any())
                return ApiResponse<string>.Error(HttpStatusCode.BadRequest, "لا يمكن حذف الجهة لأنها تحتوي على أشخاص. قم بحذف الأشخاص أولاً.");

            _context.Authorities.Remove(authority);
            await _context.SaveChangesAsync(ct);
            return ApiResponse<string>.Success("تم حذف الجهة بنجاح");
        }
    }
}
