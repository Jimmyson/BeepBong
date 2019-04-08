using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BeepBong.DataAccess;
using BeepBong.Domain.Models;

namespace BeepBong.Web.Pages.Samples
{
    public class EditModel : PageModel
    {
        private readonly BeepBongContext _context;

        public EditModel(BeepBongContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Sample Sample { get; set; }

        public async Task<IActionResult> OnGetAsync(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Sample = await _context.Samples
                .Include(s => s.Track).FirstOrDefaultAsync(m => m.SampleId == id);

            if (Sample == null)
            {
                return NotFound();
            }
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

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(Sample).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SampleExists(Sample.SampleId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("../Tracks/Details", new {id = Sample.TrackId});
        }

        private bool SampleExists(Guid id)
        {
            return _context.Samples.Any(e => e.SampleId == id);
        }
    }
}
