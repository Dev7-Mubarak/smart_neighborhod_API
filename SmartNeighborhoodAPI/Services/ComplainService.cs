using System.Net;

namespace SmartNeighborhoodAPI.Services
{
    public class ComplainService
    {
        private readonly ApplicationDbContext _context;
        readonly IMapper _mapper;

        public ComplainService(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<ApiResponse<ComplainDTo>> AddAsync(ComplainDTo ComplainDTo)
        {

            var Complain = _mapper.Map<Complain>(ComplainDTo);


            await _context.AddAsync(Complain);
            if (await _context.SaveChangesAsync() > 0)
                return ApiResponse<ComplainDTo>.Success(ComplainDTo, "Added Successed");

            return ApiResponse<ComplainDTo>.Error(HttpStatusCode.BadRequest, "Complain not add");


        }
        public async Task<ApiResponse<string>> DeleteAsync(int id)
        {
            var entity = await _context.Complains.FirstOrDefaultAsync(x => x.Id == id); ;
            if (entity == null)
                return ApiResponse<string>.Error(HttpStatusCode.NotFound, "Complain Not Found");

            _context.Complains.Remove(entity);
            if (await _context.SaveChangesAsync() > 0)
                return ApiResponse<string>.Success("Complain Deleted Successfully");

            return ApiResponse<string>.Error(HttpStatusCode.NotModified, "Faild To Delete the Complain");
        }
        public async Task<ApiResponse<IEnumerable<ComplainDTo>>> GetAll()
        {
            var Complains = await _context.Complains.AsNoTracking().ToListAsync();
            if (Complains.Count > 0)
            {
                var ComplainDTos = _mapper.Map<IEnumerable<ComplainDTo>>(Complains);
                return ApiResponse<IEnumerable<ComplainDTo>>.Success(ComplainDTos);
            }

            return ApiResponse<IEnumerable<ComplainDTo>>.Error(HttpStatusCode.NotFound, "No Complains Found");



        }
        public async Task<ApiResponse<ComplainDTo>> GetByIdAsync(int id)
        {
            var Complain = await _context.Complains.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
            if (Complain == null)
                return ApiResponse<ComplainDTo>.Error(HttpStatusCode.NotFound, "Complain Not Found");


            var ComplainDTo = _mapper.Map<ComplainDTo>(Complain);
            return ApiResponse<ComplainDTo>.Success(ComplainDTo);
        }
        public async Task<ApiResponse<string>> UpdateAsync(int id, ComplainDTo ComplainDTo)
        {
            var ExsitComplain = await _context.Complains.FirstOrDefaultAsync(x => x.Id == id);

            if (ExsitComplain is null)
                return ApiResponse<string>.Error(HttpStatusCode.NotFound, "Complain Not Found");
            var UpdateComplain = _mapper.Map(ComplainDTo, ExsitComplain);

            _context.Complains.Update(UpdateComplain);
            if (await _context.SaveChangesAsync() > 0)
                return ApiResponse<string>.Success("Complain Updated Successfully");

            return ApiResponse<string>.Error(HttpStatusCode.NotModified, "Faild To Update Complain");


        }
    }
}
