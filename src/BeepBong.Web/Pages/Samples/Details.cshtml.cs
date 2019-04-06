using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BeepBong.DataAccess;
using BeepBong.Domain.Models;
using BeepBong.Web.ViewModels;

namespace BeepBong.Web.Pages.Samples
{
    public class DetailsModel : PageModel
    {
        private readonly BeepBongContext _context;

        public DetailsModel(BeepBongContext context)
        {
            _context = context;
        }

        public SampleViewModel Sample { get; set; }

        public async Task<IActionResult> OnGetAsync(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Sample = await _context.Samples.Select(s => new SampleViewModel() {
                    SampleId = s.SampleId,
                    Duration = s.Duration,
                    SampleRate = s.SampleRate,
                    SampleCount = s.SampleCount,
                    Channels = s.Channels,
                    BitRate = s.BitRate,
                    BitRateMode = s.BitRateMode,
                    Codec = s.Codec,
                    Notes = s.Notes,
                    TrackId = s.TrackId,
                    TrackName = s.Track.Name + (!(String.IsNullOrWhiteSpace(s.Track.Subtitle)) ? " (" + s.Track.Subtitle + ")" : String.Empty)
                })
                .FirstOrDefaultAsync(m => m.SampleId == id);

            if (Sample == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
