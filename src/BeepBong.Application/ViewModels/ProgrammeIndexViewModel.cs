using System;

namespace BeepBong.Application.ViewModels
{
    public class ProgrammeIndexViewModel
    {
        public Guid ProgrammeId { get; set; }
        public string Name { get; set; }
        public DateTime? AirDate { get; set; }
        public string Channel { get; set; }
        public Guid? ImageId { get; set; }
        public bool ContainsLibrary { get; set; }
        public int TrackCount { get; set; }
    }
}