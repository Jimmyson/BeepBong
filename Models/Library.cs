using System;
using System.Collections.Generic;

namespace BeepBong.Models
{
	public class Library
	{
		public Library()
		{
			LibraryProgrammes = new List<LibraryProgramme>();
		}

		public Guid LibraryId { get; set; }
		public string AlbumName { get; set; }
		public string Label { get; set; }
		public string Catalog { get; set; }
		public string MBID { get; set; }
		
		public ICollection<LibraryProgramme> LibraryProgrammes { get; set; }
	}
}