using SmartNeighborhoodAPI.Entites;
using SmartNeighborhoodAPI.Helpers;

namespace SmartNeighborhoodAPI.Interfaces
{
    public interface IManagerAccountService
    {
        Task<ApiResponse<AppUser>> CreateManagerAccountAsync(int personId, string email, string password, string role);
        Task<ApiResponse<string>> DeleteManagerAccountAsync(string userId);
    }
}
