using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using BeepBong.DataAccess;
using BeepBong.Application.ViewModels;
using BeepBong.Application.Commands;

namespace BeepBong.Web.Pages.Samples
{
    public class UploadModel : PageModel
    {
        private readonly BeepBongContext _context;

        public UploadModel(BeepBongContext context) => _context = context;

        public IActionResult OnGet()
        {
            if (!Request.Query.Keys.Contains("TrackId")) {
                ViewData["TrackId"] = new SelectList(_context.Tracks
                                                    .Where(t => t.TrackList.Library == false)
                                                    .Select(t => new {
                                                        TrackId = t.TrackId,
                                                        Name = t.Name + (!String.IsNullOrEmpty(t.Variant) ? " (" + t.Variant + ")" : ""),
                                                        Programme = t.TrackList.Name
                                                    }), "TrackId", "Name", 1, "Programme");
            }
            return Page();
        }

        [BindProperty]
        public SampleCreateViewModel Sample { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            // // Save Waveform to Model
            // if (Sample.WaveformImage != null && Sample.WaveformImage.Length > 0) {
            //     s.Waveform = Sample.WaveformImage;
            // }

            // // Save Spectrograph to Model
            // if (Sample.SpecImage != null && Sample.SpecImage.Length > 0) {
            //     s.Spectrograph = Sample.SpecImage;
            // }

            await new SampleCreateCommand(_context).SendCommandAsync(Sample);

            return RedirectToPage("/Tracks/Details", new {id = Sample.TrackId});
        }
    }
}