using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BeepBong.DataAccess;
using BeepBong.Domain.Models;
using BeepBong.Application.Commands;
using BeepBong.Application.Queries;
using BeepBong.Application.ViewModels;

namespace BeepBong.Web.Pages.Tracks
{
    public class DeleteModel : PageModel
    {
        private readonly BeepBongContext _context;
		private readonly TrackDeleteCommand _command;

        public DeleteModel(BeepBongContext context)
        {
            _context = context;
        }

        [BindProperty]
        public TrackDetailViewModel Track { get; set; }

        public async Task<IActionResult> OnGetAsync(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var query = new TrackDetailQuery(_context).GetQuery(id.Value);
            Track = await query.FirstOrDefaultAsync(m => m.TrackId == id);

            if (Track == null)
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

            new TrackDeleteCommand(_context).SendCommand(id.Value);

            await _context.SaveChangesAsync();

            return RedirectToPage("../Programmes");
        }
    }
}
