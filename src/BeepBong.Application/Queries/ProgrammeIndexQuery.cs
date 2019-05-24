using BeepBong.Application.ViewModels;
using BeepBong.DataAccess;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BeepBong.Application.Queries
{
    public class ProgrammeIndexQuery
    {
        private readonly BeepBongContext _context;

        public ProgrammeIndexQuery(BeepBongContext context) => _context = context;

        public IQueryable<ProgrammeIndexViewModel> GetQuery(Guid? channelId = null)
        {
            return _context.Programmes
                .Include(p => p.ProgrammeTrackLists)
                .ThenInclude(ptl => ptl.TrackList)
                .ThenInclude(tl => tl.Tracks)
                .WhereIf(channelId != null, p => p.ProgrammeId == channelId)
                .Select(p => new ProgrammeIndexViewModel() {
                    ProgrammeId = p.ProgrammeId,
                    Name = p.Name,
                    Year = (p.AirDate.HasValue) ? p.AirDate.Value.Year.ToString() : null,
                    Channel = (p.Channel != null) ? p.Channel.Name : null,
                    Logo = p.LogoLocation,
                    ContainsLibrary = p.ProgrammeTrackLists.Any(ptl => ptl.TrackList.Library == true),
                    TrackCount = p.ProgrammeTrackLists.Select(ptl => ptl.TrackList.Tracks.Count).Sum()
                })
                .OrderBy(ls => ls.Name)
                .ThenBy(ls => ls.Year)
                .AsQueryable();
        }
    }
}