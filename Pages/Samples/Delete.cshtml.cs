using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BeepBong;
using BeepBong.Models;

namespace BeepBong.Pages.Samples
{
    public class DeleteModel : PageModel
    {
        private readonly BeepBong.BeepBongContext _context;

        public DeleteModel(BeepBong.BeepBongContext context)
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
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Sample = await _context.Samples.FindAsync(id);

            if (Sample != null)
            {
                _context.Samples.Remove(Sample);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("../Tracks/Details", new {id = Sample.TrackId});
        }
    }
}
