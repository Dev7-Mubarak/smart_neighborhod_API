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
        Task<ApiResponse<string>> SendResetCodeAsync(ForgotPasswordDto model);
        Task<ApiResponse<string>> RegisterAsync(RegisterDto model);
        Task<ApiResponse<string>> VerifyResetCodeAsync(VerifyResetCodeDto dto);
        Task<ApiResponse<string>> ResetPasswordAsync(ResetPasswordDto dto);
    }
}
