using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using BeepBong.DataAccess;
// using System.IO;
using BeepBong.Application.ViewModels;
using BeepBong.Application.Commands;
using BeepBong.Web.ViewModel;
using BeepBong.Application;
using System.IO;
using BeepBong.Domain.Models;

namespace BeepBong.Web.Pages.Programmes
{
    public class CreateModel : PageModel
    {
        private readonly BeepBongContext _context;

        public CreateModel(BeepBongContext context) => _context = context;

        public IActionResult OnGet() => Page();

        [BindProperty]
        public ProgrammeUploadViewModel Programme { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            ProgrammeEditViewModel p = new ProgrammeEditViewModel()
            {
                Name = Programme.Name,
                AirDate = Programme.AirDate,
                ChannelId = Programme.ChannelId,
            };

            if (Programme.ImageUpload != null && Programme.ImageUpload.Length > 0) {
                
                using (var ms = new MemoryStream()) {
                    await Programme.ImageUpload.CopyToAsync(ms);

                    Image i = new Image();

                    using (ImageProcessing imageProc = new ImageProcessing(ms.ToArray()))
                    {
                        imageProc.DownscaleImage();
                        i.Base64 = imageProc.ToBase64();
                        i.Height = imageProc.Height;
                        i.MimeType = imageProc.MimeType;
                        i.Width = imageProc.Width;

                        p.Image = i;
                    }
                }
            }

            new ProgrammeEditCommand(_context).SendCommand(p);

            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}