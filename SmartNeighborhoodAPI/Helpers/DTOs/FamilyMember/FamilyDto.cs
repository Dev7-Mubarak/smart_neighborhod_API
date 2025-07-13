using SmartNeighborhoodAPI.Entites.Enums;

namespace SmartNeighborhoodAPI.Helpers.DTOs.FamilyMember
{
    public class FamilyDto
    {

        [Required(ErrorMessage = "Family Catgory id is reqired")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Family Catgory id is reqired")]
        public int FamilyCatgoryId { get; set; }

        [Required(ErrorMessage = "Location is reqired")]
        public string Location { get; set; }

        [Required(ErrorMessage = "Family Type id is reqired")]
        public int FamilyTypeId { get; set; }
        [MaxLength(150)]
        public string FamilyNotes { get; set; }
        [Required(ErrorMessage = "Block id is reqired")]
        public int BlockId { get; set; }
        [Required(ErrorMessage = "Person id is reqired")]
        public int FamilyHeadId { get; set; }
    }
}
