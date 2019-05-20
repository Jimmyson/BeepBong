using System;
using System.Collections.Generic;

namespace BeepBong.Domain.Models
{
    public class Programme
    {
        public Programme()
        {
            ProgrammeTrackLists = new List<ProgrammeTrackList>();
        }

        public Guid ProgrammeId { get; set; }
        public string Name { get; set; }
        public DateTime AirDate { get; set; }
        
        public string LogoLocation { get; set; }

        public ICollection<ProgrammeTrackList> ProgrammeTrackLists { get; set; }

        public Guid ChannelId { get; set; }
        public Channel Channel { get; set; }
    }
}