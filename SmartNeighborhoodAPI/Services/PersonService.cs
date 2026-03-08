using SmartNeighborhoodAPI.Entites;
using SmartNeighborhoodAPI.Entites.Enums;
using SmartNeighborhoodAPI.Helpers;
using SmartNeighborhoodAPI.Helpers.DTOs.Person;
using SmartNeighborhoodAPI.Interfaces;
using SmartNeighborhoodAPI.Services;
using System.Net;
using SmartNeighborhoodAPI.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace OurProjectSmartNeiborhood.Services
{

    public class PersonService : IPersonService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly ImageService _imageService;
        private string _personImagePath;
        private readonly UserContextService _userContextService;
        private readonly ILogger<PersonService> _logger;
        private readonly UserManager<AppUser> _userManager;


        public PersonService(ApplicationDbContext context, IMapper mapper, IWebHostEnvironment webHostEnvironment, ImageService imageService, ILogger<PersonService> logger, UserContextService userContextService, UserManager<AppUser> userManager)
        {
            _context = context;
            _mapper = mapper;
            _webHostEnvironment = webHostEnvironment;
            _personImagePath = $"{_webHostEnvironment.WebRootPath}{FileHelper.PersonImagesPath}";
            _imageService = imageService;
            _logger = logger;
            _userContextService = userContextService;
            _userManager = userManager;
        }

        public async Task<ApiResponse<Person>> AddAsync(CreatePersonDto dto)
        {
            string personImage = string.Empty;
            if (dto.Image != null)
            {
                personImage = await _imageService.SaveImageAsync(dto.Image, _personImagePath);
            }

            var person = new Person
            {
                FirstName = dto.FirstName,
                SecondName = dto.SecondName,
                ThirdName = dto.ThirdName,
                LastName = dto.LastName,
                PhoneNumber = dto.PhoneNumber,
                DateOfBirth = dto.DateOfBirth,
                Gender = dto.Gender,
                Image = string.IsNullOrEmpty(personImage) ? null : personImage,
                BloodType = dto.BloodType,
                MaritalStatus = dto.MaritalStatus,
                OccupationStatus = dto.OccupationStatus,
                Job = dto.Job,
                NationalId = dto.NationalId,
                VehicleType = dto.VehicleType,
                VehicleRegistrationNumber = dto.VehicleRegistrationNumber,
                ResidencyStatus = dto.ResidencyStatus,
                HasChronicDiseases = dto.HasChronicDiseases,
                ChronicDiseasesNotes = dto.ChronicDiseasesNotes
            };

            await _context.People.AddAsync(person);
            if (await _context.SaveChangesAsync() > 0)
            {
                return ApiResponse<Person>.Success(person, "تم أضافة الشحص بنجاح");
            }

            return ApiResponse<Person>.Error(HttpStatusCode.BadGateway, "فشلت عملية الاضافة");
        }
        public async Task<ApiResponse<string>> DeleteAsync(int id)
        {
            var entity = await _context.People
                .Include(p => p.FamilyMembers)
                .FirstOrDefaultAsync(p => p.Id == id);
            if (entity == null)

                return ApiResponse<string>.Error(HttpStatusCode.NotFound, "الشخص غير موجود.");
            var associatedUser = await _userManager.Users.FirstOrDefaultAsync(u => u.PersonId == id);
            if (associatedUser != null)
            {
                var deleteResult = await _userManager.DeleteAsync(associatedUser);
                if (!deleteResult.Succeeded)
                {
                    _logger.LogError("Failed to delete associated user for person {PersonId}", id);
                    return ApiResponse<string>.Error(HttpStatusCode.InternalServerError, "فشل في حذف الحساب المرتبط بالشخص.");
                }
            }



            _context.Remove(entity);
            if (!string.IsNullOrEmpty(entity.Image))
            {
                await _imageService.DeleteImageAsync(entity.Image, _personImagePath);
            }

            if (await _context.SaveChangesAsync() > 0)
                return ApiResponse<string>.Success("تم حذف الشخص بنجاح.");

            return ApiResponse<string>.Error(HttpStatusCode.NotModified, "فشل في حذف الشخص.");
        }
        public async Task<ApiResponse<PaginatedResult<PersonDto>>> GetAllAsync(PersonFilterParams filter)
        {
            IQueryable<Person> query = _context.People.AsNoTracking();

            // ── Optional filters (all translated to SQL) ────────────────────────────
            if (!string.IsNullOrWhiteSpace(filter.Search))
                query = query.Where(p =>
                    p.FirstName.Contains(filter.Search) ||
                    p.SecondName.Contains(filter.Search) ||
                    p.ThirdName.Contains(filter.Search) ||
                    p.LastName.Contains(filter.Search));

            if (filter.Gender.HasValue)
                query = query.Where(p => p.Gender == filter.Gender.Value);

            if (filter.MaritalStatus.HasValue)
                query = query.Where(p => p.MaritalStatus == filter.MaritalStatus.Value);

            if (filter.OccupationStatus.HasValue)
                query = query.Where(p => p.OccupationStatus == filter.OccupationStatus.Value);

            if (filter.ResidencyStatus.HasValue)
                query = query.Where(p => p.ResidencyStatus == filter.ResidencyStatus.Value);

            if (filter.PersonType.HasValue)
                query = query.Where(p => p.personType == filter.PersonType.Value);

            if (filter.HasChronicDiseases.HasValue)
                query = query.Where(p => p.HasChronicDiseases == filter.HasChronicDiseases.Value);

            if (filter.BloodType.HasValue)
                query = query.Where(p => p.BloodType == filter.BloodType.Value);

            // ── Optional sorting ───────────────────────────────────────────────
            bool descending = string.Equals(filter.SortOrder, "desc", StringComparison.OrdinalIgnoreCase);
            query = filter.SortBy?.ToLowerInvariant() switch
            {
                "lastname" => descending ? query.OrderByDescending(p => p.LastName) : query.OrderBy(p => p.LastName),
                "dateofbirth" => descending ? query.OrderByDescending(p => p.DateOfBirth) : query.OrderBy(p => p.DateOfBirth),
                _ => descending ? query.OrderByDescending(p => p.FirstName) : query.OrderBy(p => p.FirstName)
            };

            // ── Paginate on the entity query (filtering/sorting fully in SQL) ────────
            var paginated = await query.ToPaginatedListAsync(filter.PageNumber, filter.PageSize);

            // ── Project entities → DTOs client-side (GetDisplayName uses reflection) ─
            var dtos = paginated.items.Select(p => new PersonDto
            {
                Id = p.Id,
                FullName = p.FirstName,
                FirstName = p.FirstName,
                SecondName = p.SecondName,
                ThirdName = p.ThirdName,
                LastName = p.LastName,
                PhoneNumber = p.PhoneNumber,
                DateOfBirth = p.DateOfBirth,
                Image = string.IsNullOrEmpty(p.Image) ? null : p.Image,
                Gender = GetDisplayName(p.Gender),
                BloodType = GetDisplayName(p.BloodType),
                OccupationStatus = GetDisplayName(p.OccupationStatus),
                MaritalStatus = GetDisplayName(p.MaritalStatus),
                PersonType = GetDisplayName(p.personType),
                Job = p.Job,
                NationalId = p.NationalId,
                VehicleType = GetDisplayName(p.VehicleType),
                VehicleRegistrationNumber = p.VehicleRegistrationNumber,
                ResidencyStatus = GetDisplayName(p.ResidencyStatus),
                HasChronicDiseases = p.HasChronicDiseases,
                ChronicDiseasesNotes = p.ChronicDiseasesNotes
            }).ToList();

            var result = PaginatedResult<PersonDto>.Success(
                dtos, paginated.TotalCount, paginated.CurrentPage, paginated.PageSize);

            return ApiResponse<PaginatedResult<PersonDto>>.Success(result, "تم جلب الأشخاص بنجاح.");
        }



        public async Task<ApiResponse<PersonDto>> GetByIdAsync(int id)
        {
            var person = await _context.People.Include(p => p.FamilyMembers)
                .ThenInclude(fm => fm.MemberFamilyRole)
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == id);

            if (person == null)
                return ApiResponse<PersonDto>.Error(HttpStatusCode.NotFound, "الشخص غير موجود.");

            var dto = new PersonDto
            {
                FullName = person.FullName,
                FirstName = person.FirstName,
                SecondName = person.SecondName,
                ThirdName = person.ThirdName,
                LastName = person.LastName,
                PhoneNumber = person.PhoneNumber,
                DateOfBirth = person.DateOfBirth,
                Image = string.IsNullOrEmpty(person.Image) ? null : person.Image,
                Gender = GetDisplayName(person.Gender),
                BloodType = GetDisplayName(person.BloodType),
                OccupationStatus = GetDisplayName(person.OccupationStatus),
                MaritalStatus = GetDisplayName(person.MaritalStatus),
                Job = person.Job,
                NationalId = person.NationalId,
                VehicleType = GetDisplayName(person.VehicleType),
                VehicleRegistrationNumber = person.VehicleRegistrationNumber,
                ResidencyStatus = GetDisplayName(person.ResidencyStatus),
                HasChronicDiseases = person.HasChronicDiseases,
                ChronicDiseasesNotes = person.ChronicDiseasesNotes
            };

            return ApiResponse<PersonDto>.Success(dto, "تم جلب بيانات الشخص بنجاح.");
        }
        public async Task<ApiResponse<string>> UpdateAsync(int id, CreatePersonDto dto)
        {
            var existingPerson = await _context.People.Include(p => p.FamilyMembers).FirstOrDefaultAsync(x => x.Id == id);

            if (existingPerson is null)
                return ApiResponse<string>.Error(HttpStatusCode.NotFound, "الشخص غير موجود.");

            existingPerson.FirstName = dto.FirstName;
            existingPerson.SecondName = dto.SecondName;
            existingPerson.ThirdName = dto.ThirdName;
            existingPerson.LastName = dto.LastName;
            existingPerson.PhoneNumber = dto.PhoneNumber;
            existingPerson.Job = dto.Job;
            existingPerson.Gender = dto.Gender;
            existingPerson.BloodType = dto.BloodType;
            existingPerson.MaritalStatus = dto.MaritalStatus;
            existingPerson.OccupationStatus = dto.OccupationStatus;
            existingPerson.NationalId = dto.NationalId;

            if (dto.Image != null)
            {
                if (existingPerson.Image != null)
                {
                    await _imageService.DeleteImageAsync(existingPerson.Image, _personImagePath);
                }
                existingPerson.Image = await _imageService.SaveImageAsync(dto.Image, _personImagePath);
            }

            _context.People.Update(existingPerson);
            if (await _context.SaveChangesAsync() > 0)
                return ApiResponse<string>.Success("تم تحديث بيانات الشخص بنجاح.");

            return ApiResponse<string>.Error(HttpStatusCode.NotModified, "فشل في تحديث بيانات الشخص.");
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