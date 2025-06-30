using SmartNeighborhoodAPI.Entites;
using System.Net;

namespace SmartNeighborhoodAPI.Services
{
    public class ComplainService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly ImageService _imageService;
        private readonly string _complainImagePath;

        public ComplainService(
            ApplicationDbContext context,
            IMapper mapper,
            IWebHostEnvironment webHostEnvironment,
            ImageService imageService)
        {
            _context = context;
            _mapper = mapper;
            _webHostEnvironment = webHostEnvironment;
            _imageService = imageService;
            _complainImagePath = $"{_webHostEnvironment.WebRootPath}{FileHelper.ComplainImagesPath}";
        }

        public async Task<ApiResponse<string>> UpdateAsync(int id, AddComplainDto dto)
        {
            var exist = await _context.Complains
                .Include(x => x.ConfilctParties)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (exist == null)
                return ApiResponse<string>.Error(HttpStatusCode.NotFound, "Complain not found");

            var managerExists = await _context.People.AnyAsync(p => p.Id == dto.ManagerId);
            if (!managerExists)
                return ApiResponse<string>.Error(HttpStatusCode.BadRequest, "Manager not found");

            if (dto.Image != null)
                exist.Image = await _imageService.SaveImageAsync(dto.Image, _complainImagePath);

            // Use AutoMapper to map updated fields
            _mapper.Map(dto, exist);

            // Update conflict parties if necessary

            _context.Complains.Update(exist);
            await _context.SaveChangesAsync();

            return ApiResponse<string>.Success("Complain Updated Successfully");
        }
     


        public async Task<ApiResponse<ComplainDTo>> AddAsync(AddComplainDto dto)
        {
            // Validate that the Manager exists
            var managerExists = await _context.People.AnyAsync(p => p.Id == dto.ManagerId);
            if (!managerExists)
                return ApiResponse<ComplainDTo>.Error(HttpStatusCode.BadRequest, "Manager not found");

            // Validate that the ComplainType exists
            var typeExists = await _context.ComplainTypes.AnyAsync(c => c.Id == dto.ComplainTypeId);
            if (!typeExists)
                return ApiResponse<ComplainDTo>.Error(HttpStatusCode.BadRequest, "Complain type not found");

            // Handle image saving if provided
            string complainImage = string.Empty;
            if (dto.Image != null)
            {
                complainImage = await _imageService.SaveImageAsync(dto.Image, _complainImagePath);
            }

            // Map DTO to Entity
            var complain = _mapper.Map<Complain>(dto);
            complain.Image = complainImage;

            await _context.Complains.AddAsync(complain);
            await _context.SaveChangesAsync();

            var complainDto = _mapper.Map<ComplainDTo>(complain);
            return ApiResponse<ComplainDTo>.Success(complainDto, "Added Successfully");
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
   
       


        public async Task<ApiResponse<ComplainDTo>> GetByIdAsync(int id)
        {
            var complain = await _context.Complains
                .Include(c => c.ConfilctParties)
                    .ThenInclude(cp => cp.Person)
                .AsNoTracking()
                .FirstOrDefaultAsync(c => c.Id == id);

            if (complain == null)
                return ApiResponse<ComplainDTo>.Error(HttpStatusCode.NotFound, "Complain not found");

            var dto = _mapper.Map<ComplainDTo>(complain);
            return ApiResponse<ComplainDTo>.Success(dto);
        }



        public async Task<ApiResponse<List<ManagerDropdownDto>>> GetAllManagersAsync()
        {
            var managers = await _context.Blocks
                .Where(b => b.Manager != null && b.Manager.Person != null)
                .Select(b => new ManagerDropdownDto
                {
                    Id = b.Manager.Person.Id,
                    FullName = b.Manager.Person.FirstName + " " +
                               b.Manager.Person.SecondName + " " +
                               b.Manager.Person.ThirdName + " " +
                               b.Manager.Person.LastName
                })
                .Distinct()
                .ToListAsync();

            if (managers.Count == 0)
                return ApiResponse<List<ManagerDropdownDto>>.Error(HttpStatusCode.NotFound, "No managers found");

            return ApiResponse<List<ManagerDropdownDto>>.Success(managers);
        }
    }
}

