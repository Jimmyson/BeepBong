using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BeepBong;
using BeepBong.Models;
using BeepBong.ViewModels;

namespace BeepBong.Pages.Programmes
{
    public class DetailsModel : PageModel
    {
        private readonly BeepBong.BeepBongContext _context;

        public DetailsModel(BeepBong.BeepBongContext context)
        {
            _context = context;
        }

        public ProgrammeViewModel Programme { get; set; }

        public async Task<IActionResult> OnGetAsync(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Programme = await _context.Programmes
				.Select(p => new ProgrammeViewModel
				{
					ProgrammeId = p.ProgrammeId,
					Name = p.Name,
					Year = p.Year,
					Channel = p.Channel,
					AudioComposer = p.AudioComposer,
					IsLibraryMusic = p.IsLibraryMusic,
					Tracks = p.Tracks.Where(t => t.ProgrammeId == p.ProgrammeId)
								.OrderBy(t => t.Name)
								.Select(t => new TrackViewModel {
									TrackId = t.TrackId,
									Name = t.Name,
									Subtitle = t.Subtitle,
									SampleCount = t.Samples.Count
								})
								.ToList()
				})
				.FirstOrDefaultAsync(m => m.ProgrammeId == id);

            if (Programme == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
