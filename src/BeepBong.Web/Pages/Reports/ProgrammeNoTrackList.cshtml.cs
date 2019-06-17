using System.Threading.Tasks;
using BeepBong.Application.Queries.Report;
using BeepBong.Application.ViewModels.Report;
using BeepBong.DataAccess;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BeepBong.Web.Pages.Reports
{
    public class ProgrammeNoTrackListModel : PageModel
    {
        private readonly BeepBongContext _context;

        public ProgrammeNoTrackListModel(BeepBongContext context) => _context = context;

        public PaginatedList<ProgrammeWOTrackListViewModel> ProgrammeLists { get; set; }

        public async Task OnGetAsync(int? pageNumber, int pageSize = 20)
        {
            var query = new ProgrammeWOTrackListQuery(_context).GetQuery(null);

            ProgrammeLists = await PaginatedList<ProgrammeWOTrackListViewModel>.CreateAsync(query, pageNumber ?? 1, pageSize);
        }
    }
}