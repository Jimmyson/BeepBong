using System;
using System.Collections.Generic;

namespace BeepBong.Application.ViewModels
{
    public class ProgrammeDetailViewModel
    {
        public Guid ProgrammeId { get; set; }
        public string Name { get; set; }
        public string AirDate { get; set; }
        public Guid? ChannelId { get; set; }
        public string ChannelName { get; set; }
        public Guid? ImageId { get; set; }
        public List<Guid> TrackListIds { get; set; }
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