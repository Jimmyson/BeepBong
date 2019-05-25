using System;

namespace BeepBong.Application.ViewModels
{
    public class TrackListIndexViewModel
    {
        public Guid TrackListId { get; set; }
        public string Name { get; set; }
        public string Composer { get; set; }
        public bool Library { get; set; }
        public int TrackCount { get; set; }
    }
}