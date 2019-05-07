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

namespace BeepBong.Web.Pages.Tracks
{
    public class EditModel : PageModel
    {
        private readonly BeepBongContext _context;

        public EditModel(BeepBongContext context)
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
            ViewData["ProgrammeId"] = new SelectList(_context.Programmes
                                                        .Select(p => new {
                                                            ProgrammeId = p.ProgrammeId,
                                                            Name = p.Name + " (" + p.Year + ")"
                                                        }),
                                                    "ProgrammeId", "Name");
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(Track).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TrackExists(Track.TrackId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("../Programmes/Details", new {id = Track.ProgrammeId});
        }

        private bool TrackExists(Guid id)
        {
            return _context.Tracks.Any(e => e.TrackId == id);
        }
    }
}
