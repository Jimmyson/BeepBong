using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using BeepBong;
using BeepBong.Models;

namespace BeepBong.Pages.Samples
{
    public class CreateModel : PageModel
    {
        private readonly BeepBong.BeepBongContext _context;

        public CreateModel(BeepBong.BeepBongContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
        ViewData["TrackId"] = new SelectList(_context.Tracks, "TrackId", "TrackId");
            return Page();
        }

        [BindProperty]
        public Sample Sample { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Samples.Add(Sample);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}