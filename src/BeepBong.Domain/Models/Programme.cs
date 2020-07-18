using System;
using System.Collections.Generic;

namespace BeepBong.Domain.Models
{
    public class Programme : IEquatable<Programme>
    {
        public Programme()
        {
            ProgrammeTrackLists = new List<ProgrammeTrackList>();
        }

        public Guid ProgrammeId { get; set; }
        public string Name { get; set; }
        public DateTime? AirDate { get; set; }

        public ICollection<ProgrammeTrackList> ProgrammeTrackLists { get; set; }

        public Guid? ImageId { get; set; }
        public Image Image { get; set; }

        public Guid? ChannelId { get; set; }
        public Channel Channel { get; set; }

        public override bool Equals(object obj)
        {
            return Equals(obj as Programme);
        }

        public bool Equals(Programme other)
        {
            return other != null &&
                   ProgrammeId.Equals(other.ProgrammeId) &&
                   Name == other.Name &&
                   AirDate == other.AirDate &&
                   EqualityComparer<Guid?>.Default.Equals(ImageId, other.ImageId) &&
                   EqualityComparer<Image>.Default.Equals(Image, other.Image) &&
                   EqualityComparer<Guid?>.Default.Equals(ChannelId, other.ChannelId) &&
                   EqualityComparer<Channel>.Default.Equals(Channel, other.Channel);
        }

        public override int GetHashCode()
        {
            int hashCode = 706201716;
            hashCode = hashCode * -1521134295 + ProgrammeId.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Name);
            hashCode = hashCode * -1521134295 + AirDate.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<ICollection<ProgrammeTrackList>>.Default.GetHashCode(ProgrammeTrackLists);
            hashCode = hashCode * -1521134295 + ImageId.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<Image>.Default.GetHashCode(Image);
            hashCode = hashCode * -1521134295 + ChannelId.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<Channel>.Default.GetHashCode(Channel);
            return hashCode;
        }

        public static bool operator ==(Programme left, Programme right)
        {
            return EqualityComparer<Programme>.Default.Equals(left, right);
        }

        public static bool operator !=(Programme left, Programme right)
        {
            return !(left == right);
        }
    }
}