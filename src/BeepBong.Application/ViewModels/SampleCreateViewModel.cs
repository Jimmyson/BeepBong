using System;
using BeepBong.Domain.Models;

namespace BeepBong.Application.ViewModels
{
    public class SampleCreateViewModel
    {
        public string SampleRate { get; set; }
        public string SampleCount { get; set; }
        public string AudioChannelCount { get; set; }
        public string BitRate { get; set; }
        public BitRateModeEnum BitRateMode { get; set; }
        public string BitDepth { get; set; }
        public string Codec { get; set; }
        public CompressionEnum Compression { get; set; }
        public string Fingerprint { get; set; }
        public string OtherAttributes { get; set; }
        public string Notes { get; set; }
        public string Waveform { get; set; }
        public string Spectrograph { get; set; }
        public Guid TrackId { get; set; }
    }
}