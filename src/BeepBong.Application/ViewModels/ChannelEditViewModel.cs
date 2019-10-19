using System;

namespace BeepBong.Application.ViewModels
{
    public class ChannelEditViewModel
    {
        public Guid ChannelId { get; set; }
        public string Name { get; set; }
        public DateTime? Opened { get; set; }
        public DateTime? Closed { get; set; }
        public Guid BroadcasterId { get; set; }
    }
}