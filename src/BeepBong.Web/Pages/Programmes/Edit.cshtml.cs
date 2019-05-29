using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BeepBong.DataAccess;
using BeepBong.Application.ViewModels;
using BeepBong.Application.Queries;
using BeepBong.Application.Commands;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BeepBong.Web.Pages.Programmes
{
    public class EditModel : PageModel
    {
        private readonly BeepBongContext _context;

        public EditModel(BeepBongContext context) => _context = context;

        [BindProperty]
        public ProgrammeEditViewModel Programme { get; set; }

        public async Task<IActionResult> OnGetAsync(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var query = new ProgrammeEditQuery(_context).GetQuery(id.Value);
            Programme = await query.FirstOrDefaultAsync();

            ViewData["TrackListIds"] = new SelectList(_context.TrackLists.Select(tl => new {tl.TrackListId, tl.Name}),"TrackListId", "Name");
            ViewData["ChannelIds"] = new SelectList(_context.Channels.Select(c => new {c.ChannelId, c.Name}),"ChannelId", "Name");

            if (Programme == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            new ProgrammeEditCommand(_context).SendCommand(Programme);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProgrammeExists(Programme.ProgrammeId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Details", new {id = Programme.ProgrammeId});
        }

        private bool ProgrammeExists(Guid id)
        {
            return _context.Programmes.Any(e => e.ProgrammeId == id);
        }
    }
}
