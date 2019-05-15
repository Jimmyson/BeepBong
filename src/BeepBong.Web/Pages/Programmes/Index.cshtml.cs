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

namespace BeepBong.Web.Pages.Programmes
{
    public class IndexModel : PageModel
    {
        private readonly BeepBongContext _context;

        public IndexModel(BeepBongContext context)
        {
            _context = context;
        }

        public PaginatedList<ProgrammeTrackCountViewModel> Programme { get;set; }

        public async Task OnGetAsync(int? pageNumber, int pageSize = 20)
        {
            var query = _context.Programmes
                .Include(p => p.Tracks)
                .Select(p => new ProgrammeTrackCountViewModel() {
                    ProgrammeId = p.ProgrammeId,
                    Name = p.Name,
                    Year = p.Year,
                    Channel = p.Channel,
                    AudioComposer = p.AudioComposer,
                    IsLibraryMusic = p.IsLibraryMusic,
                    TrackCount = p.Tracks.Count,
                    Logo = p.Logo
                })
                .OrderBy(p => p.Name)
                .ThenBy(p => p.Year)
                .AsQueryable();

            Programme = await PaginatedList<ProgrammeTrackCountViewModel>.CreateAsync(query, pageNumber ?? 1, pageSize);
        }
    }
}
