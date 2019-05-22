using System;
using System.Linq;
using BeepBong.Application.ViewModels;
using BeepBong.DataAccess;

namespace BeepBong.Application.Queries
{
    public class TrackEditQuery
    {
        private readonly BeepBongContext _context;

        public TrackEditQuery(BeepBongContext context) => _context = context;

        public IQueryable<TrackEditViewModel> GetQuery(Guid trackId)
        {
            return _context.Tracks
                .Where(t => t.TrackId == trackId)
                .Select(t => new TrackEditViewModel() {
                    TrackId = t.TrackId,
                    Name = t.Name,
                    Variant = t.Variant,
                    Description = t.Description,
                    TrackListId = t.TrackListId
                });
        }
    }
}