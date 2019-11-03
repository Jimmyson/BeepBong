using System;

namespace BeepBong.Application.ViewModels
{
    public class ChannelIndexViewModel
    {
        public Guid ChannelId { get; set; }
        public string Name { get; set; }
        public string Opened { get; set; }
        public string Closed { get; set; }
        public string BroadcasterName { get; set; }
        public int ProgrammeCount { get; set; }
    }
}