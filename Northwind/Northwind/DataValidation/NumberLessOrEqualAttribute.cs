using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Northwind.DataValidation
{
    public class NumberLessOrEqualAttribute : ValidationAttribute
    {
        private readonly string secondProperty;

        public NumberLessOrEqualAttribute(string secondProperty)
        {
            this.secondProperty = secondProperty;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var currentValue = (short?)value;

            var property = validationContext.ObjectType.GetProperty(secondProperty);

            if (property == null)
            {
                throw new ArgumentException($"Property {secondProperty} was not found");
            }


            var secondValue = (short?)property.GetValue(validationContext.ObjectInstance);

            if (!secondValue.HasValue || currentValue > secondValue)
            {
                return new ValidationResult($"{validationContext.DisplayName} must be less or equal to {property.Name}");
            }

            return ValidationResult.Success;
        }
    }
}
