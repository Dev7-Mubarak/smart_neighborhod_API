using System.Net;
using Microsoft.IdentityModel.Tokens;
using SmartNeighborhoodAPI.Helpers;
using SmartNeighborhoodAPI.Helpers.DTOs.Person;

namespace OurProjectSmartNeiborhood.Services
{
 

    public class PersonService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private string _personImagePath;


        public PersonService(ApplicationDbContext context, IMapper mapper, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _mapper = mapper;
            _webHostEnvironment = webHostEnvironment;
            _personImagePath = $"{_webHostEnvironment.WebRootPath}{FileHelper.PersonImagesPath}";
        }

        public async Task<ApiResponse<Person>> AddAsync(CreatePersonDto dto)
        {
            string personImage = string.Empty;
            if (dto.Image != null)
            {
                personImage = await FileHelper.SaveFileAsync(dto.Image, _personImagePath);
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
            var entity = await _context.People.Include(p => p.FamilyMembers).FirstOrDefaultAsync(p => p.Id == id);
            if (entity == null)
                return ApiResponse<string>.Error(HttpStatusCode.NotFound, "Person Not Found");

            _context.Remove(entity);
            if (await _context.SaveChangesAsync() > 0)
                return ApiResponse<string>.Success("Person Deleted Successfully");

            return ApiResponse<string>.Error(HttpStatusCode.NotModified, "Failed To Delete the Person");
        }
        public async Task<ApiResponse<IEnumerable<PersonDto>>> GetAll()
        {
            var persons = await _context.People.AsNoTracking().ToListAsync();

            if (persons.Any())
            {
                var personDtos = persons.Select(p => new PersonDto
                {
                    Id = p.Id,
                    FirstName = p.FirstName,
                    SecondName = p.SecondName,
                    ThirdName = p.ThirdName,
                    LastName = p.LastName,
                    PhoneNumber = p.PhoneNumber,
                    DateOfBirth = p.DateOfBirth,
                    Email = p.Email,
                    ImageBase64 = string.IsNullOrEmpty(p.Image)? null : GetImageBase64(p.Image),
                    Gender = p.Gender,
                    BloodType = GetDisplayName(p.BloodType),
                    IdentityNumber = p.IdentityNumber,
                    IdentityType = GetDisplayName(p.IdentityType),
                    OccupationStatus = GetDisplayName(p.OccupationStatus),
                    MaritalStatus = GetDisplayName(p.MaritalStatus),
                    Job = p.Job
                }).ToList();

                return ApiResponse<IEnumerable<PersonDto>>.Success(personDtos);
            }

            return ApiResponse<IEnumerable<PersonDto>>.Error(HttpStatusCode.BadRequest, "No Person Found");
        }
        public async Task<ApiResponse<PersonDto>> GetByIdAsync(int id)
        {
            var person = await _context.People.Include(p => p.FamilyMembers).ThenInclude(fm => fm.MemberFamilyRole).AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
            if (person == null)
                return ApiResponse<PersonDto>.Error(HttpStatusCode.NotFound, "Person Not Found");

            var personDto = new PersonDto
            {
                FirstName = person.FirstName,
                PhoneNumber = person.PhoneNumber,
                Job = person.Job,
                Email = person.Email,
                //Gender = person.Gender,
                //BloodType = person.BloodType,
                //IdentityNumber = person.IdentityNumber,
                //TypeOfIdentity = person.TypeOfIdentity,
                //Status = person.Status,
                //FamilyMembers = person.FamilyMembers?.Select(fm => new FamilyMemberDto
                //{
                //    Id = fm.Id,
                //    FamilyId = fm.FamilyId,
                //    MemberTypeId = fm.MemberTypeId
                //}).ToList()
            };

            return ApiResponse<PersonDto>.Success(personDto);
        }

        public async Task<ApiResponse<string>> UpdateAsync(int id, PersonDto personDto)
        {
            var existingPerson = await _context.People.Include(p => p.FamilyMembers).FirstOrDefaultAsync(x => x.Id == id);

            if (existingPerson is null)
                return ApiResponse<string>.Error(HttpStatusCode.NotFound, "Person Not Found");

            existingPerson.FirstName = personDto.FirstName;
            existingPerson.PhoneNumber = personDto.PhoneNumber;
            existingPerson.Job = personDto.Job;
            existingPerson.Email = personDto.Email;
            //existingPerson.Gender = personDto.Gender;
            //existingPerson.BloodType = personDto.BloodType;
            //existingPerson.IdentityNumber = personDto.IdentityNumber;
            //existingPerson.TypeOfIdentity = personDto.TypeOfIdentity;
            //existingPerson.Status = personDto.Status;

            _context.People.Update(existingPerson);
            if (await _context.SaveChangesAsync() > 0)
                return ApiResponse<string>.Success("Person Updated Successfully");

            return ApiResponse<string>.Error(HttpStatusCode.NotModified, "Failed To Update Person");
        }


        private  string GetDisplayName<T>(T enumValue)
        {
            var memberInfo = typeof(T).GetMember(enumValue.ToString()).FirstOrDefault();
            var displayAttr = memberInfo?.GetCustomAttributes(typeof(DisplayAttribute), false)
                                        .FirstOrDefault() as DisplayAttribute;

            return displayAttr?.Name ?? enumValue.ToString();
        }

        private string GetImageBase64(string imagePath)
        {
            if (string.IsNullOrEmpty(imagePath) || !File.Exists(imagePath))
                return null;

            byte[] imageBytes = File.ReadAllBytes(imagePath);
            return Convert.ToBase64String(imageBytes);
        }
    }

}