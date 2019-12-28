using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace BeepBong.Web.Vue.ViewModel
{
    public class ProgrammeUploadViewModel : IImageUpload
    {
        public Guid ProgrammeId { get; set; }
        public string Name { get; set; }

        [DataType(DataType.Date)]
        public DateTime? AirDate { get; set; }

        public IFormFile Image { get; set; }
        public Guid? ImageId { get; set; }
        public bool? ImageChange { get; set; }
        public Guid? ChannelId { get; set; }
        public List<Guid> TrackListIds { get; set; }
    }
}