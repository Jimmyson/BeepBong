using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using BeepBong.DataAccess;
using BeepBong.Application.ViewModels;
using BeepBong.Application.Commands;
using BeepBong.Web.ViewModel;
using System.IO;
using BeepBong.Domain.Models;
using BeepBong.Application;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Linq;

namespace BeepBong.Web.Pages.Channels
{
    public class CreateModel : PageModel
    {
        private readonly BeepBongContext _context;
        private readonly ChannelEditCommand _command;

        public CreateModel(BeepBongContext context)
        {
            _context = context;
            _command = new ChannelEditCommand(_context);
        }

        public IActionResult OnGet() 
        {
            ViewData["BroadcasterIds"] = new SelectList(_context.Broadcasters.Select(b => new {b.BroadcasterId, b.Name}), "BroadcasterId", "Name");

            return Page();
        }

        [BindProperty]
        public ChannelEditViewModel Channel { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (_command.Exists(Channel))
            {
                ModelState.AddModelError("Exists", "A channel already exists with these properties");
            }

            if (!ModelState.IsValid)
            {
                return OnGet();
            }
            
            _command.SendCommand(Channel);

            await _context.SaveChangesAsync();

            return RedirectToPage("../Broadcasters/Index");
        }
    }
}