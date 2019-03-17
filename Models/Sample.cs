using System;

namespace BeepBong.Models
{
	public class Sample
	{
		public Guid SampleId { get; set; }
		public string Duration { get; set; }
		public string SampleRate { get; set; }
		public int Channels { get; set; }
		public int BitRate { get; set; }
		public string Codec { get; set; }
		public CompressionEnum? Compression { get; set; }
		public string Checksum { get; set; }

		public Guid TrackId { get; set; }
		public Track Track { get; set; }
	}

	public enum CompressionEnum
	{
		Uncompressed,
		Compressed
	}
}