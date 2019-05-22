using BeepBong.Application.ViewModels;
using FluentValidation;

namespace BeepBong.Application.Commands.Validation
{
    public class BroadcasterEditValidator : AbstractValidator<BroadcasterEditViewModel>
    {
        public BroadcasterEditValidator()
        {
            RuleFor(p => p.Name).NotNull().NotEmpty().NoURLInString();
            RuleFor(p => p.Country).NoURLInString();
        }
    }
}