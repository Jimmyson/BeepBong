using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BeepBong.DataAccess;
using BeepBong.Domain.Models;
using BeepBong.Application.Queries;
using BeepBong.Application.ViewModels;
using BeepBong.Application.Commands;

namespace BeepBong.Web.Pages.Samples
{
    public class DeleteModel : PageModel
    {
        private readonly BeepBongContext _context;

        public DeleteModel(BeepBongContext context) => _context = context;

        [BindProperty]
        public SampleDetailViewModel Sample { get; set; }

        public async Task<IActionResult> OnGetAsync(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var query = new SampleDetailQuery(_context).GetQuery(id.Value);
            Sample = await query.FirstOrDefaultAsync();

            // Sample = await _context.Samples
            //     .Include(s => s.Track).FirstOrDefaultAsync(m => m.SampleId == id);

            if (Sample == null)
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

            await new SampleDeleteCommand(_context).SendCommandAsync(id.Value);

            // Sample = await _context.Samples.FindAsync(id);

            // if (Sample != null)
            // {
            //     _context.Samples.Remove(Sample);
            //     await _context.SaveChangesAsync();
            // }

            return RedirectToPage("../Tracks/Details", new {id = Sample.TrackId});
        }
    }
}
