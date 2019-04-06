using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using BeepBong.DataAccess;
using BeepBong.Domain.Models;

namespace BeepBong.Web.Pages.Samples
{
    public class CreateModel : PageModel
    {
        private readonly BeepBongContext _context;

        public CreateModel(BeepBongContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
        	ViewData["TrackId"] = new SelectList(_context.Tracks
													.Where(t => t.Programme.IsLibraryMusic == false)
		   											.Select(t => new {
														   TrackId = t.TrackId,
														   Name = t.Name + ((!String.IsNullOrEmpty(t.Subtitle)) ? " [" + t.Subtitle + "]" : "") + " (" + t.Programme.Name + ")"
													   }),
													"TrackId", "Name");
			ViewData["Compression"] = new SelectList(Enum.GetValues(typeof(CompressionEnum)).Cast<CompressionEnum>());
			ViewData["BitRateMode"] = new SelectList(Enum.GetValues(typeof(BitRateModeEnum)).Cast<BitRateModeEnum>());
            return Page();
        }

        [BindProperty]
        public Sample Sample { get; set; }

        //@TODO: Unit Test this change
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

			// Check Track Library Reference
            var programmeId = _context.Tracks.Where(t => t.TrackId == Sample.TrackId).FirstOrDefault().ProgrammeId;
			var Programme = _context.Programmes.Where(p => p.ProgrammeId == programmeId).FirstOrDefault();

			if (Programme.IsLibraryMusic)
			{
				return Page();
			}

            _context.Samples.Add(Sample);
            await _context.SaveChangesAsync();

            return RedirectToPage("../Tracks/Details", new {id = Sample.TrackId});
        }
    }
}