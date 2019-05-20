using BeepBong.Domain.Models;
using FluentValidation;

namespace BeepBong.Domain.Validation
{
    public class ProgrammeValidator : AbstractValidator<Programme>
    {
        public ProgrammeValidator()
        {
            RuleFor(p => p.Name).NotNull().NotEmpty().NoURLInString();

            RuleFor(p => p.AirDate).NotNull()
                                    .GreaterThanOrEqualTo(p => p.Channel.Commencement)
                                    .When(p => p.Channel != null && p.Channel.Commencement != null)
                                    .LessThan(p => p.Channel.Closed)
                                    .When(p => p.Channel != null && p.Channel.Closed != null);
        }
    }
}