using System;
using System.Linq;
using BeepBong.Application.ViewModels;
using BeepBong.DataAccess;

namespace BeepBong.Application.Queries
{
    public class ProgrammeEditQuery
    {
        private readonly BeepBongContext _context;
        
        public ProgrammeEditQuery(BeepBongContext context) => _context = context;


        public IQueryable<ProgrammeEditViewModel> GetQuery(Guid programmeId)
        {
            return _context.ProgrammeTrackLists
                .Where(ptl => ptl.ProgrammeId == programmeId)
                .GroupBy(ptl => ptl.Programme, ptl => ptl.TrackListId, (key, g) => new { Programme = key, TrackList = g.ToList()})
                .Select(ptl => new ProgrammeEditViewModel() {
                    ProgrammeId = ptl.Programme.ProgrammeId,
                    Name = ptl.Programme.Name,
                    AirDate = ptl.Programme.AirDate,
                    LogoLocation = ptl.Programme.LogoLocation,
                    ChannelId = ptl.Programme.ChannelId,
                    TrackListIds = ptl.TrackList
                });
        }
    }
}