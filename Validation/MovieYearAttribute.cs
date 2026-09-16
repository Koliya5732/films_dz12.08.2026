using System.ComponentModel.DataAnnotations;

namespace films_dz12._08._2026.Validation
{
    public class MovieYearAttribute : ValidationAttribute
    {
        public MovieYearAttribute()
        {
            ErrorMessage = "Год фильма должен быть от 1888 до текущего года.";
        }

        protected override ValidationResult? IsValid(
            object? value,
            ValidationContext validationContext)
        {
            if (value == null)
            {
                return ValidationResult.Success;
            }

            int year = Convert.ToInt32(value);

            if (year < 1888 || year > DateTime.Now.Year)
            {
                return new ValidationResult(ErrorMessage);
            }

            return ValidationResult.Success;
        }
    }
}