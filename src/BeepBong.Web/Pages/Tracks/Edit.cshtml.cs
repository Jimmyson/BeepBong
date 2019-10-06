using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BeepBong.DataAccess;
using BeepBong.Application.ViewModels;
using BeepBong.Application.Queries;
using BeepBong.Application.Commands;

namespace BeepBong.Web.Pages.Tracks
{
    public class EditModel : PageModel
    {
        private readonly BeepBongContext _context;
        private readonly TrackEditQuery _query;

        public EditModel(BeepBongContext context)
        {
            _context = context;
            _query = new TrackEditQuery(_context);
        }

        [BindProperty]
        public TrackEditViewModel Track { get; set; }

        public async Task<IActionResult> OnGetAsync(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var query = _query.GetQuery(id.Value);
            Track = await query.FirstOrDefaultAsync();

            if (Track == null)
            {
                return NotFound();
            }
			
            ViewData["TrackListId"] = new SelectList(_context.TrackLists
                                                        .Select(tl => new {
                                                            TrackListId = tl.TrackListId,
                                                            Name = tl.Name + " (" + tl.Composer + ")"
                                                        }),
                                                    "TrackListId", "Name");
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (_query.Exists(Track))
            {
                ModelState.AddModelError("Exists", "A track list already exists with these properties");
            }

            if (!ModelState.IsValid || Track.TrackListId == Guid.Empty)
            {
                return await OnGetAsync(Track.TrackListId);
            }

            new TrackEditCommand(_context).SendCommand(Track);

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

            return RedirectToPage("../Programmes");
        }

        private bool TrackExists(Guid id)
        {
            return _context.Tracks.Any(e => e.TrackId == id);
        }
    }
}
