public class CreatePersonDto
{
    public string FullName { get; set; }
    public string PhoneNumber { get; set; }
    public string Job { get; set; }
    public string Email { get; set; }
    public DateTime DateOfBirth { get; set; }
    public byte Gender { get; set; }
    public string BloodType { get; set; }
    public string IdentityNumber { get; set; }
    public string TypeOfIdentity { get; set; }
    public string Status { get; set; }
    public IFormFile? Image { get; set; }
}
