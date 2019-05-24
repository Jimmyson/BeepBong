using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BeepBong.DataAccess;
using BeepBong.Application.ViewModels;
using BeepBong.Application.Queries;

namespace BeepBong.Web.Pages.Programmes
{
    public class DetailsModel : PageModel
    {
        private readonly BeepBongContext _context;

        public DetailsModel(BeepBongContext context) => _context = context;

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
    }
}
