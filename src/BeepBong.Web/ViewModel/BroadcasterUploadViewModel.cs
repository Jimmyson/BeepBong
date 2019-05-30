using System;
using Microsoft.AspNetCore.Http;

namespace BeepBong.Web.ViewModel
{
    public class BroadcasterUploadViewModel
    {
        public Guid BroadcasterId { get; set; }
        public string Name { get; set; }
        public string Country { get; set; }
        public Guid? ImageId { get; set; }
        public Guid? ImageIdChange { get; set; }
        public IFormFile ImageUpload { get; set; }
    }
}