using SmartNeighborhoodAPI.Helpers.DTOs.Contact;
using SmartNeighborhoodAPI.Interfaces;
using System.Net;

namespace SmartNeighborhoodAPI.Services
{
    public class ContactService : IContactService
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<ContactService> _logger;

        public ContactService(ApplicationDbContext context, ILogger<ContactService> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<ApiResponse<GetContactDto>> AddContactAsync(int authorityId, AddContactDto dto, CancellationToken ct = default)
        {
            _logger.LogInformation("إضافة شخص إلى جهة");
            var authorityExists = await _context.Authorities.AnyAsync(a => a.Id == authorityId, ct);
            if (!authorityExists)
                return ApiResponse<GetContactDto>.Error(HttpStatusCode.NotFound, "لم يتم العثور على الجهة");

            if (await _context.Contacts.AnyAsync(p => p.AuthorityId == authorityId && p.Phone == dto.Phone, ct))
                return ApiResponse<GetContactDto>.Error(HttpStatusCode.BadRequest, "يوجد شخص بنفس رقم الهاتف في هذه الجهة");


            var contact = new Contact
            {
                AuthorityId = authorityId,
                Name = dto.Name,
                Job = dto.Job,
                Phone = dto.Phone
            };

            _context.Contacts.Add(contact);
            await _context.SaveChangesAsync(ct);

            var result = new GetContactDto
            {
                Id = contact.Id,
                AuthorityId = contact.AuthorityId,
                Name = contact.Name,
                Job = contact.Job,
                Phone = contact.Phone
            };

            return ApiResponse<GetContactDto>.Success(result, "تمت إضافة الشخص بنجاح");
        }

        public async Task<ApiResponse<IEnumerable<GetContactDto>>> GetContactsByAuthorityAsync(int authorityId, CancellationToken ct = default)
        {
            _logger.LogInformation("جلب الأشخاص لجهة معينة");
            var exists = await _context.Authorities.AnyAsync(a => a.Id == authorityId, ct);
            if (!exists)
                return ApiResponse<IEnumerable<GetContactDto>>.Error(HttpStatusCode.NotFound, "لم يتم العثور على الجهة");

            var contacts = await _context.Contacts
                .AsNoTracking()
                .Where(p => p.AuthorityId == authorityId)
                .Select(p => new GetContactDto
                {
                    Id = p.Id,
                    AuthorityId = p.AuthorityId,
                    Name = p.Name,
                    Job = p.Job,
                    Phone = p.Phone
                })
                .ToListAsync(ct);

            if (!contacts.Any())
                return ApiResponse<IEnumerable<GetContactDto>>.Error(HttpStatusCode.NotFound, "لا يوجد أشخاص لهذه الجهة");

            return ApiResponse<IEnumerable<GetContactDto>>.Success(contacts, "تم جلب الأشخاص بنجاح");
        }

        public async Task<ApiResponse<GetContactDto>> GetContactByIdAsync(int id, CancellationToken ct = default)
        {
            _logger.LogInformation("جلب شخص by id");
            var person = await _context.Contacts
                .AsNoTracking()
                .Where(p => p.Id == id)
                .Select(p => new GetContactDto
                {
                    Id = p.Id,
                    AuthorityId = p.AuthorityId,
                    Name = p.Name,
                    Job = p.Job,
                    Phone = p.Phone
                })
                .FirstOrDefaultAsync(ct);

            if (person == null)
                return ApiResponse<GetContactDto>.Error(HttpStatusCode.NotFound, "لم يتم العثور على الشخص");

            return ApiResponse<GetContactDto>.Success(person, "تم جلب الشخص بنجاح");
        }

        public async Task<ApiResponse<string>> UpdateContactAsync(int id, UpdateContactDto dto, CancellationToken ct = default)
        {
            _logger.LogInformation("تحديث شخص");
            var person = await _context.Contacts.FirstOrDefaultAsync(p => p.Id == id, ct);
            if (person == null)
                return ApiResponse<string>.Error(HttpStatusCode.NotFound, "لم يتم العثور على الشخص");

            if (await _context.Contacts.AnyAsync(p => p.AuthorityId == person.AuthorityId && p.Phone == dto.Phone && p.Id != id, ct))
                return ApiResponse<string>.Error(HttpStatusCode.BadRequest, "يوجد شخص بنفس رقم الهاتف في هذه الجهة");

            person.Name = dto.Name;
            person.Job = dto.Job;
            person.Phone = dto.Phone;

            _context.Contacts.Update(person);
            await _context.SaveChangesAsync(ct);

            return ApiResponse<string>.Success("تم تحديث الشخص بنجاح");
        }

        public async Task<ApiResponse<string>> DeleteContactAsync(int id, CancellationToken ct = default)
        {
            _logger.LogInformation("حذف شخص");
            var person = await _context.Contacts.FirstOrDefaultAsync(p => p.Id == id, ct);
            if (person == null)
                return ApiResponse<string>.Error(HttpStatusCode.NotFound, "لم يتم العثور على الشخص");

            _context.Contacts.Remove(person);
            await _context.SaveChangesAsync(ct);

            return ApiResponse<string>.Success("تم حذف الشخص بنجاح");
        }
    }
}
