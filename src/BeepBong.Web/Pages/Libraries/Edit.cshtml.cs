using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BeepBong.DataAccess;
using BeepBong.Domain.Models;
using BeepBong.Application.Queries;
using BeepBong.Application.Commands;

namespace BeepBong.Web.Pages.Libraries
{
    public class EditModel : PageModel
    {
        private readonly BeepBongContext _context;
        private readonly LibraryEditCommand _command;
        private readonly LibraryEditQuery _query;

        public EditModel(BeepBongContext context)
        {
            _context = context;
            _command = new LibraryEditCommand(_context);
            _query = new LibraryEditQuery(_context);
        }

        [BindProperty]
        public Library Library { get; set; }

        public async Task<IActionResult> OnGetAsync(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var query = _query.GetQuery(id.Value);
            Library = await query.FirstOrDefaultAsync();

            if (Library == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (_query.Exists(Library))
            {
                ModelState.AddModelError("Exists", "A library already exists with these properties");
            }

            if (!ModelState.IsValid || Library.LibraryId == Guid.Empty)
            {
                return await OnGetAsync(Library.LibraryId);
            }

            _command.SendCommand(Library);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LibraryExists(Library.LibraryId))
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

        private bool LibraryExists(Guid id)
        {
            return _context.Libraries.Any(e => e.LibraryId == id);
        }
    }
}
