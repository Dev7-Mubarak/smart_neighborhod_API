using Swashbuckle.AspNetCore.Annotations;

namespace SmartNeighborhoodAPI.Helpers.DTOs.ResidentialUnits
{
    public class ReturnResidentialUnitDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string UnitManagerId { get; set; }

        public string UnitManagerName { get; set; }

    }
}
