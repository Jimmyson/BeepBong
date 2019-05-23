// using System;
// using System.Collections.Generic;
// using System.Diagnostics;
// using System.Linq;
using System.Threading.Tasks;
// using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
// using Microsoft.EntityFrameworkCore;
using BeepBong.DataAccess;
// using BeepBong.Domain.Models;
// using BeepBong.Web.ViewModels;
using BeepBong.Application.ViewModels.Report;
using BeepBong.Application.Queries.Report;

namespace BeepBong.Web.Pages.Reports
{
    public class LibrarySampleModel : PageModel
    {
        private readonly BeepBongContext _context;

        public LibrarySampleModel(BeepBongContext context) => _context = context;

        public PaginatedList<LibrarySampleViewModel> LibrarySample { get; set; }

        public async Task OnGetAsync(int? pageNumber, int pageSize = 20)
        {
            var query = new LibrarySampleQuery(_context).GetQuery();

            // LibrarySample = await _context.Samples
            //     .Where(s => s.Track.TrackList.Library == true)
            //     .Select(s => new LibrarySampleViewModel() {
            //         ProgrammeId = s.Track.TrackId,
            //         ProgrammeName = s.Track.TrackList.Name,
            //         TrackId = s.TrackId,
            //         TrackName = s.Track.Name + (!(String.IsNullOrWhiteSpace(s.Track.Variant)) ? " (" + s.Track.Variant + ")" : ""),
            //         SampleId = s.SampleId
            //     })
            //     .OrderBy(ls => ls.ProgrammeName)
            //     .ToListAsync();

            LibrarySample = await PaginatedList<LibrarySampleViewModel>.CreateAsync(query, pageNumber ?? 1, pageSize);
        }
    }
}