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
            RuleFor(s => s.AudioChannelCount).NotNull().NotEmpty().GreaterThan(0);
            RuleFor(s => s.BitRate).NotNull().NotEmpty().GreaterThan(0);
            RuleFor(s => s.BitDepth).NotNull().NotEmpty().GreaterThan(0);

            RuleFor(s => s.BitRateMode).NotNull().IsInEnum();
            RuleFor(s => s.Compression).NotNull().IsInEnum();

            RuleFor(s => s.Fingerprint).NotNull().NoURLInString();
            RuleFor(s => s.Codec).NotNull().NotEmpty().NoURLInString();
            RuleFor(s => s.OtherAttributes).NoURLInString();
            RuleFor(s => s.Notes).NoURLInString();

            RuleFor(s => s.Waveform).NoURLInString();
            RuleFor(s => s.Spectrograph).NoURLInString();
        }
    }
}