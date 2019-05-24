using System;
using System.Linq;
using BeepBong.Application.ViewModels;
using BeepBong.DataAccess;

namespace BeepBong.Application.Queries
{
    public class TrackListEditQuery
    {
        private readonly BeepBongContext _context;

        public TrackListEditQuery(BeepBongContext context) => _context = context;

        public IQueryable<TrackListEditViewModel> GetQuery(Guid TrackListId)
        {
            return _context.TrackLists
                .Where(tl => tl.TrackListId == TrackListId)
                .Select(tl => new TrackListEditViewModel() {
                    TrackListId = tl.TrackListId,
                    Name = tl.Name,
                    Composer = tl.Composer,
                    Programmes = tl.ProgrammeTrackLists.Select(ptl => ptl.ProgrammeId).ToList()
                });
        }
    }
}