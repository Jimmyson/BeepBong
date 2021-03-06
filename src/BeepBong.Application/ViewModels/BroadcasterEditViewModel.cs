using System;
using BeepBong.Application.Interfaces;
using BeepBong.Domain.Models;

namespace BeepBong.Application.ViewModels
{
    public class BroadcasterEditViewModel : IImageEntity
    {
        public Guid BroadcasterId { get; set; }
        public string Name { get; set; }
        public string Country { get; set; }
        public Guid? ImageId { get; set; }
        public bool ImageChange { get; set; }
        public Image Image { get; set; }
    }
}