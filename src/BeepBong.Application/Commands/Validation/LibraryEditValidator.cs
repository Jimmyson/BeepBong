using BeepBong.Domain.Models;
using FluentValidation;

namespace BeepBong.Application.Commands.Validation
{
    public class LibraryEditValidator : AbstractValidator<Library>
    {
        public LibraryEditValidator()
        {
            RuleFor(c => c.AlbumName).NotNull().NotEmpty().NoURLInString();
            RuleFor(c => c.Label).NoURLInString();
            RuleFor(c => c.Catalog).NoURLInString();
            RuleFor(c => c.MBID).NoURLInString();
        }
    }
}