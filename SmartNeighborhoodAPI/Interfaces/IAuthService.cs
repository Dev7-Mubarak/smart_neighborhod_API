using SmartNeighborhoodAPI.Helpers.DTOs.Auth;

namespace SmartNeighborhoodAPI.Interfaces
{
    //This service Provides methods for user authentication and account management.
    public interface IAuthService
    {
        Task<ApiResponse<UserResponse>> CreateBlockManagerAccountAsync(CreateBlockManagerDto registerDto);
        Task<ApiResponse<UserResponse>> DeleteBlockManagerAccountByIdAsync(string managerId);
        Task<ApiResponse<UserResponse>> LoginAsync(LoginDto loginDto);
        Task<ApiResponse<UserResponse>> ConfirmEmailOtp(ConfirmEmailOtpDto emailOtpDto);
    }
}
