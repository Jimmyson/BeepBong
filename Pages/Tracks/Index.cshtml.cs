using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BeepBong;
using BeepBong.Models;

namespace BeepBong.Pages.Tracks
{
    public class IndexModel : PageModel
    {
        private readonly BeepBong.BeepBongContext _context;

        public IndexModel(BeepBong.BeepBongContext context)
        {
            _context = context;
        }

        public IList<Track> Track { get;set; }

        public async Task OnGetAsync()
        {
            Track = await _context.Tracks
                .Include(t => t.Programme).ToListAsync();
        }
    }
}
