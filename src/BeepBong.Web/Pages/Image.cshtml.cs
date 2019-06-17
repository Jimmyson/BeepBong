using System;
using System.Threading.Tasks;
using BeepBong.Application.Queries;
using BeepBong.DataAccess;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace BeepBong.Web.Pages
{
    public class ImageModel : PageModel
    {
        private readonly BeepBongContext _context;

        public ImageModel(BeepBongContext context) => _context = context;

        public async Task<IActionResult> OnGetAsync(Guid? id)
        {
            if (id == null)
            {
               return NotFound();
            }

            var query = new ImageGetQuery(_context).GetQuery(id);
            var image = await query.FirstOrDefaultAsync();

            if (image == null)
            {
               return NotFound();
            }
            //return Page();
            return File(image.Data, image.MimeType);
        }
    }
}
