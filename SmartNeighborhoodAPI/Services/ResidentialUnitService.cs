using Microsoft.AspNetCore.Identity;
using SmartNeighborhoodAPI.Entites;
using SmartNeighborhoodAPI.Helpers.DTOs.Auth;
using SmartNeighborhoodAPI.Helpers.DTOs.block;
using SmartNeighborhoodAPI.Helpers.DTOs.Families;
using SmartNeighborhoodAPI.Helpers.DTOs.ResidentialUnits;
using SmartNeighborhoodAPI.Interfaces;
using System.Data;
using System.Net;

namespace SmartNeighborhoodAPI.Services
{
    public class ResidentialUnitService
    {
        private readonly ApplicationDbContext _context;
        private readonly IAuthService _authService;
        private readonly ILogger<ResidentialUnit> _logger;
        private readonly UserManager<AppUser> _userManager;


        public ResidentialUnitService(ApplicationDbContext context, IAuthService authService, ILogger<ResidentialUnit> logger, UserManager<AppUser> userManager)
        {
            _context = context;
            _authService = authService;
            _userManager = userManager;
            _logger = logger;
        }





        public async Task<ApiResponse<RetrunBlockDto>> ChangeManager(int id, ChangeManagerDto blockManagerDto)
        {
            _logger.LogInformation("Initiating change of block manager for BlockId: {BlockId}, PersonId: {PersonId}",
                id, blockManagerDto.PersonId);

            // Step 1: Validate block
            var block = await _context.Blocks.FindAsync(id);
            if (block == null)
            {
                _logger.LogWarning("Block with ID '{BlockId}' not found.", id);
                return ApiResponse<RetrunBlockDto>.Error(HttpStatusCode.NotFound, "لم يتم العثور على مربع.");
            }

            // Step 2: Validate person
            var person = await _context.People.FindAsync(blockManagerDto.PersonId);
            if (person == null)
            {
                _logger.LogWarning("Person with ID '{PersonId}' not found.", blockManagerDto.PersonId);
                return ApiResponse<RetrunBlockDto>.Error(HttpStatusCode.NotFound, "هذا الشخص غير موجود");
            }

            var existingUser = await _userManager.FindByEmailAsync(blockManagerDto.Email);

            if (existingUser != null)
            {
                _logger.LogWarning("Person with ID '{PersonId}' not found.", blockManagerDto.Email);
                return ApiResponse<RetrunBlockDto>.Error(HttpStatusCode.Conflict, "هذا الايميل مستخدم بالفعل ");
            }

            // Step 4: Create new manager account
            var createResult = await _authService.CreateBlockManagerAccountAsync(new CreateBlockManagerDto
            {
                Email = blockManagerDto.Email,
                Password = blockManagerDto.Password,
                PersonId = blockManagerDto.PersonId
            });

            if (!createResult.IsSuccess)
            {
                _logger.LogError("Failed to create new block manager. Reason: {Reason}", createResult.Message);
                return ApiResponse<RetrunBlockDto>.Error(createResult.StatusCode, createResult.Message, createResult.Errors);
            }


            var oldManagerId = block.BlockManagerId;


            // Step 5: Update block manager
            //block.UnitManagerId = createResult.Data.Id;
            _context.Blocks.Update(block);
            await _context.SaveChangesAsync();

            if (createResult.Data.Role == "BlockManager")
            {
                // Step 6: Delete old manager account (if any)
                var deleteResult = await _authService.DeleteBlockManagerAccountByIdAsync(oldManagerId);
                if (!deleteResult.IsSuccess)
                {
                    _logger.LogError("Failed to delete old block manager with ID: {OldManagerId}", oldManagerId);
                    return ApiResponse<RetrunBlockDto>.Error(deleteResult.StatusCode, deleteResult.Message, deleteResult.Errors);
                }
            }


            // Step 7: Return success response
            var returnBlockDto = new RetrunBlockDto
            {
                Id = block.Id,
                Name = block.Name,
                //ManagerId = block.UnitManagerId,    
                PersonId = person.Id,
                Email = createResult.Data.Email,
                Role = createResult.Data.Role,
                FullName = person.FullName
            };

            _logger.LogInformation("Block manager updated successfully for block '{BlockName}' (ID: {BlockId})",
                block.Name, block.Id);

            return ApiResponse<RetrunBlockDto>.Success(returnBlockDto,
                "تم تحديث مدير المربع بنجاح. تم إرسال بيانات تسجيل الدخول عبر البريد الإلكتروني.");
        }
        public async Task<ApiResponse<ReturnResidentialUnitDto>> AddAsync(AddResidentialUnitDto dto, CancellationToken ct = default)
        {
            _logger.LogInformation("Adding ResidentialUnit {Name}", dto.Name);

            var unit = new ResidentialUnit
            {
                Name = dto.Name,
                UnitManagerId = dto.UnitManagerId
            };

            // optional: create initial blocks if provided
            if (dto.Blocks != null && dto.Blocks.Any())
            {
                foreach (var b in dto.Blocks)
                {
                    unit.Blocks.Add(new Block
                    {
                        Name = b.Name,
                        BlockManagerId = b.BlockManagerId
                    });
                }
            }

            _context.ResidentialUnits.Add(unit);
            await _context.SaveChangesAsync(ct);

            // load manager username if needed
            await _context.Entry(unit).Reference(u => u.UnitManager).LoadAsync(ct);

            var dtoOut = MapToDto(unit);
            return ApiResponse<ReturnResidentialUnitDto>.Success(dtoOut, "");
        }

        public async Task<ApiResponse<PaginatedResult<ReturnResidentialUnitDto>>> GetAllAsync(string? name = null, string? managerId = null, int page = 1, int pageSize = 10, CancellationToken ct = default)
        {
            _logger.LogInformation("Fetching ResidentialUnits filter name:{Name} managerId:{ManagerId}", name, managerId);

            var query = _context.ResidentialUnits
                .AsNoTracking()
                .Include(u => u.UnitManager)
                .Include(u => u.Blocks)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(name))
                query = query.Where(u => u.Name!.Contains(name));

            if (!string.IsNullOrWhiteSpace(managerId))
                query = query.Where(u => u.UnitManagerId == managerId);

            var total = await query.CountAsync(ct);

            if (total == 0)
            {
                return ApiResponse<PaginatedResult<ReturnResidentialUnitDto>>.Success(
                    PaginatedResult<ReturnResidentialUnitDto>.Success(new List<ReturnResidentialUnitDto>(), 0, page, pageSize),
                    "No residential units found");
            }

            var units = await query
                .OrderBy(u => u.Name)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync(ct);

            var list = units.Select(MapToDto).ToList();
            var paged = PaginatedResult<ReturnResidentialUnitDto>.Success(list, total, page, pageSize);
            return ApiResponse<PaginatedResult<ReturnResidentialUnitDto>>.Success(paged);
        }

        public async Task<ApiResponse<ReturnResidentialUnitDto>> GetByIdAsync(int id, CancellationToken ct = default)
        {
            var unit = await _context.ResidentialUnits
                .AsNoTracking()
                .Include(u => u.UnitManager)
                .Include(u => u.Blocks)
                .FirstOrDefaultAsync(u => u.Id == id, ct);

            if (unit == null)
                return ApiResponse<ReturnResidentialUnitDto>.Error(HttpStatusCode.NotFound, "Residential unit not found");

            return ApiResponse<ReturnResidentialUnitDto>.Success(MapToDto(unit));
        }

        public async Task<ApiResponse<string>> UpdateAsync(int id, UpdateResidentialUnitDto dto, CancellationToken ct = default)
        {
            var unit = await _context.ResidentialUnits.Include(u => u.Blocks).FirstOrDefaultAsync(u => u.Id == id, ct);
            if (unit == null)
                return ApiResponse<string>.Error(HttpStatusCode.NotFound, "Residential unit not found");

            unit.Name = dto.Name;
            unit.UnitManagerId = dto.UnitManagerId;

            _context.ResidentialUnits.Update(unit);
            await _context.SaveChangesAsync(ct);
            return ApiResponse<string>.Success("Residential unit updated successfully");
        }

        public async Task<ApiResponse<string>> DeleteAsync(int id, CancellationToken ct = default)
        {
            var unit = await _context.ResidentialUnits.Include(u => u.Blocks).FirstOrDefaultAsync(u => u.Id == id, ct);
            if (unit == null)
                return ApiResponse<string>.Error(HttpStatusCode.NotFound, "Residential unit not found");

            // optional: prevent delete if blocks exist
            if (unit.Blocks != null && unit.Blocks.Any())
            {
                return ApiResponse<string>.Error(HttpStatusCode.BadRequest, "Cannot delete unit with existing blocks");
            }

            _context.ResidentialUnits.Remove(unit);
            await _context.SaveChangesAsync(ct);
            return ApiResponse<string>.Success("Residential unit deleted successfully");
        }

        // ---- helpers ----
        private ReturnResidentialUnitDto MapToDto(ResidentialUnit unit)
        {
            return new ReturnResidentialUnitDto
            {
                Id = unit.Id,
                Name = unit.Name,
                UnitManagerId = unit.UnitManagerId,
                UnitManagerName = unit.UnitManager?.UserName ?? string.Empty,
                Blocks = unit.Blocks?.Select(b => new BlockSummaryDto
                {
                    Id = b.Id,
                    Name = b.Name,
                    BlockManagerId = b.BlockManagerId,
                    BlockManagerName = b.BlockManager?.UserName
                }).ToList() ?? new List<BlockSummaryDto>()
            };
        }
        public async Task<ApiResponse<BlockDetailesDto>> GetDetails(int blockId, int pageNumber, int pageSize, string? search)
        {
            var block = await _context.Blocks
                .AsNoTracking()
                .Where(x => x.Id == blockId)
                .Select(x => new BlockDetailesDto
                {
                    Block = new BlockWithStatsDto
                    {
                        Id = x.Id,
                        Name = x.Name,
                        //ManagerName = x.UnitManager.Person.FullName,
                        TotalFamilies = x.Families.Count,
                        totalOrphans = x.Families.Count(f => f.FamilyCatgory.Id == 2),
                        TotalWidows = x.Families.Count(f => f.FamilyCatgory.Id == 1),
                    },
                    Families = x.Families.Select(f => new FamilyDetailsDto
                    {
                        Id = f.Id,
                        Name = f.Name,
                        FamilyCatgoryId = f.FamilyCatgoryId,
                        FamilyCatgoryName = f.FamilyCatgory.Name,
                        BlockId = f.BlockId,
                        BlockName = f.Block.Name,

                        FamilyNotes = f.FamilyNotes,
                        Location = f.Location,
                        FamilyHeadId = f.FamilyMembers
                            .Where(fm => fm.MemberFamilyRoleId == 1)
                            .Select(fm => fm.PersonId)
                            .FirstOrDefault(),

                        FamilyHeadName = f.FamilyMembers
                            .Where(fm => fm.MemberFamilyRoleId == 1)
                            .Select(fm => fm.Person.FullName)
                            .FirstOrDefault() ?? string.Empty,

                        PhoneNumber = f.FamilyMembers
                            .Where(fm => fm.MemberFamilyRoleId == 1)
                            .Select(fm => fm.Person.PhoneNumber)
                            .FirstOrDefault() ?? string.Empty,

                    }).ToList()
                })
                .FirstOrDefaultAsync();

            if (block == null)
            {
                _logger.LogWarning("Block with ID {BlockId} not found", blockId);
                return ApiResponse<BlockDetailesDto>.Error(HttpStatusCode.NotFound, "المربع غير موجود.");
            }

            return ApiResponse<BlockDetailesDto>.Success(block, "تم جلب تفاصيل المربع بنجاح.");
        }

    }
}
