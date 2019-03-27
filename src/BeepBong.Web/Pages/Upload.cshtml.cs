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
    public class UploadModel : PageModel
    {
        private readonly BeepBongContext _context;

        public UploadModel(BeepBongContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
        	ViewData["TrackId"] = new SelectList(_context.Tracks
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

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Samples.Add(Sample);
            await _context.SaveChangesAsync();

            return RedirectToPage("/Tracks/Details", new {id = Sample.TrackId});
        }
    }
}