namespace SmartNeighborhoodAPI.Helpers.DTOs
{
    public class FamilyCatgoryDto
    {
        [Required(ErrorMessage ="Family Catgory Name is required")]

        public int Id { get; set; }
        public string Name { get; set; }
    }
}
