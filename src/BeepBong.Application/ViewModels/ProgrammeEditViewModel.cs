using System;
using System.Collections.Generic;

namespace BeepBong.Application.ViewModels
{
    public class ProgrammeEditViewModel
    {
        public Guid ProgrammeId { get; set; }
        public string Name { get; set; }
        public DateTime? AirDate { get; set; }
        public string LogoLocation { get; set; }
        public Guid? ChannelId { get; set; }
        public List<Guid> TrackListIds { get; set; }
    }
}