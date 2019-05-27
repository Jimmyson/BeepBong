using System;
using System.Linq;
using BeepBong.Application.Interfaces;
using BeepBong.Application.ViewModels;
using BeepBong.DataAccess;

namespace BeepBong.Application.Queries
{
    public class SampleDetailQuery : IQuery<SampleDetailViewModel>
    {
        private readonly BeepBongContext _context;

        public SampleDetailQuery(BeepBongContext context) => _context = context;

        public IQueryable<SampleDetailViewModel> GetQuery(Guid? SampleId)
        {
            return _context.Samples
                .Where(s => s.SampleId == SampleId.Value)
                .Select(s => new SampleDetailViewModel() {
                    SampleId = s.SampleId,
                    Duration = s.Duration,
                    SampleRate = (s.SampleRate/1000).ToString() + " kHz",
                    SampleCount = s.SampleCount.ToString(),
                    AudioChannelCount = s.AudioChannelCount,
                    BitRate = (s.BitRate/1000).ToString() + " kbps",
                    BitRateMode = s.BitRateMode.ToString(),
                    BitDepth = s.BitDepth + " bits",
                    Codec = s.Codec,
                    Compression = s.Compression.ToString(),
                    Fingerprint = s.Fingerprint,
                    OtherAttributes = s.OtherAttributes,
                    Notes = s.Notes,
                    Waveform = s.Waveform,
                    Spectrograph = s.Spectrograph,
                    TrackId = s.TrackId
                });
        }
    }
}