using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BeepBong.Application.ViewModels
{
    public class ProgrammeEditViewModel
    {
        public Guid ProgrammeId { get; set; }
        public string Name { get; set; }
        [DataType(DataType.Date)]
        public DateTime? AirDate { get; set; }
        public string LogoLocation { get; set; }
        public Guid? ChannelId { get; set; }
        public List<Guid> TrackListIds { get; set; }
    }
}