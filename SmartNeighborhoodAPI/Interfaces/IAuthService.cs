using SmartNeighborhoodAPI.Helpers.DTOs.Auth;
using SmartNeighborhoodAPI.Helpers.DTOs.block;

namespace SmartNeighborhoodAPI.Interfaces
{
    public interface IAuthService
    {
        Task<ApiResponse<UserResponse>> CreateBlockManagerAccountAsync(CreateBlockManagerDto registerDto);
        Task<ApiResponse<UserResponse>> DeleteBlockManagerAccountByIdAsync(string managerId);
        Task<ApiResponse<UserResponse>> LoginAsync(LoginDto loginDto);
        Task<ApiResponse<UserResponse>> ConfirmEmailOtp(ConfirmEmailOtpDto emailOtpDto);
    }
}
