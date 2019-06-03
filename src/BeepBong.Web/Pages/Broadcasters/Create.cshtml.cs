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
using BeepBong.Application.Queries;

namespace BeepBong.Web.Pages.Broadcasters
{
    public class CreateModel : PageModel
    {
        private readonly BeepBongContext _context;
        private readonly BroadcasterEditCommand _command;
        private readonly BroadcasterEditQuery _query;

        public CreateModel(BeepBongContext context)
        {
            _context = context;
            _command = new BroadcasterEditCommand(_context);
            _query = new BroadcasterEditQuery(_context);
        }

        public IActionResult OnGet()
        {
            //var data = CultureInfo.GetCultures(CultureTypes.SpecificCultures).Select(ct => new { Code = ct.Name, Country = new RegionInfo(ct.LCID).EnglishName});

            //ViewData["CountryList"] = //new SelectList(CultureInfo.GetCultures(CultureTypes.SpecificCultures).Select(c => new {, c.}),"ChannelId", "Name");
                //new SelectList(CultureInfo.GetCultures(CultureTypes.SpecificCultures).Select(ct => new { Code = ct.Name, Country = new RegionInfo(ct.LCID).Name}));

            return Page();
        }

        [BindProperty]
        public BroadcasterUploadViewModel Broadcaster { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            BroadcasterEditViewModel b = new BroadcasterEditViewModel()
            {
                Name = Broadcaster.Name,
                Country = Broadcaster.Country
            };

            if (_query.Exists(b))
            {
                ModelState.AddModelError("Exists", "A broadcaster already exists with these properties");
            }

            if (!ModelState.IsValid)
            {
                return OnGet();
            }

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

            _command.SendCommand(b);

            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}