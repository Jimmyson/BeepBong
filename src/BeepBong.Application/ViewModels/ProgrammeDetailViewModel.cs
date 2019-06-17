using System;
using System.Collections.Generic;

namespace BeepBong.Application.ViewModels
{
    public class ProgrammeDetailViewModel
    {
        public Guid ProgrammeId { get; set; }
        public string Name { get; set; }
        public string AirDate { get; set; }
        public string ChannelName { get; set; }
        public Guid? ImageId { get; set; }
        public List<SimpleTrackList> TrackLists { get; set; }
    }

    public class SimpleTrackList
    {
        public Guid TrackListId { get; set; }
        public string Name { get; set; }
        public string Composer { get; set; }
        public bool Library { get; set; }
        public List<SimpleTrack> Tracks { get; set; }
    }

    public class SimpleTrack
    {
        public Guid TrackId { get; set; }
        public string Name { get; set; }
        public string Variant { get; set; }
        public string Description { get; set; }
        public int SampleCount { get; set; }
    }
}