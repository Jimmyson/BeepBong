using System;
using BeepBong.Domain.Models;
using Microsoft.AspNetCore.Http;

namespace BeepBong.Web.ViewModels
{
    public class SampleViewModel
    {
        public Guid SampleId { get; set; }
		public string Duration { get; set; }

		public int SampleRate { get; set; }
		public int SampleCount { get; set; }
		public int Channels { get; set; }
		public int BitRate { get; set; }
		public BitRateModeEnum BitRateMode { get; set; }
		public int BitDepth { get; set; }
		public string Codec { get; set; }
		public CompressionEnum Compression { get; set; }
		//public string Checksum { get; set; }
		public string Notes { get; set; }

		public string WaveformImage { get; set; }
		public string SpecImage { get; set; }

		public Guid TrackId { get; set; }
        public string TrackName { get; set; }
    }
}