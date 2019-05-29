using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BeepBong.DataAccess;
using BeepBong.Application.ViewModels;
using BeepBong.Application.Queries;
using BeepBong.Application.Commands;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Globalization;

namespace BeepBong.Web.Pages.Broadcasters
{
    public class EditModel : PageModel
    {
        private readonly BeepBongContext _context;

        public EditModel(BeepBongContext context) => _context = context;

        [BindProperty]
        public BroadcasterEditViewModel Broadcaster { get; set; }

        public async Task<IActionResult> OnGetAsync(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var query = new BroadcasterEditQuery(_context).GetQuery(id.Value);
            Broadcaster = await query.FirstOrDefaultAsync();
            
            var data = CultureInfo.GetCultures(CultureTypes.SpecificCultures).Select(ct => new { Code = ct.Name, Country = new RegionInfo(ct.LCID).Name});

            //ViewData["CountryList"] = //new SelectList(CultureInfo.GetCultures(CultureTypes.SpecificCultures).Select(c => new {, c.}),"ChannelId", "Name");
                //new SelectList(CultureInfo.GetCultures(CultureTypes.SpecificCultures).Select(ct => new { Code = ct.Name, Country = new RegionInfo(ct.LCID).Name}));


            if (Broadcaster == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            new BroadcasterEditCommand(_context).SendCommand(Broadcaster);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BroadcasterExists(Broadcaster.BroadcasterId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Details", new {id = Broadcaster.BroadcasterId});
        }

        private bool BroadcasterExists(Guid id)
        {
            return _context.Broadcasters.Any(e => e.BroadcasterId == id);
        }
    }
}
