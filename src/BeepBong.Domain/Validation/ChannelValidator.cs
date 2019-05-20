using BeepBong.Domain.Models;
using FluentValidation;

namespace BeepBong.Domain.Validation
{
    public class ChannelValidator : AbstractValidator<Channel>
    {
        public ChannelValidator()
        {
            RuleFor(c => c.Name).NotNull().NotEmpty().NoURLInString();
            RuleFor(c => c.Commencement).NotNull().NotEmpty().LessThan(c => c.Closed);
            RuleFor(c => c.Closed).NotNull().GreaterThan(c => c.Commencement);
        }
    }
}