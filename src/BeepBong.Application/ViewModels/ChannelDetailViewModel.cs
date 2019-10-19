using System;
using System.Collections.Generic;

namespace BeepBong.Application.ViewModels
{
    public class ChannelDetailViewModel
    {
        public Guid ChannelId { get; set; }
        public string Name { get; set; }
        public DateTime? Opened { get; set; }
        public DateTime? Closed { get; set; }
        public string BroadcasterName { get; set; }
        public List<SimpleProgramme> Programmes { get; set; }
    }

    public class SimpleProgramme
    {
        public Guid ProgrammeId { get; set; }
        public string Name { get; set; }
        public string Year { get; set; }
    }
}