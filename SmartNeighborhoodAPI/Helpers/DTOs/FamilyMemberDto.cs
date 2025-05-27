using System.ComponentModel.DataAnnotations;
using SmartNeighborhoodAPI.Helpers.DTOs.Person;

namespace SmartNeighborhoodAPI.Helpers.DTOs
{
    public class FamilyMemberDto
    {
        public PersonDto Person { get; set; }
        public string RoleName { get; set; }
    }
}

