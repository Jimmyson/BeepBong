using System;

namespace BeepBong.Domain.Models
{
    public class ProgrammeTrackList
    {
        public Guid ProgrammeId { get; set; }
        public Programme Programme { get; set; }
        
        public Guid TrackListId { get; set; }
        public TrackList TrackList { get; set; }
    }
}