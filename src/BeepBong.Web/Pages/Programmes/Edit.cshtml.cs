using System;
// using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
// using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BeepBong.DataAccess;
// using BeepBong.Domain.Models;
// using BeepBong.Web.ViewModels;
// using System.IO;
// using BeepBong.Application;
using BeepBong.Application.ViewModels;
using BeepBong.Application.Queries;
using BeepBong.Application.Commands;

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

            var query = new ProgrammeEditQuery(_context).GetQuery(id.Value);

            Programme = await query.FirstOrDefaultAsync();

            // Programme = await _context.Programmes
            //                             .Select(p => new ProgrammeEditViewModel() {
            //                                 ProgrammeId = p.ProgrammeId,
            //                                 Name = p.Name,
            //                                 Year = p.AirDate.ToString(),
            //                                 Logo = p.LogoLocation
            //                             }).FirstOrDefaultAsync(m => m.ProgrammeId == id);

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

            // Programme p = new Programme() {
            //     ProgrammeId = Programme.ProgrammeId,
            //     Name = Programme.Name,
            //     AirDate = DateTime.Parse(Programme.Year)
            // };

            // if (Programme.LogoUpload != null && Programme.LogoUpload.Length > 0)
            // {
            //     using (var ms = new MemoryStream())
            //     {
            //         await Programme.LogoUpload.CopyToAsync(ms);
                    
            //         using (ImageProcessing imageProc = new ImageProcessing(ms.ToArray()))
            //         {
            //             imageProc.DownscaleImage();
            //             p.LogoLocation = imageProc.ToDataURL();
            //         }
            //     }
            // } else 
            // {
            //     p.LogoLocation = Programme.Logo;
            // }

            // _context.Attach(p).State = EntityState.Modified;

            try
            {
                await new ProgrammeEditCommand(_context).SendCommandAsync(Programme);
                //await _context.SaveChangesAsync();
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
