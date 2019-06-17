using BeepBong.Application.Interfaces;
using BeepBong.Application.ViewModels;
using BeepBong.DataAccess;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace BeepBong.Application.Queries
{
    public class ProgrammeIndexQuery : IQuery<ProgrammeIndexViewModel>
    {
        private readonly BeepBongContext _context;

        public ProgrammeIndexQuery(BeepBongContext context) => _context = context;

        public IQueryable<ProgrammeIndexViewModel> GetQuery(Guid? id = null)
        {
            return _context.Programmes
                .Include(p => p.ProgrammeTrackLists)
                .ThenInclude(ptl => ptl.TrackList)
                .ThenInclude(tl => tl.Tracks)
                .OrderBy(p => p.Name)
                .ThenBy(p => p.AirDate)
                .Select(p => new ProgrammeIndexViewModel() {
                    ProgrammeId = p.ProgrammeId,
                    Name = p.Name,
                    Year = (p.AirDate.HasValue) ? p.AirDate.Value.Year.ToString() : null,
                    Channel = (p.Channel != null) ? p.Channel.Name : null,
                    ImageId = p.ImageId,
                    ContainsLibrary = p.ProgrammeTrackLists.Any(ptl => ptl.TrackList.Library),
                    TrackCount = p.ProgrammeTrackLists.Select(ptl => ptl.TrackList.Tracks.Count).Sum()
                });
        }
    }
}