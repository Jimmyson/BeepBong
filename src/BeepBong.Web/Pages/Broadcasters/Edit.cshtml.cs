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
using Microsoft.AspNetCore.Mvc.Rendering;
using BeepBong.Web.ViewModel;
using BeepBong.Domain.Models;
using System.IO;
using BeepBong.Application;

namespace BeepBong.Web.Pages.Broadcasters
{
    public class EditModel : PageModel
    {
        private readonly BeepBongContext _context;

        public EditModel(BeepBongContext context) => _context = context;

        [BindProperty]
        public BroadcasterUploadViewModel Broadcaster { get; set; }

        public async Task<IActionResult> OnGetAsync(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var query = new BroadcasterEditQuery(_context).GetQuery(id.Value);
            var entity = await query.FirstOrDefaultAsync();

            if (entity != null)
            {
                Broadcaster = new BroadcasterUploadViewModel() {
                    BroadcasterId = entity.BroadcasterId,
                    Name = entity.Name,
                    Country = entity.Country,
                    ImageId = entity.ImageId,
                    ImageIdChange = entity.ImageId
                };
            }

            if (Broadcaster == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid || Broadcaster.BroadcasterId == Guid.Empty)
            {
                return Page();
            }

            var model = new BroadcasterEditViewModel() {
                BroadcasterId = Broadcaster.BroadcasterId,
                Name = Broadcaster.Name,
                Country = Broadcaster.Country,
                ImageId = Broadcaster.ImageId
            };

            model.ImageChange = (Broadcaster.ImageIdChange != Broadcaster.ImageId);

            if (Broadcaster.ImageUpload != null && Broadcaster.ImageUpload.Length > 0)
            {
                // Adjust Image
                using (var ms = new MemoryStream())
                {
                    await Broadcaster.ImageUpload.CopyToAsync(ms);

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

            new BroadcasterEditCommand(_context).SendCommand(model);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BroadcasterExists(Broadcaster.BroadcasterId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool BroadcasterExists(Guid id)
        {
            return _context.Broadcasters.Any(e => e.BroadcasterId == id);
        }
    }
}
