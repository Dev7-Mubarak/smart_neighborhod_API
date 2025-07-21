namespace SmartNeighborhoodAPI.Helpers.DTOs.Auth
{
    public class ResetPasswordWithCodeDto
    {
        public string Email { get; set; }
        public string Code { get; set; }
        public string NewPassword { get; set; }
    }
}
