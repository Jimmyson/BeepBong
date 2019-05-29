using BeepBong.Domain.Models;
using FluentValidation;
using FluentValidation.Validators;

namespace BeepBong.Application.Validation
{
    public class NoURLValidator : PropertyValidator
    {
        public NoURLValidator() : base("The value contains a URL")
        {
            
        }

        protected override bool IsValid(PropertyValidatorContext context)
        {
            if (context.PropertyValue == null) return true;
            string value = context.PropertyValue as string;

            return !value.Contains("http");
        }
    }
}