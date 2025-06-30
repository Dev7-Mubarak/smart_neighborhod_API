namespace SmartNeighborhoodAPI.Helpers.DTOs
{
    public class ConflictPartyDto
    {
        public int? PersonId { get; set; }

        [MaxLength(50)]
        public string? FullName { get; set; }

        [RegularExpression(@"^\d{6,20}$", ErrorMessage = "Identity number must be numeric and between 6 to 20 digits.")]
        public string? IdentityNumber { get; set; }
    }
}
