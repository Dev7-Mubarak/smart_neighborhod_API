namespace SmartNeighborhoodAPI.Helpers.Attrbuites
{
    public class MaxFileSizeAttribute : ValidationAttribute
    {
        private readonly int _maxFileSize;

        public MaxFileSizeAttribute(int maxFileSize)
        {
            _maxFileSize = maxFileSize;
        }

        protected override ValidationResult? IsValid(object? value, System.ComponentModel.DataAnnotations.ValidationContext validationContext)
        {
            var file = value as IFormFile;

            if (file is not null)
            {
                if (file.Length > _maxFileSize)
                {
                    return new ValidationResult($"الحد الأقصى المسموح به لحجم الملف هو {_maxFileSize / 1024 / 1024} ميغابايت");
                }
            }

            return ValidationResult.Success;
        }
    }
}
