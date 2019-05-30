using System;

namespace BeepBong.Domain.Models
{
    public class Image
    {
        public Guid ImageId { get; set; }
        public string MimeType { get; set; }
        public string Base64 { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }

        public string DataURI { get => "data:" + MimeType + ";base64," + Base64; }
    }
}