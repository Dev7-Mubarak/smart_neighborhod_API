using SmartNeighborhoodAPI.Entites.Enums;
using SmartNeighborhoodAPI.Helpers.Attrbuites;
using Swashbuckle.AspNetCore.Annotations;

public class CreatePersonDto
{
    [Required(ErrorMessage = "الاسم الأول مطلوب.")]
    [MaxLength(20, ErrorMessage = "يجب ألا يزيد الاسم الأول عن 20 حرفًا.")]
    public string FirstName { get; set; }

    [Required(ErrorMessage = "الاسم الثاني مطلوب.")]
    [MaxLength(20, ErrorMessage = "يجب ألا يزيد الاسم الثاني عن 20 حرفًا.")]
    public string SecondName { get; set; }

    [Required(ErrorMessage = "الاسم الثالث مطلوب.")]
    [MaxLength(20, ErrorMessage = "يجب ألا يزيد الاسم الثالث عن 20 حرفًا.")]
    public string ThirdName { get; set; }

    [Required(ErrorMessage = "اسم العائلة مطلوب.")]
    [MaxLength(20, ErrorMessage = "يجب ألا يزيد اسم العائلة عن 20 حرفًا.")]
    public string LastName { get; set; }

    [MaxLength(30, ErrorMessage = "يجب ألا يزيد رقم الهاتف عن 30 رقمًا.")]
    [Phone(ErrorMessage = "صيغة رقم الهاتف غير صحيحة.")]
    public string? PhoneNumber { get; set; }

    [DataType(DataType.Date, ErrorMessage = "صيغة التاريخ غير صحيحة.")]
    public DateTime? DateOfBirth { get; set; }

    [SwaggerSchema("Gender options: ذكر (Male), أنثى (Female)")]
    public Gender Gender { get; set; }

    [AllowedExtensions(FileHelper.AllowedExtensions)]
    [MaxFileSize(FileHelper.MaxFileSizeInBytes)]
    public IFormFile? Image { get; set; }

    [Required(ErrorMessage = "فصيلة الدم مطلوبة.")]
    [SwaggerSchema("Blood type options: A+, A-, B+, B-, AB+, AB-, O+, O-")]
    public BloodType BloodType { get; set; }

    [Required(ErrorMessage = "الحالة الاجتماعية مطلوبة.")]
    [SwaggerSchema("Marital status options: أعزب (Single), متزوج (Married), مطلق (Divorced), أرمل (Widowed)")]
    public MaritalStatus MaritalStatus { get; set; }

    [Required(ErrorMessage = "حالة العمل مطلوبة.")]
    [SwaggerSchema("Occupation status options: موظف (Employee), طالب (Student), ربة منزل (HouseWife), عاطل عن العمل (Unemployed), صاحب عمل (SelfEmployed)")]
    public OccupationStatus OccupationStatus { get; set; }

    [MaxLength(30, ErrorMessage = "يجب ألا يزيد اسم الوظيفة عن 30 حرفًا.")]
    public string? Job { get; set; }
}
