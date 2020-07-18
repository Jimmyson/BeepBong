using BeepBong.Application.Interfaces;
using BeepBong.Application.ViewModels;
using BeepBong.DataAccess;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace BeepBong.Application.Queries
{
    public class ProgrammeDetailQuery : IQuery<ProgrammeDetailViewModel>
    {
        private readonly BeepBongContext _context;

        public ProgrammeDetailQuery(BeepBongContext context) => _context = context;

        public IQueryable<ProgrammeDetailViewModel> GetQuery(Guid? programmeId)
        {
            return _context.Programmes
                .Include(p => p.ProgrammeTrackLists)
                .ThenInclude(ptl => ptl.TrackList)
                .ThenInclude(tl => tl.Tracks)
                .Where(p => p.ProgrammeId == programmeId.Value)
                .Select(p => new ProgrammeDetailViewModel() {
                    ProgrammeId = p.ProgrammeId,
                    Name = p.Name,
                    AirDate = p.AirDate,
                    ChannelId = p.ChannelId,
                    ChannelName = (p.Channel != null) ? p.Channel.Name : null,
                    ImageId = p.ImageId,
                    TrackListIds = p.ProgrammeTrackLists.Select(ptl => ptl.TrackList).Select(tl => tl.TrackListId).ToList()
                });
        }
    }
}