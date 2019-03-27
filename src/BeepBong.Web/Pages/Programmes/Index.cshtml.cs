using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BeepBong.DataAccess;
using BeepBong.Domain.Models;

namespace BeepBong.Web.Pages.Programmes
{
    public class IndexModel : PageModel
    {
        private readonly BeepBongContext _context;

        public IndexModel(BeepBongContext context)
        {
            _context = context;
        }

        public IList<Programme> Programme { get;set; }

        public async Task OnGetAsync()
        {
            Programme = await _context.Programmes
				.Include(p => p.Tracks)
				.OrderBy(p => p.Name)
				.ThenBy(p => p.Year)
				.ToListAsync();
        }
    }
}
