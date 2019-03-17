using System;

namespace BeepBong.Models
{
	public class Sample
	{
		public Guid SampleId { get; set; }
		public TimeSpan Duration
		{
			get
			{
				return TimeSpan.FromSeconds(SampleCount / SampleRate);
			} 
		}

		public int SampleRate { get; set; }
		public int SampleCount { get; set; }
		public int Channels { get; set; }
		public int BitRate { get; set; }
		public BitRateModeEnum? BitRateMode { get; set; }
		public string Codec { get; set; }
		public CompressionEnum? Compression { get; set; }
		public string Checksum { get; set; }
		public string Notes { get; set; }

		public Guid TrackId { get; set; }
		public Track Track { get; set; }
	}

	public enum CompressionEnum
	{
		Lossless,
		Lossy
	}

	public enum BitRateModeEnum
	{
		CBR,
		VBR
	}
}