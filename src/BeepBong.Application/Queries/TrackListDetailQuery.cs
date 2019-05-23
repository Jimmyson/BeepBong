using System;
using System.Linq;
using BeepBong.Application.ViewModels;
using BeepBong.DataAccess;

namespace BeepBong.Application.Queries
{
    public class TrackListDetailQuery
    {
        private readonly BeepBongContext _context;

        public TrackListDetailQuery(BeepBongContext context) => _context = context;

        public IQueryable<TrackListDetailViewModel> GetQuery(Guid TrackListId)
        {
            return _context.TrackLists
                .Where(tl => tl.TrackListId == TrackListId)
                .Select(tl => new TrackListDetailViewModel() {
                    TrackListId = tl.TrackListId,
                    Name = tl.Name,
                    Composer = tl.Composer,
                    Programmes = tl.ProgrammeTrackLists.Select(ptl => new SimpleProgramme() {
                        ProgrammeId = ptl.ProgrammeId,
                        Name = ptl.Programme.Name,
                        Year = (ptl.Programme.AirDate.HasValue) ? ptl.Programme.AirDate.Value.Year.ToString() : null
                    }).ToList(),
                    Tracks = tl.Tracks.Select(t => new SimpleTrack() {
                        TrackId = t.TrackId,
                        Name = t.Name,
                        Variant = t.Variant,
                        Description = t.Description,
                        SampleCount = t.Samples.Count()
                    }).ToList()
                });
        }
    }
}