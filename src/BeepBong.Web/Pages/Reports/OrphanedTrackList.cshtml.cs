using System.Threading.Tasks;
using BeepBong.Application.Queries.Report;
using BeepBong.Application.ViewModels.Report;
using BeepBong.DataAccess;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BeepBong.Web.Pages.Reports
{
    public class OrphanedTrackListModel : PageModel
    {
        private readonly BeepBongContext _context;

        public OrphanedTrackListModel(BeepBongContext context) => _context = context;

        public PaginatedList<OrphanedTrackListViewModel> TrackLists { get; set; }

        public async Task OnGetAsync(int? pageNumber, int pageSize = 20)
        {
            var query = new OrphanedTrackListQuery(_context).GetQuery(null);

            TrackLists = await PaginatedList<OrphanedTrackListViewModel>.CreateAsync(query, pageNumber ?? 1, pageSize);
        }
    }
}