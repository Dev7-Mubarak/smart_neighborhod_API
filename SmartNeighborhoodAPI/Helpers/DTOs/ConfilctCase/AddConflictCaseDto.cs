namespace SmartNeighborhoodAPI.Helpers.DTOs.ConfilctCase
{
    using System.ComponentModel.DataAnnotations;
    using Microsoft.AspNetCore.Http;

    public class AddConflictCaseDto
    {
        [Required(ErrorMessage = "نوع النزاع مطلوب")]
        public int ConflictTypeId { get; set; }

        [Required(ErrorMessage = "رقم المدير مطلوب")]
        public string ManagerId { get; set; }

        [Required(ErrorMessage = "رقم الطرف الأول مطلوب")]
        public int FirstPartyId { get; set; }

        [Required(ErrorMessage = "رقم الطرف الثاني مطلوب")]
        public int SecondPartyId { get; set; }

        [StringLength(1000, ErrorMessage = "الملاحظات يجب ألا تتجاوز 1000 حرف")]
        public string Notes { get; set; }

        [Required(ErrorMessage = "نتيجة الجلسة مطلوبة")]

        public IFormFile? Image { get; set; }

        [Required(ErrorMessage = "تاريخ الجلسة مطلوب")]
        public DateTime SessionDate { get; set; }

        public bool IsResolved { get; set; }
    }

}
