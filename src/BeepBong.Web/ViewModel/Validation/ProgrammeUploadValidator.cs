using FluentValidation;
using BeepBong.Application.Validation;

namespace BeepBong.Web.ViewModel.Validation
{
    public class ProgrammeUploadValidator : AbstractValidator<ProgrammeUploadViewModel>
    {
        public ProgrammeUploadValidator()
        {
            RuleFor(b => b.Name).NotNull().NotEmpty().NoURLInString();
        }
    }
}