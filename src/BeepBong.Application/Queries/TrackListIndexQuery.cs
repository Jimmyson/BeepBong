using BeepBong.Application.ViewModels;
using BeepBong.DataAccess;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BeepBong.Application.Queries
{
    public class TrackListIndexQuery
    {
        private readonly BeepBongContext _context;

        public TrackListIndexQuery(BeepBongContext context) => _context = context;

        public IQueryable<TrackListIndexViewModel> GetQuery(Guid? channelId = null)
        {
            return _context.TrackLists
                .Include(tl => tl.Tracks)
                .Select(p => new TrackListIndexViewModel() {
                    TrackListId = p.TrackListId,
                    Name = p.Name,
                    Composer = p.Composer,
                    Library = p.Library,
                    ProgrammeCount = p.ProgrammeTrackLists.Count,
                    TrackCount = p.Tracks.Count
                })
                .OrderBy(ls => ls.Name)
                .AsQueryable();
        }
    }
}