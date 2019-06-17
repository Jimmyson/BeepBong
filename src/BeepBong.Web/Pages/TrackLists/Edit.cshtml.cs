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

namespace BeepBong.Web.Pages.TrackLists
{
    public class EditModel : PageModel
    {
        private readonly BeepBongContext _context;
        private readonly TrackListEditCommand _command;
        private readonly TrackListEditQuery _query;

        public EditModel(BeepBongContext context)
        {
            _context = context;
            _command = new TrackListEditCommand(_context);
            _query = new TrackListEditQuery(_context);
        }

        [BindProperty]
        public TrackListEditViewModel TrackList { get; set; }

        public async Task<IActionResult> OnGetAsync(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var query = _query.GetQuery(id.Value);
            TrackList = await query.FirstOrDefaultAsync();

            ViewData["ProgrammeIds"] = new SelectList(_context.Programmes.Select(p => new{p.ProgrammeId, Name = p.Name + ((p.AirDate.HasValue) ? " (" + p.AirDate.Value.Year + ")" : "")}),"ProgrammeId", "Name").OrderBy(l => l.Text);

            if (TrackList == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (_query.Exists(TrackList))
            {
                ModelState.AddModelError("Exists", "A track list already exists with these properties");
            }

            if (!ModelState.IsValid || TrackList.TrackListId == Guid.Empty)
            {
                return await OnGetAsync(TrackList.TrackListId);
            }

            _command.SendCommand(TrackList);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TrackListExists(TrackList.TrackListId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool TrackListExists(Guid id)
        {
            return _context.TrackLists.Any(e => e.TrackListId == id);
        }
    }
}
