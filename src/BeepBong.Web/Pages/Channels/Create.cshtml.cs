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

        public CreateModel(BeepBongContext context) => _context = context;

        public IActionResult OnGet() 
        {
            ViewData["BroadcasterIds"] = new SelectList(_context.Broadcasters.Select(b => new {b.BroadcasterId, b.Name}), "BroadcasterId", "Name");

            return Page();
        }

        [BindProperty]
        public ChannelEditViewModel Channel { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            
            new ChannelEditCommand(_context).SendCommand(Channel);

            await _context.SaveChangesAsync();

            return RedirectToPage("../Broadcasters/Index");
        }
    }
}