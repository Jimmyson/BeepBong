using System;

namespace BeepBong.Web.ViewModels
{
    public class LibrarySampleViewModel
    {
        public Guid ProgrammeId { get; set; }
        public string ProgrammeName { get; set; }
        public Guid TrackId { get; set; }
        public string TrackName { get; set; }
        public Guid SampleId { get; set; }
    }
}