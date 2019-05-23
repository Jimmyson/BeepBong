// using System;
// using System.Collections.Generic;
// using System.Linq;
using System.Threading.Tasks;
// using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
// using Microsoft.EntityFrameworkCore;
using BeepBong.DataAccess;
// using BeepBong.Domain.Models;
// using BeepBong.Web.ViewModels;
using BeepBong.Application.Queries;
using BeepBong.Application.ViewModels;

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

            // var query = _context.Programmes
            //     .Select(p => new ProgrammeTrackCountViewModel() {
            //         ProgrammeId = p.ProgrammeId,
            //         Name = p.Name,
            //         Year = p.AirDate.ToString(),
            //         Logo = p.LogoLocation
            //     })
            //     .OrderBy(p => p.Name)
            //     .ThenBy(p => p.Year)
            //     .AsQueryable();

            Programme = await PaginatedList<ProgrammeIndexViewModel>.CreateAsync(query, pageNumber ?? 1, pageSize);
        }
    }
}
