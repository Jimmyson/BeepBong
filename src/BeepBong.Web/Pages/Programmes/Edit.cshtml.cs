using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BeepBong.DataAccess;
using BeepBong.Application.ViewModels;
using BeepBong.Application.Queries;
using BeepBong.Application.Commands;
using BeepBong.Web.ViewModel;
using Microsoft.AspNetCore.Mvc.Rendering;
using BeepBong.Application;
using System.IO;
using BeepBong.Domain.Models;

namespace BeepBong.Web.Pages.Programmes
{
    public class EditModel : PageModel
    {
        private readonly BeepBongContext _context;
        private readonly ProgrammeEditCommand _command;
        private readonly ProgrammeEditQuery _query;

        public EditModel(BeepBongContext context)
        {
            _context = context;
            _command = new ProgrammeEditCommand(_context);
            _query = new ProgrammeEditQuery(_context);
        }

        [BindProperty]
        public ProgrammeUploadViewModel Programme { get; set; }

        public async Task<IActionResult> OnGetAsync(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var query = _query.GetQuery(id.Value);
            var entity = await query.FirstOrDefaultAsync();

            if (entity != null)
            {
                Programme = new ProgrammeUploadViewModel() {
                    ProgrammeId = entity.ProgrammeId,
                    Name = entity.Name,
                    AirDate = entity.AirDate,
                    ImageId = entity.ImageId,
                    ImageIdChange = entity.ImageId,
                    ChannelId = entity.ChannelId,
                    TrackListIds = entity.TrackListIds
                };
            }

            ViewData["TrackListIds"] = new SelectList(_context.TrackLists.Select(tl => new {tl.TrackListId, tl.Name}),"TrackListId", "Name");
            ViewData["ChannelIds"] = new SelectList(_context.Channels.Select(c => new {c.ChannelId, c.Name}) ,"ChannelId", "Name");

            if (Programme == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            ProgrammeEditViewModel model = new ProgrammeEditViewModel() {
                ProgrammeId = Programme.ProgrammeId,
                Name = Programme.Name,
                AirDate = Programme.AirDate,
                ImageId = Programme.ImageId,
                ChannelId = Programme.ChannelId,
                TrackListIds = Programme.TrackListIds
            };
            
            if (_query.Exists(model))
            {
                ModelState.AddModelError("Exists", "A programme already exists with these properties");
            }

            if (!ModelState.IsValid || Programme.ProgrammeId == Guid.Empty)
            {
                return await OnGetAsync(Programme.ProgrammeId);
            }

            model.ImageChange = (Programme.ImageIdChange != Programme.ImageId);

            if (Programme.ImageUpload != null && Programme.ImageUpload.Length > 0)
            {
                // Adjust Image
                using (var ms = new MemoryStream())
                {
                    await Programme.ImageUpload.CopyToAsync(ms);

                    Image i = new Image();
                    
                    using (ImageProcessing imageProc = new ImageProcessing(ms.ToArray()))
                    {
                        imageProc.DownscaleImage();
                        i.Base64 = imageProc.ToBase64();
                        i.MimeType = imageProc.MimeType;
                        i.Width = imageProc.Width;
                        i.Height = imageProc.Height;
                    }

                    model.Image = i;
                }
            }

            _command.SendCommand(model);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProgrammeExists(Programme.ProgrammeId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Details", new {id = Programme.ProgrammeId});
        }

        private bool ProgrammeExists(Guid id)
        {
            return _context.Programmes.Any(e => e.ProgrammeId == id);
        }
    }
}
