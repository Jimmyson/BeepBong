namespace BeepBong.Models
{
	public class Sample
	{
		public int SampleId { get; set; }
		public string Duration { get; set; }
		public string SampleRate { get; set; }
		public int Channels { get; set; }
		public int BitRate { get; set; }
		public CompressionEnum? Compression { get; set; }
		public string Checksum { get; set; }

		public int TrackId { get; set; }
		public Track Track { get; set; }
	}

	public enum CompressionEnum
	{
		Uncompressed,
		Compressed
	}
}