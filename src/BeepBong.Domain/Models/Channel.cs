using System;
using System.Collections.Generic;

namespace BeepBong.Domain.Models
{
    public class Channel : IEquatable<Channel>
    {
        public Channel()
        {
            Programmes = new List<Programme>();
        }

        public Guid ChannelId { get; set; }
        public string Name { get; set; }
        public DateTime? Opened { get; set; }
        public DateTime? Closed { get; set; }

        public Guid BroadcasterId { get; set; }
        public Broadcaster Broadcaster { get; set; }

        public ICollection<Programme> Programmes { get; set; }

        public override bool Equals(object obj)
        {
            return Equals(obj as Channel);
        }

        public bool Equals(Channel other)
        {
            return other != null &&
                   ChannelId.Equals(other.ChannelId) &&
                   Name == other.Name &&
                   Opened == other.Opened &&
                   Closed == other.Closed &&
                   BroadcasterId.Equals(other.BroadcasterId) &&
                   EqualityComparer<Broadcaster>.Default.Equals(Broadcaster, other.Broadcaster);
        }

        public override int GetHashCode()
        {
            int hashCode = 422057543;
            hashCode = hashCode * -1521134295 + ChannelId.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Name);
            hashCode = hashCode * -1521134295 + Opened.GetHashCode();
            hashCode = hashCode * -1521134295 + Closed.GetHashCode();
            hashCode = hashCode * -1521134295 + BroadcasterId.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<Broadcaster>.Default.GetHashCode(Broadcaster);
            hashCode = hashCode * -1521134295 + EqualityComparer<ICollection<Programme>>.Default.GetHashCode(Programmes);
            return hashCode;
        }

        public static bool operator ==(Channel left, Channel right)
        {
            return EqualityComparer<Channel>.Default.Equals(left, right);
        }

        public static bool operator !=(Channel left, Channel right)
        {
            return !(left == right);
        }
    }
}