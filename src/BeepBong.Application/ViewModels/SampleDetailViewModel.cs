using System;

namespace BeepBong.Application.ViewModels
{
    public class SampleDetailViewModel
    {
        public Guid SampleId { get; set; }
        public string Duration { get; set; }
        public string SampleRate { get; set; }
        public string SampleCount { get; set; }
        public int AudioChannelCount { get; set; }
        public string BitRate { get; set; }
        public string BitRateMode { get; set; }
        public string BitDepth { get; set; }
        public string Codec { get; set; }
        public string Compression { get; set; }
        public string Fingerprint { get; set; }
        public string OtherAttributes { get; set; }
        public string Notes { get; set; }
        public string Waveform { get; set; }
        public string Spectrograph { get; set; }
        public Guid TrackId { get; set; }
    }
}