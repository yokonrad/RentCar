using System.ComponentModel.DataAnnotations;

namespace Core.ValidationAttributes
{
    public class DateFromToValidator : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var model = (Entities.Reservation)validationContext.ObjectInstance;

            DateTime DateTo = Convert.ToDateTime(model.DateTo);
            DateTime DateFrom = Convert.ToDateTime(value);

            if (DateFrom >= DateTo)
            {
                return new ValidationResult("The Date To cannot be equal or lower than Date From.");
            }
            else
            {
                return ValidationResult.Success;
            }
        }
    }
}
