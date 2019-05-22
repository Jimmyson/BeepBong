using BeepBong.Application.ViewModels;
using FluentValidation;

namespace BeepBong.Application.Commands.Validation
{
    public class TrackValidator : AbstractValidator<TrackEditViewModel>
    {
        public TrackValidator()
        {
            RuleFor(t => t.Name).NotNull().NotEmpty().NoURLInString();
            RuleFor(t => t.Variant).NoURLInString();
            RuleFor(t => t.Description).NoURLInString();
        }
    }
}