using SmartNeighborhoodAPI.Entites.Enums;

namespace SmartNeighborhoodAPI.Entites
{
    public class Medicine
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(200)]
        public string Name { get; set; } = string.Empty;

        public MedicineType Type { get; set; } = MedicineType.Unknown;

        public string? Description { get; set; }

        public virtual ICollection<MedicineBatch> Batches { get; set; } = new List<MedicineBatch>();
    }
}
