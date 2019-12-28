using System;
using System.Threading.Tasks;
using BeepBong.Application.Commands;
using BeepBong.Application.Queries;
using BeepBong.Application.Validation;
using BeepBong.Application.ViewModels;
using BeepBong.DataAccess;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BeepBong.Web.Vue.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TrackController : Controller
    {
        private readonly BeepBongContext _context;

        public TrackController(BeepBongContext context)
        {
            _context = context;
        }

        // GET: api/Track/
        // [HttpGet]
        // public async Task<ActionResult<IEnumerable<TrackIndexViewModel>>> GetTracks(int? pageNumber, int? pageSize)
        // {
        //     var query = new TrackIndexQuery(_context).GetQuery(null);

        //     return await PaginatedList<TrackIndexViewModel>.CreateAsync(query, pageNumber ?? 1, pageSize ?? 20);
        // }

        // GET: api/Track/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<TrackDetailViewModel>> GetTrack(Guid id)
        {
            var query = new TrackDetailQuery(_context).GetQuery(id);

            return await query.SingleOrDefaultAsync();
        }

        // GET: api/Track/{id}/Samples
        [HttpGet("{id}/Samples")]
        public async Task<ActionResult<Pagination<SampleIndexViewModel>>> GetTracks(Guid id, int? pageNumber, int? pageSize)
        {
            var query = new SampleIndexQuery(_context).GetQuery(id);

            return await Pagination<SampleIndexViewModel>.CreateAsync(query, pageNumber ?? 1, pageSize ?? 20);
        }

        // POST: api/Track
        // @TODO: Return the created object
        [HttpPost]
        public async Task<ActionResult<TrackDetailViewModel>> PostTrack(TrackEditViewModel tvm)
        {
			// Validate Model
			TrackEditValidator validator = new TrackEditValidator();
			if (!validator.Validate(tvm).IsValid) return BadRequest();

            if (new TrackEditQuery(_context).Exists(tvm)) return Conflict();

            new TrackEditCommand(_context).SendCommand(tvm);

            await _context.SaveChangesAsync();

            return new TrackDetailViewModel();
        }

        // PUT: api/Track/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTrack(Guid id, TrackEditViewModel tvm)
        {
            if (id != tvm.TrackId) return BadRequest();
			
			// Validate Model
			TrackEditValidator validator = new TrackEditValidator();
			if (!validator.Validate(tvm).IsValid) return BadRequest();

            new TrackEditCommand(_context).SendCommand(tvm);

            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/Track/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTrack(Guid id)
        {
            new TrackDeleteCommand(_context).SendCommand(id);

            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}