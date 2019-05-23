// using System;
// using System.Collections.Generic;
// using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
// using Microsoft.AspNetCore.Mvc.Rendering;
using BeepBong.DataAccess;
// using BeepBong.Domain.Models;
// using BeepBong.Web.ViewModels;
// using BeepBong.Application;
// using System.IO;
using BeepBong.Application.ViewModels;
using BeepBong.Application.Commands;

namespace BeepBong.Web.Pages.Programmes
{
    public class CreateModel : PageModel
    {
        private readonly BeepBongContext _context;

        public CreateModel(BeepBongContext context) => _context = context;

        public IActionResult OnGet() => Page();

        [BindProperty]
        public ProgrammeEditViewModel Programme { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            await new ProgrammeEditCommand(_context).SendCommandAsync(Programme);

            // Programme p = new Programme() {
            //     ProgrammeId = Programme.ProgrammeId,
            //     Name = Programme.Name,
            //     AirDate = DateTime.Parse(Programme.Year)
            // };

            // if (Programme.LogoUpload != null && Programme.LogoUpload.Length > 0) {
            //     using (var ms = new MemoryStream()) {
            //         await Programme.LogoUpload.CopyToAsync(ms);

            //         using (ImageProcessing imageProc = new ImageProcessing(ms.ToArray()))
            //         {
            //             imageProc.DownscaleImage();
            //             p.LogoLocation = imageProc.ToDataURL();
            //         }
            //     }
            // }

            //_context.Programmes.Add(p);
            //await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}