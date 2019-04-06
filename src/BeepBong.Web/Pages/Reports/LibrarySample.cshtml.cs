using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BeepBong.DataAccess;
using BeepBong.Domain.Models;
using BeepBong.Web.ViewModels;

namespace BeepBong.Web.Pages.Reports
{
    public class LibrarySampleModel : PageModel
    {
        private readonly BeepBongContext _context;
        
        public LibrarySampleModel(BeepBongContext context)
        {
            _context = context;
        }

        public IList<LibrarySampleViewModel> LibrarySample { get; set; }

        public async Task OnGetAsync()
        {
            LibrarySample = await _context.Samples
                .Where(s => s.Track.Programme.IsLibraryMusic == true)
                .Select(s => new LibrarySampleViewModel() {
                    ProgrammeId = s.Track.ProgrammeId,
                    ProgrammeName = s.Track.Programme.Name,
                    TrackId = s.TrackId,
                    TrackName = s.Track.Name + (!(String.IsNullOrWhiteSpace(s.Track.Subtitle)) ? " (" + s.Track.Subtitle + ")" : ""),
                    SampleId = s.SampleId
                })
                .OrderBy(ls => ls.ProgrammeName)
                .ToListAsync();
        }
    }
}