using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using BeepBong.DataAccess;
using BeepBong.Application.ViewModels;
using BeepBong.Application.Commands;
using BeepBong.Application.Queries;

namespace BeepBong.Web.Pages.TrackLists
{
    public class CreateModel : PageModel
    {
        private readonly BeepBongContext _context;
        private readonly TrackListEditCommand _command;
        private readonly TrackListEditQuery _query;

        public CreateModel(BeepBongContext context)
        {
            _context = context;
            _command = new TrackListEditCommand(_context);
            _query = new TrackListEditQuery(_context);
        }

        public IActionResult OnGet() => Page();

        [BindProperty]
        public TrackListEditViewModel TrackList { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (_query.Exists(TrackList))
            {
                ModelState.AddModelError("Exists", "A track list already exists with these properties");
            }

            if (!ModelState.IsValid)
            {
                return OnGet();
            }

            _command.SendCommand(TrackList);

            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}