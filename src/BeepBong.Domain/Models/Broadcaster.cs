using System;
using System.Collections.Generic;

namespace BeepBong.Domain.Models
{
    public class Broadcaster
    {
        public Broadcaster()
        {
            Channels = new List<Channel>();
        }

        public Guid BroadcasterId { get; set; }
        public string Name { get; set; }
        public string Country { get; set; }
        public ICollection<Channel> Channels { get; set; }
    }
}