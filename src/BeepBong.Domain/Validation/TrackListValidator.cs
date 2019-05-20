using BeepBong.Domain.Models;
using FluentValidation;

namespace BeepBong.Domain.Validation
{
    public class TrackListValidator : AbstractValidator<TrackList>
    {
        public TrackListValidator()
        {
            RuleFor(p => p.Name).NotNull().NotEmpty().NoURLInString();
            RuleFor(p => p.Composer).NotNull().NoURLInString();
        }
    }
}