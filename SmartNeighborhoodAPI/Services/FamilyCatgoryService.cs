using System.Net;

namespace SmartNeighborhoodAPI.Services
{
    public class FamilyCatgoryService
    {
        private readonly ApplicationDbContext _context;
        readonly IMapper _mapper;

        public FamilyCatgoryService(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ApiResponse<string>> AddAsync(string nameFamilyCatgory)
        {


            var FamilyCatgory = new FamilyCatgory
            {
                Name = nameFamilyCatgory
            };

            var existFamilyCategory = await _context.FamilyCatgories.FirstOrDefaultAsync(x => x.Name == nameFamilyCatgory);

            if (existFamilyCategory != null)
            {

                return ApiResponse<string>.Error(HttpStatusCode.Conflict, "FamilyCatgory Is Already Exist");

            }


            await _context.FamilyCatgories.AddAsync(FamilyCatgory);
            if (await _context.SaveChangesAsync() > 0)
                return ApiResponse<string>.Success(nameFamilyCatgory, "Added Successed");

            return ApiResponse<string>.Error(HttpStatusCode.BadRequest, "FamilyCatgory not add");


        }


        public async Task<ApiResponse<IEnumerable<FamilyCatgoryDto>>> GetAll()
        {
            var FamilyCatgorys = await _context.FamilyCatgories.AsNoTracking().ToListAsync();
            if (FamilyCatgorys.Count > 0)
            {

                var FamilyCatgoryDtos = FamilyCatgorys.Select(x => new FamilyCatgoryDto
                {
                    Id = x.Id,
                    Name = x.Name,

                });


                return ApiResponse<IEnumerable<FamilyCatgoryDto>>.Success(FamilyCatgoryDtos);


            }

            return ApiResponse<IEnumerable<FamilyCatgoryDto>>.Error(HttpStatusCode.NotFound, "No FamilyCatgory Found");



        }
        public async Task<ApiResponse<FamilyCatgoryDto>> GetByIdAsync(int id)
        {
            var FamilyCatgory = await _context.FamilyCatgories.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
            if (FamilyCatgory == null)
                return ApiResponse<FamilyCatgoryDto>.Error(HttpStatusCode.NotFound, "FamilyCatgory Not Found");


            var FamilyCatgoryDto = new FamilyCatgoryDto
            {
                Id = FamilyCatgory.Id,
                Name = FamilyCatgory.Name,
            };
            return ApiResponse<FamilyCatgoryDto>.Success(FamilyCatgoryDto);
        }
    
        public async Task<ApiResponse<string>> DeleteAsync(int id)
        {
            var FamilyCatgory = await _context.FamilyCatgories.FirstOrDefaultAsync(x => x.Id == id);

            if (FamilyCatgory == null)
                return ApiResponse<string>.Error(HttpStatusCode.NotFound, "FamilyCatgory Not Found");

            _context.FamilyCatgories.Remove(FamilyCatgory);
            if (await _context.SaveChangesAsync() > 0)
                return ApiResponse<string>.Success("FamilyCatgory Deleted Successfully");

            return ApiResponse<string>.Error(HttpStatusCode.NotModified, "Faild To Delete the FamilyCatgory");
        }
        public async Task<ApiResponse<string>> UpdateAsync(int id, string nameFamilyCatgory)
        {
            var ExsitFamilyCatgory = await _context.FamilyCatgories.FirstOrDefaultAsync(x => x.Id == id);
            if(ExsitFamilyCatgory.Name == nameFamilyCatgory)
            {
                return ApiResponse<string>.Error(HttpStatusCode.Conflict, "FamilyCatgory Is Already Exist");

            }



            if (ExsitFamilyCatgory is null)
                return ApiResponse<string>.Error(HttpStatusCode.NotFound, "FamilyCatgory Not Found");

            if (ExsitFamilyCatgory.Name == nameFamilyCatgory)
                return ApiResponse<string>.Error(HttpStatusCode.Conflict, "FamilyCatgory Is Already Exist");



            ExsitFamilyCatgory.Name = nameFamilyCatgory;  

            _context.FamilyCatgories.Update(ExsitFamilyCatgory);
            if (await _context.SaveChangesAsync() > 0)
                return ApiResponse<string>.Success("FamilyCatgory Updated Successfully");


            return ApiResponse<string>.Error(HttpStatusCode.NotModified, "Faild To Update FamilyCatgory");


        }
    }
}
