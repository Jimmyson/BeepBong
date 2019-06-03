using BeepBong.Application.ViewModels;
using FluentValidation;

namespace BeepBong.Application.Validation
{
    public class TrackListEditValidator : AbstractValidator<TrackListEditViewModel>
    {
        public TrackListEditValidator()
        {
            RuleFor(tl => tl.Name).NotNull().NotEmpty().NoURLInString();
            RuleFor(tl => tl.Composer).NoURLInString();
        }
    }
}