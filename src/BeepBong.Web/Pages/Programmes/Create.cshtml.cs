using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using BeepBong.DataAccess;
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

            await new ProgrammeEditCommand(_context).SendCommandAsync(Programme);

            return RedirectToPage("./Index");
        }
    }
}