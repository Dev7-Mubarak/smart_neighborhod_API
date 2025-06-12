namespace SmartNeighborhoodAPI.Entites.Enums
{
    public enum ProjectStatus
    {
        [Display(Name = "تحت التخطيط")]
        Planned,
        [Display(Name = "قيد التنفيذ")]
        InProgress,
        [Display(Name = "مكتمل")]
        Completed,
        [Display(Name = "ملغى")]
        Cancelled
    }
}
