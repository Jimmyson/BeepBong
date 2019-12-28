using System;

namespace BeepBong.Application.ViewModels.Report
{
    [Obsolete("Use Tracklist Index View Model")]
    public class OrphanedTrackListViewModel
    {
        public Guid TrackListId { get; set; }
        public string Name { get; set; }
        public string Composer { get; set; }
        public bool Library { get; set; }
        public int TrackCount { get; set; }
    }
}