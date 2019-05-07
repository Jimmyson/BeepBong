using BeepBong.Domain.Models;
using FluentValidation;

namespace BeepBong.Domain.Validation
{
    public class ProgrammeValidator : AbstractValidator<Programme>
    {
        public ProgrammeValidator()
        {
            RuleFor(p => p.Name).NotNull().NotEmpty().NoURLInString();
            RuleFor(p => p.Year).NotNull().Length(4).Matches(@"^\d{4}$");
            //RuleFor(p => p.IsLibraryMusic).NotNull().NotEmpty();
        }
    }
}