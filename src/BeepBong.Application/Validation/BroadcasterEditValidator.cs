using BeepBong.Application.ViewModels;
using FluentValidation;

namespace BeepBong.Application.Validation
{
    public class BroadcasterEditValidator : AbstractValidator<BroadcasterEditViewModel>
    {
        public BroadcasterEditValidator()
        {
            RuleFor(b => b.Name).NotNull().NotEmpty().NoURLInString();
            RuleFor(b => b.Country).NoURLInString();
        }
    }
}