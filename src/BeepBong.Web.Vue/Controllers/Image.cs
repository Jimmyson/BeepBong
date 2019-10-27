using System;
using System.Threading.Tasks;
using BeepBong.Application.Commands;
using BeepBong.Application.Queries;
using BeepBong.Application.ViewModels;
using BeepBong.DataAccess;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BeepBong.Web.Vue.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImageController : Controller
    {
        private readonly BeepBongContext _context;

        public ImageController(BeepBongContext context)
        {
            _context = context;
        }

        // GET: api/Image/{id}
        // @TODO: Recommended to let Web Server component to handel file/image sending
        [HttpGet("{id}")]
        public async Task<ActionResult> GetImage(Guid? id)
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
            return File(image.Data, image.MimeType);
        }
    }
}