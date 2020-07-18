using System;
using System.Collections.Generic;

namespace BeepBong.Domain.Models
{
    public class Image : IEquatable<Image>
    {
        public Guid ImageId { get; set; }
        public string MimeType { get; set; }
        public string Base64 { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }

        public string DataURI { get => "data:" + MimeType + ";base64," + Base64; }

        public override bool Equals(object obj)
        {
            return Equals(obj as Image);
        }

        public bool Equals(Image other)
        {
            return other != null &&
                   ImageId.Equals(other.ImageId) &&
                   MimeType == other.MimeType &&
                   Base64 == other.Base64 &&
                   Width == other.Width &&
                   Height == other.Height &&
                   DataURI == other.DataURI;
        }

        public override int GetHashCode()
        {
            int hashCode = -1120687156;
            hashCode = hashCode * -1521134295 + ImageId.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(MimeType);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Base64);
            hashCode = hashCode * -1521134295 + Width.GetHashCode();
            hashCode = hashCode * -1521134295 + Height.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(DataURI);
            return hashCode;
        }

        public static bool operator ==(Image left, Image right)
        {
            return EqualityComparer<Image>.Default.Equals(left, right);
        }

        public static bool operator !=(Image left, Image right)
        {
            return !(left == right);
        }
    }
}