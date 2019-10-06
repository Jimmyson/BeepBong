using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BeepBong.DataAccess;
using BeepBong.Application.ViewModels;
using BeepBong.Application.Queries;

namespace BeepBong.Web.Pages.Tracks
{
    public class DetailsModel : PageModel
    {
        private readonly BeepBongContext _context;

        public DetailsModel(BeepBongContext context)
        {
            _context = context;
        }

        public TrackDetailViewModel Track { get; set; }

        public async Task<IActionResult> OnGetAsync(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var query = new TrackDetailQuery(_context).GetQuery(id.Value);
            Track = await query.FirstOrDefaultAsync(t => t.TrackId == id);

            if (Track == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
