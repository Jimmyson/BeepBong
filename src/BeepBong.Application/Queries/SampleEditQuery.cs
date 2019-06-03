using System;
using System.Linq;
using BeepBong.Application.Interfaces;
using BeepBong.Application.ViewModels;
using BeepBong.DataAccess;

namespace BeepBong.Application.Queries
{
    public class SampleEditQuery : IQuery<SampleEditViewModel>
    {
        private readonly BeepBongContext _context;

        public SampleEditQuery(BeepBongContext context) => _context = context;

        public IQueryable<SampleEditViewModel> GetQuery(Guid? SampleId)
        {
            return _context.Samples
                .Where(s => s.SampleId == SampleId.Value)
                .Select(s => new SampleEditViewModel() {
                    SampleId = s.SampleId,
                    Notes = s.Notes,
                    TrackId = s.TrackId
                });
        }

        public bool Exists(SampleCreateViewModel model)
        {
            return _context.Samples.Any(s => s.SampleRate == int.Parse(model.SampleRate)
                    && s.SampleCount == int.Parse(model.SampleCount)
                    && s.AudioChannelCount == int.Parse(model.AudioChannelCount)
                    && s.BitRate == int.Parse(model.BitRate)
                    && s.BitRateMode == model.BitRateMode
                    && s.BitDepth == int.Parse(model.BitDepth)
                    && s.Codec == model.Codec
                    && s.Fingerprint == model.Fingerprint
                    && s.Compression == model.Compression);
        }
    }
}