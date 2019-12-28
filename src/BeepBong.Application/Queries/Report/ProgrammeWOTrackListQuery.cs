using BeepBong.Application.Interfaces;
using BeepBong.Application.ViewModels;
using BeepBong.DataAccess;
using System;
using System.Linq;

namespace BeepBong.Application.Queries.Report
{
    public class ProgrammeWOTrackListQuery : IQuery<ProgrammeIndexViewModel>
    {
        private readonly BeepBongContext _context;

        public ProgrammeWOTrackListQuery(BeepBongContext context) => _context = context;

        public IQueryable<ProgrammeIndexViewModel> GetQuery(Guid? id)
        {
            return _context.Programmes
                .Where(p => p.ProgrammeTrackLists.Count == 0)
                .OrderBy(ls => ls.Name)
                .ThenBy(ls => ls.AirDate)
                .Select(p => new ProgrammeIndexViewModel() {
                    ProgrammeId = p.ProgrammeId,
                    Name = p.Name,
                    AirDate = (p.AirDate.HasValue) ? p.AirDate.Value.ToLongDateString() : null,
                    Channel = (p.Channel != null) ? p.Channel.Name : null,
                    ImageId = p.ImageId,
                    ContainsLibrary = p.ProgrammeTrackLists.Any(ptl => ptl.TrackList.Library),
                    TrackCount = p.ProgrammeTrackLists.Select(ptl => ptl.TrackList.Tracks.Count).Sum()
                });
        }
    }
}