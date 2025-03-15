namespace SmartNeighborhoodAPI.Helpers.DTOs
{
    public class GetFamilyTypeDto
    {
        [Required(ErrorMessage ="Name is reqired")]
        [MaxLength(40)]

        public int Id { get; set; }
        public string Name { get; set; }

    }
}
