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

    [MaxLength(30, ErrorMessage = "National ID must not exceed 30 characters.")]
    [SwaggerSchema("National ID / Personal ID number (e.g., Yemen national ID)")]
    public string? NationalId { get; set; }

    [SwaggerSchema("Vehicle type: Unknown, Motorcycle, Car, Pickup, Truck, Bus, Tractor, Bicycle")]
    public VehicleType? VehicleType { get; set; }

    [MaxLength(50, ErrorMessage = "Vehicle registration number must not exceed 50 characters.")]
    [SwaggerSchema("Vehicle registration number issued by government (if any)")]
    public string? VehicleRegistrationNumber { get; set; }

    [Required(ErrorMessage = "Residency status is required.")]
    [SwaggerSchema("Residency status: Resident or Displaced")]
    public ResidencyStatus ResidencyStatus { get; set; }

    [SwaggerSchema("Does the person suffer from chronic diseases?")]
    public bool? HasChronicDiseases { get; set; }

    [MaxLength(250, ErrorMessage = "Disease notes must not exceed 250 characters.")]
    [SwaggerSchema("Notes about chronic diseases (optional)")]
    public string? ChronicDiseasesNotes { get; set; }
}
