using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BeepBong.DataAccess;
using BeepBong.Domain.Models;

namespace BeepBong.Web.Pages.Programmes
{
    public class DeleteModel : PageModel
    {
        private readonly BeepBongContext _context;

        public DeleteModel(BeepBongContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Programme Programme { get; set; }

        public async Task<IActionResult> OnGetAsync(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Programme = await _context.Programmes.FirstOrDefaultAsync(m => m.ProgrammeId == id);

            if (Programme == null)
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

            Programme = await _context.Programmes.FindAsync(id);

            if (Programme != null)
            {
                _context.Programmes.Remove(Programme);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
