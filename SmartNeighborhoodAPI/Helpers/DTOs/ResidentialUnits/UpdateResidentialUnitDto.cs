using Swashbuckle.AspNetCore.Annotations;

namespace SmartNeighborhoodAPI.Helpers.DTOs.ResidentialUnits
{
    public class UpdateResidentialUnitDto
    {
        [Required]
        public string Name { get; set; }

    }
}
