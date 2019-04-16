using System;
using System.Collections.Generic;
using BeepBong.Domain.Models;

namespace BeepBong.Web.ViewModels
{
    public class TrackSampleListViewModel
    {
        public TrackSampleListViewModel()
        {
            Samples = new List<Sample>();
        }

        public Guid TrackId { get; set; }
        public string Name { get; set; }
        public string Subtitle { get; set; }
        public string Description { get; set; }
        public bool IsLibraryMusic { get; set; }
        public Guid ProgrammeId { get; set; }
        public string ProgrammeName { get; set; }
        public ICollection<Sample> Samples { get; set; }
    }
}