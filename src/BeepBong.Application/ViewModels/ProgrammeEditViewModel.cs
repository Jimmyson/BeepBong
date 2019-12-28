using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using BeepBong.Application.Interfaces;
using BeepBong.Domain.Models;

namespace BeepBong.Application.ViewModels
{
    public class ProgrammeEditViewModel : IImageEntity
    {
        public Guid ProgrammeId { get; set; }
        public string Name { get; set; }

        [DataType(DataType.Date)]
        public DateTime? AirDate { get; set; }

        public Image Image { get; set; }
        public Guid? ImageId { get; set; }
        public bool ImageChange { get; set; }
        public Guid? ChannelId { get; set; }
        public List<Guid> TrackListIds { get; set; }
    }
}