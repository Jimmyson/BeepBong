using System;
using Microsoft.AspNetCore.Http;

namespace BeepBong.Web.Vue.ViewModel
{
    public class BroadcasterUploadViewModel
    {
        public Guid BroadcasterId { get; set; }
        public string Name { get; set; }
        public string Country { get; set; }
        public Guid? ImageId { get; set; }
        public bool? ImageChange { get; set; }
        public IFormFile Image { get; set; }
    }
}