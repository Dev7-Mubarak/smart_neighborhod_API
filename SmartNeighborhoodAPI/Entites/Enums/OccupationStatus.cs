using System.ComponentModel.DataAnnotations;

namespace SmartNeighborhoodAPI.Entites.Enums
{
    public enum OccupationStatus
    {
        [Display(Name = "طالب")]
        Student = 1,
        [Display(Name = "يعمل")]
        Employee,
        [Display(Name = "عاطل عن العمل")]
        Unemployed,
        [Display(Name = "ربة منزل")]
        HouseWife,
        [Display(Name = "عمل حر")]
        SelfEmployed
    }
}
