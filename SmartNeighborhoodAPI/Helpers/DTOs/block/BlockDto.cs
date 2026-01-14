namespace SmartNeighborhoodAPI.Helpers.DTOs.block
{
    public class BlockDto
    {
        [Required(ErrorMessage = "The Name Is Required")]
        [MaxLength(30)]
        public string Name { get; set; }
        public int PersonId { get; set; }
        [Required(ErrorMessage = "Identifier is required")] 
        public string Identifier { get; set; }
        [Required(ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]   
        public string Password { get; set; }

        public int ResitinalUnitId { get; set; }
    }
}
