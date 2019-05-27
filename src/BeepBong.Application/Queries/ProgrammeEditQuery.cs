using System;
using System.Linq;
using BeepBong.Application.Interfaces;
using BeepBong.Application.ViewModels;
using BeepBong.DataAccess;

namespace BeepBong.Application.Queries
{
    public class ProgrammeEditQuery : IQuery<ProgrammeEditViewModel>
    {
        private readonly BeepBongContext _context;
        
        public ProgrammeEditQuery(BeepBongContext context) => _context = context;


        public IQueryable<ProgrammeEditViewModel> GetQuery(Guid? programmeId)
        {
            return _context.Programmes
                .Where(p => p.ProgrammeId == programmeId.Value)
                .Select(p => new ProgrammeEditViewModel() {
                    ProgrammeId = p.ProgrammeId,
                    Name = p.Name,
                    AirDate = p.AirDate,
                    LogoLocation = p.LogoLocation,
                    ChannelId = p.ChannelId,
                    TrackListIds = p.ProgrammeTrackLists.Select(ptl => ptl.TrackListId).ToList()
                });
        }
    }
}