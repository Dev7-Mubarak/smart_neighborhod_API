using SmartNeighborhoodAPI.Entites.Enums;
using Swashbuckle.AspNetCore.Annotations;

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
        [SwaggerSchema("Blood type: A+, A-, B+, B-, AB+, AB-, O+, O-")]
        public string BloodType { get; set; }       
        [SwaggerSchema("Occupation status: موظف (Employee), طالب (Student), ربة منزل (HouseWife), عاطل عن العمل (Unemployed), صاحب عمل (SelfEmployed)")]
        public string OccupationStatus { get; set; } 
        [SwaggerSchema("Marital status: أعزب (Single), متزوج (Married), مطلق (Divorced), أرمل (Widowed)")]
        public string MaritalStatus { get; set; }    
        [SwaggerSchema("Person type: مواطن (Citizen), مدير وحدة سكنية (UnitManager), مدير مربع (BlockManager), مدير حي سكني (Admin)")]
        public string PersonType { get; set; }    
        public string Job { get; set; }
    }
}
