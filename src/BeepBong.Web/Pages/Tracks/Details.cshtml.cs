using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BeepBong.DataAccess;
using BeepBong.Domain.Models;

namespace BeepBong.Web.Pages.Tracks
{
    public class DetailsModel : PageModel
    {
        private readonly BeepBongContext _context;

        public DetailsModel(BeepBongContext context)
        {
            _context = context;
        }

        public Track Track { get; set; }

        public async Task<IActionResult> OnGetAsync(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Track = await _context.Tracks
                .Include(t => t.Programme)
				.Include(t => t.Samples)
				.FirstOrDefaultAsync(m => m.TrackId == id);

            if (Track == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
