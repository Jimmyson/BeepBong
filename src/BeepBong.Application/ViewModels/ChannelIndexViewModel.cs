using System;

namespace BeepBong.Application.ViewModels
{
    public class ChannelIndexViewModel
    {
        public Guid ChannelId { get; set; }
        public string Name { get; set; }
        public DateTime? Opened { get; set; }
        public DateTime? Closed { get; set; }
        public string BroadcasterName { get; set; }
        public int ProgrammeCount { get; set; }
    }
}