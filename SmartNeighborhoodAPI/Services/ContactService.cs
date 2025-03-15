//using System.Net;

//namespace SmartNeighborhoodAPI.Services
//{
//    public class ContactService
//    {
//        private readonly ApplicationDbContext _context;
//        readonly IMapper _mapper;

//        public ContactService(ApplicationDbContext context, IMapper mapper)
//        {
//            _context = context;
//            _mapper = mapper;

//        }
//        public async Task<ApiResponse<ContactInfoDto>> AddAsync(ContactInfoDto ContactInfoDto)
//        {

//            var contactInfo = _mapper.Map<ContactInfoDto>(ContactInfoDto);
//            var ContactInfo = _mapper.Map<ContactInfo>(contactInfo);

//            await _context.AddAsync(ContactInfo);
//            if (await _context.SaveChangesAsync() > 0)
//                return ApiResponse<ContactInfoDto>.Success(contactInfo, "Added Successed");

//            return ApiResponse<ContactInfoDto>.Error(HttpStatusCode.BadRequest, "ContactInfo not add");


//        }
//        public async Task<ApiResponse<string>> DeleteAsync(int id)
//        {
//            var entity = await GetByIdAsync(id);
//            if (entity == null)
//                return ApiResponse<string>.Error(HttpStatusCode.BadRequest, "ContactInfo Not Found");

//            _context.Remove(entity);
//            if (await _context.SaveChangesAsync() > 0)
//                return ApiResponse<string>.Success("ContactInfo Deleted Successfully");

//            return ApiResponse<string>.Error(HttpStatusCode.BadRequest, "Faild To Delete the ContactInfo");
//        }
//        public async Task<ApiResponse<IQueryable<ContactInfoDto>>> GetAll()
//        {
//            var ContactInfos = _context.ContactInfos.Include("").AsNoTracking().ToList();
//            if (ContactInfos.Count > 0)
//            {
//                var ContactInfoDtos = _mapper.Map<IQueryable<ContactInfoDto>>(ContactInfos);
//                return ApiResponse<IQueryable<ContactInfoDto>>.Success(ContactInfoDtos);
//            }

//            return ApiResponse<IQueryable<ContactInfoDto>>.Error(HttpStatusCode.BadRequest, "No ContactInfo Found");



//        }
//        public async Task<ApiResponse<ContactInfoDto>> GetByIdAsync(int id)
//        {
//            var ContactInfo = await _context.ContactInfos.Include("").AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
//            if (ContactInfo == null)
//                return ApiResponse<ContactInfoDto>.Error(HttpStatusCode.BadRequest, "ContactInfo Not Found");


//            var ContactInfoDto = _mapper.Map<ContactInfoDto>(ContactInfo);
//            return ApiResponse<ContactInfoDto>.Success(ContactInfoDto);
//        }
//        public async Task<ApiResponse<string>> UpdateAsync(int id, ContactInfoDto ContactInfoDto)
//        {
//            var ExsitContactInfo = await _context.ContactInfos.FirstOrDefaultAsync(x => x.Id == id);

//            if (ExsitContactInfo is null)
//                return ApiResponse<string>.Error(HttpStatusCode.BadRequest, "ContactInfo Not Found");
//            var UpdateContactInfo = _mapper.Map(ContactInfoDto, ExsitContactInfo);

//            _context.ContactInfos.Update(UpdateContactInfo);
//            if (await _context.SaveChangesAsync() > 0)
//                return ApiResponse<string>.Success("ContactInfo Updated Successfully");

//            return ApiResponse<string>.Error(HttpStatusCode.BadRequest, "Faild To Update ContactInfo");


//        }
//    }
//}
