using System;
using System.Collections.Generic;

namespace BeepBong.Domain.Models
{
	public class Programme
	{
		public Programme()
		{
			Tracks = new List<Track>();
			LibraryProgrammes = new List<LibraryProgramme>();
		}

		public Guid ProgrammeId { get; set; }
		public string Name { get; set; }
		public string Year { get; set; }
		public string Channel { get; set; }
		public string AudioComposer { get; set; }
		
		public string Logo { get; set; }
		public bool IsLibraryMusic { get; set; }

		public ICollection<Track> Tracks { get; set; }
		public ICollection<LibraryProgramme> LibraryProgrammes { get; set; }
	}
}