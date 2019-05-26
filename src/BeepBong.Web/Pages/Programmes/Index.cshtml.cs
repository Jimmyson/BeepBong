using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.RazorPages;
using BeepBong.DataAccess;
using BeepBong.Application.Queries;
using BeepBong.Application.ViewModels;
using System;

namespace BeepBong.Web.Pages.Programmes
{
    public class IndexModel : PageModel
    {
        private readonly BeepBongContext _context;

        public IndexModel(BeepBongContext context) => _context = context;

        public PaginatedList<ProgrammeIndexViewModel> Programme { get;set; }

        public async Task OnGetAsync(int? pageNumber, int pageSize = 20)
        {
            var query = new ProgrammeIndexQuery(_context).GetQuery();

            Programme = await PaginatedList<ProgrammeIndexViewModel>.CreateAsync(query, pageNumber ?? 1, pageSize);
        }
    }
}
