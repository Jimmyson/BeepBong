using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BeepBong.DataAccess;
using BeepBong.Application.Queries;
using BeepBong.Application.ViewModels;

namespace BeepBong.Web.Pages.Programmes
{
    public class DeleteModel : PageModel
    {
        private readonly BeepBongContext _context;

        public DeleteModel(BeepBongContext context) => _context = context;

        [BindProperty]
        public ProgrammeDetailViewModel Programme { get; set; }

        public async Task<IActionResult> OnGetAsync(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var query = new ProgrammeDetailQuery(_context).GetQuery(id.Value);
            Programme = await query.FirstOrDefaultAsync();

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

            new ProgrammeDeleteCommand(_context).SendCommand(id.Value);

            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
