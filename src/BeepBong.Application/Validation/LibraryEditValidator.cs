using BeepBong.Domain.Models;
using FluentValidation;

namespace BeepBong.Application.Validation
{
    public class LibraryEditValidator : AbstractValidator<Library>
    {
        public LibraryEditValidator()
        {
            RuleFor(l => l.AlbumName).NotNull().NotEmpty().NoURLInString();
            RuleFor(l => l.Label).NoURLInString();
            RuleFor(l => l.Catalog).NoURLInString();
            RuleFor(l => l.MBID).NoURLInString();
        }
    }
}