using System;
using System.Collections.Generic;

namespace BeepBong.Application.ViewModels
{
    public class BroadcasterDetailViewModel
    {
        public Guid BroadcasterId { get; set; }
        public string Name { get; set; }
        public string Country { get; set; }
        public Guid? ImageId { get; set; }
        public List<Guid> ChannelList { get; set; }
    }
}