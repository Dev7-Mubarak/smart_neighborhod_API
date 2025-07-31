using SmartNeighborhoodAPI.Entites;
using System.Net;

namespace SmartNeighborhoodAPI.Services
{
    public class DeliveryCaptainService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly ILogger<DeliveryCaptain> _logger;

        public DeliveryCaptainService(ApplicationDbContext context, IMapper mapper, ILogger<DeliveryCaptain> logger)
        {
            _context = context;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<ApiResponse<DeliveryCaptain>> AddAsync(DeliveryCaptain deliveryCaptain)
        {
            _logger.LogInformation("Adding a new DeliveryCaptain: {@DeliveryCaptain}", deliveryCaptain);

            var exists = await _context.DeliveryCaptains.AnyAsync(x => x.Id == deliveryCaptain.Id);
            if (exists)
            {
                _logger.LogWarning("DeliveryCaptain with Id {Id} already exists.", deliveryCaptain.Id);
                return ApiResponse<DeliveryCaptain>.Error(HttpStatusCode.Conflict, "DeliveryCaptain already exists.");
            }

            await _context.DeliveryCaptains.AddAsync(deliveryCaptain);
            if (await _context.SaveChangesAsync() <= 0)
            {
                _logger.LogError("Failed to add DeliveryCaptain with Id {Id}.", deliveryCaptain.Id);
                return ApiResponse<DeliveryCaptain>.Error(HttpStatusCode.BadRequest, "Failed to add DeliveryCaptain.");
            }

            _logger.LogInformation("DeliveryCaptain with Id {Id} added successfully.", deliveryCaptain.Id);
            return ApiResponse<DeliveryCaptain>.Success(deliveryCaptain, "DeliveryCaptain added successfully.");
        }

        public async Task<ApiResponse<List<DeliveryCaptain>>> GetAllAsync()
        {
            _logger.LogInformation("Retrieving all DeliveryCaptains.");

            var captains = await _context.DeliveryCaptains.AsNoTracking().ToListAsync();

            if (!captains.Any())
            {
                _logger.LogWarning("No DeliveryCaptains found.");
                return ApiResponse<List<DeliveryCaptain>>.Error(HttpStatusCode.NotFound, "No DeliveryCaptains found.");
            }

            _logger.LogInformation("Successfully retrieved all DeliveryCaptains.");
            return ApiResponse<List<DeliveryCaptain>>.Success(captains);
        }

        public async Task<ApiResponse<DeliveryCaptain>> GetByIdAsync(string id)
        {
            _logger.LogInformation("Retrieving DeliveryCaptain with Id: {Id}", id);

            var captain = await _context.DeliveryCaptains.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);

            if (captain == null)
            {
                _logger.LogWarning("DeliveryCaptain with Id {Id} not found.", id);
                return ApiResponse<DeliveryCaptain>.Error(HttpStatusCode.NotFound, "DeliveryCaptain not found.");
            }

            _logger.LogInformation("Successfully retrieved DeliveryCaptain with Id: {Id}", id);
            return ApiResponse<DeliveryCaptain>.Success(captain);
        }

        public async Task<ApiResponse<string>> UpdateAsync(string id, DeliveryCaptain updatedCaptain)
        {
            _logger.LogInformation("Updating DeliveryCaptain with Id: {Id}", id);

            var existingCaptain = await _context.DeliveryCaptains.FirstOrDefaultAsync(x => x.Id == id);
            if (existingCaptain == null)
            {
                _logger.LogWarning("DeliveryCaptain with Id {Id} not found.", id);
                return ApiResponse<string>.Error(HttpStatusCode.NotFound, "DeliveryCaptain not found.");
            }

            existingCaptain.FullName = updatedCaptain.FullName;
            existingCaptain.PhoneNumber = updatedCaptain.PhoneNumber;
            existingCaptain.FinancialAccountName = updatedCaptain.FinancialAccountName;
            existingCaptain.FinancialAccountNumber = updatedCaptain.FinancialAccountNumber;

            _context.DeliveryCaptains.Update(existingCaptain);

            if (await _context.SaveChangesAsync() > 0)
            {
                _logger.LogInformation("DeliveryCaptain with Id {Id} updated successfully.", id);
                return ApiResponse<string>.Success("DeliveryCaptain updated successfully.");
            }

            _logger.LogError("Failed to update DeliveryCaptain with Id {Id}.", id);
            return ApiResponse<string>.Error(HttpStatusCode.NotModified, "Failed to update DeliveryCaptain.");
        }

        public async Task<ApiResponse<string>> DeleteAsync(string id)
        {
            _logger.LogInformation("Attempting to delete DeliveryCaptain with Id {Id}", id);

            var entity = await _context.DeliveryCaptains.FirstOrDefaultAsync(x => x.Id == id);
            if (entity == null)
            {
                _logger.LogWarning("DeliveryCaptain with Id {Id} not found.", id);
                return ApiResponse<string>.Error(HttpStatusCode.NotFound, "DeliveryCaptain not found.");
            }

            _context.DeliveryCaptains.Remove(entity);
            if (await _context.SaveChangesAsync() <= 0)
            {
                _logger.LogError("Failed to delete DeliveryCaptain with Id {Id}.", id);
                return ApiResponse<string>.Error(HttpStatusCode.NotModified, "Failed to delete DeliveryCaptain.");
            }

            _logger.LogInformation("DeliveryCaptain with Id {Id} deleted successfully.", id);
            return ApiResponse<string>.Success("DeliveryCaptain deleted successfully.");
        }
    }
}