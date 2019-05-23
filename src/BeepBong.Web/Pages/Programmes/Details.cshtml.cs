using System;
// using System.Collections.Generic;
// using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BeepBong.DataAccess;
// using BeepBong.Domain.Models;
// using BeepBong.Web.ViewModels;
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

            // Programme = await _context.Programmes
            //     .Select(p => new ProgrammeViewModel
            //     {
            //         ProgrammeId = p.ProgrammeId,
            //         Name = p.Name,
            //         Year = p.AirDate.ToString(),
            //     })
            //     .FirstOrDefaultAsync(m => m.ProgrammeId == id);

            if (Programme == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
