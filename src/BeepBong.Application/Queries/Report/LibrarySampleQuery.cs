using BeepBong.Application.Interfaces;
using BeepBong.Application.ViewModels;
using BeepBong.Application.ViewModels.Report;
using BeepBong.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BeepBong.Application.Queries.Report
{
    public class LibrarySampleQuery : IQuery<LibrarySampleViewModel>
    {
        private readonly BeepBongContext _context;

        public LibrarySampleQuery(BeepBongContext context) => _context = context;

        public IQueryable<LibrarySampleViewModel> GetQuery(Guid? id)
        {
            return _context.Samples
                .Where(s => s.Track.TrackList.Library == true)
                .Select(s => new LibrarySampleViewModel() {
                    TrackListId = s.Track.TrackListId,
                    TrackListName = s.Track.TrackList.Name,
                    TrackId = s.TrackId,
                    TrackName = s.Track.Name,
                    TrackVariant = s.Track.Variant,
                    SampleId = s.SampleId,
                    Fingerprint = s.Fingerprint,
                })
                .OrderBy(ls => ls.TrackListName)
                .ThenBy(ls => ls.TrackName)
                .ThenBy(ls => ls.TrackVariant)
                .AsQueryable();
        }
    }
}