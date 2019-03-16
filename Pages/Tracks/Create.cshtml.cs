using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using BeepBong;
using BeepBong.Models;

namespace BeepBong.Pages.Tracks
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
			ViewData["ProgrammeId"] = new SelectList(_context.Programmes
														.Select(p => new {
															ProgrammeId = p.ProgrammeId,
															Name = p.Name + " (" + p.Year + ")"
														}),
													"ProgrammeId", "Name");
            return Page();
        }

        [BindProperty]
        public Track Track { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Tracks.Add(Track);
            await _context.SaveChangesAsync();

            return RedirectToPage("../Programmes/Details", new {id = Track.ProgrammeId});
        }
    }
}