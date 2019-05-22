using BeepBong.Application.ViewModels;
using FluentValidation;

namespace BeepBong.Application.Commands.Validation
{
    public class SampleEditValidator : AbstractValidator<SampleEditViewModel>
    {
        public SampleEditValidator()
        {
            RuleFor(s => s.Notes).NoURLInString();
        }
    }
}