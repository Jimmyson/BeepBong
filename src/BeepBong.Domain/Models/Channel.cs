using System;
using System.Collections.Generic;

namespace BeepBong.Domain.Models
{
    public class Channel
    {
        public Channel()
        {
            Programmes = new List<Programme>();
        }

        public Guid ChannelId { get; set; }
        public string Name { get; set; }
        public DateTime Commencement { get; set; }
        public DateTime? Closed { get; set; }

        public Guid BroadcasterId { get; set; }
        public Broadcaster Broadcaster { get; set; }

        public ICollection<Programme> Programmes { get; set; }
    }
}