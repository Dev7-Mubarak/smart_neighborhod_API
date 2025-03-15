using System.Net;

namespace SmartNeighborhoodAPI.Services
{
    public class GroupService
    {
        private readonly ApplicationDbContext _context;
        readonly IMapper _mapper;

        public GroupService(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;

        }
        public async Task<ApiResponse<GroupDto>> AddAsync(GroupDto GroupDto)
        {

            var group = _mapper.Map<Group>(GroupDto);

            var existGroup = await _context.Groups.FirstOrDefaultAsync(x => x.Name ==GroupDto.Name);
            if (existGroup != null)
                return ApiResponse<GroupDto>.Error(HttpStatusCode.Conflict, "Group Is Already Exist");



            await _context.AddAsync(group);
            if (await _context.SaveChangesAsync() > 0)
                return ApiResponse<GroupDto>.Success(GroupDto, "Added Successed");

            return ApiResponse<GroupDto>.Error(HttpStatusCode.BadRequest, "Group not add");


        }
        public async Task<ApiResponse<string>> DeleteAsync(int id)
        {
            var entity = await _context.Groups.FirstOrDefaultAsync(x => x.Id == id); ;
            if (entity == null)
                return ApiResponse<string>.Error(HttpStatusCode.NotFound, "Group Not Found");

            _context.Groups.Remove(entity);
            if (await _context.SaveChangesAsync() > 0)
                return ApiResponse<string>.Success(message: "ggug");

            return ApiResponse<string>.Error(HttpStatusCode.NotModified, "Faild To Delete the Group");
        }
        public async Task<ApiResponse<IEnumerable<GroupDto>>> GetAll()
        {
            var Groups = await _context.Groups.AsNoTracking().ToListAsync();
            if (Groups.Count > 0)
            {
                var GroupDtos = _mapper.Map<IEnumerable<GroupDto>>(Groups);
                return ApiResponse<IEnumerable<GroupDto>>.Success(GroupDtos);
            }

            return ApiResponse<IEnumerable<GroupDto>>.Error(HttpStatusCode.NotFound, "No Group Found");



        }
        public async Task<ApiResponse<GroupDto>> GetByIdAsync(int id)
        {
            var Group = await _context.Groups.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
            if (Group == null)
                return ApiResponse<GroupDto>.Error(HttpStatusCode.NotFound, "Group Not Found");


            var GroupDto = _mapper.Map<GroupDto>(Group);
            return ApiResponse<GroupDto>.Success(GroupDto);
        }
        public async Task<ApiResponse<string>> UpdateAsync(int id, GroupDto GroupDto)
        {
            var ExsitGroup = await _context.Groups.FirstOrDefaultAsync(x => x.Id == id);

            if (ExsitGroup is null)
                return ApiResponse<string>.Error(HttpStatusCode.NotFound, "Group Not Found");
            var UpdateGroup = _mapper.Map(GroupDto, ExsitGroup);

            _context.Groups.Update(UpdateGroup);
            if (await _context.SaveChangesAsync() > 0)
                return ApiResponse<string>.Success("Group Updated Successfully");

            return ApiResponse<string>.Error(HttpStatusCode.NotModified, "Faild To Update Group");


        }
    }
}
