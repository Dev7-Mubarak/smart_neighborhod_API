namespace SmartNeighborhoodAPI.Helpers.DTOs
{
    public class ProjectFamilyDto
    {
        [Required(ErrorMessage ="Name is reqired")]
        [MaxLength(10)]
        public string Name { get; set; }
        [Required(ErrorMessage ="Project Id is reqired")]
        public int ProjectId { get; set; }
        [Required(ErrorMessage ="Family id is reqired")]
        public int FamilyId { get; set; }
        [Required(ErrorMessage ="Block id is reqired")]
        public int BlockId { get; set; }
    }
}
