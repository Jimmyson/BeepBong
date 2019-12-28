using BeepBong.Application.ViewModels;
using FluentValidation;

namespace BeepBong.Application.Validation
{
    public class ChannelEditValidator : AbstractValidator<ChannelEditViewModel>
    {
        public ChannelEditValidator()
        {
            RuleFor(c => c.Name).NotNull().NotEmpty().NoURLInString();
            RuleFor(c => c.Opened).LessThan(c => c.Closed).When(c => c.Closed != null);
            RuleFor(c => c.Closed).GreaterThan(c => c.Opened);
        }
    }
}