using SmartNeighborhoodAPI.Entites.Enums;

namespace SmartNeighborhoodAPI.Helpers.DTOs.Project
{
    public class ProjectDto
    {
        [Required(ErrorMessage = "اسم المشروع مطلوب")]
        [MaxLength(40, ErrorMessage = "الحد الأقصى للاسم هو 40 حرفًا")]
        public string Name { get; set; }

        [Required(ErrorMessage = "الوصف مطلوب")]
        public string? Description { get; set; }

        [Required(ErrorMessage = "رقم المدير مطلوب")]
        public int ManagerId { get; set; }

        [Required(ErrorMessage = "رقم فئة المشروع مطلوب")]
        public int ProjectCatgoryId { get; set; }

        [Required(ErrorMessage = "تاريخ البدء مطلوب")]
        public DateTime StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        [Required(ErrorMessage = "حالة المشروع مطلوبة")]
        public ProjectStatus ProjectStatus { get; set; }

        public double? Budget { get; set; }

        [Required(ErrorMessage = "أولوية المشروع مطلوبة")]
        public ProjectPriority ProjectPriority { get; set; }
    }
}
