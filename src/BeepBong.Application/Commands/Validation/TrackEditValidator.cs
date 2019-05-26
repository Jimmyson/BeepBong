using BeepBong.Application.ViewModels;
using FluentValidation;

namespace BeepBong.Application.Commands.Validation
{
    public class TrackEditValidator : AbstractValidator<TrackEditViewModel>
    {
        public TrackEditValidator()
        {
            RuleFor(t => t.Name).NotNull().NotEmpty().NoURLInString();
            RuleFor(t => t.Variant).NoURLInString();
            RuleFor(t => t.Description).NoURLInString();
        }
    }
}