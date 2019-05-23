using System;
// using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
// using System.IO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using BeepBong.DataAccess;
// using BeepBong.Domain.Models;
// using BeepBong.Web.ViewModels;
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
            // ViewData["Compression"] = new SelectList(Enum.GetValues(typeof(CompressionEnum)).Cast<CompressionEnum>());
            // ViewData["BitRateMode"] = new SelectList(Enum.GetValues(typeof(BitRateModeEnum)).Cast<BitRateModeEnum>());
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

            await new SampleCreateCommand(_context).SendCommandAsync(Sample);

            // Sample s = new Sample() {
            //     SampleId = Sample.SampleId,
            //     SampleRate = Sample.SampleRate,
            //     SampleCount = Sample.SampleCount,
            //     AudioChannelCount = Sample.Channels,
            //     BitRate = Sample.BitRate,
            //     BitRateMode = Sample.BitRateMode,
            //     BitDepth = Sample.BitDepth,
            //     Codec = Sample.Codec,
            //     Compression = Sample.Compression,
            //     Notes = Sample.Notes,
            //     TrackId = Sample.TrackId
            // };

            // // Save Waveform to Model
            // if (Sample.WaveformImage != null && Sample.WaveformImage.Length > 0) {
            //     s.Waveform = Sample.WaveformImage;
            // }

            // // Save Spectrograph to Model
            // if (Sample.SpecImage != null && Sample.SpecImage.Length > 0) {
            //     s.Spectrograph = Sample.SpecImage;
            // }

            // _context.Samples.Add(s);
            // await _context.SaveChangesAsync();

            return RedirectToPage("/Tracks/Details", new {id = Sample.TrackId});
        }
    }
}