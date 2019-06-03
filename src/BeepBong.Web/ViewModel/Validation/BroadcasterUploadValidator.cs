using FluentValidation;
using BeepBong.Application.Validation;

namespace BeepBong.Web.ViewModel.Validation
{
    public class BroadcasterUploadValidator : AbstractValidator<BroadcasterUploadViewModel>
    {
        public BroadcasterUploadValidator()
        {
            RuleFor(b => b.Name).NotNull().NotEmpty().NoURLInString();
            RuleFor(b => b.Country).NoURLInString();
        }
    }
}