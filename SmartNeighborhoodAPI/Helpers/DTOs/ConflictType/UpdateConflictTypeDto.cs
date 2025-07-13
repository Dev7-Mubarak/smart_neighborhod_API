namespace SmartNeighborhoodAPI.Helpers.DTOs.ConflictType
{
    public class UpdateConflictTypeDto
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }
    }
}
