namespace SmartNeighborhoodAPI.Helpers.DTOs
{
    public class MemberTypeDto
    {
        [Required(ErrorMessage ="Name is reqired")]
        [MaxLength(40)]
        public string Name { get; set; }

        public int Id { get; set; }
    }
}
