namespace SmartNeighborhoodAPI.Interfaces
{
    public interface IAuthService
    {
        Task<ApiResponse<UserResponse>> RegisterAsync(RegisterDto registerDto);
        Task<ApiResponse<UserResponse>> LoginAsync(LoginDto loginDto);
    }
}
