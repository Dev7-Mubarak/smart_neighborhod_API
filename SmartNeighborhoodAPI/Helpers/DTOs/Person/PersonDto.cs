using SmartNeighborhoodAPI.Entites.Enums;

namespace SmartNeighborhoodAPI.Helpers.DTOs.Person
{
    public class PersonDto
    {
        public int Id { get; set; }
        public string? FullName { get; set; }
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public string ThirdName { get; set; }
        public string LastName { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Image { get; set; }
        public string? Gender { get; set; }
        public string BloodType { get; set; }       
        public string OccupationStatus { get; set; } 
        public string MaritalStatus { get; set; }    
        public string Job { get; set; }
    }
}
