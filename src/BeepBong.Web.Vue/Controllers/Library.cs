using System;
using System.Threading.Tasks;
using BeepBong.Application.Commands;
using BeepBong.Application.Queries;
using BeepBong.Application.ViewModels;
using BeepBong.DataAccess;
using BeepBong.Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BeepBong.Web.Vue.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LibraryController : Controller
    {
        private readonly BeepBongContext _context;

        public LibraryController(BeepBongContext context)
        {
            _context = context;
        }

        // GET: api/Library/
        [HttpGet]
        public async Task<ActionResult<Pagination<Library>>> GetLibraries(int? pageNumber, int? pageSize)
        {
            var query = _context.Libraries;

            return await Pagination<Library>.CreateAsync(query, pageNumber ?? 1, pageSize ?? 20);
        }

        // GET: api/Library/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Library>> GetLibrary(Guid id)
        {
            var query = new LibraryDetailQuery(_context).GetQuery(id);

            return await query.SingleOrDefaultAsync();
        }

        // POST: api/Library
        // @TODO: Return the created object
        [HttpPost]
        public async Task<ActionResult<Library>> PostLibrary(Library library)
        {
            if (new LibraryEditQuery(_context).Exists(library)) return BadRequest();

            new LibraryEditCommand(_context).SendCommand(library);

            await _context.SaveChangesAsync();

            return new Library();
        }

        // PUT: api/Library/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> PutLibrary(Guid id, Library library)
        {
            if (id != library.LibraryId) return BadRequest();

            new LibraryEditCommand(_context).SendCommand(library);

            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/Library/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLibrary(Guid id)
        {
            new LibraryDeleteCommand(_context).SendCommand(id);

            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}