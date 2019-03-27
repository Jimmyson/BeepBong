using BeepBong.Domain.Models;
using FluentValidation;

namespace BeepBong.Domain.Validation
{
	public class TrackValidator : AbstractValidator<Track>
	{
		public TrackValidator()
		{
			RuleFor(t => t.Name).NotNull().NotEmpty().NoURLInString();
			RuleFor(t => t.Subtitle).NoURLInString();

			RuleFor(t => t.Samples).Empty().When(t => t.Programme.IsLibraryMusic == true);
		}
	}
}