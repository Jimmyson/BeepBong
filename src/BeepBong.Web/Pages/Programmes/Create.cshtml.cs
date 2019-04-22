using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using BeepBong.DataAccess;
using BeepBong.Domain.Models;
using BeepBong.Web.ViewModels;
using System.IO;

namespace BeepBong.Web.Pages.Programmes
{
    public class CreateModel : PageModel
    {
        private readonly BeepBongContext _context;

        public CreateModel(BeepBongContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public ProgrammeEditViewModel Programme { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

			Programme p = new Programme() {
				ProgrammeId = Programme.ProgrammeId,
				Name = Programme.Name,
				Year = Programme.Year,
				Channel = Programme.Channel,
				AudioComposer = Programme.AudioComposer,
				IsLibraryMusic = Programme.IsLibraryMusic
			};

			if (Programme.Logo != null && Programme.Logo.Length > 0) {
				using (var ms = new MemoryStream()) {
					await Programme.Logo.CopyToAsync(ms);
					byte[] image = ms.ToArray();

					p.Logo = "data:" + Programme.Logo.ContentType + ";base64," + Convert.ToBase64String(image);
				}
			}

            _context.Programmes.Add(p);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}