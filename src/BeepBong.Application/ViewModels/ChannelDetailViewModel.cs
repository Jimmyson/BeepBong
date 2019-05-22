using System;
using System.Collections.Generic;

namespace BeepBong.Application.ViewModels
{
    public class ChannelDetailViewModel
    {
        public Guid ChannelId { get; set; }
        public string Name { get; set; }
        public DateTime Commencement { get; set; }
        public DateTime? Closed { get; set; }
        public Guid BroadcasterId { get; set; }
        public List<SimpleProgramme> Programmes { get; set; }
    }

    public class SimpleProgramme
    {
        public string Name { get; set; }
        public string Year { get; set; }
    }
}