using System;

namespace BeepBong.Application.ViewModels
{
    public class TrackEditViewModel
    {
        public Guid TrackId { get; set; }
        public string Name { get; set; }
        public string Variant { get; set; }
        public string Description { get; set; }
        public Guid TrackListId { get; set; }
    }
}