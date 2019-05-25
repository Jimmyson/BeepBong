using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using BeepBong.DataAccess;
using BeepBong.Application.ViewModels;
using BeepBong.Application.Commands;

namespace BeepBong.Web.Pages.Broadcasters
{
    public class CreateModel : PageModel
    {
        private readonly BeepBongContext _context;

        public CreateModel(BeepBongContext context) => _context = context;

        public IActionResult OnGet() => Page();

        [BindProperty]
        public BroadcasterEditViewModel Broadcaster { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            await new BroadcasterEditCommand(_context).SendCommandAsync(Broadcaster);

            return RedirectToPage("./Index");
        }
    }
}