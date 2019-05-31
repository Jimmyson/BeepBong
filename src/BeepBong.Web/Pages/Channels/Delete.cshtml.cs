using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BeepBong.DataAccess;
using BeepBong.Application.Queries;
using BeepBong.Application.ViewModels;
using BeepBong.Application.Commands;

namespace BeepBong.Web.Pages.Channels
{
    public class DeleteModel : PageModel
    {
        private readonly BeepBongContext _context;

        public DeleteModel(BeepBongContext context) => _context = context;

        [BindProperty]
        public ChannelDetailViewModel Channel { get; set; }

        public async Task<IActionResult> OnGetAsync(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var query = new ChannelDetailQuery(_context).GetQuery(id.Value);
            Channel = await query.FirstOrDefaultAsync();

            if (Channel == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            new ChannelDeleteCommand(_context).SendCommand(id.Value);

            await _context.SaveChangesAsync();

            return RedirectToPage("../Broadcasters/Index");
        }
    }
}
