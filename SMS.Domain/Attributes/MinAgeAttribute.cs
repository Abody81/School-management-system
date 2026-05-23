#pragma warning disable

using System.ComponentModel.DataAnnotations;

namespace SMS.Domain.Attributes
{
    public class MinAge : ValidationAttribute
    {
        private readonly int _minAge;

        public MinAge(int minAge)
        {
            _minAge = minAge;
            ErrorMessage = $"Minimum age allowed is {_minAge} years.";
        }

        protected override ValidationResult? IsValid(object value, ValidationContext validationContext)
        {
            if (value is DateTime dateOfBirth)
            { 
                var today = DateTime.Today;
                var age = today.Year - dateOfBirth.Year;

                if (dateOfBirth.Date > today.AddYears(-age))
                    age--;

                if (age >= _minAge)
                    return ValidationResult.Success;

                return new ValidationResult(ErrorMessage);
            }

            return ValidationResult.Success;
        }
    }
}