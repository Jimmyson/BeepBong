using System;

namespace BeepBong.Application.ViewModels.Report
{
    [Obsolete("Use Programme Index View Model")]
    public class ProgrammeWOTrackListViewModel
    {
        public Guid ProgrammeId { get; set; }
        public string Name { get; set; }
        public string Year { get; set; }
        public string ChannelName { get; set; }
    }
}