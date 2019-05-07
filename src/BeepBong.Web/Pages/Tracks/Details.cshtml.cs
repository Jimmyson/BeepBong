using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BeepBong.DataAccess;
using BeepBong.Domain.Models;
using BeepBong.Web.ViewModels;

namespace BeepBong.Web.Pages.Tracks
{
    public class DetailsModel : PageModel
    {
        private readonly BeepBongContext _context;

        public DetailsModel(BeepBongContext context)
        {
            _context = context;
        }

        public TrackSampleListViewModel Track { get; set; }

        public async Task<IActionResult> OnGetAsync(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Track = await _context.Tracks
                .Select(t => new TrackSampleListViewModel() {
                    TrackId = t.TrackId,
                    Name = t.Name,
                    Subtitle = t.Subtitle,
                    Description = t.Description,
                    ProgrammeId = t.ProgrammeId,
                    ProgrammeName = t.Programme.Name,
                    IsLibraryMusic = t.Programme.IsLibraryMusic,
                    Samples = (t.Programme.IsLibraryMusic) ? new List<Sample>() : t.Samples
                })
                .FirstOrDefaultAsync(t => t.TrackId == id);

            if (Track == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
