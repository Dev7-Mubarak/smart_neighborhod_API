using SmartNeighborhoodAPI.Entites;
using System.Net;

namespace SmartNeighborhoodAPI.Services
{
    public class ConfilctPartyService
    {
        private readonly ApplicationDbContext _context;
        readonly IMapper _mapper;

        public ConfilctPartyService(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;

        }
        //public async Task<ApiResponse<AddConfilctPartyDto>> AddFirstPartyAsync(AddConfilctPartyDto dto)
        //{
        //    // Check if complain exists
        //    var complain = await _context.Complains.FindAsync(dto.ComplainId);
        //    if (complain == null)
        //    {
        //        return ApiResponse<AddConfilctPartyDto>.Error(HttpStatusCode.NotFound, "Complain not found");
        //    }

        //    int? personIdToUse = null;

        //    if (dto.PersonId.HasValue)
        //    {
        //        // Check if internal person exists
        //        var person = await _context.Persons.FindAsync(dto.PersonId.Value);
        //        if (person == null)
        //        {
        //            return ApiResponse<AddConfilctPartyDto>.Error(HttpStatusCode.NotFound, "Person ID not found");
        //        }
        //        personIdToUse = person.Id;
        //    }
        //    else
        //    {
        //        // Handle external person
        //        if (string.IsNullOrWhiteSpace(dto.FullName) || string.IsNullOrWhiteSpace(dto.IdentityNumber))
        //        {
        //            return ApiResponse<AddConfilctPartyDto>.Error(HttpStatusCode.BadRequest, "Full name and Identity number are required for external persons");
        //        }

        //        var externalPerson = new ExternalPerson
        //        {
        //            FullName = dto.FullName,
        //            IdentityNumber = dto.IdentityNumber
        //        };

        //        await _context.ExternalPersons.AddAsync(externalPerson);
        //        await _context.SaveChangesAsync();

        //        personIdToUse = externalPerson.Id;
        //    }

        //    var conflictParty = new ConfilctParty
        //    {
        //        ComplainId = dto.ComplainId,
        //        IsFirstParty = true,
        //        PersonId = personIdToUse.Value
        //    };

        //    await _context.ConfilctParties.AddAsync(conflictParty);

        //    if (await _context.SaveChangesAsync() > 0)
        //    {
        //        return ApiResponse<AddConfilctPartyDto>.Success(dto, "Added Successfully");
        //    }

        //    return ApiResponse<AddConfilctPartyDto>.Error(HttpStatusCode.BadRequest, "Conflict party not added");
        //}
        //public async Task<ApiResponse<AddConfilctPartyDto>> AddSecondPartyAsync(AddConfilctPartyDto dto)
        //{
        //    // Check if complain exists
        //    var complain = await _context.Complains.FindAsync(dto.ComplainId);
        //    if (complain == null)
        //    {
        //        return ApiResponse<AddConfilctPartyDto>.Error(HttpStatusCode.NotFound, "Complain not found");
        //    }

        //    int? personIdToUse = null;

        //    if (dto.PersonId.HasValue)
        //    {
        //        // Internal person check
        //        var person = await _context.Persons.FindAsync(dto.PersonId.Value);
        //        if (person == null)
        //        {
        //            return ApiResponse<AddConfilctPartyDto>.Error(HttpStatusCode.NotFound, "Person ID not found");
        //        }

        //        personIdToUse = person.Id;
        //    }
        //    else
        //    {
        //        // Handle external person
        //        if (string.IsNullOrWhiteSpace(dto.FullName) || string.IsNullOrWhiteSpace(dto.IdentityNumber))
        //        {
        //            return ApiResponse<AddConfilctPartyDto>.Error(HttpStatusCode.BadRequest, "Full name and identity number are required for external persons");
        //        }

        //        var externalPerson = new ExternalPerson
        //        {
        //            FullName = dto.FullName,
        //            IdentityNumber = dto.IdentityNumber
        //        };

        //        await _context.ExternalPersons.AddAsync(externalPerson);
        //        await _context.SaveChangesAsync();

        //        personIdToUse = externalPerson.Id;
        //    }

        //    var conflictParty = new ConfilctParty
        //    {
        //        ComplainId = dto.ComplainId,
        //        IsFirstParty = false, // <- this is the only key difference
        //        PersonId = personIdToUse.Value
        //    };

        //    await _context.ConfilctParties.AddAsync(conflictParty);

        //    if (await _context.SaveChangesAsync() > 0)
        //    {
        //        return ApiResponse<AddConfilctPartyDto>.Success(dto, "Second party added successfully");
        //    }

        //    return ApiResponse<AddConfilctPartyDto>.Error(HttpStatusCode.BadRequest, "Second party not added");
        //}


        public async Task<ApiResponse<string>> DeleteAsync(int id)
        {
            var entity = await _context.ConfilctParties.FirstOrDefaultAsync(x => x.Id == id); ;
            if (entity == null)
                return ApiResponse<string>.Error(HttpStatusCode.NotFound, "ConfilctParty Not Found");

            _context.ConfilctParties.Remove(entity);
            if (await _context.SaveChangesAsync() > 0)
                return ApiResponse<string>.Success("ConfilctParty Deleted Successfully");

            return ApiResponse<string>.Error(HttpStatusCode.NotModified, "Faild To Delete the ConfilctParty");
        }
        public async Task<ApiResponse<IEnumerable<AddConfilctPartyDto>>> GetAll()
        {
            var ConfilctPartys = await _context.ConfilctParties.AsNoTracking().ToListAsync();
            if (ConfilctPartys.Count > 0)
            {
                var ConfilctPartyDtos = _mapper.Map<IEnumerable<AddConfilctPartyDto>>(ConfilctPartys);
                return ApiResponse<IEnumerable<AddConfilctPartyDto>>.Success(ConfilctPartyDtos);
            }

            return ApiResponse<IEnumerable<AddConfilctPartyDto>>.Error(HttpStatusCode.NotFound, "No ConfilctPartys Found");



        }
        public async Task<ApiResponse<AddConfilctPartyDto>> GetByIdAsync(int id)
        {
            var ConfilctParty = await _context.ConfilctParties.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
            if (ConfilctParty == null)
                return ApiResponse<AddConfilctPartyDto>.Error(HttpStatusCode.NotFound, "ConfilctParty Not Found");


            var ConfilctPartyDto = _mapper.Map<AddConfilctPartyDto>(ConfilctParty);
            return ApiResponse<AddConfilctPartyDto>.Success(ConfilctPartyDto);
        }
        public async Task<ApiResponse<string>> UpdateAsync(int id, AddConfilctPartyDto ConfilctPartyDto)
        {
            var ExsitConfilctParty = await _context.ConfilctParties.FirstOrDefaultAsync(x => x.Id == id);

            if (ExsitConfilctParty is null)
                return ApiResponse<string>.Error(HttpStatusCode.NotFound, "ConfilctParty Not Found");
            var UpdateConfilctParty = _mapper.Map(ConfilctPartyDto, ExsitConfilctParty);

            _context.ConfilctParties.Update(UpdateConfilctParty);
            if (await _context.SaveChangesAsync() > 0)
                return ApiResponse<string>.Success("ConfilctParty Updated Successfully");

            return ApiResponse<string>.Error(HttpStatusCode.NotModified, "Faild To Update ConfilctParty");


        }
    }
}
