using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.RazorPages;
using BeepBong.DataAccess;
using BeepBong.Application.Queries;
using BeepBong.Application.ViewModels;

namespace BeepBong.Web.Pages.Broadcasters
{
    public class IndexModel : PageModel
    {
        private readonly BeepBongContext _context;

        public IndexModel(BeepBongContext context) => _context = context;

        public PaginatedList<BroadcasterIndexViewModel> Broadcaster { get;set; }

        public async Task OnGetAsync(int? pageNumber, int pageSize = 20)
        {
            var query = new BroadcasterIndexQuery(_context).GetQuery(null);

            Broadcaster = await PaginatedList<BroadcasterIndexViewModel>.CreateAsync(query, pageNumber ?? 1, pageSize);
        }
    }
}
