using System.Text.Json;
using System.Text.RegularExpressions;
using ValidationContext = System.ComponentModel.DataAnnotations.ValidationContext;


namespace SmartNeighborhoodAPI.Helpers
{
    public class PasswordValidationAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var password = value as string;
            if (string.IsNullOrEmpty(password))
            {
                return new ValidationResult("Password is required.");
            }

            var errors = new List<object>();

            if (!password.Any(char.IsUpper))
            {
                errors.Add(new ErrorDetails("Password", "Passwords must have at least one uppercase ('A'-'Z')."));
            }
            if (!password.Any(char.IsDigit))
            {
                errors.Add(new ErrorDetails("Password", "Passwords must have at least one digit ('0'-'9')."));
            }
            if (!Regex.IsMatch(password, @"[^a-zA-Z0-9]"))
            {
                errors.Add(new ErrorDetails("Password", "Passwords must have at least one non-alphanumeric character."));
            }

            if (errors.Any())
            {
                return new ValidationResult(JsonSerializer.Serialize(errors));
            }

            return ValidationResult.Success;
        }
    }

}
