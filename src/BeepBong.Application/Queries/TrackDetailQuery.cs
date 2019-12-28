using System;
using System.Linq;
using BeepBong.Application.Interfaces;
using BeepBong.Application.ViewModels;
using BeepBong.DataAccess;

namespace BeepBong.Application.Queries
{
    public class TrackDetailQuery : IQuery<TrackDetailViewModel>
    {
        private readonly BeepBongContext _context;

        public TrackDetailQuery(BeepBongContext context) => _context = context;

        public IQueryable<TrackDetailViewModel> GetQuery(Guid? trackId)
        {
            return _context.Tracks
                .Where(t => t.TrackId == trackId.Value)
                .Select(t => new TrackDetailViewModel() {
                    TrackId = t.TrackId,
                    Name = t.Name,
                    Variant = t.Variant,
                    Description = t.Description,
                    TrackListId = t.TrackListId,
                    InLibrary = t.TrackList.Library
                });
        }
    }
}