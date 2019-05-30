using BeepBong.Application.ViewModels;
using FluentValidation;

namespace BeepBong.Application.Validation
{
    public class ProgrammeEditValidator : AbstractValidator<ProgrammeEditViewModel>
    {
        public ProgrammeEditValidator()
        {
            RuleFor(p => p.Name).NotNull().NotEmpty().NoURLInString();
        }
    }
}