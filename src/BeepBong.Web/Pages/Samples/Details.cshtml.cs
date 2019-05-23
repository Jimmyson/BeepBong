using System;
// using System.Collections.Generic;
// using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BeepBong.DataAccess;
// using BeepBong.Domain.Models;
// using BeepBong.Web.ViewModels;
using BeepBong.Application.ViewModels;
using BeepBong.Application.Queries;

namespace BeepBong.Web.Pages.Samples
{
    public class DetailsModel : PageModel
    {
        private readonly BeepBongContext _context;

        public DetailsModel(BeepBongContext context) => _context = context;

        public SampleDetailViewModel Sample { get; set; }

        public async Task<IActionResult> OnGetAsync(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var query = new SampleDetailQuery(_context).GetQuery(id.Value);
            Sample = await query.FirstOrDefaultAsync();

            // Sample = await _context.Samples.Select(s => new SampleViewModel() {
            //         SampleId = s.SampleId,
            //         Duration = s.Duration,
            //         SampleRate = s.SampleRate,
            //         SampleCount = s.SampleCount,
            //         Channels = s.AudioChannelCount,
            //         BitRate = s.BitRate,
            //         BitRateMode = s.BitRateMode,
            //         BitDepth = s.BitDepth,
            //         Codec = s.Codec,
            //         Notes = s.Notes,
            //         TrackId = s.TrackId,
            //         TrackName = s.Track.Name + (!(String.IsNullOrWhiteSpace(s.Track.Variant)) ? " (" + s.Track.Variant + ")" : String.Empty),
            //         WaveformImage = s.Waveform,
            //         SpecImage = s.Spectrograph
            //     })
            //     .FirstOrDefaultAsync(m => m.SampleId == id);

            if (Sample == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
