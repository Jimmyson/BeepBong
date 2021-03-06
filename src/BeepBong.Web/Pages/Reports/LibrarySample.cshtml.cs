using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.RazorPages;
using BeepBong.DataAccess;
using BeepBong.Application.ViewModels.Report;
using BeepBong.Application.Queries.Report;

namespace BeepBong.Web.Pages.Reports
{
    public class LibrarySampleModel : PageModel
    {
        private readonly BeepBongContext _context;

        public LibrarySampleModel(BeepBongContext context) => _context = context;

        public PaginatedList<LibrarySampleViewModel> LibrarySample { get; set; }

        public async Task OnGetAsync(int? pageNumber, int pageSize = 20)
        {
            var query = new LibrarySampleQuery(_context).GetQuery(null);

            LibrarySample = await PaginatedList<LibrarySampleViewModel>.CreateAsync(query, pageNumber ?? 1, pageSize);
        }
    }
}