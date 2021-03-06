using System;
using System.Collections.Generic;

namespace BeepBong.Application.ViewModels
{
    public class TrackDetailViewModel
    {
        public Guid TrackId { get; set; }
        public string Name { get; set; }
        public string Variant { get; set; }
        public string Description { get; set; }
        public Guid TrackListId { get; set; }
        public bool InLibrary { get; set; }
    }
}