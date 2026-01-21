using SmartNeighborhoodAPI.Entites;
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
        public async Task<ApiResponse<PaginatedResult<PersonDto>>> GetAllAsync(
              int pageNumber = 1,
              int pageSize = 10,
              string? search = null)
        {
            var query = _context.People.AsNoTracking();

            if (!string.IsNullOrEmpty(search))
            {
                var result = await query.Where(x => x.FirstName.Contains(search) ||
                                                    x.SecondName.Contains(search) ||
                                                    x.ThirdName.Contains(search) ||
                                                    x.LastName.Contains(search))
                                        .Select(p => new PersonDto
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
                                            Job = p.Job
                                        })
                                        .ToPaginatedListAsync(pageNumber, pageSize);

                if (result == null)
                    return ApiResponse<PaginatedResult<PersonDto>>.Error(HttpStatusCode.NotFound, "لا يوجد أشخاص مطابقين للبحث.");

                return ApiResponse<PaginatedResult<PersonDto>>.Success(result, "تم جلب الأشخاص بنجاح.");
            }

            var people = await query.Select(p => new PersonDto
            {
                Id = p.Id,
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
                Job = p.Job
            }).ToPaginatedListAsync(pageNumber, pageSize);

            if (people == null)
                return ApiResponse<PaginatedResult<PersonDto>>.Error(HttpStatusCode.NotFound, "لا يوجد أشخاص.");

            return ApiResponse<PaginatedResult<PersonDto>>.Success(people, "تم جلب الأشخاص بنجاح.");
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
                Job = person.Job
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