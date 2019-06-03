using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BeepBong.DataAccess;
using BeepBong.Application.Queries;
using BeepBong.Application.ViewModels;
using BeepBong.Application.Commands;

namespace BeepBong.Web.Pages.Samples
{
    public class EditModel : PageModel
    {
        private readonly BeepBongContext _context;

        public EditModel(BeepBongContext context) => _context = context;

        [BindProperty]
        public SampleEditViewModel Sample { get; set; }

        public async Task<IActionResult> OnGetAsync(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var query = new SampleEditQuery(_context).GetQuery(id.Value);
            Sample = await query.FirstOrDefaultAsync();

            if (Sample == null)
            {
                return NotFound();
            }

            ViewData["TrackId"] = new SelectList(_context.Tracks
                                                    .Where(t => t.TrackList.Library == false)
                                                       .Select(t => new {
                                                           TrackId = t.TrackId,
                                                           Name = t.Name + ((!String.IsNullOrEmpty(t.Variant)) ? " [" + t.Variant + "]" : "") + " (" + t.TrackList.Name + ")"
                                                       }),
                                                    "TrackId", "Name");
            
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid || Sample.SampleId == Guid.Empty)
            {
                return await OnGetAsync(Sample.SampleId);
            }

            new SampleEditCommand(_context).SendCommand(Sample);

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
