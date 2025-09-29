using SmartNeighborhoodAPI.Entites.Enums;
using SmartNeighborhoodAPI.Helpers.Attrbuites;

public class CreatePersonDto
{
    [Required(ErrorMessage = "ÇáÇÓã ÇáÃæá ãØáæÈ.")]
    [MaxLength(20, ErrorMessage = "íÌÈ ÃáÇ íÒíÏ ÇáÇÓã ÇáÃæá Úä 20 ÍÑİğÇ.")]
    public string FirstName { get; set; }

    [Required(ErrorMessage = "ÇáÇÓã ÇáËÇäí ãØáæÈ.")]
    [MaxLength(20, ErrorMessage = "íÌÈ ÃáÇ íÒíÏ ÇáÇÓã ÇáËÇäí Úä 20 ÍÑİğÇ.")]
    public string SecondName { get; set; }

    [Required(ErrorMessage = "ÇáÇÓã ÇáËÇáË ãØáæÈ.")]
    [MaxLength(20, ErrorMessage = "íÌÈ ÃáÇ íÒíÏ ÇáÇÓã ÇáËÇáË Úä 20 ÍÑİğÇ.")]
    public string ThirdName { get; set; }

    [Required(ErrorMessage = "ÇÓã ÇáÚÇÆáÉ ãØáæÈ.")]
    [MaxLength(20, ErrorMessage = "íÌÈ ÃáÇ íÒíÏ ÇÓã ÇáÚÇÆáÉ Úä 20 ÍÑİğÇ.")]
    public string LastName { get; set; }

    [Required(ErrorMessage = "ÑŞã ÇáåÇÊİ ãØáæÈ.")]
    [MaxLength(30, ErrorMessage = "íÌÈ ÃáÇ íÒíÏ ÑŞã ÇáåÇÊİ Úä 30 ÑŞãğÇ.")]
    [Phone(ErrorMessage = "ÕíÛÉ ÑŞã ÇáåÇÊİ ÛíÑ ÕÍíÍÉ.")]
    public string PhoneNumber { get; set; }

    [Required(ErrorMessage = "ÍŞá æÇÊÓÇÈ ãØáæÈ.")]
    public bool IsWhatsapp { get; set; }

    [Required(ErrorMessage = "ÍŞá ÑŞã ááÊæÇÕá ãØáæÈ.")]
    public bool IsContactNumber { get; set; }

    [Required(ErrorMessage = "ÊÇÑíÎ ÇáãíáÇÏ ãØáæÈ.")]
    [DataType(DataType.Date, ErrorMessage = "ÕíÛÉ ÇáÊÇÑíÎ ÛíÑ ÕÍíÍÉ.")]
    public DateTime DateOfBirth { get; set; }

    [Required(ErrorMessage = "ÇáÌäÓ ãØáæÈ.")]
    public Gender Gender { get; set; }

    [AllowedExtensions(FileHelper.AllowedExtensions)]
    [MaxFileSize(FileHelper.MaxFileSizeInBytes)]
    public IFormFile? Image { get; set; }

    [Required(ErrorMessage = "İÕíáÉ ÇáÏã ãØáæÈÉ.")]
    public BloodType BloodType { get; set; }

    [Required(ErrorMessage = "ÇáÍÇáÉ ÇáÇÌÊãÇÚíÉ ãØáæÈÉ.")]
    public MaritalStatus MaritalStatus { get; set; }

    [Required(ErrorMessage = "ÍÇáÉ ÇáÚãá ãØáæÈÉ.")]
    public OccupationStatus OccupationStatus { get; set; }

    [MaxLength(30, ErrorMessage = "íÌÈ ÃáÇ íÒíÏ ÇÓã ÇáæÙíİÉ Úä 30 ÍÑİğÇ.")]
    public string? Job { get; set; }

}
