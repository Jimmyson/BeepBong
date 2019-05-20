using System;
using System.Collections.Generic;

namespace BeepBong.Domain.Models
{
    public class Library
    {
        public Guid LibraryId { get; set; }
        public string AlbumName { get; set; }
        public string Label { get; set; }
        public string Catalog { get; set; }
        public string MBID { get; set; }
    }
}