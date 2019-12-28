using System;
using System.Linq;
using BeepBong.Application.Interfaces;
using BeepBong.Application.ViewModels;
using BeepBong.DataAccess;

namespace BeepBong.Application.Queries
{
    public class SampleIndexQuery : IQuery<SampleIndexViewModel>
    {
        private readonly BeepBongContext _context;

        public SampleIndexQuery(BeepBongContext context) => _context = context;

        public IQueryable<SampleIndexViewModel> GetQuery(Guid? id = null)
        {
            return _context.Samples
                .WhereIf(id != null, s => s.TrackId == id.Value)
                .Select(s => new SampleIndexViewModel() {
                    SampleId = s.SampleId,
                    Duration = s.Duration,
                    SampleRate = (s.SampleRate/1000).ToString() + " kHz",
                    BitDepth = s.BitDepth + " bits",
                    Codec = s.Codec,
                    Compression = s.Compression.ToString(),
                    Fingerprint = s.Fingerprint,
                });
        }
    }
}