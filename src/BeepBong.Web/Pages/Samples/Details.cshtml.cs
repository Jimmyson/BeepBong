using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BeepBong.DataAccess;
using BeepBong.Domain.Models;

namespace BeepBong.Web.Pages.Samples
{
    public class DetailsModel : PageModel
    {
        private readonly BeepBongContext _context;

        public DetailsModel(BeepBongContext context)
        {
            _context = context;
        }

        public Sample Sample { get; set; }

        public async Task<IActionResult> OnGetAsync(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Sample = await _context.Samples
                .Include(s => s.Track).FirstOrDefaultAsync(m => m.SampleId == id);

            if (Sample == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
