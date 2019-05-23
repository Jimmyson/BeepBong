// using System;
// using System.Collections.Generic;
// using System.Linq;
using System.Threading.Tasks;
// using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
// using Microsoft.EntityFrameworkCore;
using BeepBong.DataAccess;
using BeepBong.Domain.Models;

namespace BeepBong.Web.Pages.Libraries
{
    public class IndexModel : PageModel
    {
        private readonly BeepBongContext _context;

        public IndexModel(BeepBongContext context) => _context = context;

        public PaginatedList<Library> Library { get;set; }

        public async Task OnGetAsync(int? pageNumber, int pageSize = 20)
        {
            var query = _context.Libraries;

            Library = await PaginatedList<Library>.CreateAsync(query, pageNumber ?? 1, pageSize);

            // Library = await _context.Libraries.ToListAsync();
        }
    }
}
