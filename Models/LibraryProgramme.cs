using System;

namespace BeepBong.Models
{
	public class LibraryProgramme
	{
		public Guid ProgrammeId { get; set; }
		public Programme Programme { get; set; }

		public Guid LibraryId { get; set; }
		public Library Library { get; set; }
	}
}