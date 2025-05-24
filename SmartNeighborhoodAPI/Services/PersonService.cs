using System.Net;
using Microsoft.IdentityModel.Tokens;
using OurProjectSmartNeiborhood.Entites;
using SmartNeighborhoodAPI.Helpers;
using SmartNeighborhoodAPI.Helpers.DTOs.Person;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace OurProjectSmartNeiborhood.Services
{

    public class PersonService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly ImageService _imageService;
        private string _personImagePath;


        public PersonService(ApplicationDbContext context, IMapper mapper, IWebHostEnvironment webHostEnvironment, ImageService imageService)
        {
            _context = context;
            _mapper = mapper;
            _webHostEnvironment = webHostEnvironment;
            _personImagePath = $"{_webHostEnvironment.WebRootPath}{FileHelper.PersonImagesPath}";
            _imageService = imageService;
        }

        public async Task<ApiResponse<Person>> AddAsync(CreatePersonDto dto)
        {
            string personImage = string.Empty;
            if (dto.Image != null)
            {
                personImage = await _imageService.SaveImageAsync(dto.Image, _personImagePath);
            }

            var person= new Person
            {
                FirstName = dto.FirstName,
                SecondName = dto.SecondName,
                ThirdName = dto.ThirdName,
                LastName = dto.LastName,
                PhoneNumber = dto.PhoneNumber,
                IsWhatsapp = dto.IsWhatsapp,
                IsContactNumber = dto.IsContactNumber,
                Email = dto.Email,
                DateOfBirth = dto.DateOfBirth,
                Gender = dto.Gender,
                Image = string.IsNullOrEmpty(personImage)? null : personImage,
                BloodType = dto.BloodType,
                IdentityNumber = dto.IdentityNumber,
                IdentityType = dto.IdentityType,
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
                return ApiResponse<string>.Error(HttpStatusCode.NotFound, "Person Not Found");

            _context.Remove(entity);
            if(!string.IsNullOrEmpty(entity.Image))
            {
               await _imageService.DeleteImageAsync(entity.Image, _personImagePath);
            }

            if (await _context.SaveChangesAsync() > 0)
                return ApiResponse<string>.Success("Person Deleted Successfully");

            return ApiResponse<string>.Error(HttpStatusCode.NotModified, "Failed To Delete the Person");
        }
        public async Task<ApiResponse<PaginatedResult<PersonDto>>> GetAllAsync(
            int pageNumber = 1,
            int pageSize = 10,
            string? search = null)
        {
            var query = _context.People.AsNoTracking();

            if (!string.IsNullOrEmpty(search))
            {
                var resullt = await query.Where(x => x.FirstName.Contains(search) ||
                x.SecondName.Contains(search) ||
                x.ThirdName.Contains(search) ||
                x.LastName.Contains(search)
                ).Select(p => new PersonDto
                {
                    Id = p.Id,
                    FirstName = p.FirstName,
                    SecondName = p.SecondName,
                    ThirdName = p.ThirdName,
                    LastName = p.LastName,
                    PhoneNumber = p.PhoneNumber,
                    DateOfBirth = p.DateOfBirth,
                    Email = p.Email,
                    Image = string.IsNullOrEmpty(p.Image) ? null : p.Image,
                    Gender = GetDisplayName(p.Gender),
                    BloodType = GetDisplayName(p.BloodType),
                    IdentityNumber = p.IdentityNumber,
                    IdentityType = GetDisplayName(p.IdentityType),
                    OccupationStatus = GetDisplayName(p.OccupationStatus),
                    MaritalStatus = GetDisplayName(p.MaritalStatus),
                    Job = p.Job
                })
                .ToPaginatedListAsync(pageNumber, pageSize);

                if (resullt == null)
                    return ApiResponse<PaginatedResult<PersonDto>>.Error(HttpStatusCode.NotFound, "No Person Found");

                return ApiResponse<PaginatedResult<PersonDto>>.Success(resullt);
            }
            var people = await query
                 .Select(p => new PersonDto
                 {
                     Id = p.Id,
                     FirstName = p.FirstName,
                     SecondName = p.SecondName,
                     ThirdName = p.ThirdName,
                     LastName = p.LastName,
                     PhoneNumber = p.PhoneNumber,
                     DateOfBirth = p.DateOfBirth,
                     Email = p.Email,
                     Image = string.IsNullOrEmpty(p.Image) ? null : p.Image,
                     Gender = GetDisplayName(p.Gender),
                     BloodType = GetDisplayName(p.BloodType),
                     IdentityNumber = p.IdentityNumber,
                     IdentityType = GetDisplayName(p.IdentityType),
                     OccupationStatus = GetDisplayName(p.OccupationStatus),
                     MaritalStatus = GetDisplayName(p.MaritalStatus),
                     Job = p.Job
                 })
                 .ToPaginatedListAsync(pageNumber, pageSize);

            if(people == null )
                return ApiResponse<PaginatedResult<PersonDto>>.Error(HttpStatusCode.NotFound, "No Person Found");


            return ApiResponse<PaginatedResult<PersonDto>>.Success(people);
        }

        public async Task<ApiResponse<PersonDto>> GetByIdAsync(int id)
        {
            var person = await _context.People.Include(p => p.FamilyMembers)
                .ThenInclude(fm => fm.MemberFamilyRole)
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == id);

            if (person == null)
                return ApiResponse<PersonDto>.Error(HttpStatusCode.NotFound, "Person Not Found");

            var dto = new PersonDto
            {
                FirstName = person.FirstName,
                SecondName = person.SecondName,
                ThirdName = person.ThirdName,
                LastName = person.LastName,
                PhoneNumber = person.PhoneNumber,
                DateOfBirth = person.DateOfBirth,
                Email = person.Email,
                Image = string.IsNullOrEmpty(person.Image) ? null : person.Image,
                Gender = GetDisplayName(person.Gender),
                BloodType = GetDisplayName(person.BloodType),
                IdentityNumber = person.IdentityNumber,
                IdentityType = GetDisplayName(person.IdentityType),
                OccupationStatus = GetDisplayName(person.OccupationStatus),
                MaritalStatus = GetDisplayName(person.MaritalStatus),
                Job = person.Job

            };

            return ApiResponse<PersonDto>.Success(dto);
        }
        public async Task<ApiResponse<string>> UpdateAsync(int id, CreatePersonDto dto)
        {
            var existingPerson = await _context.People.Include(p => p.FamilyMembers).FirstOrDefaultAsync(x => x.Id == id);

            if (existingPerson is null)
                return ApiResponse<string>.Error(HttpStatusCode.NotFound, "Person Not Found");

            existingPerson.FirstName = dto.FirstName;
            existingPerson.SecondName = dto.SecondName;
            existingPerson.ThirdName = dto.ThirdName;
            existingPerson.LastName = dto.LastName;
            existingPerson.PhoneNumber = dto.PhoneNumber;
            existingPerson.Job = dto.Job;
            existingPerson.Email = dto.Email;
            existingPerson.Gender = dto.Gender;
            existingPerson.BloodType = dto.BloodType;
            existingPerson.IdentityNumber = dto.IdentityNumber;
            existingPerson.PhoneNumber = dto.PhoneNumber;
            existingPerson.Job = dto.Job;
            existingPerson.MaritalStatus = dto.MaritalStatus;
            existingPerson.OccupationStatus = dto.OccupationStatus;
            existingPerson.IsContactNumber = dto.IsContactNumber;
            existingPerson.IsWhatsapp = dto.IsWhatsapp;

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
                return ApiResponse<string>.Success("Person Updated Successfully");

            return ApiResponse<string>.Error(HttpStatusCode.NotModified, "Failed To Update Person");
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