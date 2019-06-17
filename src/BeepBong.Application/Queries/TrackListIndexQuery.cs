using BeepBong.Application.Interfaces;
using BeepBong.Application.ViewModels;
using BeepBong.DataAccess;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace BeepBong.Application.Queries
{
    public class TrackListIndexQuery : IQuery<TrackListIndexViewModel>
    {
        private readonly BeepBongContext _context;

        public TrackListIndexQuery(BeepBongContext context) => _context = context;

        public IQueryable<TrackListIndexViewModel> GetQuery(Guid? id = null)
        {
            return _context.TrackLists
                .Include(tl => tl.Tracks)
                .OrderBy(ls => ls.Name)
                .Select(p => new TrackListIndexViewModel() {
                    TrackListId = p.TrackListId,
                    Name = p.Name,
                    Composer = p.Composer,
                    Library = p.Library,
                    ProgrammeCount = p.ProgrammeTrackLists.Count,
                    TrackCount = p.Tracks.Count
                });
        }
    }
}