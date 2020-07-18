using System;
using System.Collections.Generic;

namespace BeepBong.Domain.Models
{
    public class TrackList : IEquatable<TrackList>
    {
        public TrackList()
        {
            Tracks = new List<Track>();
            ProgrammeTrackLists = new List<ProgrammeTrackList>();
        }

        public Guid TrackListId { get; set; }
        public string Name { get; set; }
        public bool Library { get; set; }
        public string Composer { get; set; }

        public ICollection<Track> Tracks { get; set; }
        public ICollection<ProgrammeTrackList> ProgrammeTrackLists { get; set; }

        public override bool Equals(object obj)
        {
            return Equals(obj as TrackList);
        }

        public bool Equals(TrackList other)
        {
            return other != null &&
                   TrackListId.Equals(other.TrackListId) &&
                   Name == other.Name &&
                   Library == other.Library &&
                   Composer == other.Composer;
        }

        public override int GetHashCode()
        {
            int hashCode = 1699316532;
            hashCode = hashCode * -1521134295 + TrackListId.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Name);
            hashCode = hashCode * -1521134295 + Library.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Composer);
            hashCode = hashCode * -1521134295 + EqualityComparer<ICollection<Track>>.Default.GetHashCode(Tracks);
            hashCode = hashCode * -1521134295 + EqualityComparer<ICollection<ProgrammeTrackList>>.Default.GetHashCode(ProgrammeTrackLists);
            return hashCode;
        }

        public static bool operator ==(TrackList left, TrackList right)
        {
            return EqualityComparer<TrackList>.Default.Equals(left, right);
        }

        public static bool operator !=(TrackList left, TrackList right)
        {
            return !(left == right);
        }
    }
}