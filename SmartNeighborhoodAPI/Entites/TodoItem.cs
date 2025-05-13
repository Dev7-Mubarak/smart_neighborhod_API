using System.Text.Json.Serialization;

namespace SmartNeighborhoodAPI.Entites
{
    public class TodoItem
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Title { get; set; }

        [StringLength(500)]
        public string? Description { get; set; }

        public bool IsCompleted { get; set; }

        [Required]
        public int CategoryId { get; set; }
        [JsonIgnore]
        public Category? Category { get; set; }
    }
}
