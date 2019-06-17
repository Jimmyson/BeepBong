using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using BeepBong.DataAccess;
using BeepBong.Application.ViewModels;
using BeepBong.Application.Commands;
using BeepBong.Web.ViewModel;
using BeepBong.Application;
using System.IO;
using BeepBong.Domain.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Linq;
using BeepBong.Application.Queries;

namespace BeepBong.Web.Pages.Programmes
{
    public class CreateModel : PageModel
    {
        private readonly BeepBongContext _context;
        private readonly ProgrammeEditCommand _command;
        private readonly ProgrammeEditQuery _query;

        public CreateModel(BeepBongContext context)
        {
            _context = context;
            _command = new ProgrammeEditCommand(_context);
            _query = new ProgrammeEditQuery(_context);
        }

        public IActionResult OnGet()
        {
            ViewData["TrackListIds"] = new SelectList(_context.TrackLists.Select(tl => new {tl.TrackListId, tl.Name}),"TrackListId", "Name");
            ViewData["ChannelIds"] = new SelectList(_context.Channels.Select(c => new {c.ChannelId, c.Name}),"ChannelId", "Name");

            return Page();
        }

        [BindProperty]
        public ProgrammeUploadViewModel Programme { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            ProgrammeEditViewModel p = new ProgrammeEditViewModel()
            {
                Name = Programme.Name,
                AirDate = Programme.AirDate,
                ChannelId = Programme.ChannelId,
            };

            if (_query.Exists(p))
            {
                ModelState.AddModelError("Exists", "A programme already exists with these properties");
            }

            if (!ModelState.IsValid)
            {
                return OnGet();
            }

            if (Programme.ImageUpload?.Length > 0)
            {
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

           _command.SendCommand(p);

            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}