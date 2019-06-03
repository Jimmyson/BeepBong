using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using BeepBong.DataAccess;
using BeepBong.Domain.Models;
using BeepBong.Application.Commands;

namespace BeepBong.Web.Pages.Libraries
{
    public class CreateModel : PageModel
    {
        private readonly BeepBongContext _context;
        private readonly LibraryEditCommand _command;

        public CreateModel(BeepBongContext context)
        {
            _context = context;
            _command = new LibraryEditCommand(_context);
        }

        public IActionResult OnGet() => Page();

        [BindProperty]
        public Library Library { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (_command.Exists(Library))
            {
                ModelState.AddModelError("Exists", "A library already exists with these properties");
            }

            if (!ModelState.IsValid)
            {
                return OnGet();
            }

            _command.SendCommand(Library);

            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}