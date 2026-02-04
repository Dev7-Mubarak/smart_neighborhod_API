using SmartNeighborhoodAPI.Helpers.DTOs.Families;
using SmartNeighborhoodAPI.Helpers.DTOs.FamilyMember;
using SmartNeighborhoodAPI.Helpers.DTOs.Teams;

namespace SmartNeighborhoodAPI.Interfaces
{
    public interface IFamilyService
    {
        Task<ApiResponse<ReturnFamilyDto>> AddAsync(FamilyDto familyDto);
        Task<ApiResponse<string>> DeleteAsync(int id);
        Task<ApiResponse<List<ReturnFamilyDto>>> GetAllAsync();
        Task<ApiResponse<ReturnFamilyDto>> GetById(int id);
        Task<ApiResponse<ReturnFamilyInfoDto>> GetDetailesAsync(int id);
        Task<ApiResponse<ReturnFamilyDto>> UpdateAsync(int id, FamilyDto familyDto);
    }
}