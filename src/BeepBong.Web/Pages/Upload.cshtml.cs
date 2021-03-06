using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using BeepBong.DataAccess;
using BeepBong.Application.ViewModels;
using BeepBong.Application.Commands;
using BeepBong.Application.Queries;

namespace BeepBong.Web.Pages.Samples
{
    public class UploadModel : PageModel
    {
        private readonly BeepBongContext _context;
        private readonly SampleCreateCommand _command;
        private readonly SampleEditQuery _query;

        public UploadModel(BeepBongContext context)
        {
            _context = context;
            _command = new SampleCreateCommand(_context);
            _query = new SampleEditQuery(_context);
        }

        public IActionResult OnGet()
        {
            if (!Request.Query.Keys.Contains("TrackId")) {
                ViewData["TrackId"] = new SelectList(_context.Tracks
                                                    .Where(t => t.TrackList.Library)
                                                    .Select(t => new {
                                                        t.TrackId,
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
			if (IsSampleEmpty())
			{
				return OnGet();
			}

            if (_query.Exists(Sample))
            {
                ModelState.AddModelError("Exists", "A sample already exists with these properties");
            }

            if (!ModelState.IsValid)
            {
                return OnGet();
            }

            // // Save Waveform to Model
            // if (Sample.WaveformImage != null && Sample.WaveformImage.Length > 0) {
            //     s.Waveform = Sample.WaveformImage;
            // }

            // // Save Spectrograph to Model
            // if (Sample.SpecImage != null && Sample.SpecImage.Length > 0) {
            //     s.Spectrograph = Sample.SpecImage;
            // }

            _command.SendCommand(Sample);

            await _context.SaveChangesAsync();

            return RedirectToPage("/Tracks/Details", new {id = Sample.TrackId});
        }

		private bool IsSampleEmpty()
		{
			return Sample.SampleRate == null
                || Sample.SampleCount == null
                || Sample.AudioChannelCount == null
                || Sample.BitRate == null
                || Sample.BitDepth == null
                || Sample.Codec == null
                || Sample.Fingerprint == null;
		}
    }
}