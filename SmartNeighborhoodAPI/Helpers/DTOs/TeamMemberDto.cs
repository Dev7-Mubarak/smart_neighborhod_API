namespace SmartNeighborhoodAPI.Helpers.DTOs
{
    public class TeamMemberDto
    {
        [Required(ErrorMessage ="Team id is required")]
        public int TeamId { get; set; }
        [Required(ErrorMessage ="Member id is required")]
        public int MemberId { get; set; }
        [Required(ErrorMessage ="DateOfJoin is required")]
        public DateTime DateOfJoin { get; set; }

        [Required(ErrorMessage ="Member job is required")]
        [MaxLength(13)]
        public string MemberJob { get; set; }
    }
}
