namespace SmartNeighborhoodAPI.Helpers.DTOs
{
    public class GroupDto
    {
        [Required(ErrorMessage ="Name is reqired")]
        [MaxLength(40)]
        public string Name { get; set; }
    }
}
