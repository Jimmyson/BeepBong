using System;
using System.Collections.Generic;

namespace BeepBong.Domain.Models
{
    public class TrackList
    {
        public TrackList()
        {
           Tracks = new List<Track>();
           ProgrammeTrackLists = new List<ProgrammeTrackList>();
        }

        public Guid TrackListId { get; set; }
        public string Name { get; set; }
        public bool Library { get; set; }
        public string Composer { get; set; }

        public ICollection<Track> Tracks { get; set; }
        public ICollection<ProgrammeTrackList> ProgrammeTrackLists { get; set; }
    }
}