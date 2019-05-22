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
            return _context.ProgrammeTrackLists
                .Where(ptl => ptl.TrackListId == TrackListId)
                .GroupBy(ptl => ptl.TrackList, ptl => ptl.ProgrammeId, (key, g) => new { TrackList = key, Programme = g.ToList()})
                .Select(ptl => new TrackListEditViewModel() {
                    TrackListId = ptl.TrackList.TrackListId,
                    Name = ptl.TrackList.Name,
                    Composer = ptl.TrackList.Composer,
                    Programmes = ptl.Programme
                });
        }
    }
}