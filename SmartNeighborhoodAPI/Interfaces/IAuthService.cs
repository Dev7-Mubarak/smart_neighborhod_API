using SmartNeighborhoodAPI.Helpers.DTOs.Auth;

namespace SmartNeighborhoodAPI.Interfaces
{
    public interface IAuthService
    {
        Task<ApiResponse<UserResponse>> CreateBlockManagerAsync(CreateBlockManagerDto registerDto);
        Task<ApiResponse<UserResponse>> LoginAsync(LoginDto loginDto);
        Task<ApiResponse<UserResponse>> ConfirmEmailOtp(ConfirmEmailOtpDto emailOtpDto);
    }
}
