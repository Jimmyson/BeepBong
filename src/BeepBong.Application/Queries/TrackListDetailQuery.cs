using System;
using System.Linq;
using BeepBong.Application.Interfaces;
using BeepBong.Application.ViewModels;
using BeepBong.DataAccess;

namespace BeepBong.Application.Queries
{
    public class TrackListDetailQuery : IQuery<TrackListDetailViewModel>
    {
        private readonly BeepBongContext _context;

        public TrackListDetailQuery(BeepBongContext context) => _context = context;

        public IQueryable<TrackListDetailViewModel> GetQuery(Guid? TrackListId)
        {
            return _context.TrackLists
                .Where(tl => tl.TrackListId == TrackListId.Value)
                .Select(tl => new TrackListDetailViewModel() {
                    TrackListId = tl.TrackListId,
                    Name = tl.Name,
                    Composer = tl.Composer,
                    Library = tl.Library,
                    Programmes = tl.ProgrammeTrackLists.Select(ptl => ptl.ProgrammeId).ToList(),
                    Tracks = tl.Tracks.Select(t => new SimpleTrack() {
                        TrackId = t.TrackId,
                        Name = t.Name,
                        Variant = t.Variant,
                        Description = t.Description,
                        SampleCount = t.Samples.Count
                    }).ToList()
                });
        }
    }
}