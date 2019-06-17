using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace BeepBong.Web.ViewModel
{
    public class ProgrammeUploadViewModel
    {
        public Guid ProgrammeId { get; set; }
        public string Name { get; set; }

        [DataType(DataType.Date)]
        public DateTime? AirDate { get; set; }

        public IFormFile ImageUpload { get; set; }
        public Guid? ImageId { get; set; }
        public Guid? ImageIdChange { get; set; }
        public Guid? ChannelId { get; set; }
        public List<Guid> TrackListIds { get; set; }
    }
}