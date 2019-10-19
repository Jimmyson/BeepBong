using System;
using System.Collections.Generic;

namespace BeepBong.Application.ViewModels
{
    public class BroadcasterIndexViewModel
    {
        public Guid BroadcasterId { get; set; }
        public string Name { get; set; }
        public string Country { get; set; }
        public Guid? ImageId { get; set; }
    }
}