using SmartNeighborhoodAPI.Helpers.DTOs.Auth;
using SmartNeighborhoodAPI.Helpers.DTOs.block;

namespace SmartNeighborhoodAPI.Interfaces
{
    public interface IBlockServices
    {
        Task<ApiResponse<RetrunBlockDto>> AddAsync(BlockDto blockDto);
        Task<ApiResponse<RetrunBlockDto>> ChangeManager(int id, ChangeManagerDto blockManagerDto);
        Task<ApiResponse<string>> DeleteAsync(int id);
        Task<ApiResponse<IEnumerable<RetrunBlockDto>>> GetAllAsync();
        Task<ApiResponse<Block>> GetByIdAsync(int id);
        Task<ApiResponse<BlockDetailesDto>> GetDetails(int blockId, int pageNumber, int pageSize, string? search);
        Task<ApiResponse<string>> UpdateAsync(int id, UpdateBlockDto blockDto);
    }
}