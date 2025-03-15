using System.Net;

namespace SmartNeighborhoodAPI.Services
{
    public class FamilyTypeService
    {
        private readonly ApplicationDbContext _context;
        readonly IMapper _mapper;

        public FamilyTypeService(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;

        }



        public async Task<ApiResponse<GetFamilyTypeDto>> GetByIdAsync(int id)
        {
            var FamilyType = await _context.FamilyTypes.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
            if (FamilyType == null)
                return ApiResponse<GetFamilyTypeDto>.Error(HttpStatusCode.NotFound, "FamilyType Not Found");


            var familyTypeDto = new GetFamilyTypeDto
            {
                Id = id,
                Name = FamilyType.Name
            };

            return ApiResponse<GetFamilyTypeDto>.Success(familyTypeDto);
        }
        public async Task<ApiResponse<IEnumerable<GetFamilyTypeDto>>> GetAll()
        {
            var FamiliesTypes = _context.FamilyTypes.AsNoTracking().ToList();
            if (FamiliesTypes.Count > 0)
            {
                var failiesTypeDtos = FamiliesTypes.Select(x => new GetFamilyTypeDto
                {
                    Id = x.Id,
                    Name = x.Name,
                });


                return ApiResponse<IEnumerable<GetFamilyTypeDto>>.Success(failiesTypeDtos);
            }

            return ApiResponse<IEnumerable<GetFamilyTypeDto>>.Error(HttpStatusCode.NotFound, "No FamilyType Found");



        }
 
        public async Task<ApiResponse<string>> AddAsync(string nameFamilyType)
        {
    

            var existFamilyType = await _context.FamilyTypes.FirstOrDefaultAsync(x => x.Name == nameFamilyType);
            if (existFamilyType != null)
                return ApiResponse<string>.Error(HttpStatusCode.Conflict, "FamilyType Is Already Exist");

            var familyType = new FamilyType
            {
                Name = nameFamilyType
            };  

            await _context.FamilyTypes.AddAsync(familyType);
            if (await _context.SaveChangesAsync() > 0)
                return ApiResponse<string>.Success(nameFamilyType, "Added Successed");

            return ApiResponse<string>.Error(HttpStatusCode.BadRequest, "FamilyType not add");


        }
        public async Task<ApiResponse<string>> DeleteAsync(int id)
        {
            var entity = await _context.FamilyTypes.FindAsync(id);
            if (entity == null)
                return ApiResponse<string>.Error(HttpStatusCode.NotFound, "FamilyType Not Found");

            _context.FamilyTypes.Remove(entity);
            if (await _context.SaveChangesAsync() > 0)
                return ApiResponse<string>.Success("FamilyType Deleted Successfully");

            return ApiResponse<string>.Error(HttpStatusCode.NotModified, "Faild To Delete the FamilyType");
        }
      
        public async Task<ApiResponse<string>> UpdateAsync(int id, string nameFamilyTypeDto)
        {
            var ExsitFamilyType = await _context.FamilyTypes.FirstOrDefaultAsync(x => x.Id == id);

            if (ExsitFamilyType is null)
                return ApiResponse<string>.Error(HttpStatusCode.NotFound, "FamilyType Not Found");


            if (ExsitFamilyType.Name == nameFamilyTypeDto)
                return ApiResponse<string>.Error(HttpStatusCode.Conflict, "FamilyCatgory Is Already Exist");

            ExsitFamilyType.Name = nameFamilyTypeDto;

            _context.FamilyTypes.Update(ExsitFamilyType);
            if (await _context.SaveChangesAsync() > 0)
                return ApiResponse<string>.Success("FamilyType Updated Successfully");

            return ApiResponse<string>.Error(HttpStatusCode.NotModified, "Faild To Update FamilyType");


        }
    }
}
