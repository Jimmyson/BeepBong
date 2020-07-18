using System;
using System.Collections.Generic;

namespace BeepBong.Domain.Models
{
    public class Track : IEquatable<Track>
    {
        public Track()
        {
            Samples = new List<Sample>();
        }

        public Guid TrackId { get; set; }
        public string Name { get; set; }
        public string Variant { get; set; }
        public string Description { get; set; }

        public ICollection<Sample> Samples { get; set; }

        public Guid TrackListId { get; set; }
        public TrackList TrackList { get; set; }

        public override bool Equals(object obj)
        {
            return Equals(obj as Track);
        }

        public bool Equals(Track other)
        {
            return other != null &&
                   TrackId.Equals(other.TrackId) &&
                   Name == other.Name &&
                   Variant == other.Variant &&
                   Description == other.Description &&
                   TrackListId.Equals(other.TrackListId) &&
                   EqualityComparer<TrackList>.Default.Equals(TrackList, other.TrackList);
        }

        public override int GetHashCode()
        {
            int hashCode = 369174383;
            hashCode = hashCode * -1521134295 + TrackId.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Name);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Variant);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Description);
            hashCode = hashCode * -1521134295 + EqualityComparer<ICollection<Sample>>.Default.GetHashCode(Samples);
            hashCode = hashCode * -1521134295 + TrackListId.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<TrackList>.Default.GetHashCode(TrackList);
            return hashCode;
        }

        public static bool operator ==(Track left, Track right)
        {
            return EqualityComparer<Track>.Default.Equals(left, right);
        }

        public static bool operator !=(Track left, Track right)
        {
            return !(left == right);
        }
    }
}