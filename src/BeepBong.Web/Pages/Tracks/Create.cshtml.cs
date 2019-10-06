using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using BeepBong.DataAccess;
using BeepBong.Application.Commands;
using BeepBong.Application.ViewModels;
using BeepBong.Application.Queries;

namespace BeepBong.Web.Pages.Tracks
{
    public class CreateModel : PageModel
    {
        private readonly BeepBongContext _context;
		private readonly TrackEditCommand _command;
		private readonly TrackEditQuery _query;

		public CreateModel(BeepBongContext context)
        {
            _context = context;
            _command = new TrackEditCommand(_context);
            _query = new TrackEditQuery(_context);
        }

        public IActionResult OnGet()
        {
            ViewData["TrackListId"] = new SelectList(_context.TrackLists
                                                        .Select(tl => new {
                                                            TrackListId = tl.TrackListId,
                                                            Name = tl.Name + " (" + tl.Composer + ")"
                                                        }),
                                                    "TrackListId", "Name");
            return Page();
        }

        [BindProperty]
        public TrackEditViewModel Track { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (_query.Exists(Track))
            {
                ModelState.AddModelError("Exists", "A track list already exists with these properties");
            }

            if (!ModelState.IsValid)
            {
                return Page();
            }

            _command.SendCommand(Track);
            await _context.SaveChangesAsync();

            return RedirectToPage("../Programmes");
        }
    }
}