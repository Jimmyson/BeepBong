using System.Collections.Generic;

namespace BeepBong.ViewModels
{
	public class ProgrammeViewModel
	{
		public ProgrammeViewModel()
		{
			Tracks = new List<TrackViewModel>();
		}

		public int ProgrammeId { get; set; }
		public string Name { get; set; }
		public string Year { get; set; }
		public string Channel { get; set; }
		public string AudioComposer { get; set; }

		public ICollection<TrackViewModel> Tracks { get; set; }
	}
}