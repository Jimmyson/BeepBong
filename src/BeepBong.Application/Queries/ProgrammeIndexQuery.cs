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
            return _context.ProgrammeTrackLists
                .Include(ptl => ptl.Programme)
                .Include(ptl => ptl.TrackList)
                .WhereIf(channelId != null, ptl => ptl.ProgrammeId == channelId)
                .GroupBy(ptl => ptl.Programme, ptl => ptl.TrackList, (key, g) => new {Programme = key, TrackList = g.ToList()})
                .Select(ptl => new ProgrammeIndexViewModel() {
                    ProgrammeId = ptl.Programme.ProgrammeId,
                    Name = ptl.Programme.Name,
                    Year = ptl.Programme.AirDate.Year.ToString(),
                    Channel = ptl.Programme.Channel.Name,
                    Logo = ptl.Programme.LogoLocation,
                    ContainsLibrary = ptl.TrackList.Any(tl => tl.Library),
                    TrackCount = ptl.TrackList.Sum(tl => tl.Tracks.Count),
                })
                .OrderBy(ls => ls.Name)
                .ThenBy(ls => ls.Year)
                .AsQueryable();
        }
    }
}