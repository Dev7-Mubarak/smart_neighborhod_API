using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MramProject.Interface;
using SmartNeighborhoodAPI.Entites;
using SmartNeighborhoodAPI.Helpers.DTOs.Government_InstitutionsContacts;
using System.Net;

namespace MramProject.Services
{
    public class GovernmentInstitutionContactService : IGovernmentInstitutionContactService
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<GovernmentInstitutionContactService> _logger;

        public GovernmentInstitutionContactService(ApplicationDbContext context, ILogger<GovernmentInstitutionContactService> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<ApiResponse<GetGovernmentInstitutionContactDto>> AddContactAsync(int GovernmentInstitutionId, AddGovernmentInstitutionContactDto dto, CancellationToken ct = default)
        {
            _logger.LogInformation("إضافة جهة اتصال إلى جهة");
            var authorityExists = await _context.GovernmentInstitutions.AnyAsync(a => a.Id == GovernmentInstitutionId, ct);
            if (!authorityExists)
                return ApiResponse<GetGovernmentInstitutionContactDto>.Error(HttpStatusCode.NotFound, "لم يتم العثور على الجهة");

            if (await _context.GovernmentInstitutionContacts.AnyAsync(p => p.GovernmentInstitutionId == GovernmentInstitutionId && p.Phone == dto.Phone, ct))
                return ApiResponse<GetGovernmentInstitutionContactDto>.Error(HttpStatusCode.BadRequest, "يوجد جهة اتصال بنفس رقم الهاتف في هذه الجهة");

            var contact = new GovernmentInstitutionContact
            {
                GovernmentInstitutionId = GovernmentInstitutionId,
                Name = dto.Name,
                Job = dto.Job,
                Phone = dto.Phone
            };

            _context.GovernmentInstitutionContacts.Add(contact);
            await _context.SaveChangesAsync(ct);

            var result = new GetGovernmentInstitutionContactDto
            {
                Id = contact.Id,
                GovernmentInstitutionId = contact.GovernmentInstitutionId,
                Name = contact.Name,
                Job = contact.Job,
                Phone = contact.Phone
            };

            return ApiResponse<GetGovernmentInstitutionContactDto>.Success(result, "تمت إضافة جهة الاتصال بنجاح");
        }

        public async Task<ApiResponse<IEnumerable<GetGovernmentInstitutionContactDto>>> GetContactsByAuthorityAsync(int GovernmentInstitutionId, CancellationToken ct = default)
        {
            _logger.LogInformation("جلب جهات الاتصال لجهة معينة");
            var exists = await _context.GovernmentInstitutions.AnyAsync(a => a.Id == GovernmentInstitutionId, ct);
            if (!exists)
                return ApiResponse<IEnumerable<GetGovernmentInstitutionContactDto>>.Error(HttpStatusCode.NotFound, "لم يتم العثور على الجهة");

            var contacts = await _context.GovernmentInstitutionContacts
                .AsNoTracking()
                .Where(p => p.GovernmentInstitutionId == GovernmentInstitutionId)
                .Select(p => new GetGovernmentInstitutionContactDto
                {
                    Id = p.Id,
                    GovernmentInstitutionId = p.GovernmentInstitutionId,
                    Name = p.Name,
                    Job = p.Job,
                    Phone = p.Phone
                })
                .ToListAsync(ct);

            if (!contacts.Any())
                return ApiResponse<IEnumerable<GetGovernmentInstitutionContactDto>>.Error(HttpStatusCode.NotFound, "لا يوجد جهات اتصال لهذه الجهة");

            return ApiResponse<IEnumerable<GetGovernmentInstitutionContactDto>>.Success(contacts, "تم جلب جهات الاتصال بنجاح");
        }

        public async Task<ApiResponse<GetGovernmentInstitutionContactDto>> GetContactByIdAsync(int id, CancellationToken ct = default)
        {
            _logger.LogInformation("جلب جهة اتصال by id");
            var person = await _context.GovernmentInstitutionContacts
                .AsNoTracking()
                .Where(p => p.Id == id)
                .Select(p => new GetGovernmentInstitutionContactDto
                {
                    Id = p.Id,
                    GovernmentInstitutionId = p.GovernmentInstitutionId,
                    Name = p.Name,
                    Job = p.Job,
                    Phone = p.Phone
                })
                .FirstOrDefaultAsync(ct);

            if (person == null)
                return ApiResponse<GetGovernmentInstitutionContactDto>.Error(HttpStatusCode.NotFound, "لم يتم العثور على جهة الاتصال");

            return ApiResponse<GetGovernmentInstitutionContactDto>.Success(person, "تم جلب جهة الاتصال بنجاح");
        }

        public async Task<ApiResponse<string>> UpdateContactAsync(int id, UpdateGovernmentInstitutionContactDto dto, CancellationToken ct = default)
        {
            _logger.LogInformation("تحديث جهة اتصال");
            var person = await _context.GovernmentInstitutionContacts.FirstOrDefaultAsync(p => p.Id == id, ct);
            if (person == null)
                return ApiResponse<string>.Error(HttpStatusCode.NotFound, "لم يتم العثور على جهة الاتصال");

            if (await _context.GovernmentInstitutionContacts.AnyAsync(p => p.GovernmentInstitutionId == person.GovernmentInstitutionId && p.Phone == dto.Phone && p.Id != id, ct))
                return ApiResponse<string>.Error(HttpStatusCode.BadRequest, "يوجد جهة اتصال بنفس رقم الهاتف في هذه الجهة");

            person.Name = dto.Name;
            person.Job = dto.Job;
            person.Phone = dto.Phone;

            _context.GovernmentInstitutionContacts.Update(person);
            await _context.SaveChangesAsync(ct);

            return ApiResponse<string>.Success("تم تحديث جهة الاتصال بنجاح");
        }

        public async Task<ApiResponse<string>> DeleteContactAsync(int id, CancellationToken ct = default)
        {
            _logger.LogInformation("حذف جهة اتصال");
            var person = await _context.GovernmentInstitutionContacts.FirstOrDefaultAsync(p => p.Id == id, ct);
            if (person == null)
                return ApiResponse<string>.Error(HttpStatusCode.NotFound, "لم يتم العثور على جهة الاتصال");

            _context.GovernmentInstitutionContacts.Remove(person);
            await _context.SaveChangesAsync(ct);

            return ApiResponse<string>.Success("تم حذف جهة الاتصال بنجاح");
        }
    }
}

