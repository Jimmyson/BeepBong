using BeepBong.Application.ViewModels;
using FluentValidation;

namespace BeepBong.Application.Commands.Validation
{
    public class ChannelEditValidator : AbstractValidator<ChannelEditViewModel>
    {
        public ChannelEditValidator()
        {
            RuleFor(c => c.Name).NotNull().NotEmpty().NoURLInString();
            RuleFor(c => c.Commencement).NotNull().NotEmpty().LessThan(c => c.Closed);
            RuleFor(c => c.Closed).GreaterThan(c => c.Commencement);
        }
    }
}