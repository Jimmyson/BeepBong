using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.RazorPages;
using BeepBong.DataAccess;
using BeepBong.Application.Queries;
using BeepBong.Application.ViewModels;
using System;

namespace BeepBong.Web.Pages.TrackLists
{
    public class IndexModel : PageModel
    {
        private readonly BeepBongContext _context;

        public IndexModel(BeepBongContext context) => _context = context;

        public PaginatedList<TrackListIndexViewModel> TrackList { get;set; }

        public async Task OnGetAsync(int? pageNumber, int pageSize = 20, Guid? channelId = null)
        {
            var query = new TrackListIndexQuery(_context).GetQuery(channelId);

            TrackList = await PaginatedList<TrackListIndexViewModel>.CreateAsync(query, pageNumber ?? 1, pageSize);
        }
    }
}
