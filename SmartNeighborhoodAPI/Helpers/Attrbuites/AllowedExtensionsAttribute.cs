namespace SmartNeighborhoodAPI.Helpers.Attrbuites
{
    public class AllowedExtensionsAttribute : ValidationAttribute
    {
        private readonly string _allowedExtensions;

        public AllowedExtensionsAttribute(string allowedExtensions)
        {
            _allowedExtensions = allowedExtensions;
        }

        protected override ValidationResult? IsValid(object? value, System.ComponentModel.DataAnnotations.ValidationContext validationContext)
        {
            var file = value as IFormFile;

            if (file is not null)
            {
                var extension = Path.GetExtension(file.FileName);

                var IsAllowed = _allowedExtensions.Split(',').Contains(extension, StringComparer.OrdinalIgnoreCase);

                if (!IsAllowed)
                {
                    return new ValidationResult($"فقط الامتدادات التالية مسموح بها: {_allowedExtensions}!");
                }
            }

            return ValidationResult.Success;
        }
    }
}
