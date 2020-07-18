using BeepBong.Application.Interfaces;
using BeepBong.Application.ViewModels;
using BeepBong.DataAccess;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace BeepBong.Application.Queries
{
    public class ProgrammeTracklistIndexQuery : IQuery<ProgrammeIndexViewModel>
    {
        private readonly BeepBongContext _context;

        public ProgrammeTracklistIndexQuery(BeepBongContext context) => _context = context;

        public IQueryable<ProgrammeIndexViewModel> GetQuery(Guid? id = null)
        {
            return _context.ProgrammeTrackLists
                .Include(ptl => ptl.Programme)
                .WhereIf(id != null, ptl => ptl.TrackListId == id.Value) // Filter on Channel ID
                .OrderBy(ptl => ptl.Programme.Name)
                .ThenBy(ptl => ptl.Programme.AirDate)
                .Select(ptl => new ProgrammeIndexViewModel() {
                    ProgrammeId = ptl.ProgrammeId,
                    Name = ptl.Programme.Name,
                    AirDate = ptl.Programme.AirDate,
                    Channel = (ptl.Programme.Channel != null) ? ptl.Programme.Channel.Name : null,
                    ImageId = ptl.Programme.ImageId,
                    ContainsLibrary = ptl.TrackList.Library,
                    TrackCount = ptl.TrackList.Tracks.Count
                });
        }
    }
}