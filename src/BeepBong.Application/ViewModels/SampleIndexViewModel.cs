using System;

namespace BeepBong.Application.ViewModels
{
    public class SampleIndexViewModel
    {
        public Guid SampleId { get; set; }
        public string Duration { get; set; }
        public string SampleRate { get; set; }
        public string BitDepth { get; set; }
        public string Codec { get; set; }
        public string Compression { get; set; }
        public string Fingerprint { get; set; }
    }
}