using System;
using System.Linq;
using BeepBong.Application.Interfaces;
using BeepBong.Application.ViewModels;
using BeepBong.DataAccess;

namespace BeepBong.Application.Queries
{
    public class TrackDetailQuery : IQuery<TrackDetailViewModel>
    {
        private readonly BeepBongContext _context;

        public TrackDetailQuery(BeepBongContext context) => _context = context;

        public IQueryable<TrackDetailViewModel> GetQuery(Guid? trackId)
        {
            return _context.Tracks
                .Where(t => t.TrackId == trackId.Value)
                .Select(t => new TrackDetailViewModel() {
                    TrackId = t.TrackId,
                    Name = t.Name,
                    Variant = t.Variant,
                    Description = t.Description,
                    TrackListId = t.TrackListId,
                    Samples = t.Samples.Select(s => new SimpleSample()
                    {
                        SampleId = s.SampleId,
                        Duration = s.Duration,
                        SampleRate = (s.SampleRate/1000).ToString() + " kHz",
                        AudioChannelCount = s.AudioChannelCount,
                        BitRate = (s.BitRate/1000).ToString() + " kbps",
                        BitRateMode = s.BitRateMode.ToString(),
                        BitDepth = s.BitDepth + " bits",
                        Codec = s.Codec,
                        Compression = s.Compression.ToString(),
                        FingerprintShort = s.Fingerprint.Substring(0,6),
                        OtherAttributes = s.OtherAttributes
                    }).ToList()
                });
        }
    }
}