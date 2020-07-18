using System;
using System.Collections.Generic;

namespace BeepBong.Domain.Models
{
    public class Broadcaster : IEquatable<Broadcaster>
    {
        public Broadcaster()
        {
            Channels = new List<Channel>();
        }

        public Guid BroadcasterId { get; set; }
        public string Name { get; set; }
        public string Country { get; set; }
        public ICollection<Channel> Channels { get; set; }

        public Guid? ImageId { get; set; }
        public Image Image { get; set; }

        public override bool Equals(object obj)
        {
            return Equals(obj as Broadcaster);
        }

        public bool Equals(Broadcaster other)
        {
            return other != null &&
                   BroadcasterId.Equals(other.BroadcasterId) &&
                   Name == other.Name &&
                   Country == other.Country &&
                   EqualityComparer<Guid?>.Default.Equals(ImageId, other.ImageId) &&
                   EqualityComparer<Image>.Default.Equals(Image, other.Image);
        }

        public override int GetHashCode()
        {
            int hashCode = -428302341;
            hashCode = hashCode * -1521134295 + BroadcasterId.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Name);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Country);
            hashCode = hashCode * -1521134295 + EqualityComparer<ICollection<Channel>>.Default.GetHashCode(Channels);
            hashCode = hashCode * -1521134295 + ImageId.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<Image>.Default.GetHashCode(Image);
            return hashCode;
        }

        public static bool operator ==(Broadcaster left, Broadcaster right)
        {
            return EqualityComparer<Broadcaster>.Default.Equals(left, right);
        }

        public static bool operator !=(Broadcaster left, Broadcaster right)
        {
            return !(left == right);
        }
    }
}