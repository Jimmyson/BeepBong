using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BeepBong.DataAccess;
using BeepBong.Domain.Models;

namespace BeepBong.Web.Pages.Libraries
{
    public class DetailsModel : PageModel
    {
        private readonly BeepBongContext _context;

        public DetailsModel(BeepBongContext context)
        {
            _context = context;
        }

        public Library Library { get; set; }

        public async Task<IActionResult> OnGetAsync(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Library = await _context.Libraries.FirstOrDefaultAsync(m => m.LibraryId == id);

            if (Library == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
