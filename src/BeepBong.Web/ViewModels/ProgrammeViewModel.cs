using System;
using System.Collections.Generic;

namespace BeepBong.Web.ViewModels
{
	public class ProgrammeViewModel
	{
		public ProgrammeViewModel()
		{
			Tracks = new List<TrackViewModel>();
		}

		public Guid ProgrammeId { get; set; }
		public string Name { get; set; }
		public string Year { get; set; }
		public string Channel { get; set; }
		public string AudioComposer { get; set; }
		public bool IsLibraryMusic { get; set; }

		public ICollection<TrackViewModel> Tracks { get; set; }
	}
}