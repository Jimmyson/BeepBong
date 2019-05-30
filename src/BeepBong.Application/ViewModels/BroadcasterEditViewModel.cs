using System;
using System.Collections.Generic;
using BeepBong.Domain.Models;

namespace BeepBong.Application.ViewModels
{
    public class BroadcasterEditViewModel
    {
        public Guid BroadcasterId { get; set; }
        public string Name { get; set; }
        public string Country { get; set; }
        public Guid? ImageId { get; set; }
        public bool ImageChange { get; set; }
        public Image Image { get; set; }
    }
}