using System;

namespace BeepBong.Domain.Models
{
	public class Sample
	{
		public Guid SampleId { get; set; }
		public string Duration { get; set; }

		public int SampleRate { get; set; }
		public int SampleCount { get; set; }
		public int Channels { get; set; }
		public int BitRate { get; set; }
		public BitRateModeEnum BitRateMode { get; set; }
		public string Codec { get; set; }
		public CompressionEnum Compression { get; set; }
		//public string Checksum { get; set; }
		public string Notes { get; set; }

		public string Waveform { get; set; }
		public string Spectrograph { get; set; }

		public Guid TrackId { get; set; }
		public Track Track { get; set; }
	}

	public enum CompressionEnum
	{
		None,
		Lossless,
		Lossy
	}

	public enum BitRateModeEnum
	{
		CBR,
		VBR
	}
}