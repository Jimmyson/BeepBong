using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BeepBong.DataAccess;
using BeepBong.Domain.Models;

namespace BeepBong.Web.Pages.Samples
{
    public class IndexModel : PageModel
    {
        private readonly BeepBongContext _context;

        public IndexModel(BeepBongContext context)
        {
            _context = context;
        }

        public IList<Sample> Sample { get;set; }

        public async Task OnGetAsync()
        {
            Sample = await _context.Samples
                .Include(s => s.Track).ToListAsync();
        }
    }
}
