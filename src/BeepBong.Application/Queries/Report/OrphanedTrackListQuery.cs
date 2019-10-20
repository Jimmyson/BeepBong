using System;
using System.Linq;
using BeepBong.Application.Interfaces;
using BeepBong.Application.ViewModels;
using BeepBong.DataAccess;

namespace BeepBong.Application.Queries.Report
{
    public class OrphanedTrackListQuery : IQuery<TrackListIndexViewModel>
    {
        private readonly BeepBongContext _context;

        public OrphanedTrackListQuery(BeepBongContext context) => _context = context;

        public IQueryable<TrackListIndexViewModel> GetQuery(Guid? id)
        {
            return _context.TrackLists
                .Where(tl => tl.ProgrammeTrackLists.Count == 0)
                .OrderBy(tl => tl.Name)
                .Select(tl => new TrackListIndexViewModel() {
                    TrackListId = tl.TrackListId,
                    Name = tl.Name,
                    Composer = tl.Composer,
                    Library = tl.Library,
                    TrackCount = tl.Tracks.Count,
                    ProgrammeCount = tl.ProgrammeTrackLists.Count
                });
        }
    }
}