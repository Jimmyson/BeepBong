using BeepBong.Application.ViewModels;
using FluentValidation;

namespace BeepBong.Application.Commands.Validation
{
    public class SampleCreateValidator : AbstractValidator<SampleCreateViewModel>
    {
        public SampleCreateValidator()
        {
            RuleFor(s => s.SampleRate).NotNull().NotEmpty();
            RuleFor(s => s.SampleCount).NotNull().NotEmpty();
            RuleFor(s => s.AudioChannelCount).NotNull().NotEmpty();
            RuleFor(s => s.BitRate).NotNull().NotEmpty();
            RuleFor(s => s.BitDepth).NotNull().NotEmpty();

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