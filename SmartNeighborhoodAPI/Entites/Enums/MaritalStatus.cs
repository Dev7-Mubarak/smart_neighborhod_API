using System.ComponentModel.DataAnnotations;

namespace SmartNeighborhoodAPI.Entites.Enums
{
    public enum MaritalStatus
    {
        [Display(Name = "أعزب")]
        Single = 1,
        [Display(Name = "متزوج")]
        Married,
        [Display(Name = "مطلق")]
        Divorced,
        [Display(Name = "أرمل")]
        Widowed
    }
}
