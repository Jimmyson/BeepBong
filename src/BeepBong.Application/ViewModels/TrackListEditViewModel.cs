using System;
using System.Collections.Generic;

namespace BeepBong.Application.ViewModels
{
    public class TrackListEditViewModel
    {
        public Guid TrackListId { get; set; }
        public string Name { get; set; }
        public string Composer { get; set; }
        public List<Guid> Programmes { get; set; }
    }
}