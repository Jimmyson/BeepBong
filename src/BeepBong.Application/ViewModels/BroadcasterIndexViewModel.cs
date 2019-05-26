using System;
using System.Collections.Generic;

namespace BeepBong.Application.ViewModels
{
    public class BroadcasterIndexViewModel
    {
        public Guid BroadcasterId { get; set; }
        public string Name { get; set; }
        public string Country { get; set; }
        public List<SimpleChannel> ChannelList { get; set; }
    }

    public class SimpleChannel
    {
        public Guid ChannelId { get; set; }
        public string Name { get; set; }
        public DateTime? Commencement { get; set; }
        public DateTime? Closed { get; set; }
        public int ProgrammeCount { get; set; }
    }
}