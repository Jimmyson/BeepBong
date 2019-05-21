using System;
using System.Collections.Generic;

namespace BeepBong.Application.ViewModels
{
    public class BroadcasterEditViewModel
    {
        public Guid BroadcasterId { get; set; }
        public string Name { get; set; }
        public string Country { get; set; }
    }
}