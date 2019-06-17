using System;
using System.Collections.Generic;

namespace BeepBong.Application.ViewModels
{
    public class TrackDetailViewModel
    {
        public Guid TrackId { get; set; }
        public string Name { get; set; }
        public string Variant { get; set; }
        public string Description { get; set; }
        public Guid TrackListId { get; set; }
        public List<SimpleSample> Samples { get; set; }
    }

    public class SimpleSample
    {
        public Guid SampleId { get; set; }
        public string Duration { get; set; }
        public string SampleRate { get; set; }
        public int AudioChannelCount { get; set; }
        public string BitRate { get; set; }
        public string BitRateMode { get; set; }
        public string BitDepth { get; set; }
        public string Codec { get; set; }
        public string Compression { get; set; }
        public string FingerprintShort { get; set; }
        public string OtherAttributes { get; set; }
    }
}