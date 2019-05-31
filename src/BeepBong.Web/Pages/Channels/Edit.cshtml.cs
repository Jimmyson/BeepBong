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
using BeepBong.Web.ViewModel;
using BeepBong.Domain.Models;
using System.IO;
using BeepBong.Application;

namespace BeepBong.Web.Pages.Channels
{
    public class EditModel : PageModel
    {
        private readonly BeepBongContext _context;

        public EditModel(BeepBongContext context) => _context = context;

        [BindProperty]
        public ChannelEditViewModel Channel { get; set; }

        public async Task<IActionResult> OnGetAsync(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var query = new ChannelEditQuery(_context).GetQuery(id.Value);
            Channel = await query.FirstOrDefaultAsync();

            ViewData["BroadcasterIds"] = new SelectList(_context.Broadcasters.Select(b => new {b.BroadcasterId, b.Name}), "BroadcasterId", "Name");

            if (Channel == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid || Channel.ChannelId == Guid.Empty)
            {
                return Page();
            }

            new ChannelEditCommand(_context).SendCommand(Channel);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ChannelExists(Channel.ChannelId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("../Broadcasters/Index");
        }

        private bool ChannelExists(Guid id)
        {
            return _context.Channels.Any(e => e.ChannelId == id);
        }
    }
}
