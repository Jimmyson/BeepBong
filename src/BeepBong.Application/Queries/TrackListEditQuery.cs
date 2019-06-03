using System;
using System.Linq;
using BeepBong.Application.Interfaces;
using BeepBong.Application.ViewModels;
using BeepBong.DataAccess;

namespace BeepBong.Application.Queries
{
    public class TrackListEditQuery : IQuery<TrackListEditViewModel>
    {
        private readonly BeepBongContext _context;

        public TrackListEditQuery(BeepBongContext context) => _context = context;

        public IQueryable<TrackListEditViewModel> GetQuery(Guid? TrackListId)
        {
            return _context.TrackLists
                .Where(tl => tl.TrackListId == TrackListId)
                .Select(tl => new TrackListEditViewModel() {
                    TrackListId = tl.TrackListId,
                    Name = tl.Name,
                    Library = tl.Library,
                    Composer = tl.Composer,
                    Programmes = tl.ProgrammeTrackLists.Select(ptl => ptl.ProgrammeId).ToList()
                });
        }

        public bool Exists(TrackListEditViewModel model)
        {
            return _context.TrackLists.Any(tl =>
                tl.TrackListId != model.TrackListId
                && tl.Name.ToLower() == model.Name.ToLower()
                && tl.Composer.ToLower() == model.Composer.ToLower());
        }
    }
}