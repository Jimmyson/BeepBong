using System;
using System.Linq;
using BeepBong.Application.Interfaces;
using BeepBong.Application.ViewModels;
using BeepBong.DataAccess;

namespace BeepBong.Application.Queries
{
    public class TrackEditQuery : IQuery<TrackEditViewModel>
    {
        private readonly BeepBongContext _context;

        public TrackEditQuery(BeepBongContext context) => _context = context;

        public IQueryable<TrackEditViewModel> GetQuery(Guid? trackId)
        {
            return _context.Tracks
                .Where(t => t.TrackId == trackId.Value)
                .Select(t => new TrackEditViewModel() {
                    TrackId = t.TrackId,
                    Name = t.Name,
                    Variant = t.Variant,
                    Description = t.Description,
                    TrackListId = t.TrackListId
                });
        }

        public bool Exists(TrackEditViewModel model)
        {
            return _context.Tracks.Any(t =>
                t.TrackId != model.TrackId
                && string.Equals(t.Name, model.Name, StringComparison.OrdinalIgnoreCase)
                && string.Equals(t.Variant, model.Variant, StringComparison.OrdinalIgnoreCase)
                && string.Equals(t.Description, model.Description, StringComparison.OrdinalIgnoreCase)
                && t.TrackListId == model.TrackListId);
        }
    }
}