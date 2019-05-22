using BeepBong.Application.ViewModels;
using FluentValidation;

namespace BeepBong.Application.Commands.Validation
{
    public class TrackListEditValidator : AbstractValidator<TrackListEditViewModel>
    {
        public TrackListEditValidator()
        {
            RuleFor(p => p.Name).NotNull().NotEmpty().NoURLInString();
            RuleFor(p => p.Composer).NotNull().NoURLInString();
        }
    }
}