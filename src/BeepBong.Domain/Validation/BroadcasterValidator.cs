using BeepBong.Domain.Models;
using FluentValidation;

namespace BeepBong.Domain.Validation
{
    public class BroadcasterValidator : AbstractValidator<Broadcaster>
    {
        public BroadcasterValidator()
        {
            RuleFor(p => p.Name).NotNull().NotEmpty().NoURLInString();
            RuleFor(p => p.Country).NotNull().NotEmpty().NoURLInString();
        }
    }
}