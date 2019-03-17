using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BeepBong;
using BeepBong.Models;

namespace BeepBong.Pages.Tracks
{
    public class DeleteModel : PageModel
    {
        private readonly BeepBong.BeepBongContext _context;

        public DeleteModel(BeepBong.BeepBongContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Track Track { get; set; }

        public async Task<IActionResult> OnGetAsync(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Track = await _context.Tracks
                .Include(t => t.Programme).FirstOrDefaultAsync(m => m.TrackId == id);

            if (Track == null)
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

            Track = await _context.Tracks.FindAsync(id);

            if (Track != null)
            {
                _context.Tracks.Remove(Track);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("../Programmes/Details", new {id = Track.ProgrammeId});
        }
    }
}
