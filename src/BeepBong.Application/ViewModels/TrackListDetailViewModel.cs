using System;
using System.Collections.Generic;

namespace BeepBong.Application.ViewModels
{
    public class TrackListDetailViewModel
    {
        public Guid TrackListId { get; set; }
        public string Name { get; set; }
        public string Composer { get; set; }
        public List<SimpleProgramme> Programmes { get; set; }
        public List<SimpleTrack> Tracks { get; set; }
    }
}