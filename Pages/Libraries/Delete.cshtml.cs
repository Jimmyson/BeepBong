using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BeepBong;
using BeepBong.Models;

namespace BeepBong.Pages.Libraries
{
    public class DeleteModel : PageModel
    {
        private readonly BeepBong.BeepBongContext _context;

        public DeleteModel(BeepBong.BeepBongContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Library Library { get; set; }

        public async Task<IActionResult> OnGetAsync(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Library = await _context.Library.FirstOrDefaultAsync(m => m.LibraryId == id);

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

            Library = await _context.Library.FindAsync(id);

            if (Library != null)
            {
                _context.Library.Remove(Library);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
