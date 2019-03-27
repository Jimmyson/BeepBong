using BeepBong.Domain.Models;
using FluentValidation;

namespace BeepBong.Domain.Validation
{
	public class LibraryValidator : AbstractValidator<Library>
	{
		public LibraryValidator()
		{
			RuleFor(c => c.AlbumName).NotNull().NotEmpty().NoURLInString();
			RuleFor(c => c.Label).NoURLInString();
			RuleFor(c => c.Catalog).NoURLInString();
			RuleFor(c => c.MBID).NoURLInString();
		}
	}
}