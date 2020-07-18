using System;
using System.Collections.Generic;

namespace BeepBong.Domain.Models
{
    public class Library : IEquatable<Library>
    {
        public Guid LibraryId { get; set; }
        public string AlbumName { get; set; }
        public string Label { get; set; }
        public string Catalog { get; set; }
        public string MBID { get; set; }

        public override bool Equals(object obj)
        {
            return Equals(obj as Library);
        }

        public bool Equals(Library other)
        {
            return other != null &&
                   LibraryId.Equals(other.LibraryId) &&
                   AlbumName == other.AlbumName &&
                   Label == other.Label &&
                   Catalog == other.Catalog &&
                   MBID == other.MBID;
        }

        public override int GetHashCode()
        {
            int hashCode = -1208889036;
            hashCode = hashCode * -1521134295 + LibraryId.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(AlbumName);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Label);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Catalog);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(MBID);
            return hashCode;
        }

        public static bool operator ==(Library left, Library right)
        {
            return EqualityComparer<Library>.Default.Equals(left, right);
        }

        public static bool operator !=(Library left, Library right)
        {
            return !(left == right);
        }
    }
}