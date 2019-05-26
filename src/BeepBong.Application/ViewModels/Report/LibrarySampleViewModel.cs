using System;

namespace BeepBong.Application.ViewModels.Report
{
    public class LibrarySampleViewModel
    {
        public Guid TrackListId { get; set; }
        public string TrackListName { get; set; }
        public Guid TrackId { get; set; }
        public string TrackName { get; set; }
        public string TrackVariant { get; set; }
        public Guid SampleId { get; set; }
        public string Fingerprint { get; set; }
    }
}