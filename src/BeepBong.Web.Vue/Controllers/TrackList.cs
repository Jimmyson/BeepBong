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
    public class TrackListController : Controller
    {
        private readonly BeepBongContext _context;

        public TrackListController(BeepBongContext context)
        {
            _context = context;
        }

        // GET: api/TrackList/
        [HttpGet]
        public async Task<ActionResult<Pagination<TrackListIndexViewModel>>> GetTrackLists(int? pageNumber, int? pageSize)
        {
            var query = new TrackListIndexQuery(_context).GetQuery(null);

            return await Pagination<TrackListIndexViewModel>.CreateAsync(query, pageNumber ?? 1, pageSize ?? 20);
        }

        // GET: api/TrackList/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<TrackListDetailViewModel>> GetTrackList(Guid id)
        {
            var query = new TrackListDetailQuery(_context).GetQuery(id);

            return await query.SingleOrDefaultAsync();
        }

        // POST: api/TrackList
        // @TODO: Return the created object
        [HttpPost]
        public async Task<ActionResult<TrackListDetailViewModel>> PostTrackList(TrackListEditViewModel tlvm)
        {
            if (new TrackListEditQuery(_context).Exists(tlvm)) return BadRequest();

            new TrackListEditCommand(_context).SendCommand(tlvm);

            await _context.SaveChangesAsync();

            return new TrackListDetailViewModel();
        }

        // PUT: api/TrackList/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTrackList(Guid id, TrackListEditViewModel tlvm)
        {
            if (id != tlvm.TrackListId) return BadRequest();

            new TrackListEditCommand(_context).SendCommand(tlvm);

            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/TrackList/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTrackList(Guid id)
        {
            new TrackListDeleteCommand(_context).SendCommand(id);

            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}