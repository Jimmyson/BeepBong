using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using BeepBong.DataAccess;
using BeepBong.Application.ViewModels;
using BeepBong.Application.Commands;
using BeepBong.Web.ViewModel;
using System.IO;
using BeepBong.Domain.Models;
using BeepBong.Application;

namespace BeepBong.Web.Pages.Broadcasters
{
    public class CreateModel : PageModel
    {
        private readonly BeepBongContext _context;

        public CreateModel(BeepBongContext context) => _context = context;

        public IActionResult OnGet() => Page();

        [BindProperty]
        public BroadcasterUploadViewModel Broadcaster { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            BroadcasterEditViewModel b = new BroadcasterEditViewModel()
            {
                Name = Broadcaster.Name,
                Country = Broadcaster.Country
            };

            if (Broadcaster.ImageUpload != null && Broadcaster.ImageUpload.Length > 0) {
                
                using (var ms = new MemoryStream()) {
                    await Broadcaster.ImageUpload.CopyToAsync(ms);

                    Image i = new Image();

                    using (ImageProcessing imageProc = new ImageProcessing(ms.ToArray()))
                    {
                        imageProc.DownscaleImage();
                        i.Base64 = imageProc.ToBase64();
                        i.Height = imageProc.Height;
                        i.MimeType = imageProc.MimeType;
                        i.Width = imageProc.Width;

                        b.Image = i;
                    }
                }
            }
            
            new BroadcasterEditCommand(_context).SendCommand(b);

            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}