namespace SmartNeighborhoodAPI.Helpers.DTOs.Contact
{
    public class AddContactDto
    {
        [Required]
        [MaxLength(255)]
        public string Name { get; set; } = null!;

        [MaxLength(255)]
        public string Job { get; set; }

        [Required]
        [Phone]
        [MaxLength(50)]
        public string Phone { get; set; } = null!;
    }
}
