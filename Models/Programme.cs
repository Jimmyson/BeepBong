using System.Collections.Generic;

namespace BeepBong.Models
{
	public class Programme
	{
		public Programme()
		{
			Tracks = new List<Track>();
		}

		public int ProgrammeId { get; set; }
		public string Name { get; set; }
		public string Year { get; set; }
		public string Channel { get; set; }
		public string AudioComposer { get; set; }

		public ICollection<Track> Tracks { get; set; }
	}
}