using System;
using System.Collections.Generic;

namespace BeepBong.Models
{
	public class Track
	{
		public Track()
		{
			//Tags = new HashSet<Tag>();
			Samples = new List<Sample>();
		}

		public Guid TrackId { get; set; }
		public string Name { get; set; }
		public string Subtitle { get; set; }

		//public ICollection<Tag> Tags { get; set; }
		public ICollection<Sample> Samples { get; set; }

		public Guid ProgrammeId { get; set; }
		public Programme Programme { get; set; }
	}
}