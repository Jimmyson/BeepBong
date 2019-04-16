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
			RuleFor(t => t.Description).NoURLInString();

			RuleFor(t => t.Samples).Empty().When(t => t.Programme != null && t.Programme.IsLibraryMusic == true);
		}
	}
}