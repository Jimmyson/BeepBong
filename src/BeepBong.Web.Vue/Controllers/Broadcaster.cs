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
    public class BroadcasterController : Controller
    {
        private readonly BeepBongContext _context;

        public BroadcasterController(BeepBongContext context)
        {
            _context = context;
        }

        // GET: api/Broadcaster/
        [HttpGet]
        public async Task<ActionResult<Pagination<BroadcasterIndexViewModel>>> GetBroadcasters(int? pageNumber, int? pageSize)
        {
            var query = new BroadcasterIndexQuery(_context).GetQuery(null);

            return await Pagination<BroadcasterIndexViewModel>.CreateAsync(query, pageNumber ?? 1, pageSize ?? 20);
        }

        // GET: api/Broadcaster/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<BroadcasterDetailViewModel>> GetBroadcaster(Guid id)
        {
            var query = new BroadcasterDetailQuery(_context).GetQuery(id);

            return await query.SingleOrDefaultAsync();
        }

        // GET: api/Broadcaster/{id}/Channels
        // Retrieve channels from the Broadcaster
        [HttpGet("{id}/Channels")]
        public async Task<ActionResult<Pagination<ChannelIndexViewModel>>> GetBroadcasterChannels(Guid id, int? pageNumber, int? pageSize)
        {
            var query = new ChannelIndexQuery(_context).GetQuery(id);

            return await Pagination<ChannelIndexViewModel>.CreateAsync(query, pageNumber ?? 1, pageSize ?? 20);
        }

        // POST: api/Broadcaster
        // @TODO: Return the created object
        [HttpPost]
        public async Task<ActionResult<BroadcasterDetailViewModel>> PostBroadcaster(BroadcasterEditViewModel bvm)
        {
            if (new BroadcasterEditQuery(_context).Exists(bvm)) return BadRequest();

            new BroadcasterEditCommand(_context).SendCommand(bvm);

            await _context.SaveChangesAsync();

            return new BroadcasterDetailViewModel();
        }

        // PUT: api/Broadcaster/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBroadcaster(Guid id, BroadcasterEditViewModel bvm)
        {
            if (id != bvm.BroadcasterId) return BadRequest();

            new BroadcasterEditCommand(_context).SendCommand(bvm);

            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/Broadcaster/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBroadcaster(Guid id)
        {
            new BroadcasterDeleteCommand(_context).SendCommand(id);

            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}