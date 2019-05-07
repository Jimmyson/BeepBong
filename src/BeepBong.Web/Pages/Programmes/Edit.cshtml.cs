using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BeepBong.DataAccess;
using BeepBong.Domain.Models;
using BeepBong.Web.ViewModels;
using System.IO;

namespace BeepBong.Web.Pages.Programmes
{
    public class EditModel : PageModel
    {
        private readonly BeepBongContext _context;

        public EditModel(BeepBongContext context)
        {
            _context = context;
        }

        [BindProperty]
        public ProgrammeEditViewModel Programme { get; set; }

        public async Task<IActionResult> OnGetAsync(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Programme = await _context.Programmes
                                        .Select(p => new ProgrammeEditViewModel() {
                                            ProgrammeId = p.ProgrammeId,
                                            Name = p.Name,
                                            Year = p.Year,
                                            Channel = p.Channel,
                                            AudioComposer = p.AudioComposer,
                                            Logo = p.Logo,
                                            IsLibraryMusic = p.IsLibraryMusic
                                        }).FirstOrDefaultAsync(m => m.ProgrammeId == id);

            if (Programme == null)
            {
                return NotFound();
            }
            return Page();
        }

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

            if (Programme.LogoUpload != null && Programme.LogoUpload.Length > 0) {
                using (var ms = new MemoryStream()) {
                    await Programme.LogoUpload.CopyToAsync(ms);
                    byte[] image = ms.ToArray();

                    p.Logo = "data:" + Programme.LogoUpload.ContentType + ";base64," + Convert.ToBase64String(image);
                }
            } else {
                p.Logo = Programme.Logo;
            }

            _context.Attach(p).State = EntityState.Modified;

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
