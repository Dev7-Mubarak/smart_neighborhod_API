using System.ComponentModel.DataAnnotations.Schema;

namespace SmartNeighborhoodAPI.Entites
{
    public class MedicineBatch
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey(nameof(Medicine))]
        public int MedicineId { get; set; }

        public virtual Medicine? Medicine { get; set; }

        public int Quantity { get; set; }


        public DateTime? ArrivalDate { get; set; }

        public DateTime? ExpireDate { get; set; }

        [MaxLength(250)]
        public string? Provider { get; set; }
    }
}
