using System;

namespace BeepBong.Web.ViewModels
{
	public class TrackViewModel
	{
		public Guid TrackId { get; set; }
		public string Name { get; set; }
		public string Subtitle { get; set; }
		public string Description { get; set; }
		public int SampleCount { get; set; }
	}
}