using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BeepBong.DataAccess;
using BeepBong.Domain.Models;
using BeepBong.Application.Queries;

namespace BeepBong.Web.Pages.Libraries
{
    public class DetailsModel : PageModel
    {
        private readonly BeepBongContext _context;

        public DetailsModel(BeepBongContext context) => _context = context;

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
    }
}
