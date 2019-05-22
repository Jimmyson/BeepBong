using System;

namespace BeepBong.Application.ViewModels
{
    public class SampleEditViewModel
    {
        public Guid SampleId { get; set; }
        public string Notes { get; set; }
        public Guid TrackId { get; set; }  
    }
}