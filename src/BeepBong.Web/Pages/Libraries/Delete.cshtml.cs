using System;
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
    public class DeleteModel : PageModel
    {
        private readonly BeepBongContext _context;

        public DeleteModel(BeepBongContext context) => _context = context;

        [BindProperty]
        public Library Library { get; set; }

        public async Task<IActionResult> OnGetAsync(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var query = new LibraryDetailQuery(_context).GetQuery(id.Value);
            Library = await query.FirstOrDefaultAsync();

            if (Library == null)
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

            new LibraryDeleteCommand(_context).SendCommand(id.Value);

            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
