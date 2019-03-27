using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using BeepBong.DataAccess;
using BeepBong.Domain.Models;

namespace BeepBong.Web.Pages.Libraries
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
        public Library Library { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Library.Add(Library);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}