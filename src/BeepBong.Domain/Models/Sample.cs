using System;
using System.Collections.Generic;

namespace BeepBong.Domain.Models
{
    public class Sample : IEquatable<Sample>
    {
        public Guid SampleId { get; set; }

        public int SampleRate { get; set; }
        public int SampleCount { get; set; }
        public int AudioChannelCount { get; set; }
        public int BitRate { get; set; }
        public BitRateModeEnum BitRateMode { get; set; }
        public int BitDepth { get; set; }
        public string Codec { get; set; }
        public CompressionEnum Compression { get; set; }
        public string Fingerprint { get; set; }
        public string OtherAttributes { get; set; }
        public string Notes { get; set; }

        public string Waveform { get; set; }
        public string Spectrograph { get; set; }

        public Guid TrackId { get; set; }
        public Track Track { get; set; }

        public string Duration { get => TimeSpan.FromSeconds(SampleCount / SampleRate).ToString(); }

        public override bool Equals(object obj)
        {
            return Equals(obj as Sample);
        }

        public bool Equals(Sample other)
        {
            return other != null &&
                   SampleId.Equals(other.SampleId) &&
                   SampleRate == other.SampleRate &&
                   SampleCount == other.SampleCount &&
                   AudioChannelCount == other.AudioChannelCount &&
                   BitRate == other.BitRate &&
                   BitRateMode == other.BitRateMode &&
                   BitDepth == other.BitDepth &&
                   Codec == other.Codec &&
                   Compression == other.Compression &&
                   Fingerprint == other.Fingerprint &&
                   OtherAttributes == other.OtherAttributes &&
                   Notes == other.Notes &&
                   Waveform == other.Waveform &&
                   Spectrograph == other.Spectrograph &&
                   TrackId.Equals(other.TrackId) &&
                   EqualityComparer<Track>.Default.Equals(Track, other.Track) &&
                   Duration == other.Duration;
        }

        public override int GetHashCode()
        {
            int hashCode = 409561430;
            hashCode = hashCode * -1521134295 + SampleId.GetHashCode();
            hashCode = hashCode * -1521134295 + SampleRate.GetHashCode();
            hashCode = hashCode * -1521134295 + SampleCount.GetHashCode();
            hashCode = hashCode * -1521134295 + AudioChannelCount.GetHashCode();
            hashCode = hashCode * -1521134295 + BitRate.GetHashCode();
            hashCode = hashCode * -1521134295 + BitRateMode.GetHashCode();
            hashCode = hashCode * -1521134295 + BitDepth.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Codec);
            hashCode = hashCode * -1521134295 + Compression.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Fingerprint);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(OtherAttributes);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Notes);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Waveform);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Spectrograph);
            hashCode = hashCode * -1521134295 + TrackId.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<Track>.Default.GetHashCode(Track);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Duration);
            return hashCode;
        }

        public static bool operator ==(Sample left, Sample right)
        {
            return EqualityComparer<Sample>.Default.Equals(left, right);
        }

        public static bool operator !=(Sample left, Sample right)
        {
            return !(left == right);
        }
    }

    public enum CompressionEnum
    {
        None,
        Lossless,
        Lossy
    }

    public enum BitRateModeEnum
    {
        CBR,
        VBR
    }
}