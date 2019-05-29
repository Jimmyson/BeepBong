using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using BeepBong.DataAccess;
using BeepBong.Application.ViewModels;
using BeepBong.Application.Commands;

namespace BeepBong.Web.Pages.TrackLists
{
    public class CreateModel : PageModel
    {
        private readonly BeepBongContext _context;

        public CreateModel(BeepBongContext context) => _context = context;

        public IActionResult OnGet() => Page();

        [BindProperty]
        public TrackListEditViewModel TrackList { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            new TrackListEditCommand(_context).SendCommand(TrackList);

            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}