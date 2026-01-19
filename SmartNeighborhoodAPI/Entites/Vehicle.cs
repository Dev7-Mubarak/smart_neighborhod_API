using SmartNeighborhoodAPI.Entites.Enums;

namespace SmartNeighborhoodAPI.Entites
{
    public class Vehicle
    {
        public int Id { get; set; }

        [MaxLength(20)]
        public string? PlateNumber { get; set; }   

        public int OwnerResidentId { get; set; }
        public Person Owner { get; set; }        

        [MaxLength(50)]
        public string? Model { get; set; }

        public VehicleType Type { get; set; }

        public DateTime? LicenseExpiryDate { get; set; }
    }
}
