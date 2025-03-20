using System.Net;

namespace OurProjectSmartNeiborhood.Services
{
    public class PersonDto
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string PhoneNumber { get; set; }
        public string Job { get; set; }
        public string Email { get; set; }
        public DateTime DateOfBirth { get; set; }
        public byte Gender { get; set; }
        public string BloodType { get; set; }
        public string IdentityNumber { get; set; }
        public string TypeOfIdentity { get; set; }
        public string Status { get; set; }
        public List<FamilyMemberDto> FamilyMembers { get; set; }
    }

    public class FamilyMemberDto
    {
        public int Id { get; set; }
        public int? FamilyId { get; set; }
        public int MemberTypeId { get; set; }
    }

    public class CreatePersonDto
    {
        public string FullName { get; set; }
        public string PhoneNumber { get; set; }
        public string Job { get; set; }
        public string Email { get; set; }
        public DateTime DateOfBirth { get; set; }
        public byte Gender { get; set; }
        public string BloodType { get; set; }
        public string IdentityNumber { get; set; }
        public string TypeOfIdentity { get; set; }
        public string Status { get; set; }
        public List<CreateFamilyMemberDto> FamilyMembers { get; set; }
    }

    public class CreateFamilyMemberDto
    {
        public int FamilyId { get; set; }
        public int MemberTypeId { get; set; }
    }

    public class PersonService
    {
        private readonly ApplicationDbContext _context;

        public PersonService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<ApiResponse<PersonDto>> AddAsync(CreatePersonDto createPersonDto)
        {
            var person = new Person
            {
                FullName = createPersonDto.FullName,
                PhoneNumber = createPersonDto.PhoneNumber,
                Job = createPersonDto.Job,
                Email = createPersonDto.Email,
                DateOfBirth = createPersonDto.DateOfBirth,
                Gender = createPersonDto.Gender,
                BloodType = createPersonDto.BloodType,
                IdentityNumber = createPersonDto.IdentityNumber,
                TypeOfIdentity = createPersonDto.TypeOfIdentity,
                Status = createPersonDto.Status,
                FamilyMembers = new List<FamilyMember>()
            };

            if (createPersonDto.FamilyMembers != null && createPersonDto.FamilyMembers.Any())
            {
                foreach (var fmDto in createPersonDto.FamilyMembers)
                {
                    var family = await _context.Families.FindAsync(fmDto.FamilyId);
                    if (family == null)
                        return ApiResponse<PersonDto>.Error(HttpStatusCode.NotFound, "No Family Found");

                    var memberType = await _context.MemberTypes.FindAsync(fmDto.MemberTypeId);
                    if (memberType == null)
                        return ApiResponse<PersonDto>.Error(HttpStatusCode.NotFound, "No FamilyMembers Found");

                    var familyMember = new FamilyMember
                    {
                        Person = person,
                        Family = family,
                        MemberType = memberType
                    };

                    person.FamilyMembers.Add(familyMember);
                }
            }

            await _context.People.AddAsync(person);
            if (await _context.SaveChangesAsync() > 0)
            {
                var personDto = new PersonDto
                {
                    Id = person.Id,
                    FullName = person.FullName,
                    PhoneNumber = person.PhoneNumber,
                    Job = person.Job,
                    Email = person.Email,
                    DateOfBirth = person.DateOfBirth,
                    Gender = person.Gender,
                    BloodType = person.BloodType,
                    IdentityNumber = person.IdentityNumber,
                    TypeOfIdentity = person.TypeOfIdentity,
                    Status = person.Status,
                    FamilyMembers = person.FamilyMembers.Select(fm => new FamilyMemberDto
                    {
                        Id = fm.Id,
                        FamilyId = fm.FamilyId,
                        MemberTypeId = fm.MemberTypeId
                    }).ToList()
                };

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

        public async Task<ApiResponse<IEnumerable<PersonDto>>> GetAll()
        {
            var persons = await _context.People.Include(p => p.FamilyMembers).ThenInclude(fm => fm.MemberType).AsNoTracking().ToListAsync();
            if (persons.Any())
            {
                var personDtos = persons.Select(p => new PersonDto
                {
                    Id = p.Id,
                    FullName = p.FullName,
                    PhoneNumber = p.PhoneNumber,
                    Job = p.Job,
                    Email = p.Email,
                    DateOfBirth = p.DateOfBirth,
                    Gender = p.Gender,
                    BloodType = p.BloodType,
                    IdentityNumber = p.IdentityNumber,
                    TypeOfIdentity = p.TypeOfIdentity,
                    Status = p.Status,
                    FamilyMembers = p.FamilyMembers?.Select(fm => new FamilyMemberDto
                    {
                        Id = fm.Id,
                        FamilyId = fm.FamilyId,
                        MemberTypeId = fm.MemberTypeId
                    }).ToList()
                }).ToList();

                return ApiResponse<IEnumerable<PersonDto>>.Success(personDtos);
            }

            return ApiResponse<IEnumerable<PersonDto>>.Error(HttpStatusCode.BadRequest, "No Person Found");
        }

        public async Task<ApiResponse<PersonDto>> GetByIdAsync(int id)
        {
            var person = await _context.People.Include(p => p.FamilyMembers).ThenInclude(fm => fm.MemberType).AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
            if (person == null)
                return ApiResponse<PersonDto>.Error(HttpStatusCode.NotFound, "Person Not Found");

            var personDto = new PersonDto
            {
                Id = person.Id,
                FullName = person.FullName,
                PhoneNumber = person.PhoneNumber,
                Job = person.Job,
                Email = person.Email,
                DateOfBirth = person.DateOfBirth,
                Gender = person.Gender,
                BloodType = person.BloodType,
                IdentityNumber = person.IdentityNumber,
                TypeOfIdentity = person.TypeOfIdentity,
                Status = person.Status,
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

            existingPerson.FullName = personDto.FullName;
            existingPerson.PhoneNumber = personDto.PhoneNumber;
            existingPerson.Job = personDto.Job;
            existingPerson.Email = personDto.Email;
            existingPerson.DateOfBirth = personDto.DateOfBirth;
            existingPerson.Gender = personDto.Gender;
            existingPerson.BloodType = personDto.BloodType;
            existingPerson.IdentityNumber = personDto.IdentityNumber;
            existingPerson.TypeOfIdentity = personDto.TypeOfIdentity;
            existingPerson.Status = personDto.Status;

            existingPerson.FamilyMembers = personDto.FamilyMembers?.Select(fm => new FamilyMember
            {
                FamilyId = fm.FamilyId,
                MemberTypeId = fm.MemberTypeId
            }).ToList();

            _context.People.Update(existingPerson);
            if (await _context.SaveChangesAsync() > 0)
                return ApiResponse<string>.Success("Person Updated Successfully");

            return ApiResponse<string>.Error(HttpStatusCode.NotModified, "Failed To Update Person");
        }
    }

}







