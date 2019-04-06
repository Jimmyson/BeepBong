using BeepBong.Domain.Models;
using FluentValidation;

namespace BeepBong.Domain.Validation
{
	public class SampleValidator : AbstractValidator<Sample>
	{
		public SampleValidator()
		{
			//RuleFor(s => s.Duration).NotNull().NotEmpty();

			RuleFor(s => s.SampleRate).NotNull().NotEmpty().GreaterThan(0);
			RuleFor(s => s.SampleCount).NotNull().NotEmpty().GreaterThan(0);
			RuleFor(s => s.Channels).NotNull().NotEmpty().GreaterThan(0);
			RuleFor(s => s.BitRate).NotNull().NotEmpty().GreaterThan(0);

			RuleFor(s => s.BitRateMode).NotNull().IsInEnum();
			RuleFor(s => s.Compression).NotNull().IsInEnum();

			RuleFor(s => s.Codec).NotNull().NotEmpty().NoURLInString();
			RuleFor(s => s.Notes).NoURLInString();
		}
	}
}