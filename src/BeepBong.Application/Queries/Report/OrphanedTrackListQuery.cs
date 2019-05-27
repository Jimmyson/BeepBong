using System;
using System.Linq;
using BeepBong.Application.Interfaces;
using BeepBong.Application.ViewModels.Report;
using BeepBong.DataAccess;

namespace BeepBong.Application.Queries.Report
{
    public class OrphanedTrackListQuery : IQuery<OrphanedTrackListViewModel>
    {
        private readonly BeepBongContext _context;

        public OrphanedTrackListQuery(BeepBongContext context) => _context = context;

        public IQueryable<OrphanedTrackListViewModel> GetQuery(Guid? id)
        {
            return _context.TrackLists
                .Where(tl => !tl.ProgrammeTrackLists.Any())
                .Select(tl => new OrphanedTrackListViewModel() {
                    TrackListId = tl.TrackListId,
                    Name = tl.Name,
                    Composer = tl.Composer,
                    Library = tl.Library,
                    TrackCount = tl.Tracks.Count
                })
                .OrderBy(tl => tl.Name);
        }
    }
}