using System;

namespace BeepBong.Web.ViewModels
{
    public class ProgrammeTrackCountViewModel
    {
        public Guid ProgrammeId { get; set; }
        public string Name { get; set; }
        public string Year { get; set; }
        public string Channel { get; set; }
        public string AudioComposer { get; set; }
        public string Logo { get; set; }
        public bool IsLibraryMusic { get; set; }
        public int TrackCount { get; set; }
    }
}