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

			RuleFor(s => s.BitRateMode).NotNull().NotEmpty().IsInEnum();
			RuleFor(s => s.Compression).NotNull().NotEmpty().IsInEnum();

			RuleFor(s => s.Codec).NotNull().NotEmpty();
			RuleFor(s => s.Notes).NotNull().NotEmpty();
		}
	}
}