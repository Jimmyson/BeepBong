using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BeepBong.DataAccess;
using BeepBong.Domain.Models;

namespace BeepBong.Web.Pages.Tracks
{
    public class IndexModel : PageModel
    {
        private readonly BeepBongContext _context;

        public IndexModel(BeepBongContext context)
        {
            _context = context;
        }

        public IList<Track> Track { get;set; }

        public async Task OnGetAsync()
        {
            Track = await _context.Tracks
                .Include(t => t.Programme)
                .OrderBy(t => t.Programme.Name)
                .ThenBy(t => t.Programme.Year)
                .ThenBy(t => t.Name).ToListAsync();
        }
    }
}
