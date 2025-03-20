namespace SmartNeighborhoodAPI.Helpers.DTOs
{
    public class PersonComplainDto
    {
        [Required(ErrorMessage ="Person id is reqired")]
        public int PesonId { get; set; }
        [Required(ErrorMessage ="Complain id is reqired")]
        public int ComplainId { get; set; }
        [Required(ErrorMessage ="ConfilctParty id is reqired")]
        public int ConfilctPartyId { get; set; }
    }
}
