using BeepBong.Application.ViewModels;
using FluentValidation;

namespace BeepBong.Application.Commands.Validation
{
    public class ProgrammeEditValidator : AbstractValidator<ProgrammeEditViewModel>
    {
        public ProgrammeEditValidator()
        {
            RuleFor(p => p.Name).NotNull().NotEmpty().NoURLInString();
            RuleFor(p => p.LogoLocation).NoURLInString();

            // RuleFor(p => p.AirDate).NotNull()
            //                         .GreaterThanOrEqualTo(p => p.Channel.Commencement)
            //                         .When(p => p.Channel != null && p.Channel.Commencement != null)
            //                         .LessThan(p => p.Channel.Closed)
            //                         .When(p => p.Channel != null && p.Channel.Closed != null);
        }
    }
}