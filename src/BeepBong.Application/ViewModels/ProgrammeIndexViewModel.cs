using System;

namespace BeepBong.Application.ViewModels
{
    public class ProgrammeIndexViewModel
    {
        public Guid ProgrammeId { get; set; }
        public string Name { get; set; }
        public string Year { get; set; }
        public string Channel { get; set; }
        public string Logo { get; set; }
        public bool ContainsLibrary { get; set; }
        public int TrackCount { get; set; }
    }
}