using System.Net;

namespace OurProjectSmartNeiborhood.Services
{
 

    public class PersonService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public PersonService(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ApiResponse<PersonDto>> AddAsync(CreatePersonDto createPersonDto)
        {
            var person = _mapper.Map<Person>(createPersonDto);


            await _context.People.AddAsync(person);
            if (await _context.SaveChangesAsync() > 0)
            {
                var personDto = _mapper.Map<PersonDto>(person);

                return ApiResponse<PersonDto>.Success(personDto);
            }

            return ApiResponse<PersonDto>.Error(HttpStatusCode.BadRequest, "Failed to add person");
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

        public async Task<ApiResponse<IEnumerable<Person>>> GetAll()
        {
            var persons = await _context.People.AsNoTracking().ToListAsync();

            if (persons.Any())
            {
                return ApiResponse<IEnumerable<Person>>.Success(persons);
            }

            return ApiResponse<IEnumerable<Person>>.Error(HttpStatusCode.BadRequest, "No Person Found");
        }

        public async Task<ApiResponse<PersonDto>> GetByIdAsync(int id)
        {
            var person = await _context.People.Include(p => p.FamilyMembers).ThenInclude(fm => fm.MemberFamilyRole).AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
            if (person == null)
                return ApiResponse<PersonDto>.Error(HttpStatusCode.NotFound, "Person Not Found");

            var personDto = new PersonDto
            {
                Id = person.Id,
                FirstName = person.FirstName,
                PhoneNumber = person.PhoneNumber,
                Job = person.Job,
                Email = person.Email,
                DateOfBirth = person.DateOfBirth,
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
            existingPerson.DateOfBirth = personDto.DateOfBirth;
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
    }

}