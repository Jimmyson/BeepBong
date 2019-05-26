using System;
using System.Collections.Generic;

namespace BeepBong.Domain.Models
{
    public class Track
    {
        public Track()
        {
            Samples = new List<Sample>();
        }

        public Guid TrackId { get; set; }
        public string Name { get; set; }
        public string Variant { get; set; }
        public string Description { get; set; }

        public ICollection<Sample> Samples { get; set; }

        public Guid TrackListId { get; set; }
        public TrackList TrackList { get; set; }
    }
}