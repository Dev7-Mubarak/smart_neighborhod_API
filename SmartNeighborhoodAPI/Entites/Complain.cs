using System.ComponentModel.DataAnnotations.Schema;

namespace OurProjectSmartNeiborhood.Entites
{
    public class Complain
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Outcome { get; set; }

        [Required]
        public int ComplainTypeId { get; set; }

        [Required]
        [MaxLength(500)]
        public string Notes { get; set; }

        [Required]
        public DateTime SessionDate { get; set; }

        [Required]
        public int ManagerId { get; set; }

        public bool IsResolved { get; set; }

        public string? Image { get; set; }

        public ComplainType ComplainType { get; set; }

        public Person Manager { get; set; }

        public ICollection<ConfilctParty> ConfilctParties { get; set; } = new List<ConfilctParty>();
    }

}
