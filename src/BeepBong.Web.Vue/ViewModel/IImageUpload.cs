using System;
using Microsoft.AspNetCore.Http;

namespace BeepBong.Web.Vue.ViewModel
{
    public interface IImageUpload
    {
        IFormFile Image { get; set; }
        Guid? ImageId { get; set; }
        bool? ImageChange { get; set; }
    }
}