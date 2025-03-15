using System.Net;

namespace SmartNeighborhoodAPI.Services
{
    public class ComplainTypeService
    {
        private readonly ApplicationDbContext _context;
        readonly IMapper _mapper;

        public ComplainTypeService(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;

        }
        public async Task<ApiResponse<ComplainTypeDto>> AddAsync(ComplainTypeDto ComplainTypeDto)
        {

            var ComplainType = _mapper.Map<ComplainType>(ComplainTypeDto);

            var existComplainType = await _context.ComplainTypes.FirstOrDefaultAsync(x => x.Name == ComplainTypeDto.Name);
            if (existComplainType != null)
                return ApiResponse<ComplainTypeDto>.Error(HttpStatusCode.Conflict, "ComplainType Is Already Exist");




            await _context.AddAsync(ComplainType);
            if (await _context.SaveChangesAsync() > 0)
                return ApiResponse<ComplainTypeDto>.Success(ComplainTypeDto, "Added Successed");

            return ApiResponse<ComplainTypeDto>.Error(HttpStatusCode.BadRequest, "ComplainType not add");


        }
        public async Task<ApiResponse<string>> DeleteAsync(int id)
        {
            var entity = await _context.ComplainTypes.FirstOrDefaultAsync(x => x.Id == id); ;
            if (entity == null)
                return ApiResponse<string>.Error(HttpStatusCode.NotFound, "ComplainType Not Found");

            _context.ComplainTypes.Remove(entity);
            if (await _context.SaveChangesAsync() > 0)
                return ApiResponse<string>.Success("ComplainType Deleted Successfully");

            return ApiResponse<string>.Error(HttpStatusCode.NotModified, "Faild To Delete the ComplainType");
        }
        public async Task<ApiResponse<IEnumerable<ComplainTypeDto>>> GetAll()
        {
            var ComplainTypes = await _context.ComplainTypes.AsNoTracking().ToListAsync();
            if (ComplainTypes.Count > 0)
            {
                var ComplainTypeDtos = _mapper.Map<IEnumerable<ComplainTypeDto>>(ComplainTypes);
                return ApiResponse<IEnumerable<ComplainTypeDto>>.Success(ComplainTypeDtos);
            }

            return ApiResponse<IEnumerable<ComplainTypeDto>>.Error(HttpStatusCode.NotFound, "No ComplainTypes Found");



        }
        public async Task<ApiResponse<ComplainTypeDto>> GetByIdAsync(int id)
        {
            var ComplainType = await _context.ComplainTypes.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
            if (ComplainType == null)
                return ApiResponse<ComplainTypeDto>.Error(HttpStatusCode.NotFound, "ComplainType Not Found");


            var ComplainTypeDto = _mapper.Map<ComplainTypeDto>(ComplainType);
            return ApiResponse<ComplainTypeDto>.Success(ComplainTypeDto);
        }
        public async Task<ApiResponse<string>> UpdateAsync(int id, ComplainTypeDto ComplainTypeDto)
        {
            var ExsitComplainType = await _context.ComplainTypes.FirstOrDefaultAsync(x => x.Id == id);

            if (ExsitComplainType is null)
                return ApiResponse<string>.Error(HttpStatusCode.NotFound, "ComplainType Not Found");
            var UpdateComplainType = _mapper.Map(ComplainTypeDto, ExsitComplainType);

            _context.ComplainTypes.Update(UpdateComplainType);
            if (await _context.SaveChangesAsync() > 0)
                return ApiResponse<string>.Success("ComplainType Updated Successfully");

            return ApiResponse<string>.Error(HttpStatusCode.NotModified, "Faild To Update ComplainType");


        }
    }
}
